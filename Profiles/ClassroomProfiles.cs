using AutoMapper;
using RESTAPIRNSQLServer.Models;
using RESTAPIRNSQLServer.DTOs.ClassDTOs;

namespace RESTAPIRNSQLServer.Profiles
{
    class ClassroomProfiles : Profile
    {
        public ClassroomProfiles()
        {
            CreateMap<Classroom, ClassReadDTO>();
        }
    }
}