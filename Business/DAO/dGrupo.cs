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


		public static string[] Post(cGrupo obj)
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

				if(obj.Arvores == null || obj.Arvores.Count() == 0)
				{
					retorno[0] = "S";
					retorno[1] = "Códigos das arvores não informados.";
					return retorno;
				}

				foreach(var item in obj.Arvores)
				{
					if(dArvore.Get(item.arv_in_codigo) == null)
					{
						retorno[0] = "S";
						retorno[1] = "Códiogo de árvore não cadastrado: " + item.arv_in_codigo;
						return retorno;
					}
				}

				string sql;

				sql = @"BEGIN ";

				sql += @"INSERT INTO mgcustom.api_grupo
						   (gru_in_codigo,
						    gru_st_descricao)
						 VALUES
						   ((SELECT nvl(MAX(g.gru_in_codigo), 0) + 1 FROM mgcustom.api_grupo g),
							upper('{0}'));";

				sql = String.Format(sql, obj.gru_st_descricao);


				foreach(var item in obj.Arvores)
				{
					sql += @"INSERT INTO mgcustom.api_grupo_arvore
								   (gru_in_codigo,
									arv_in_codigo)
								 VALUES
								   ((SELECT MAX(g.gru_in_codigo) FROM mgcustom.api_grupo g),
									{0});";

					sql = String.Format(sql, item.arv_in_codigo);
				}


				sql += "COMMIT;END;";


				using(var conn = new OracleConnection(dConfig.ObterConteudo().OracleConn))
				{
					OracleCommand cmd = new OracleCommand(sql, conn);

					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();
					conn.Dispose();

					retorno[0] = "N";
					retorno[1] = "Grupo de árvore cadastrada com sucesso!";
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


		public static string[] Delete(int id)
		{
			string[] retorno = new string[2];

			try
			{
				string sql;

				sql = @"BEGIN ";
				sql += @"DELETE FROM mgcustom.api_grupo g WHERE g.gru_in_codigo = {0};";

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
					retorno[1] = "Grupo de árvore excluído com sucesso!";
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
