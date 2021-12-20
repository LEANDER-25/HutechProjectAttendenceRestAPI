using System.Threading.Tasks;
using RESTAPIRNSQLServer.DTOs.SystemDTOs;

namespace RESTAPIRNSQLServer.IServices
{
    public interface IGatewayService
    {
        Task<UserWithToken> StudentLogin(UserLoginDTO user);
        Task<UserWithToken> TeacherLogin(UserLoginDTO user);
        Task<bool> StudentRegister(StudentRegisterDTO student);
        Task<bool> TeacherRegister(TeacherRegisterDTO teacher);        

    }
}