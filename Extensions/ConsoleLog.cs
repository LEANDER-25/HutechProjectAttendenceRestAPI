using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RESTAPIRNSQLServer.Extensions
{
    public static class ConsoleLog
    {
        public static void LogOut(this object objects)
        {
            var jsonString = JsonConvert.SerializeObject(objects);
            jsonString = jsonString.Replace("{", "{\n");
            jsonString = jsonString.Replace(",", ",\n");
            jsonString = jsonString.Replace("}", "\n}");
            Console.WriteLine(jsonString);
        }
        public static void LogOutList(this IEnumerable<object> objs)
        {
            foreach (var item in objs)
            {
                item.LogOut();
            }
        }
    }
}