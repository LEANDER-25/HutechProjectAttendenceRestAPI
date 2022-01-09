using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTAPIRNSQLServer.Applications.System;
using RESTAPIRNSQLServer.Applications.System.FilterPipeLines;
using RESTAPIRNSQLServer.DTOs.ImageDTOs;
using RESTAPIRNSQLServer.IServices;

namespace RESTAPIRNSQLServer.Controllers
{
    [Route("api/photos")]
    [ApiController]
    [ResourceFilter]
    public class ImageController : ControllerBase
    {
        private const string ImageContentType = "image/png";

        private readonly IImageService _service;

        public ImageController(IImageService service)
        {
            _service = service;
        }
        [HttpPost("checkin")]
        public async Task<ActionResult> SaveAttendenceImage([FromQuery] int schedule, [FromForm] ImageUploadDTO uploadDTO)
        {
            await Task.Delay(2000);
            uploadDTO.EntityType = EntityEnum.Attendence;
            uploadDTO.BusinessCode = uploadDTO.BusinessCode + "-" + schedule.ToString();
            try
            {
                string name = await _service.SaveAttendenceImage(uploadDTO);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("attendence")]
        public async Task<ActionResult> GetAttendenceImage([FromQuery]string imagename)
        {
            try
            {
                var photo = await _service.GetAttendenceImage(imagename);
                return File(photo, ImageContentType);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("student")]
        public async Task<ActionResult> SaveStudentImage([FromForm] ImageUploadDTO uploadDTO)
        {
            await Task.Delay(2000);
            uploadDTO.EntityType = EntityEnum.Stundent;
            try
            {
                string name = await _service.SaveStudentImage(uploadDTO);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("student")]
        public async Task<ActionResult> GetStudentImage([FromQuery]string imagename)
        {
            try
            {
                var photo = await _service.GetStudentImage(imagename);
                return File(photo, ImageContentType);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}