using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RESTAPIRNSQLServer.Applications.System.Enums;

namespace RESTAPIRNSQLServer.Applications.System.FilterPipeLines
{
    public class PrivateAccessFilter : Attribute, IActionFilter
    {
        public AccessForm AccessForm { get; set; }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var claims = SystemTools.ClaimIdentityFromToken(context);
            var queryString = context.HttpContext.Request.QueryString.ToString();
            queryString = queryString.Substring(1);
            queryString = queryString.ToLower();
            string[] queryCollection = null;
            string temp = string.Empty;
            bool found = false;
            if (queryString.Contains("&"))
            {
                queryCollection = queryString.Split('&');
                foreach (var item in queryCollection)
                {
                    var itemSplits = item.Split('=');
                    if (this.AccessForm.ToString().ToLower().Contains(itemSplits[0]))
                    {
                        found = true;
                        temp = itemSplits[1];
                    }
                }
            }
            else
            {
                var splits = queryString.Split('=');
                if (this.AccessForm.ToString().ToLower().Contains(splits[0]))
                {
                    found = true;
                    temp = splits[1];
                }
            }
            if (found == false)
            {
                context.Result = new UnauthorizedObjectResult("Message: Your access is denied!");
            }
            else
            {
                switch (this.AccessForm)
                {
                    case AccessForm.StudentCode:
                    case AccessForm.TeacherCode:
                        if (!temp.Equals(claims[2].Value))
                        {
                            context.Result = new UnauthorizedObjectResult("Message: Your access is denied! You are trying to view the other user information!");
                            break;
                        }
                        break;
                    default:
                        if (!temp.Equals(claims[0].Value))
                        {
                            context.Result = new UnauthorizedObjectResult("Message: Your access is denied! You are trying to view the other user information!");
                            break;
                        }
                        break;
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

    }
}