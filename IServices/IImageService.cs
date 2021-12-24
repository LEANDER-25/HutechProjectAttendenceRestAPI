using System.Threading.Tasks;
using RESTAPIRNSQLServer.DTOs.ImageDTOs;

namespace RESTAPIRNSQLServer.IServices
{
    public interface IImageService
    {
        Task<string> SaveAttendenceImage(ImageUploadDTO uploadDTO);
        Task<byte[]> GetAttendenceImage(string imageName);
        Task<string> SaveStudentImage(ImageUploadDTO uploadDTO);
        Task<byte[]> GetStudentImage(string studentID);
    }
}