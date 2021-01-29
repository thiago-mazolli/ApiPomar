using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiPomar.Controllers
{
	[EnableCors("*", "*", "*")]
	[RoutePrefix("api/ApiPomar")]
	public class ApiPomarController : ApiController
	{
		[AcceptVerbs("GET", "POST")]
		[HttpPost]
		[Route("Teste")]
		public string Teste()
		{
			return "Teste API Pomar OK";
		}

		// GET: api/ApiPomar/5
		public string Get(int id)
		{
			return "value";
		}

		// POST: api/ApiPomar
		public void Post([FromBody]string value)
		{
		}

		// PUT: api/ApiPomar/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/ApiPomar/5
		public void Delete(int id)
		{
		}
	}
}
