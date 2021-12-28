using System;
using System.Text.RegularExpressions;
using RESTAPIRNSQLServer.DTOs.CheckInDTOs;
using RESTAPIRNSQLServer.Models;

namespace RESTAPIRNSQLServer.Applications.Logic
{
    public class ValidationInput
    {
        public static bool IsEmailValid(string email)
        {
            if (email.Length < 15)
                return false;
            Regex emailPolicyExpression = new Regex(@"((?=.*\d)(?=.*[a-z])(?=.*[@.]))");
            return emailPolicyExpression.IsMatch(email);
        }
        public static bool IsPasswordValid(string password)
        {
            if (password.Length < 8 || password.Length > 20)
                return false;
            Regex passwordPolicyExpression = new Regex(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]))");
            return passwordPolicyExpression.IsMatch(password);
        }
        public static bool IsStudentCodeValid(string code)
        {
            if (code.Length < 5)
                return false;
            Regex studentCodePolicyExpression = new Regex(@"((?=.*\d))");
            return studentCodePolicyExpression.IsMatch(code);
        }
        public static bool IsImageNameValid(string imageName)
        {
            //^\d+(\.\d+)*$
            Regex studentCodePolicyExpression = new Regex(@"^\d+(\.\d+)*$");
            var isPass = studentCodePolicyExpression.IsMatch(imageName);
            if (isPass == false)
            {
                return false;
            }
            int dotCount = 0;
            foreach (var item in imageName)
            {
                if (item.Equals('.'))
                {
                    dotCount++;
                }
            }
            if (dotCount != 5)
            {
                return false;
            }
            return true;
        }
        public static bool AreDigitsOnly(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            foreach (char character in text)
            {
                if (character < '0' || character > '9')
                    return false;
            }

            return true;
        }
        public static bool ValidateLocation(AttendenceWriteDTO newAttendence, Room room)
        {
            if (newAttendence.Latitude != null && newAttendence.Longtitude != null)
            {
                if (GeocodingMath.IsYInRangeOfX(
                    targetLatitude: newAttendence.Latitude,
                    targetLongtitude: newAttendence.Longtitude,
                    central: room.RoomLocation
                ) is false)
                {
                    throw new Exception("Location is out of range of Study Room");
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (newAttendence.CurLocation == null)
                {
                    throw new Exception("Missing location of student");
                }
                else
                {
                    if (GeocodingMath.IsYInRangeOfX(target: newAttendence.CurLocation, central: room.RoomLocation) is false)
                    {
                        throw new Exception("Location is out of range of Study Room");
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
    }
}