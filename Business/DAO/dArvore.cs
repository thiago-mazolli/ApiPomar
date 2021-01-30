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

		//public static cRetorno Add(cArvore objArvore) {
		//	try
		//	{
		//		if(objArvore == null)
		//		{
		//			throw new ArgumentNullException("item");
		//		}

		//		string sql;

		//		sql = @"BEGIN";
		//		sql += @"
		//                      INSERT INTO MGADO.ADO_INT_AGENTES
		//                        (INT_ST_OPERACAO,
		//                         INT_DT_OPERACAO,
		//                         AGN_ST_CPF,
		//                         AGN_ST_NOME,
		//                         AGN_CH_TIPOPESSOAFJ,
		//                         UF_ST_SIGLA,
		//                         PA_ST_SIGLA,
		//                         AGN_ST_STATUS,
		//                         AGN_DT_INICIOMOV,
		//                         AGN_ST_CNPJ_EMPRESA,
		//                         FNC_ST_ALTERNATIVO,
		//                         PRO_ST_CODIGO,
		//                         TIP_ST_ALTERNATIVO,
		//                         TUR_ST_ALTERNATIVO,
		//                         AGN_CH_MULTIOBRA,
		//                         AGN_DT_NASCIMENTO)
		//                      VALUES
		//                        ('{0}',--INT_ST_OPERACAO,
		//                         to_date('{1}','DD/MM/RRRR'),--INT_DT_OPERACAO,
		//                         '{2}',--AGN_ST_CPF,
		//                         '{3}',--AGN_ST_NOME,
		//                         '{4}',--AGN_CH_TIPOPESSOAFJ,
		//                         '{5}',--UF_ST_SIGLA,
		//                         '{6}',--PA_ST_SIGLA,
		//                         '{7}',--AGN_ST_STATUS,
		//                         TO_DATE('{8}','DD/MM/RRRR'),--AGN_DT_INICIOMOV,
		//                         '{9}',--AGN_ST_CNPJ_EMPRESA,
		//                         '{10}',--FNC_ST_ALTERNATIVO,
		//                         '{11}',--PRO_ST_CODIGO,
		//                         '{12}',--TIP_ST_ALTERNATIVO,
		//                         '{13}',--TUR_ST_ALTERNATIVO,
		//                         '{14}',--AGN_CH_MULTIOBRA,
		//                         TO_DATE('{15}','DD/MM/RRRR'));--AGN_DT_NASCIMENTO);
		//                       ";


		//		sql = String.Format(sql,
		//					  INT_ST_OPERACAO,
		//					  INT_DT_OPERACAO,
		//					  AGN_ST_CPF,
		//					  AGN_ST_NOME,
		//					  AGN_CH_TIPOPESSOAFJ,
		//					  UF_ST_SIGLA,
		//					  PA_ST_SIGLA,
		//					  AGN_ST_STATUS,
		//					  AGN_DT_INICIOMOV,
		//					  AGN_ST_CNPJ_EMPRESA,
		//					  FNC_ST_ALTERNATIVO,
		//					  PRO_ST_CODIGO,
		//					  TIP_ST_ALTERNATIVO,
		//					  TUR_ST_ALTERNATIVO,
		//					  AGN_CH_MULTIOBRA,
		//					  AGN_DT_NASCIMENTO
		//					  );

		//		sql += "COMMIT;END;";

		//		using(var oconn = new OracleConnection(ConfigurationSettings.AppSettings["ConnectString"]))
		//		{
		//			OracleCommand ocmd = new OracleCommand(sql, oconn);
		//			try
		//			{
		//				oconn.Open();
		//				ocmd.ExecuteNonQuery();
		//				oconn.Close();
		//				return "Processo concluído com sucesso!";
		//			}
		//			catch(Exception erro)
		//			{
		//				return "Houve o seguinte erro ao inserir os registros no Mega: " + erro.Message.ToString();
		//			}
		//		}



		//	}
		//	catch(Exception)
		//	{

		//		throw;
		//	}

		//	if(objArvore == null)
		//	{
		//		throw new ArgumentNullException("item");
		//	}

		//	arvore.Add(objArvore);
		//	return objArvore;
		//}

	}
}
