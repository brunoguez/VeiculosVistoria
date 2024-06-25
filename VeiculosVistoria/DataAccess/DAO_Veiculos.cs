using System.Data;
using System.Data.SqlClient;
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

        public List<String> GetAnoFabricacao()
        {
            List<String> x = new();
            x.Add("");
            SqlCommand cmd = new("select distinct Ano_Fabricacao ano from Veiculos order by Ano_Fabricacao desc", VerificaConexao());
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    x.Add(Convert.ToString(dr["ano"]));
                }
            }
            catch
            {
                throw;
            }
            finally { if (cmd?.Connection.State == ConnectionState.Open) { cmd.Connection.Close(); cmd.Dispose(); } }

            return x;
        }

        public List<Veiculo> GetPlaca(String placa, String ano = null)
        {
            List<Veiculo> x = new();

            SqlCommand cmd = new("select * from Veiculos where (Ano_Fabricacao = @ano or @ano is null) and (Placa like @placa or @placa is null)", VerificaConexao());
            try
            {
                if (ano != "")
                {
                    cmd.Parameters.Add(new SqlParameter("ano", ano));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("ano", DBNull.Value));
                }
                //cmd.Parameters.Add(new SqlParameter("placa", (placa + "%")));
                cmd.Parameters.Add(new SqlParameter("@placa", SqlDbType.NVarChar, 20, "Placa"));
                cmd.Parameters["@placa"].Value = placa + "%";

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Veiculo c = new Veiculo();
                    if (dr["Placa"] != DBNull.Value)
                    {
                        c.Placa = Convert.ToString(dr["Placa"]);
                    }
                    else
                    {
                        c.Placa = "";
                    }
                    c.Chassi = Convert.ToString(dr["Chassi"]);
                    if (dr["Motor"] != DBNull.Value)
                    {
                        c.Motor = Convert.ToString(dr["Motor"]);
                    }
                    else
                    {
                        c.Motor = "";
                    }
                    c.Ano_Fabricacao = Convert.ToInt32(dr["Ano_Fabricacao"]);
                    c.Ano_Modelo = Convert.ToInt32(dr["Ano_Modelo"]);
                    if (dr["Marca"] != DBNull.Value)
                    {
                        c.Marca = Convert.ToString(dr["Marca"]);
                    }
                    else
                    {
                        c.Marca = "";
                    }
                    if (dr["Linha"] != DBNull.Value)
                    {
                        c.Linha = Convert.ToString(dr["Linha"]);
                    }
                    else
                    {
                        c.Linha = "";
                    }
                    if (dr["Descricao"] != DBNull.Value)
                    {
                        c.Descricao = Convert.ToString(dr["Descricao"]);
                    }
                    else
                    {
                        c.Descricao = "";
                    }
                    if (dr["Potencia"] != DBNull.Value)
                    {
                        c.Potencia = Convert.ToDouble(dr["Potencia"], CultureInfo.CreateSpecificCulture("pt-BR"));
                    }
                    else
                    {
                        c.Potencia = 0;
                    }
                    if (dr["Observacoes"] != DBNull.Value)
                    {
                        c.Observacoes = Convert.ToString(dr["Observacoes"]);
                    }
                    else
                    {
                        c.Observacoes = "";
                    }
                    x.Add(c);
                }
            }
            catch
            {
                throw;
            }
            finally { if (cmd?.Connection.State == ConnectionState.Open) { cmd.Connection.Close(); cmd.Dispose(); } }

            return x;
        }

        public List<Veiculo> GetMotor(String motor, String ano = null)
        {
            List<Veiculo> x = new();

            SqlCommand cmd = new("select * from Veiculos where (Ano_Fabricacao = @ano or @ano is null) and (Motor like @motor or @motor is null)", VerificaConexao());
            try
            {
                if (ano != "")
                {
                    cmd.Parameters.Add(new SqlParameter("ano", ano));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("ano", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@motor", SqlDbType.NVarChar, 20, "motor"));
                cmd.Parameters["@motor"].Value = motor + "%";

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Veiculo c = new Veiculo();
                    if (dr["Placa"] != DBNull.Value)
                    {
                        c.Placa = Convert.ToString(dr["Placa"]);
                    }
                    else
                    {
                        c.Placa = "";
                    }
                    c.Chassi = Convert.ToString(dr["Chassi"]);
                    if (dr["Motor"] != DBNull.Value)
                    {
                        c.Motor = Convert.ToString(dr["Motor"]);
                    }
                    else
                    {
                        c.Motor = "";
                    }
                    c.Ano_Fabricacao = Convert.ToInt32(dr["Ano_Fabricacao"]);
                    c.Ano_Modelo = Convert.ToInt32(dr["Ano_Modelo"]);
                    if (dr["Marca"] != DBNull.Value)
                    {
                        c.Marca = Convert.ToString(dr["Marca"]);
                    }
                    else
                    {
                        c.Marca = "";
                    }
                    if (dr["Linha"] != DBNull.Value)
                    {
                        c.Linha = Convert.ToString(dr["Linha"]);
                    }
                    else
                    {
                        c.Linha = "";
                    }
                    if (dr["Descricao"] != DBNull.Value)
                    {
                        c.Descricao = Convert.ToString(dr["Descricao"]);
                    }
                    else
                    {
                        c.Descricao = "";
                    }
                    if (dr["Potencia"] != DBNull.Value)
                    {
                        c.Potencia = Convert.ToDouble(dr["Potencia"], CultureInfo.CreateSpecificCulture("pt-BR"));
                    }
                    else
                    {
                        c.Potencia = 0;
                    }
                    if (dr["Observacoes"] != DBNull.Value)
                    {
                        c.Observacoes = Convert.ToString(dr["Observacoes"]);
                    }
                    else
                    {
                        c.Observacoes = "";
                    }
                    x.Add(c);
                }
            }
            catch
            {
                throw;
            }
            finally { if (cmd?.Connection.State == ConnectionState.Open) { cmd.Connection.Close(); cmd.Dispose(); } }

            return x;
        }

        public List<Veiculo> GetChassi(String chassi, String ano = null)
        {
            List<Veiculo> x = new();

            SqlCommand cmd = new("select * from Veiculos where (Ano_Fabricacao = @ano or @ano is null) and (Chassi like @chassi or @chassi is null)", VerificaConexao());
            try
            {
                if (ano != "")
                {
                    cmd.Parameters.Add(new SqlParameter("ano", ano));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("ano", DBNull.Value));
                }
                cmd.Parameters.Add(new SqlParameter("@chassi", SqlDbType.NVarChar, 20, "chassi"));
                cmd.Parameters["@chassi"].Value = chassi + "%";

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Veiculo c = new Veiculo();
                    if (dr["Placa"] != DBNull.Value)
                    {
                        c.Placa = Convert.ToString(dr["Placa"]);
                    }
                    else
                    {
                        c.Placa = "";
                    }
                    c.Chassi = Convert.ToString(dr["Chassi"]);
                    if (dr["Motor"] != DBNull.Value)
                    {
                        c.Motor = Convert.ToString(dr["Motor"]);
                    }
                    else
                    {
                        c.Motor = "";
                    }
                    c.Ano_Fabricacao = Convert.ToInt32(dr["Ano_Fabricacao"]);
                    c.Ano_Modelo = Convert.ToInt32(dr["Ano_Modelo"]);
                    if (dr["Marca"] != DBNull.Value)
                    {
                        c.Marca = Convert.ToString(dr["Marca"]);
                    }
                    else
                    {
                        c.Marca = "";
                    }
                    if (dr["Linha"] != DBNull.Value)
                    {
                        c.Linha = Convert.ToString(dr["Linha"]);
                    }
                    else
                    {
                        c.Linha = "";
                    }
                    if (dr["Descricao"] != DBNull.Value)
                    {
                        c.Descricao = Convert.ToString(dr["Descricao"]);
                    }
                    else
                    {
                        c.Descricao = "";
                    }
                    if (dr["Potencia"] != DBNull.Value)
                    {
                        c.Potencia = Convert.ToDouble(dr["Potencia"], CultureInfo.CreateSpecificCulture("pt-BR"));
                    }
                    else
                    {
                        c.Potencia = 0;
                    }
                    if (dr["Observacoes"] != DBNull.Value)
                    {
                        c.Observacoes = Convert.ToString(dr["Observacoes"]);
                    }
                    else
                    {
                        c.Observacoes = "";
                    }
                    x.Add(c);
                }
            }
            catch
            {
                throw;
            }
            finally { if (cmd?.Connection.State == ConnectionState.Open) { cmd.Connection.Close(); cmd.Dispose(); } }

            return x;
        }

        public void Insert(Veiculo chassi)
        {
            SqlCommand cmd = new("insert into Veiculos values (@Placa, @Chassi, @Motor, @Ano_Fabricacao, @Ano_Modelo, @Marca, @Linha, @Descricao, @Potencia, @Observacoes)", VerificaConexao());
            try
            {
                cmd.Parameters.Add(new SqlParameter("Placa", chassi.Placa is null ? DBNull.Value : chassi.Placa));
                cmd.Parameters.Add(new SqlParameter("Chassi", chassi.Chassi));
                cmd.Parameters.Add(new SqlParameter("Motor", chassi.Motor is null ? DBNull.Value : chassi.Motor));
                cmd.Parameters.Add(new SqlParameter("Ano_Fabricacao", chassi.Ano_Fabricacao));
                cmd.Parameters.Add(new SqlParameter("Ano_Modelo", chassi.Ano_Modelo));
                cmd.Parameters.Add(new SqlParameter("Marca", chassi.Marca is null ? DBNull.Value : chassi.Marca));
                cmd.Parameters.Add(new SqlParameter("Linha", chassi.Linha is null ? DBNull.Value : chassi.Linha));
                cmd.Parameters.Add(new SqlParameter("Descricao", chassi.Descricao is null ? DBNull.Value : chassi.Descricao));
                cmd.Parameters.Add(new SqlParameter("Potencia", chassi.Potencia == 0 || chassi.Potencia is null ? DBNull.Value : chassi.Potencia));
                cmd.Parameters.Add(new SqlParameter("Observacoes", chassi.Observacoes is null ? DBNull.Value : chassi.Observacoes));
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally { if (cmd?.Connection.State == ConnectionState.Open) { cmd.Connection.Close(); cmd.Dispose(); } }
        }

        public void Update(Veiculo chassi)
        {
            SqlCommand cmd = new(@"Update Veiculos set
                                   Placa = @Placa, Motor=@Motor, Ano_Fabricacao=@Ano_Fabricacao, Ano_Modelo=@Ano_Modelo, Marca=@Marca, Linha=@Linha, Descricao=@Descricao, Potencia=@Potencia, Observacoes=@Observacoes
                                   where Chassi = @Chassi", VerificaConexao());
            try
            {
                cmd.Parameters.Add(new SqlParameter("Placa", chassi.Placa));
                cmd.Parameters.Add(new SqlParameter("Chassi", chassi.Chassi));
                cmd.Parameters.Add(new SqlParameter("Motor", chassi.Motor));
                cmd.Parameters.Add(new SqlParameter("Ano_Fabricacao", chassi.Ano_Fabricacao));
                cmd.Parameters.Add(new SqlParameter("Ano_Modelo", chassi.Ano_Modelo));
                cmd.Parameters.Add(new SqlParameter("Marca", chassi.Marca));
                cmd.Parameters.Add(new SqlParameter("Linha", chassi.Linha));
                cmd.Parameters.Add(new SqlParameter("Descricao", chassi.Descricao));
                cmd.Parameters.Add(new SqlParameter("Potencia", chassi.Potencia));
                cmd.Parameters.Add(new SqlParameter("Observacoes", chassi.Observacoes));
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally { if (cmd?.Connection.State == ConnectionState.Open) { cmd.Connection.Close(); cmd.Dispose(); } }
        }

        public Veiculo GetChassi(String chassi)
        {
            Veiculo c = new();
            SqlCommand cmd = new("select top 1 * from Veiculos where Chassi = @Chassi", VerificaConexao());
            try
            {
                cmd.Parameters.Add(new SqlParameter("Chassi", chassi));

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    // tipo.Id_Sorvete = dr["Id_Sorvete"] == DBNull.Value ? -1 : Convert.ToInt32(dr["Id_Sorvete"]);

                    c.Chassi = Convert.ToString(dr["Chassi"]);
                    c.Placa = dr["Placa"] == DBNull.Value ? "" : Convert.ToString(dr["Placa"]);
                    c.Motor = dr["Motor"] == DBNull.Value ? "" : Convert.ToString(dr["Motor"]);
                    c.Ano_Fabricacao = Convert.ToInt32(dr["Ano_Fabricacao"]);
                    c.Ano_Modelo = Convert.ToInt32(dr["Ano_Modelo"]);
                    c.Marca = dr["Marca"] == DBNull.Value ? "" : Convert.ToString(dr["Marca"]);
                    c.Linha = dr["Linha"] == DBNull.Value ? "" : Convert.ToString(dr["Linha"]);
                    c.Descricao = dr["Descricao"] == DBNull.Value ? "" : Convert.ToString(dr["Descricao"]);
                    c.Potencia = dr["Potencia"] == DBNull.Value ? 0 : Convert.ToDouble(dr["Potencia"]);
                    c.Observacoes = dr["Observacoes"] == DBNull.Value ? "" : Convert.ToString(dr["Observacoes"]);
                }
            }
            catch
            {
                throw;
            }
            finally { if (cmd?.Connection.State == ConnectionState.Open) { cmd.Connection.Close(); cmd.Dispose(); } }

            return c;
        }
    }
}