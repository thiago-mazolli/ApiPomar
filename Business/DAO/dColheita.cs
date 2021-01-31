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
										//col_dt_datacolheita = TryParseWithDefault.ToDateTime(linha["COL_DT_DATACOLHEITA"].ToString()),
										col_dt_datacolheita = TryParseWithDefault.ToDateTime(linha["COL_DT_DATACOLHEITA"].ToString()).ToString("dd/MM/yyyy"),
										//col_dt_datacolheita = linha["COL_DT_DATACOLHEITA"].ToString(),
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


		public static List<cColheita> GetByData(string data)
		{
			List<cColheita> colheita = GetAll().Where(e => Convert.ToDateTime(e.col_dt_datacolheita).ToString("dd-MM-yyyy") == data).Select(e => e).ToList();

			return colheita;
		}


		public static string[] Post(cColheita obj)
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

				if(obj.Arvore == null)
				{
					retorno[0] = "S";
					retorno[1] = "Código da árvore não informado.";
					return retorno;
				}

				if(dEspecie.Get(obj.Arvore.arv_in_codigo) == null)
				{
					retorno[0] = "S";
					retorno[1] = "Código da espécie não cadastrado.";
					return retorno;
				}

				string sql;

				sql = @"BEGIN ";
				sql += @"INSERT INTO mgcustom.api_colheita
						   (col_in_codigo,
						    col_dt_datacolheita,
							col_re_peso,
							arv_in_codigo)
						 VALUES
						   ((SELECT nvl(MAX(c.col_in_codigo), 0) + 1 FROM mgcustom.api_colheita c),
							to_date('{0}', 'DD/MM/RRRR'),
							{1},
							{2});";

				sql = String.Format(sql, obj.col_dt_datacolheita, obj.col_re_peso.ToString().Replace(".", "").Replace(",", "."), obj.Arvore.arv_in_codigo);
				//sql = String.Format(sql, obj.col_dt_datacolheita, TryParseWithDefault.ToDecimal(obj.col_re_peso.ToString(), 0), obj.Arvore.arv_in_codigo);

				sql += "COMMIT;END;";

				using(var conn = new OracleConnection(dConfig.ObterConteudo().OracleConn))
				{
					OracleCommand cmd = new OracleCommand(sql, conn);

					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();
					conn.Dispose();

					retorno[0] = "N";
					retorno[1] = "Colheita cadastrada com sucesso!";
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


		public static string[] Put(int id, cColheita obj)
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

				if(obj.Arvore == null)
				{
					retorno[0] = "S";
					retorno[1] = "Código da árvore não informado.";
					return retorno;
				}

				if(dEspecie.Get(obj.Arvore.arv_in_codigo) == null)
				{
					retorno[0] = "S";
					retorno[1] = "Código da espécie não cadastrado.";
					return retorno;
				}

				string sql;

				sql = @"BEGIN ";
				sql += @"UPDATE mgcustom.api_colheita c
						    SET c.col_dt_datacolheita = to_date('{1}', 'DD/MM/RRRR'),
						  	    c.col_re_peso         = {2},
							    c.arv_in_codigo       = {3}
						  WHERE c.col_in_codigo = {0};";

				sql = String.Format(sql, id, obj.col_dt_datacolheita, obj.col_re_peso.ToString().Replace(".", "").Replace(",", "."), obj.Arvore.arv_in_codigo);
				//sql = String.Format(sql, obj.col_dt_datacolheita, TryParseWithDefault.ToDecimal(obj.col_re_peso.ToString(), 0), obj.Arvore.arv_in_codigo);

				sql += "COMMIT;END;";

				using(var conn = new OracleConnection(dConfig.ObterConteudo().OracleConn))
				{
					OracleCommand cmd = new OracleCommand(sql, conn);

					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();
					conn.Dispose();

					retorno[0] = "N";
					retorno[1] = "Colheita alterada com sucesso!";
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
				sql += @"DELETE FROM mgcustom.api_colheita c WHERE c.col_in_codigo = {0};";

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
					retorno[1] = "Colheita excluída com sucesso!";
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
