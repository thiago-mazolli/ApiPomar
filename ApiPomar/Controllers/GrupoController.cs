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


		// POST: ApiPomar/Grupo
		[AcceptVerbs("POST")]
		[HttpDelete]
		[Route("Grupo")]
		public HttpResponseMessage Post([FromBody]cGrupo json)
        {
			string[] retorno = dGrupo.Post(json);

			if(retorno[0] == "S")
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Não foi possível cadastrar o grupo de árvore. " + retorno[1]);
			}
			return Request.CreateResponse(HttpStatusCode.OK, retorno[1]);
		}


        // PUT: api/Grupo/5
        public void Put(int id, [FromBody]string value)
        {
        }


		// DELETE: ApiPomar/Grupo/5
		[AcceptVerbs("DELETE")]
		[HttpDelete]
		[Route("Grupo/{id}")]
		public HttpResponseMessage Delete(int id)
		{
			string[] retorno = dGrupo.Delete(id);

			if(retorno[0] == "S")
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Não foi possível excluir o grupo de árvore. " + retorno[1]);
			}
			return Request.CreateResponse(HttpStatusCode.OK, retorno[1]);
		}
	}
}
