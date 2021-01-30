using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Business.DAO
{
	public class dColheita
	{

		private static List<cColheita> listColheita = new List<cColheita>();

		public static List<cColheita> GetAll()
		{

			using(var conn = new OracleConnection(dConfig.ObterConteudo().OracleConn))
			{
				var sqlcmd = string.Format(@"SELECT a.* FROM mgcustom.api_colheita a ORDER BY a.col_dt_datacolheita DESC");

				var cmd = new OracleCommand(sqlcmd, conn);

				try
				{
					conn.Open();

					listColheita = (from IDataRecord linha in cmd.ExecuteReader()
									select new cColheita()
									{
										col_in_codigo = TryParseWithDefault.ToInt32(linha["COL_IN_CODIGO"].ToString(), 0),
										col_dt_datacolheita = TryParseWithDefault.ToDateTime(linha["COL_DT_DATACOLHEITA"].ToString()),
										//col_dt_datacolheita = linha["COL_DT_DATACOLHEITA"].ToString()),
										col_re_peso = TryParseWithDefault.ToDecimal(linha["COL_RE_PESO"].ToString(), 0),

										//BUSCA OS DADOS DAS ÁRVORES
										Arvore = dArvore.Get(TryParseWithDefault.ToInt32(linha["ARV_IN_CODIGO"].ToString(), 0)),
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

			return listColheita;
		}


		public static cColheita Get(int codigo)
		{
			cColheita colheita = GetAll().Where(e => e.col_in_codigo == codigo).Select(e => e).FirstOrDefault();

			return colheita;
		}

		public static List<cColheita> GetByData(DateTime data)
		{
			List<cColheita> colheita = GetAll().Where(e => e.col_dt_datacolheita == data).Select(e => e).ToList();

			return colheita;
		}
	}
}
