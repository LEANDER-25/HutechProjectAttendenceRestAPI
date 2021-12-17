using AutoMapper;
using RESTAPIRNSQLServer.DTOs.LessonDTOs;
using RESTAPIRNSQLServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPIRNSQLServer.Profiles
{
    public class LessonProfiles : Profile
    {
        public LessonProfiles()
        {
            CreateMap<Lession, LessonReadDTO>();
            CreateMap<LessonWriteDTO, Lession>();
        }
    }
}
