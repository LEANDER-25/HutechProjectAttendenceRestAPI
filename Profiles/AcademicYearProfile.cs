using AutoMapper;
using RESTAPIRNSQLServer.DTOs.AcademicYearDTOs;
using RESTAPIRNSQLServer.Models;

namespace RESTAPIRNSQLServer.Profiles
{
    public class AcademicYearProfile : Profile
    {
        public AcademicYearProfile()
        {
            CreateMap<AcademicYear, AcademicYearReadDTO>();
        }
    }
}