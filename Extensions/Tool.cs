using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;
using RESTAPIRNSQLServer.DTOs.ScheduleDTOs;
using RESTAPIRNSQLServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace RESTAPIRNSQLServer.Extensions
{
    public static class Tool
    {
        public static string MD5Hash(this string password)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(password));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public static IQueryable<ScheduleReadDTO> SelectScheduleHardCode(this IQueryable<Schedule> list)
        {
            var result = list.Select(
                s => new ScheduleReadDTO
                {
                    ScheduleId = s.ScheduleId,
                    CourseId = s.CourseId,
                    ClassId = s.ClassId,
                    SubjectId = s.SubjectId,
                    SubjectCode = s.Assign.Course.Subject.SubjectCode,
                    SubjectName = s.Assign.Course.Subject.SubjectName,
                    StudyDate = s.StudyDate,
                    RoomId = s.RoomId
                }
            );
            return result;
        }
        public static int BinarySearch<T>(List<T> list, T target)
        {
            var result = BinarySearchRun(list, 0, list.Count - 1, target);
            if(result < 0)
            {
                throw new Exception("Element not found");
            }
            return result;
        }
        private static int BinarySearchRun<T>(List<T> arr, int l, int r, T x)
        {
            if (r >= l)
            {
                int mid = l + (r - l) / 2;
                var itemHashCode = arr[mid].GetHashCode();
                var xHashCode = x.GetHashCode();

                // If the element is present at the middle
                // itself
                if (itemHashCode == xHashCode)
                    return mid;

                // If element is smaller than mid, then
                // it can only be present in left subarray
                if (itemHashCode > xHashCode)
                    return BinarySearchRun(arr, l, mid - 1, x);

                // Else the element can only be present
                // in right subarray
                return BinarySearchRun(arr, mid + 1, r, x);
            }

            // We reach here when element is not
            // present in array
            return -1;
        }
        public static IQueryable<Schedule> SchedulesOrdered(this IQueryable<Schedule> schedules)
        {
            return schedules.OrderBy(s => s.ClassId).ThenBy(s => s.CourseId).ThenBy(s => s.ScheduleId);
        }
        ///<summary>
        ///Include Assign and Course and Subject not Include Teacher
        ///</summary>
        public static IQueryable<Schedule> ScheduleIncludeBottomAssign(this IQueryable<Schedule> schedules)
        {
            return schedules.Include(s => s.Assign).ThenInclude(a => a.Course).ThenInclude(c => c.Subject);
        }
        ///<summary>
        ///Include Assign and StudyShifts
        ///</summary>
        public static IQueryable<Schedule> ScheduleIncludeLeftAssign(this IQueryable<Schedule> schedules)
        {
            return schedules.Include(s => s.Assign).ThenInclude(a => a.StudyShifts);
        }
        ///<summary>
        ///Include Assign and Class
        ///</summary>
        public static IQueryable<Schedule> ScheduleIncludeRightAssign(this IQueryable<Schedule> schedules)
        {
            return schedules.Include(s => s.Assign).ThenInclude(a => a.Class);
        }
    }
}