using System;
using System.Collections.Generic;
using RESTAPIRNSQLServer.DTOs.CheckInDTOs;

namespace RESTAPIRNSQLServer.Applications.Logic
{
    public class ValidAttendenceDevice
    {
        public static bool IsDuplicateDevice(List<AttendenceReadDTO> checkinlist, AttendenceWriteDTO newAttendence)
        {
            foreach (var item in checkinlist)
            {
                if ((item.DeviceId != null || !item.DeviceId.Equals("")) && newAttendence.DeviceId.Equals(item.DeviceId))
                {
                    throw new Exception($"Duplicate device, you are using same device with student {item.StudentCode}");
                }
            }
            return true;
        }
    }
}