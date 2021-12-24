using System;
using Microsoft.AspNetCore.Http;
using RESTAPIRNSQLServer.Applications.System;

namespace RESTAPIRNSQLServer.DTOs.ImageDTOs
{
    public class ImageUploadDTO
    {
        public string BusinessCode { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IFormFile Image { get; set; }
        public EntityEnum EntityType { get; set; }
    }    
}