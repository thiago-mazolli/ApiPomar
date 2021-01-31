using Business.DAO;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiPomar.Controllers
{
	[EnableCors("*", "*", "*")]
	[RoutePrefix("ApiPomar")]
	public class EspecieController : ApiController
	{
		// GET: ApiPomar/Especie
		[AcceptVerbs("GET")]
		[HttpGet]
		[Route("Especie")]
		public HttpResponseMessage Get()
		{
			List<cEspecie> dados = dEspecie.GetAll();

			if(dados == null || dados.Count == 0)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, "Dados não encontratos");
			}

			return Request.CreateResponse(HttpStatusCode.OK, dados);
		}


		// GET: ApiPomar/Especie/5
		[AcceptVerbs("GET")]
		[HttpGet]
		[Route("Especie/{id}")]
		public HttpResponseMessage Get(int id)
		{
			cEspecie dados = dEspecie.Get(id);

			if(dados == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, "Dados não encontratos para o código: " + id);
			}

			return Request.CreateResponse(HttpStatusCode.OK, dados);
		}


		// POST: ApiPomar/Especie
		[AcceptVerbs("POST")]
		[HttpPost]
		[Route("Especie")]
		public HttpResponseMessage Post([FromBody]cEspecie json)
		{
			string[] retorno = dEspecie.Post(json);

			if(retorno[0] == "S")
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Não foi possível cadastrar a espécie. " + retorno[1]);
			}
			return Request.CreateResponse(HttpStatusCode.OK, retorno[1]);
		}


		// PUT: ApiPomar/Especie/5
		[AcceptVerbs("PUT")]
		[HttpPut]
		[Route("Especie/{id}")]
		public HttpResponseMessage Put(int id, [FromBody]cEspecie json)
		{
			string[] retorno = dEspecie.Put(id, json);

			if(retorno[0] == "S")
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Não foi possível alterar a espécie. " + retorno[1]);
			}
			return Request.CreateResponse(HttpStatusCode.OK, retorno[1]);
		}



		// DELETE: ApiPomar/Especie/5
		[AcceptVerbs("DELETE")]
		[HttpDelete]
		[Route("Especie/{id}")]
		public HttpResponseMessage Delete(int id)
		{
			string[] retorno = dEspecie.Delete(id);

			if(retorno[0] == "S")
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Não foi possível excluir a espécie. " + retorno[1]);
			}
			return Request.CreateResponse(HttpStatusCode.OK, retorno[1]);
		}
	}
}
