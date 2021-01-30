using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
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
	}
}
