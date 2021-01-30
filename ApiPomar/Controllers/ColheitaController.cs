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

		// GET: api/Colheita/5
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

		// GET: api/Colheita/5
		[AcceptVerbs("GET")]
		[HttpGet]
		[Route("Colheita/Data/{data}")]
		public HttpResponseMessage GetByData(string data)
		{
			List<cColheita> dados = dColheita.GetByData(Convert.ToDateTime(data));

			if(dados == null || dados.Count == 0)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, "Dados não encontratos para a data: " + data);
			}

			return Request.CreateResponse(HttpStatusCode.OK, dados);
		}

		// POST: api/Colheita
		public void Post([FromBody]string value)
        {
        }

        // PUT: api/Colheita/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Colheita/5
        public void Delete(int id)
        {
        }
    }
}
