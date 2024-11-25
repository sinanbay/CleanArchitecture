using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Attributes
{
    public class AuthorizeBasicAuthAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Items["BasicAuth"] is not true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
