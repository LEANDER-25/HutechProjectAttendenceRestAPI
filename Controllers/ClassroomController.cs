using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.IServices;

namespace RESTAPIRNSQLServer.Controllers
{
    [Route("api/classrooms")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassService _service;

        public ClassroomController(IClassService service)
        {
            _service = service;
        }
        [HttpGet("all")]
        public async Task<ActionResult> GetAllClassrooms()
        {
            try
            {
                var classrooms = await _service.GetAllClass();
                return Ok(classrooms);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error is occured while getting classrooms data!");
            }
        }
        [HttpGet("student")]
        public async Task<ActionResult> GetClassroomsByStudent([FromQuery] FilterClassroomItems filter)
        {
            try
            {
                var classrooms = await _service.GetClassByStudent(filter.StudentCode);
                return Ok(classrooms);
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error is occured while getting classrooms data of student: {filter.StudentCode} !");
            }
        }
        [HttpGet("specify")]
        public async Task<ActionResult> GetSpecifyClass([FromQuery] FilterClassroomItems filter)
        {
            try
            {
                var classrooms = await _service.GetSpecifyClass(filter);
                return Ok(classrooms);
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error is occured while getting classroom data!");
            }
        }
    }
}