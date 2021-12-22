using AutoMapper;
using RESTAPIRNSQLServer.DTOs.ScheduleDTOs;
using RESTAPIRNSQLServer.Models;

namespace RESTAPIRNSQLServer.Profiles
{
    public class ScheduleProfiles : Profile
    {
        public ScheduleProfiles()
        {
            CreateMap<Schedule, ScheduleReadDTO>();
        }
    }
}