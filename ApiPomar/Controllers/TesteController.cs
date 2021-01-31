using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiPomar.Controllers
{
	[EnableCors("*", "*", "*")]
	[RoutePrefix("ApiPomar")]
	public class TesteController : ApiController
    {
		// GET: ApiPomar/Teste
		[AcceptVerbs("GET")]
		[HttpGet]
		[Route("")]
		public string Get()
        {
            return "API OK";
        }
    }
}
