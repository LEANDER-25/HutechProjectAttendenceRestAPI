using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RESTAPIRNSQLServer.Applications.System.FilterPipeLines
{
    public class AuthorizeActionFilter : Attribute,IActionFilter
    {
        public string Role { get; set; }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var decodeToken = context.HttpContext.User.Identity as ClaimsIdentity;
            var claims = decodeToken.Claims.ToList();
            var roleToken = claims[1].Value;
            if(!roleToken.Equals(this.Role))
            {
                context.Result = new UnauthorizedObjectResult("Message: Your access is denied!");
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

    }
}