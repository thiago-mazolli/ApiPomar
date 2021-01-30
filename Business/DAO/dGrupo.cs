using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Business.DAO
{
	public class dGrupo
	{
		private static List<cGrupo> listGrupo = new List<cGrupo>();

		public static List<cGrupo> GetAll()
		{

			using(var conn = new OracleConnection(dConfig.ObterConteudo().OracleConn))
			{
				var sqlcmd = string.Format(@"SELECT a.* 
											   FROM mgcustom.api_grupo a 
											  ORDER BY a.gru_st_descricao ASC");

				var cmd = new OracleCommand(sqlcmd, conn);

				try
				{
					conn.Open();

					listGrupo = (from IDataRecord linha in cmd.ExecuteReader()
								 select new cGrupo()
								 {
									 gru_in_codigo = TryParseWithDefault.ToInt32(linha["GRU_IN_CODIGO"].ToString(), 0),
									 gru_st_descricao = linha["GRU_ST_DESCRICAO"].ToString(),

									 //LISTA DE ÁRVORES DO GRUPO
									 Arvores = dArvore.GetByGrupo(TryParseWithDefault.ToInt32(linha["GRU_IN_CODIGO"].ToString(), 0)),
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

			return listGrupo;
		}

		public static cGrupo Get(int codigo)
		{
			cGrupo grupo = GetAll().Where(g => g.gru_in_codigo == codigo).Select(g => g).FirstOrDefault();

			return grupo;
		}
	}
}
