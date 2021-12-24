using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.DTOs.CheckInDTOs;
using RESTAPIRNSQLServer.IServices;

namespace RESTAPIRNSQLServer.Controllers
{
    [Route("api/attendences")]
    [ApiController]
    public class AttendenceController : ControllerBase
    {
        private readonly IAttendenceService _service;

        public AttendenceController(IAttendenceService service)
        {
            _service = service;
        }
        [HttpGet("list")]
        public async Task<ActionResult> GetListAttendenceOfSchedule([FromQuery] FilterScheduleItems filter, [FromQuery] PaginationOption option)
        {
            try
            {
                var list = await _service.GetAttendenceListBySchedule(filter, option);
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("detail")]
        public async Task<ActionResult> GetDetailAttendenceReport([FromQuery] FilterAttendenceItems filter)
        {
            try
            {
                var report = await _service.GetSingleRecordAttendence(filter);
                report.PhotoPath = $"https://" +
                $"{Request.Host}" +
                "/api/photos/attendence/" +
                $"{report.SubjectId}.{report.CourseId}.{report.ClassId}.{report.ScheduleId}.{report.StudentId}";
                return Ok(report);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("checkin")]
        public async Task<ActionResult> CreateAttendenceReport([FromBody] AttendenceWriteDTO newAttendence)
        {
            try
            {
                var isCreated = await _service.InsertNewRecord(newAttendence);
                if (isCreated is false)
                    return StatusCode(500, "Fail to create the record");
                else
                    return StatusCode(201, "Created successfully record");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}