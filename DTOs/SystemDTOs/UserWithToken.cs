using RESTAPIRNSQLServer.DTOs.PersonDTOs;
using RESTAPIRNSQLServer.Services.SystemServices;
using RESTAPIRNSQLServer.Applications.System;

namespace RESTAPIRNSQLServer.DTOs.SystemDTOs
{
    public class UserWithToken
    {
        // public string AccessToken { get; set; }
        // public string RefreshToken { get; set; }
        public string Token { get; set; }

        public string Code { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role? Role { get; set;}
    }
}