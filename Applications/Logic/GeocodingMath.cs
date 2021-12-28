using System;

namespace RESTAPIRNSQLServer.Applications.Logic
{
    public class GeocodingMath
    {
        //Range in 50 m
        public const double Radius = 0.05;
        // Calculate the distance from point X to point Y
        // X: central point
        // Y: target point
        public static double GetDistanceFromXtoYFlat(Point APoint, Point OPoint)
        {
            // Find the distance with pitago formula
            // bc^2 = ac^2 + ab^2
            
            var ac = APoint.Latitude - OPoint.Latitude;
            var ab = APoint.Longtitude - OPoint.Longtitude;
            var bc = Math.Sqrt(Math.Pow(ac, 2) + Math.Pow(ab, 2));
            return bc;
        }
        public static bool IsYInRangeOfX(string target, string central)
        {
            Point APoint = Point.ParseDMSString(target);
            Point OPoint = Point.ParseDMSString(central);
            var distanceFromXtoY = GetDistanceFromXtoYEarth(central: OPoint, target: APoint);
            if (distanceFromXtoY < Radius) return true;
            else return false;
        }
        public static bool IsYInRangeOfX(string Latitude2, string Longtitude2, string Latitude1, string Longtitude1) {
            string target = Latitude2 + " " + Longtitude2;
            string central = Latitude1 + " " + Longtitude1;
            if (IsYInRangeOfX(target, central) is true) return true;
            else return false;
        }
        public static bool IsYInRangeOfX(string targetLatitude, string targetLongtitude, string central) {
            string target = targetLatitude + " " + targetLongtitude;
            if (IsYInRangeOfX(target, central) is true) return true;
            else return false;
        }
        public static double GetDistanceFromXtoYEarth(Point central, Point target)
        {
            //R = 6371 m
            double R = 6371;
            double dLat = (target.Latitude - central.Latitude) * (Math.PI / 180);
            double dLon = (target.Longtitude - central.Longtitude) * (Math.PI / 180);
            double la1ToRad = central.Latitude * (Math.PI / 180);
            double la2ToRad = target.Latitude * (Math.PI / 180);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(la1ToRad)
                    * Math.Cos(la2ToRad) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            //return in km
            return d;
        }
    }
}