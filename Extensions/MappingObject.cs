using RESTAPIRNSQLServer.DTOs.SystemDTOs;
using RESTAPIRNSQLServer.Models;

namespace RESTAPIRNSQLServer.Extensions
{
    public static class MappingObject
    {
        public static Student MapToStudent(this StudentRegisterDTO registerDTO)
        {
            return new Student()
            {
                StudentCode = registerDTO.BusinessCode,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email
            };
        }
        public static Teacher MapToTeacher(this TeacherRegisterDTO registerDTO)
        {
            return new Teacher()
            {
                TeacherCode = registerDTO.BusinessCode,
                TeacherFirstName = registerDTO.FirstName,
                TeacherLastName = registerDTO.LastName,
                Email = registerDTO.Email
            };
        }
    }
}