using System.Text.RegularExpressions;
using System;

namespace RESTAPIRNSQLServer.Applications.Logic
{
    public class Point
    {
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        private static string[] Split(string input)
        {
            var temp = input;
            Regex policyExpression = new Regex(@"(?=[0-9a-zA-Z.])");
            foreach (var item in temp)
            {
                if(!policyExpression.IsMatch(item.ToString()))
                {
                    input = input.Replace(item, ' ');
                }
            }
            if(input.Contains("  "))
            {
                input = input.Replace("  ", " ");
            }
            var splits = input.Split(" ");
            return splits;
        }
        // example: fullLongLat = 36°57'9"N 110°4'21"E
        // example: fullLongLat = 36°57'9" N 110°4'21" E
        // example: fullLongLat = 36.9525 110.0725
        public static Point ParseDMSString(string fullLongLat)
        {
            Regex policyExpression = new Regex(@"(?=[a-zA-Z])");
            if (policyExpression.IsMatch(fullLongLat) is false)
            {
                var splits = fullLongLat.Split(" ");
                for(int i = 0; i < splits.Length; i++)
                {
                    splits[i] = splits[i].Replace(".", ",");
                }
                return new Point
                {
                    Latitude = Double.Parse(splits[0]),
                    Longtitude = Double.Parse(splits[1])
                };
            }
            else
            {
                var parts = Split(fullLongLat);
                for (int i = 0; i < parts.Length; i++)
                {
                    parts[i] = parts[i].Replace(".", ",");
                }
                var lat = ConvertDMSToDD(parts[0], parts[1], parts[2], parts[3]);
                var lng = ConvertDMSToDD(parts[4], parts[5], parts[6], parts[7]);
                return new Point
                {
                    Latitude = lat,
                    Longtitude = lng
                };
            }
        }
        public static double ConvertDMSToDD(string degrees, string minutes, string seconds, string direction)
        {
            var degree = Double.Parse(degrees);
            var minute = Double.Parse(minutes);
            var second = Double.Parse(seconds);
            var dd = degree + minute / 60 + second / (3600);

            if (direction == "S" || direction == "W")
            {
                dd = dd * -1;
            } 
            // Don't do anything for N or E
            return dd;
        }
    }
}