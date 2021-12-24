using AutoMapper;
using RESTAPIRNSQLServer.DTOs.CheckInDTOs;
using RESTAPIRNSQLServer.Models;

namespace RESTAPIRNSQLServer.Profiles
{
    public class AttendenceProfiles : Profile
    {
        public AttendenceProfiles()
        {
            CreateMap<CheckIn, AttendenceReadDTO>();
            CreateMap<AttendenceWriteDTO, CheckIn>();
        }
    }
}