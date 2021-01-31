using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Business.DAO
{
	public class dEspecie
	{
		private static List<cEspecie> listEspecie = new List<cEspecie>();


		public static List<cEspecie> GetAll()
		{
			using(var conn = new OracleConnection(dConfig.ObterConteudo().OracleConn))
			{
				var sqlcmd = string.Format(@"SELECT a.* 
											   FROM mgcustom.api_especie a 
											  ORDER BY a.esp_st_descricao ASC");

				var cmd = new OracleCommand(sqlcmd, conn);

				try
				{
					conn.Open();

					listEspecie = (from IDataRecord linha in cmd.ExecuteReader()
								   select new cEspecie()
								   {
									   esp_in_codigo = TryParseWithDefault.ToInt32(linha["ESP_IN_CODIGO"].ToString(), 0),
									   esp_st_descricao = linha["ESP_ST_DESCRICAO"].ToString(),
								   }).ToList();

					conn.Close();
					conn.Dispose();
				}
				catch(Exception ex)
				{
					conn.Close();
					conn.Dispose();
					throw new Exception(ex.Message);
				}
			}

			return listEspecie;
		}


		public static cEspecie Get(int codigo)
		{
			cEspecie especie = GetAll().Where(e => e.esp_in_codigo == codigo).Select(e => e).FirstOrDefault();

			return especie;
		}


		public static string[] Post(cEspecie obj)
		{
			string[] retorno = new string[2];

			try
			{
				if(obj == null)
				{
					retorno[0] = "S";
					retorno[1] = "Dados não informados.";
					return retorno;
				}

				string sql;

				sql = @"BEGIN ";
				sql += @"INSERT INTO mgcustom.api_especie
						   (esp_in_codigo,
						    esp_st_descricao)
						 VALUES
						   ((SELECT nvl(MAX(e.esp_in_codigo), 0) + 1 FROM mgcustom.api_especie e),
							upper('{0}'));";

				sql = String.Format(sql, obj.esp_st_descricao);

				sql += "COMMIT;END;";

				using(var conn = new OracleConnection(dConfig.ObterConteudo().OracleConn))
				{
					OracleCommand cmd = new OracleCommand(sql, conn);

					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();
					conn.Dispose();

					retorno[0] = "N";
					retorno[1] = "Espécie cadastrada com sucesso!";
					return retorno;
				}
			}
			catch(Exception ex)
			{
				retorno[0] = "S";
				retorno[1] = "Erro ao inserir o registro: " + ex.Message.ToString();
				return retorno;
			}
		}


		public static string[] Put(int id, cEspecie obj)
		{
			string[] retorno = new string[2];

			try
			{
				if(obj == null)
				{
					retorno[0] = "S";
					retorno[1] = "Dados não informados.";
					return retorno;
				}

				string sql;

				sql = @"BEGIN ";
				sql += @"UPDATE mgcustom.api_especie e SET e.esp_st_descricao = upper('{1}') WHERE e.esp_in_codigo = {0};";

				sql = String.Format(sql, id, obj.esp_st_descricao);

				sql += "COMMIT;END;";

				using(var conn = new OracleConnection(dConfig.ObterConteudo().OracleConn))
				{
					OracleCommand cmd = new OracleCommand(sql, conn);

					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();
					conn.Dispose();

					retorno[0] = "N";
					retorno[1] = "Espécie alterada com sucesso!";
					return retorno;
				}
			}
			catch(Exception ex)
			{
				retorno[0] = "S";
				retorno[1] = "Erro ao alterar o registro: " + ex.Message.ToString();
				return retorno;
			}
		}


		public static string[] Delete(int id)
		{
			string[] retorno = new string[2];

			try
			{
				string sql;

				sql = @"BEGIN ";
				sql += @"DELETE FROM mgcustom.api_especie WHERE esp_in_codigo = {0};";

				sql = String.Format(sql, id);

				sql += "COMMIT;END;";

				using(var conn = new OracleConnection(dConfig.ObterConteudo().OracleConn))
				{
					OracleCommand cmd = new OracleCommand(sql, conn);

					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();
					conn.Dispose();

					retorno[0] = "N";
					retorno[1] = "Espécie excluída com sucesso!";
					return retorno;
				}
			}
			catch(Exception ex)
			{
				retorno[0] = "S";
				retorno[1] = "Erro ao excluir registro: " + ex.Message.ToString();
				return retorno;
			}
		}

	}
}
