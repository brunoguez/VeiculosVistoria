using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;
using VeiculosVistoria.DataAccess;
using static System.Windows.Forms.LinkLabel;

namespace VeiculosVistoria.Models
{
    public class Connection
    {
        public string? Server { get; set; }
        public string? Database { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public static string CreateConnectionString()
        {
            string databasePath = Path.Combine(Environment.CurrentDirectory, "veiculos.db");
            return $"Data Source={databasePath};Version=3;";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<FormBusca>();

            IConfigurationRoot configuration = builder.Build();

            string pathEncrypt = configuration["PathDbConfig"]
                ?? throw new Exception("PathDbConfig vazia");
            string pathDbConfig = AesEncryption.DecryptString(pathEncrypt);

            if (!File.Exists(pathDbConfig))
                throw new Exception("Faltando PathDbConfig no config.json");

            string fileReader = File.ReadAllText(pathDbConfig);
            Connection? connEncripted = JsonConvert.DeserializeObject<Connection>(fileReader)
                ?? throw new Exception("Erro ao serealizar config.json");
            connEncripted.DecryptConnection();
            return $"Server={connEncripted.Server};Database={connEncripted.Database};User Id={connEncripted.User};Password={connEncripted.Password};";
        }
        private void DecryptConnection()
        {
            Server = AesEncryption.DecryptString(Server);
            Database = AesEncryption.DecryptString(Database);
            User = AesEncryption.DecryptString(User);
            Password = AesEncryption.DecryptString(Password);
        }

        private static BlockingCollection<Veiculo> Queue = new();
        public static void ImportaDados()
        {
            string pathBackup = Path.Combine(Environment.CurrentDirectory, "pathBackup.txt");
            if (!File.Exists(pathBackup))
            {
                return;
            }

            string path = File.ReadAllText(pathBackup);

            var processaFila = Task.Run(ProcessQueue);

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using var reader = new StreamReader(fileStream);
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null)
                    {
                        string[] values = line.Split("|*|<->|*|");
                        Veiculo veiculo = new();
                        veiculo.Placa = values[1] == "NULL" || string.IsNullOrEmpty(values[1]) ? null : values[1];
                        veiculo.Chassi = values[2] == "NULL" || string.IsNullOrEmpty(values[2]) ? null : values[2];
                        veiculo.Motor = values[3] == "NULL" || string.IsNullOrEmpty(values[3]) ? null : values[3];
                        veiculo.Ano_Fabricacao = values[4] == "NULL" || string.IsNullOrEmpty(values[4]) ? -1 : int.Parse(values[4]);
                        veiculo.Ano_Modelo = values[5] == "NULL" || string.IsNullOrEmpty(values[5]) ? -1 : int.Parse(values[5]);
                        veiculo.Marca = values[6] == "NULL" || string.IsNullOrEmpty(values[6]) ? null : values[6];
                        veiculo.Linha = values[7] == "NULL" || string.IsNullOrEmpty(values[7]) ? null : values[7];
                        veiculo.Descricao = values[8] == "NULL" || string.IsNullOrEmpty(values[8]) ? null : values[8];
                        veiculo.Potencia = values[9] == "NULL" || string.IsNullOrEmpty(values[9]) ? null : double.Parse(values[9]);
                        veiculo.Observacoes = values[10] == "NULL" || string.IsNullOrEmpty(values[10]) ? null : values[10];
                        Queue.Add(veiculo);
                    }
                }
            }

