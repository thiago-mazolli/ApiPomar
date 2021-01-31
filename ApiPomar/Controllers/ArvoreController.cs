using Business.DAO;
using Business.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiPomar.Controllers
{
	[EnableCors("*", "*", "*")]
	[RoutePrefix("ApiPomar")]
	public class ArvoreController : ApiController
	{
		// GET: ApiPomar/Arvore
		[AcceptVerbs("GET")]
		[HttpGet]
		[Route("Arvore")]
		public HttpResponseMessage Get()
		{
			List<cArvore> dados = dArvore.GetAll();

			if(dados == null || dados.Count == 0)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, "Dados não encontratos");
			}

			return Request.CreateResponse(HttpStatusCode.OK, dados);
		}


		// GET: ApiPomar/Arvore/5
		[AcceptVerbs("GET")]
		[HttpGet]
		[Route("Arvore/{id}")]
		public HttpResponseMessage Get(int id)
		{
			cArvore dados = dArvore.Get(id);

			if(dados == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, "Dados não encontratos para o código: " + id);
			}

			return Request.CreateResponse(HttpStatusCode.OK, dados);
		}


		// POST: ApiPomar/Arvore
		[AcceptVerbs("POST")]
		[HttpPost]
		[Route("Arvore")]
		public HttpResponseMessage Post([FromBody]cArvore json)
		{
			string[] retorno = dArvore.Post(json);

			if(retorno[0] == "S")
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Não foi possível cadastrar a árvore. " + retorno[1]);
			}
			return Request.CreateResponse(HttpStatusCode.OK, retorno[1]);
		}


		// PUT: ApiPomar/Arvore/5
		[AcceptVerbs("PUT")]
		[HttpPut]
		[Route("Arvore/{id}")]
		public HttpResponseMessage Put(int id, [FromBody]cArvore json)
		{
			string[] retorno = dArvore.Put(id, json);

			if(retorno[0] == "S")
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Não foi possível alterar a árvore. " + retorno[1]);
			}
			return Request.CreateResponse(HttpStatusCode.OK, retorno[1]);
		}


		// DELETE: ApiPomar/Arvore/5
		[AcceptVerbs("DELETE")]
		[HttpDelete]
		[Route("Arvore/{id}")]
		public HttpResponseMessage Delete(int id)
		{
			string[] retorno = dArvore.Delete(id);

			if(retorno[0] == "S")
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Não foi possível excluir a árvore. " + retorno[1]);
			}
			return Request.CreateResponse(HttpStatusCode.OK, retorno[1]);
		}
	}
}
