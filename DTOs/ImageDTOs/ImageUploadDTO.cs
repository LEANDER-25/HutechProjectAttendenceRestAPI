using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using RESTAPIRNSQLServer.Applications.System;

namespace RESTAPIRNSQLServer.DTOs.ImageDTOs
{
    public class ImageUploadDTO
    {
        [Required]
        public string BusinessCode { get; set; }
        public DateTime? CreatedAt { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public EntityEnum EntityType { get; set; }
    }    
}