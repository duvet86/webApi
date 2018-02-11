using System.Web.Http;
using Test_API.Filters;

namespace Test_API.Controllers
{
    public class ValueController : ApiController
    {
        [JwtAuthentication]
        public string Get()
        {
            return "value";
        }
    }
}