            Queue.CompleteAdding();
            processaFila.Wait();

        }

        private static List<Veiculo> InsertsComErro = new();

        private static void ProcessQueue()
        {
            var dao = new DAO_Veiculos();

            using SQLiteConnection conn = dao.VerificaConn();
            using var cmd = conn.CreateCommand();
            int i = 1;
            List<string> values = new();
            foreach (var item in Queue.GetConsumingEnumerable())
            {
                values.Add($"(@{i}_Placa, @{i}_Chassi, @{i}_Motor, @{i}_Ano_Fabricacao, @{i}_Ano_Modelo, @{i}_Marca, @{i}_Linha, @{i}_Descricao, @{i}_Potencia, @{i}_Observacoes)");

                cmd.Parameters.AddRange(new SQLiteParameter[]
                {
                    new($"{i}_Placa", item.Placa is null ? DBNull.Value : item.Placa),
                    new($"{i}_Chassi", item.Chassi),
                    new($"{i}_Motor", item.Motor is null ? DBNull.Value : item.Motor),
                    new($"{i}_Ano_Fabricacao", item.Ano_Fabricacao),
                    new($"{i}_Ano_Modelo", item.Ano_Modelo),
                    new($"{i}_Marca", item.Marca is null ? DBNull.Value : item.Marca),
                    new($"{i}_Linha", item.Linha is null ? DBNull.Value : item.Linha),
                    new($"{i}_Descricao", item.Descricao is null ? DBNull.Value : item.Descricao),
                    new($"{i}_Potencia", item.Potencia == 0 || item.Potencia is null ? DBNull.Value : item.Potencia),
                    new($"{i}_Observacoes", item.Observacoes is null ? DBNull.Value : item.Observacoes),
                });

                if (i >= 200)
                {
                    cmd.CommandText = $"insert into Veiculos (Placa,Chassi,Motor,Ano_Fabricacao,Ano_Modelo,Marca,Linha,Descricao,Potencia,Observacoes) values {string.Join(',', values)}";
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    values = new List<string>();
                    i = 1;
                    Debug.WriteLine(Queue.Count);
                    continue;
                }

                i++;
            }

            if (cmd?.Connection.State == ConnectionState.Open)
            {
                cmd.Connection.Close(); cmd.Dispose();
            }

            //using SqlConnection connection = dao.VerificaConexao();
            //SqlCommand cmd = connection.CreateCommand();
            //int i = 1;
            //List<string> values = new();
            //foreach (var item in Queue.GetConsumingEnumerable())
            //{
            //    values.Add($"(@{i}_Placa, @{i}_Chassi, @{i}_Motor, @{i}_Ano_Fabricacao, @{i}_Ano_Modelo, @{i}_Marca, @{i}_Linha, @{i}_Descricao, @{i}_Potencia, @{i}_Observacoes)");

            //    cmd.Parameters.AddRange(new SqlParameter[]
            //    {
            //        new($"{i}_Placa", item.Placa is null ? DBNull.Value : item.Placa),
            //        new($"{i}_Chassi", item.Chassi),
            //        new($"{i}_Motor", item.Motor is null ? DBNull.Value : item.Motor),
            //        new($"{i}_Ano_Fabricacao", item.Ano_Fabricacao),
            //        new($"{i}_Ano_Modelo", item.Ano_Modelo),
            //        new($"{i}_Marca", item.Marca is null ? DBNull.Value : item.Marca),
            //        new($"{i}_Linha", item.Linha is null ? DBNull.Value : item.Linha),
            //        new($"{i}_Descricao", item.Descricao is null ? DBNull.Value : item.Descricao),
            //        new($"{i}_Potencia", item.Potencia == 0 || item.Potencia is null ? DBNull.Value : item.Potencia),
            //        new($"{i}_Observacoes", item.Observacoes is null ? DBNull.Value : item.Observacoes),
            //    });

            //    if (i >= 200)
            //    {
            //        cmd.CommandText = $"insert into Veiculos values {string.Join(',', values)}";
            //        cmd.ExecuteNonQuery();
            //        cmd.Parameters.Clear();
            //        values = new List<string>();
            //        i = 1;
            //        Debug.WriteLine(Queue.Count);
            //        continue;
            //    }

            //    i++;
            //}

            //if (cmd?.Connection.State == ConnectionState.Open)
            //{
            //    cmd.Connection.Close(); cmd.Dispose();
            //}
        }
    };
}
