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
		// GET: ApiPomar/arvore
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
		public void Post([FromBody]string value)
		{
		}

		// PUT: ApiPomar/Arvore/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: ApiPomar/Arvore/5
		public void Delete(int id)
		{
		}
	}
}
