using RESTAPIRNSQLServer.Services.SystemServices;

namespace RESTAPIRNSQLServer.DTOs.SystemDTOs
{
    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
}