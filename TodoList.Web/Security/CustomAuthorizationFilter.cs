using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebMatrix.WebData;

namespace TodoList.Web.Security
{
    public class CustomAuthorizationFilter : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var identity = Thread.CurrentPrincipal.Identity;
            if (identity == null && HttpContext.Current != null)
                identity = HttpContext.Current.User.Identity;

            if (identity != null && identity.IsAuthenticated)
            {
                var basicAuth = identity as BasicAuthenticationIdentity;

                if (WebSecurity.Login(basicAuth.Name, basicAuth.Password))
                    return true;
            }

            return false;
        }
    }
}