using Business.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Business.DAO
{
	public class dArvore
	{
		private static List<cArvore> listArvore = new List<cArvore>();


		public static List<cArvore> GetAll()
		{

			using(var conn = new OracleConnection(dConfig.ObterConteudo().OracleConn))
			{
				var sqlcmd = string.Format(@"SELECT a.* FROM mgcustom.api_arvore a ORDER BY a.arv_st_descricao ASC");

				var cmd = new OracleCommand(sqlcmd, conn);

				try
				{
					conn.Open();

					listArvore = (from IDataRecord linha in cmd.ExecuteReader()
								  select new cArvore()
								  {
									  arv_in_codigo = TryParseWithDefault.ToInt32(linha["ARV_IN_CODIGO"].ToString(), 0),
									  arv_st_descricao = linha["ARV_ST_DESCRICAO"].ToString(),
									  arv_in_idade = TryParseWithDefault.ToInt32(linha["ARV_IN_IDADE"].ToString(), 0),

									  //BUSCA OS DADOS DA ESPECIE
									  Especie = dEspecie.Get(TryParseWithDefault.ToInt32(linha["ESP_IN_CODIGO"].ToString(), 0)),
								  }).ToList();

					//using(OracleDataReader reader = cmd.ExecuteReader())
					//{
					//	while(reader.Read())
					//	{
					//		cArvore objStud = new cArvore();
					//		objStud.arv_in_codigo = Convert.ToInt32(reader["arv_in_codigo"]);
					//		objStud.arv_st_descricao = Convert.ToString(reader["arv_st_descricao"]);
					//		listArvore.Add(objStud);
					//	}
					//}

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

			return listArvore;
		}


		public static cArvore Get(int codigo)
		{
			cArvore arvore = GetAll().Where(e => e.arv_in_codigo == codigo).Select(e => e).FirstOrDefault();

			return arvore;
		}


		public static List<cArvore> GetByGrupo(int grupo)
		{
			using(var conn = new OracleConnection(dConfig.ObterConteudo().OracleConn))
			{
				var sqlcmd = string.Format(@"SELECT a.*
										       FROM mgcustom.api_arvore a 
											   JOIN mgcustom.api_grupo_arvore g ON g.arv_in_codigo = a.arv_in_codigo
											  WHERE g.gru_in_codigo = {0}
											  ORDER BY a.arv_st_descricao ASC", grupo);

				var cmd = new OracleCommand(sqlcmd, conn);

				try
				{
					conn.Open();

					listArvore = (from IDataRecord linha in cmd.ExecuteReader()
								  select new cArvore()
								  {
									  arv_in_codigo = TryParseWithDefault.ToInt32(linha["ARV_IN_CODIGO"].ToString(), 0),
									  arv_st_descricao = linha["ARV_ST_DESCRICAO"].ToString(),
									  arv_in_idade = TryParseWithDefault.ToInt32(linha["ARV_IN_IDADE"].ToString(), 0),

									  //BUSCA OS DADOS DA ESPECIE
									  Especie = dEspecie.Get(TryParseWithDefault.ToInt32(linha["ESP_IN_CODIGO"].ToString(), 0)),
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

				return listArvore;
			}
		}


		public static string[] Post(cArvore obj)
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

				if(obj.Especie == null)
				{
					retorno[0] = "S";
					retorno[1] = "Código da espécie não informado.";
					return retorno;
				}

				if(dEspecie.Get(obj.Especie.esp_in_codigo) == null)
				{
					retorno[0] = "S";
					retorno[1] = "Código da espécie não cadastrado.";
					return retorno;
				}

				string sql;

				sql = @"BEGIN ";
				sql += @"INSERT INTO mgcustom.api_arvore
						   (arv_in_codigo,
						    arv_st_descricao,
							arv_in_idade,
							esp_in_codigo)
						 VALUES
						   ((SELECT nvl(MAX(a.arv_in_codigo), 0) + 1 FROM mgcustom.api_arvore a),
							upper('{0}'),
							{1},
							{2});";

				sql = String.Format(sql, obj.arv_st_descricao, obj.arv_in_idade, obj.Especie.esp_in_codigo);

				sql += "COMMIT;END;";

				using(var conn = new OracleConnection(dConfig.ObterConteudo().OracleConn))
				{
					OracleCommand cmd = new OracleCommand(sql, conn);

					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();
					conn.Dispose();

					retorno[0] = "N";
					retorno[1] = "Árvore cadastrada com sucesso!";
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


		public static string[] Put(int id, cArvore obj)
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

				if(obj.Especie == null)
				{
					retorno[0] = "S";
					retorno[1] = "Código da espécie não informado.";
					return retorno;
				}

				if(dEspecie.Get(obj.Especie.esp_in_codigo) == null)
				{
					retorno[0] = "S";
					retorno[1] = "Código da espécie não cadastrado.";
					return retorno;
				}

				string sql;

				sql = @"BEGIN ";
				sql += @"UPDATE mgcustom.api_arvore a
						    SET a.arv_st_descricao = upper('{1}'),
							    a.arv_in_idade     = {2},
							    a.esp_in_codigo    = {3}
						  WHERE a.arv_in_codigo = {0};";

				sql = String.Format(sql, id, obj.arv_st_descricao, obj.arv_in_idade, obj.Especie.esp_in_codigo);

				sql += "COMMIT;END;";

				using(var conn = new OracleConnection(dConfig.ObterConteudo().OracleConn))
				{
					OracleCommand cmd = new OracleCommand(sql, conn);

					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();
					conn.Dispose();

					retorno[0] = "N";
					retorno[1] = "Árvore alterada com sucesso!";
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
				sql += @"DELETE FROM mgcustom.api_arvore a WHERE a.arv_in_codigo = {0};";

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
					retorno[1] = "Árvores excluída com sucesso!";
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
