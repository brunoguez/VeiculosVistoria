using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using VeiculosVistoria.Models;

namespace VeiculosVistoria.DataAccess
{
    public class DAO_Veiculos
    {
        private readonly string conexao;
        public DAO_Veiculos()
        {
            conexao = Connection.CreateConnectionString();
            using SQLiteConnection connection = VerificaConn();

            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Veiculos(
                    Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                    Placa TEXT,
                    Chassi TEXT NOT NULL,
                    Motor TEXT,
                    Ano_Fabricacao INTEGER NOT NULL,
                    Ano_Modelo INTEGER NOT NULL,
                    Marca TEXT,
                    Linha TEXT,
                    Descricao TEXT,
                    Potencia DECIMAL(6, 2),
                    Observacoes TEXT
                )";

            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            string createIndexPlacaQuery = "CREATE INDEX IF NOT EXISTS IX_Veiculos_Placa ON Veiculos(Placa)";
            using (var command = new SQLiteCommand(createIndexPlacaQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            string createIndexChassiQuery = "CREATE INDEX IF NOT EXISTS IX_Veiculos_Chassi ON Veiculos(Chassi)";
            using (var command = new SQLiteCommand(createIndexChassiQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public SQLiteConnection VerificaConn()
        {
            try
            {
                SQLiteConnection connection = new(conexao);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                return connection;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SqlConnection VerificaConexao()
        {
            var conn = new SqlConnection(conexao);

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
            }
            catch
            {
                throw;
            }

            return conn;
        }

        public List<string> GetAnoFabricacao()
        {
            List<string> x = new() { "" };

            try
            {
                using SQLiteConnection conn = VerificaConn();
                using SQLiteCommand command = conn.CreateCommand();
                command.CommandText = "select distinct Ano_Fabricacao ano from Veiculos order by Ano_Fabricacao desc";
                using SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    x.Add(Convert.ToString(reader["ano"]));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return x;
        }

        public List<Veiculo> GetPlaca(string placa, string ano = null)
        {
            List<Veiculo> x = new();

            using SQLiteCommand cmd = new(@"select * from Veiculos 
                where (Ano_Fabricacao = @ano or @ano is null) 
                and (Placa like @placa or @placa is null)", VerificaConn());
            try
            {
                cmd.Parameters.Add(new("ano", string.IsNullOrEmpty(ano) ? DBNull.Value : ano));
                cmd.Parameters.Add(new("placa", placa + "%"));

                using SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Veiculo veiculo = new();
                    veiculo.Placa = dr["Placa"] == DBNull.Value ? null : dr["Placa"].ToString();
                    veiculo.Chassi = dr["Chassi"].ToString();
                    veiculo.Motor = dr["Motor"] == DBNull.Value ? null : dr["Motor"].ToString();
                    veiculo.Ano_Fabricacao = dr.GetInt32("Ano_Fabricacao");
                    veiculo.Ano_Modelo = dr.GetInt32("Ano_Modelo");
                    veiculo.Marca = dr["Marca"] == DBNull.Value ? null : dr["Marca"].ToString();
                    veiculo.Linha = dr["Linha"] == DBNull.Value ? null : dr["Linha"].ToString();
                    veiculo.Descricao = dr["Descricao"] == DBNull.Value ? null : dr["Descricao"].ToString();
                    veiculo.Potencia = dr["Potencia"] == DBNull.Value ? 0 : Convert.ToDouble(dr["Potencia"], CultureInfo.CreateSpecificCulture("pt-BR"));
                    veiculo.Observacoes = dr["Observacoes"] == DBNull.Value ? null : dr["Observacoes"].ToString();
                    x.Add(veiculo);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cmd?.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }

            return x;
        }

        public List<Veiculo> GetMotor(string motor, string ano = null)
        {
            List<Veiculo> x = new();

            using SQLiteCommand cmd = new("select * from Veiculos where (Ano_Fabricacao = @ano or @ano is null) and (Motor like @motor or @motor is null)", VerificaConn());
            try
            {
                cmd.Parameters.Add(new("ano", string.IsNullOrEmpty(ano) ? DBNull.Value : ano));
                cmd.Parameters.Add(new("motor", motor + "%"));

                using SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Veiculo veiculo = new();
                    veiculo.Placa = dr["Placa"] == DBNull.Value ? null : dr["Placa"].ToString();
                    veiculo.Chassi = dr["Chassi"] == DBNull.Value ? null : dr["Chassi"].ToString();
                    veiculo.Motor = dr["Motor"] == DBNull.Value ? null : dr["Motor"].ToString();
                    veiculo.Ano_Fabricacao = dr.GetInt32("Ano_Fabricacao");
                    veiculo.Ano_Modelo = dr.GetInt32("Ano_Modelo");
                    veiculo.Marca = dr["Marca"] == DBNull.Value ? null : dr["Marca"].ToString();
                    veiculo.Linha = dr["Linha"] == DBNull.Value ? null : dr["Linha"].ToString(); ;
                    veiculo.Descricao = dr["Descricao"] == DBNull.Value ? null : dr["Descricao"].ToString();
                    veiculo.Potencia = dr["Potencia"] == DBNull.Value ? 0 : Convert.ToDouble(dr["Potencia"], CultureInfo.CreateSpecificCulture("pt-BR"));
                    veiculo.Observacoes = dr["Observacoes"] == DBNull.Value ? null : dr["Observacoes"].ToString();
                    x.Add(veiculo);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cmd?.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }

            return x;
        }

        public List<Veiculo> GetChassi(string chassi, string ano = null)
        {
            List<Veiculo> x = new();

            using SQLiteCommand cmd = new("select * from Veiculos where (Ano_Fabricacao = @ano or @ano is null) and (Chassi like @chassi or @chassi is null)", VerificaConn());
            try
            {
                cmd.Parameters.Add(new("ano", string.IsNullOrEmpty(ano) ? DBNull.Value : ano));
                cmd.Parameters.Add(new("chassi", chassi + "%"));

                using SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Veiculo veiculo = new();
                    veiculo.Placa = dr["Placa"] == DBNull.Value ? null : dr["Placa"].ToString();
                    veiculo.Chassi = dr["Chassi"] == DBNull.Value ? null : dr["Chassi"].ToString();
                    veiculo.Motor = dr["Motor"] == DBNull.Value ? null : dr["Motor"].ToString();
                    veiculo.Ano_Fabricacao = dr.GetInt32("Ano_Fabricacao");
                    veiculo.Ano_Modelo = dr.GetInt32("Ano_Modelo");
                    veiculo.Marca = dr["Marca"] == DBNull.Value ? null : dr["Marca"].ToString();
                    veiculo.Linha = dr["Linha"] == DBNull.Value ? null : dr["Linha"].ToString(); ;
                    veiculo.Descricao = dr["Descricao"] == DBNull.Value ? null : dr["Descricao"].ToString();
                    veiculo.Potencia = dr["Potencia"] == DBNull.Value ? 0 : Convert.ToDouble(dr["Potencia"], CultureInfo.CreateSpecificCulture("pt-BR"));
                    veiculo.Observacoes = dr["Observacoes"] == DBNull.Value ? null : dr["Observacoes"].ToString();
                    x.Add(veiculo);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cmd?.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }

            return x;
        }

        public void Insert(Veiculo chassi)
        {
            using SQLiteConnection conn = VerificaConn();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"insert into Veiculos (Placa, Chassi, Motor, Ano_Fabricacao, Ano_Modelo, Marca, Linha, Descricao, Potencia, Observacoes) 
                values (@Placa, @Chassi, @Motor, @Ano_Fabricacao, @Ano_Modelo, @Marca, @Linha, @Descricao, @Potencia, @Observacoes)";
            try
            {
                cmd.Parameters.Add(new("Placa", chassi.Placa is null ? DBNull.Value : chassi.Placa));
                cmd.Parameters.Add(new("Chassi", chassi.Chassi is null ? DBNull.Value : chassi.Chassi));
                cmd.Parameters.Add(new("Motor", chassi.Motor is null ? DBNull.Value : chassi.Motor));
                cmd.Parameters.Add(new("Ano_Fabricacao", chassi.Ano_Fabricacao));
                cmd.Parameters.Add(new("Ano_Modelo", chassi.Ano_Modelo));
                cmd.Parameters.Add(new("Marca", chassi.Marca is null ? DBNull.Value : chassi.Marca));
                cmd.Parameters.Add(new("Linha", chassi.Linha is null ? DBNull.Value : chassi.Linha));
                cmd.Parameters.Add(new("Descricao", chassi.Descricao is null ? DBNull.Value : chassi.Descricao));
                cmd.Parameters.Add(new("Potencia", chassi.Potencia == 0 || chassi.Potencia is null ? DBNull.Value : chassi.Potencia));
                cmd.Parameters.Add(new("Observacoes", chassi.Observacoes is null ? DBNull.Value : chassi.Observacoes));
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cmd?.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }
        }

        public void Update(Veiculo chassi)
        {
            using var conn = VerificaConn();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"Update Veiculos set Placa=@Placa, Motor=@Motor, Ano_Fabricacao=@Ano_Fabricacao, 
                Ano_Modelo=@Ano_Modelo, Marca=@Marca, Linha=@Linha, Descricao=@Descricao, Potencia=@Potencia, 
                Observacoes=@Observacoes where Chassi = @Chassi";
            try
            {
                cmd.Parameters.Add(new("Placa", string.IsNullOrEmpty(chassi.Placa) ? DBNull.Value : chassi.Placa));
                cmd.Parameters.Add(new("Chassi", string.IsNullOrEmpty(chassi.Chassi) ? DBNull.Value : chassi.Chassi));
                cmd.Parameters.Add(new("Motor", string.IsNullOrEmpty(chassi.Motor) ? DBNull.Value : chassi.Motor));
                cmd.Parameters.Add(new("Ano_Fabricacao", chassi.Ano_Fabricacao));
                cmd.Parameters.Add(new("Ano_Modelo", chassi.Ano_Modelo));
                cmd.Parameters.Add(new("Marca", string.IsNullOrEmpty(chassi.Marca) ? DBNull.Value : chassi.Marca));
                cmd.Parameters.Add(new("Linha", string.IsNullOrEmpty(chassi.Linha) ? DBNull.Value : chassi.Linha));
                cmd.Parameters.Add(new("Descricao", string.IsNullOrEmpty(chassi.Descricao) ? DBNull.Value : chassi.Descricao));
                cmd.Parameters.Add(new("Potencia", chassi.Potencia is null ? DBNull.Value : chassi.Potencia));
                cmd.Parameters.Add(new("Observacoes", string.IsNullOrEmpty(chassi.Observacoes) ? DBNull.Value : chassi.Observacoes));
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cmd?.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close(); cmd.Dispose();
            }
        }

        public Veiculo GetChassi(string chassi)
        {
            Veiculo veiculo = new();
            using var conn = VerificaConn();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "select top 1 * from Veiculos where Chassi = @Chassi";
            try
            {
                cmd.Parameters.Add(new("Chassi", chassi));

                SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    veiculo.Chassi = Convert.ToString(dr["Chassi"]);
                    veiculo.Placa = dr["Placa"] == DBNull.Value ? "" : Convert.ToString(dr["Placa"]);
                    veiculo.Motor = dr["Motor"] == DBNull.Value ? "" : Convert.ToString(dr["Motor"]);
                    veiculo.Ano_Fabricacao = Convert.ToInt32(dr["Ano_Fabricacao"]);
                    veiculo.Ano_Modelo = Convert.ToInt32(dr["Ano_Modelo"]);
                    veiculo.Marca = dr["Marca"] == DBNull.Value ? "" : Convert.ToString(dr["Marca"]);
                    veiculo.Linha = dr["Linha"] == DBNull.Value ? "" : Convert.ToString(dr["Linha"]);
                    veiculo.Descricao = dr["Descricao"] == DBNull.Value ? "" : Convert.ToString(dr["Descricao"]);
                    veiculo.Potencia = dr["Potencia"] == DBNull.Value ? 0 : Convert.ToDouble(dr["Potencia"]);
                    veiculo.Observacoes = dr["Observacoes"] == DBNull.Value ? "" : Convert.ToString(dr["Observacoes"]);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cmd?.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close(); 
            }

            return veiculo;
        }
    }
}