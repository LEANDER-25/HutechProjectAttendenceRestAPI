using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RESTAPIRNSQLServer.Applications.System.FilterPipeLines
{
    public class ResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var host = context.HttpContext.Request.Host;
            var path = context.HttpContext.Request.Path;
            Console.WriteLine($"[Resource Filter] At {DateTime.Now} : {host}/{path} is executing...");
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            var host = context.HttpContext.Request.Host;
            var path = context.HttpContext.Request.Path;
            Console.WriteLine($"[Resource Filter] At {DateTime.Now} : {host}/{path} is executed");
        }

    }
}