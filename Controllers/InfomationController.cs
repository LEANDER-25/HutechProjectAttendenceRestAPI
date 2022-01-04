using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.IServices;

namespace RESTAPIRNSQLServer.Controllers
{
    [Route("api/information")]
    [ApiController]
    public class InfomationController : ControllerBase
    {
        private readonly IClassService _classroomService;
        private readonly IAcademicYearService _academicService;

        public InfomationController(IClassService classroomService, IAcademicYearService academicService)
        {
            _classroomService = classroomService;
            _academicService = academicService;
        }
        //GET : api/information/classrooms/all
        [HttpGet("classrooms/all")]
        public async Task<ActionResult> GetAllClassrooms()
        {
            try
            {
                var classrooms = await _classroomService.GetAllClass();
                return Ok(classrooms);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error is occured while getting classrooms data!");
            }
        }
        [HttpGet("classrooms/student")]
        public async Task<ActionResult> GetClassroomsByStudent([FromQuery] FilterClassroomItems filter)
        {
            try
            {
                var classrooms = await _classroomService.GetClassByStudent(filter.StudentCode);
                return Ok(classrooms);
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error is occured while getting classrooms data of student: {filter.StudentCode} !");
            }
        }
        [HttpGet("classrooms/specify")]
        public async Task<ActionResult> GetSpecifyClass([FromQuery] FilterClassroomItems filter)
        {
            try
            {
                var classrooms = await _classroomService.GetSpecifyClass(filter);
                return Ok(classrooms);
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error is occured while getting classroom data!");
            }
        }
        [HttpGet("years/all")]
        public async Task<ActionResult> GetAllAcedemicYears()
        {
            try
            {
                var yearsList = await _academicService.GetAllYears();
                return Ok(yearsList);                 
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error is occured while getting academic year data!");
            }
        }
        [HttpGet("years")]
        public async Task<ActionResult> GetAllAcedemicYears([FromQuery] string year)
        {
            try
            {
                var yearDto = await _academicService.GetSpecifyYears(year);
                return Ok(yearDto);                 
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error is occured while getting academic year data!");
            }
        }
    }
}