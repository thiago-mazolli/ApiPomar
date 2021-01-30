using Business.DAO;
using System.Collections.Generic;
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
		public void Post([FromBody]string value)
        {
        }

		// PUT: ApiPomar/Especie/5
		public void Put(int id, [FromBody]string value)
        {
        }

		// DELETE: ApiPomar/Especie/5
		public void Delete(int id)
        {
        }
    }
}
