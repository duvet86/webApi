using System.Net;
using System.Web.Http;
using Test_API.Business;
using Test_API.Dtcs;
using Test_API.Edm;

namespace Test_API.Controllers
{
    public class TokenController : ApiController
    {
        // This is naive endpoint for demo, it should use Basic authentication to provide token or POST request
        [AllowAnonymous]
        public TokenDtc Get(string username, string password)
        {
            if (CheckUser(username, password))
            {
                return new TokenDtc()
                {
                    Token = JwtManager.GenerateToken(username)
                };
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        public bool CheckUser(string username, string password)
        {
            using (var dbContext = new DBEntities())
            {
                return dbContext.IsValidUser(username, password);
            }
        }
    }
}
