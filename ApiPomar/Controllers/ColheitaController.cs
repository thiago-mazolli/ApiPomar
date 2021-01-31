using Business.DAO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiPomar.Controllers
{
	[EnableCors("*", "*", "*")]
	[RoutePrefix("ApiPomar")]
	public class ColheitaController : ApiController
    {
		// GET: ApiPomar/Colheita
		[AcceptVerbs("GET")]
		[HttpGet]
		[Route("Colheita")]
		public HttpResponseMessage Get()
		{
			List<cColheita> dados = dColheita.GetAll();

			if(dados == null || dados.Count == 0)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, "Dados não encontratos");
			}

			return Request.CreateResponse(HttpStatusCode.OK, dados);
		}


		// GET: ApiPomar/Colheita/5
		[AcceptVerbs("GET")]
		[HttpGet]
		[Route("Colheita/{id}")]
		public HttpResponseMessage Get(int id)
        {
			cColheita dados = dColheita.Get(id);

			if(dados == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, "Dados não encontratos para o código: " + id);
			}

			return Request.CreateResponse(HttpStatusCode.OK, dados);
		}


		// GET: ApiPomar/Colheita/Data/31-01-2021
		[AcceptVerbs("GET")]
		[HttpGet]
		[Route("Colheita/Data/{data}")]
		public HttpResponseMessage GetByData(string data)
		{
			//List<cColheita> dados = dColheita.GetByData(Convert.ToDateTime(data));
			List<cColheita> dados = dColheita.GetByData(data);

			if(dados == null || dados.Count == 0)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, "Dados não encontratos para a data: " + data);
			}

			return Request.CreateResponse(HttpStatusCode.OK, dados);
		}


		// POST: ApiPomar/Colheita
		[AcceptVerbs("POST")]
		[HttpPost]
		[Route("Colheita")]
		public HttpResponseMessage Post([FromBody]cColheita json)
		{
			string[] retorno = dColheita.Post(json);

			if(retorno[0] == "S")
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Não foi possível cadastrar a colheita. " + retorno[1]);
			}
			return Request.CreateResponse(HttpStatusCode.OK, retorno[1]);
		}


		// PUT: ApiPomar/Colheita/5
		public void Put(int id, [FromBody]string value)
        {
        }


		// DELETE: ApiPomar/Colheita/5
		[AcceptVerbs("DELETE")]
		[HttpDelete]
		[Route("Colheita/{id}")]
		public HttpResponseMessage Delete(int id)
		{
			string[] retorno = dColheita.Delete(id);

			if(retorno[0] == "S")
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Não foi possível excluir a colheita. " + retorno[1]);
			}
			return Request.CreateResponse(HttpStatusCode.OK, retorno[1]);
		}
	}
}
