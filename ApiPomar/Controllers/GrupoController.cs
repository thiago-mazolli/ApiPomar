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
	public class GrupoController : ApiController
    {
		// GET: ApiPomar/Grupo
		[AcceptVerbs("GET")]
		[HttpGet]
		[Route("Grupo")]
		public HttpResponseMessage Get()
		{
			List<cGrupo> dados = dGrupo.GetAll();

			if(dados == null || dados.Count == 0)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, "Dados não encontratos");
			}

			return Request.CreateResponse(HttpStatusCode.OK, dados);
		}

		// GET: ApiPomar/Grupo/5
		[AcceptVerbs("GET")]
		[HttpGet]
		[Route("Grupo/{id}")]
		public HttpResponseMessage Get(int id)
		{
			cGrupo dados = dGrupo.Get(id);

			if(dados == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, "Dados não encontratos para o código: " + id);
			}

			return Request.CreateResponse(HttpStatusCode.OK, dados);
		}

		// POST: api/Grupo
		public void Post([FromBody]string value)
        {
        }

        // PUT: api/Grupo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Grupo/5
        public void Delete(int id)
        {
        }
    }
}
