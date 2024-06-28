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
            using SQLiteCommand command = connection.CreateCommand();

            command.CommandText = @"
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
                    Potencia REAL,
                    Observacoes TEXT,
                    DataIntegracao TEXT
                )";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE INDEX IF NOT EXISTS IX_Veiculos_Placa ON Veiculos(Placa)";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE INDEX IF NOT EXISTS IX_Veiculos_Chassi ON Veiculos(Chassi)";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE INDEX IF NOT EXISTS IX_Veiculos_Motor ON Veiculos(Motor)";
            command.ExecuteNonQuery();

            command.CommandText = "PRAGMA table_info(Veiculos)";
            bool columnDataIntegracaoExits = false;
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string? columnName = reader["name"].ToString();
                    if (string.Equals("DataIntegracao", columnName, StringComparison.OrdinalIgnoreCase))
                    {
                        columnDataIntegracaoExits = true;
                        break;
                    }
                }
            }
            if (!columnDataIntegracaoExits)
            {
                command.CommandText = "ALTER TABLE Veiculos ADD COLUMN DataIntegracao TEXT";
                command.ExecuteNonQuery();
            }

            //*******TODO: INSERIR CONSULTA NA API PARA VERIFICAR O QUE JÁ INTEGROU
        }

        public async Task<SQLiteConnection> VerificaConnAsync()
        {
            try
            {
                SQLiteConnection connection = new(conexao);
                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();
                return connection;
            }
            catch (Exception)
            {
                throw;
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
                    x.Add(reader["ano"].ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }

            return x;
        }

        public List<Veiculo> GetPlaca(string placa, Action<double> setarProgresso, string? ano = null)
        {
            setarProgresso(0);
            List<Veiculo> x = new();

            using SQLiteCommand cmd = new(@"WITH Contagem AS (
                select count(1) from Veiculos 
                    where (@ano is null or Ano_Fabricacao = @ano) 
                    and (@placa is null or Placa like @placa)
                ) select *, (select * from Contagem) count from Veiculos 
                where (@ano is null or Ano_Fabricacao = @ano) 
                and (@placa is null or Placa like @placa)", VerificaConn());
            try
            {
                cmd.Parameters.AddWithValue("@ano", string.IsNullOrEmpty(ano) ? DBNull.Value : ano);
                cmd.Parameters.AddWithValue("@placa", string.IsNullOrEmpty(placa) ? DBNull.Value : placa + "%");

                using SQLiteDataReader dr = cmd.ExecuteReader();
                double somaProgresso = 0;
                while (dr.Read())
                {
                    int contagem = dr.GetInt32("count");
                    somaProgresso += 90 / contagem;
                    setarProgresso(somaProgresso);
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
            setarProgresso(100);
            return x;
        }

        public List<Veiculo> GetMotor(string motor, Action<double> setarProgresso, string? ano = null)
        {
            setarProgresso(0);
            List<Veiculo> x = new();

            using SQLiteCommand cmd = new(@"WITH Contagem AS (
                    select count(1) from Veiculos 
                    where (Ano_Fabricacao = @ano or @ano is null) 
                    and (Motor like @motor or @motor is null)
                ) select *, (select * from Contagem) count from Veiculos 
                where (Ano_Fabricacao = @ano or @ano is null) 
                and (Motor like @motor or @motor is null)", VerificaConn());
            try
            {
                cmd.Parameters.Add(new("ano", string.IsNullOrEmpty(ano) ? DBNull.Value : ano));
                cmd.Parameters.Add(new("motor", motor + "%"));

                using SQLiteDataReader dr = cmd.ExecuteReader();

                double somaProgresso = 0;
                while (dr.Read())
                {
                    int contagem = dr.GetInt32("count");
                    somaProgresso += 90 / contagem;
                    setarProgresso(somaProgresso);
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
            setarProgresso(100);
            return x;
        }

        public async Task<List<Veiculo>> GetChassi(string chassi, Action<double> setarProgresso, string? ano = null)
        {
            setarProgresso(10);
            List<Veiculo> x = new();

            var conn = await VerificaConnAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"WITH Contagem AS (
                    select count(1) from Veiculos 
                    where (Ano_Fabricacao = @ano or @ano is null) 
                    and (Chassi like @chassi or @chassi is null)
                ) select *, (select * from Contagem) count from Veiculos 
                where (Ano_Fabricacao = @ano or @ano is null) 
                and (Chassi like @chassi or @chassi is null)";

            try
            {
                cmd.Parameters.Add(new("ano", string.IsNullOrEmpty(ano) ? DBNull.Value : ano));
                cmd.Parameters.Add(new("chassi", chassi + "%"));

                using var dr = await cmd.ExecuteReaderAsync();
                double somaProgresso = 0;
                while (dr.Read())
                {
                    int contagem = dr.GetInt32("count");
                    somaProgresso += 90 / contagem;
                    setarProgresso(somaProgresso);
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
            setarProgresso(100);
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
            cmd.CommandText = "select * from Veiculos where Chassi = @Chassi limit 1";
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