using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.Applications.System.FilterPipeLines;
using RESTAPIRNSQLServer.DTOs.CheckInDTOs;
using RESTAPIRNSQLServer.IServices;

namespace RESTAPIRNSQLServer.Controllers
{
    [Route("api/attendences")]
    [ApiController]
    [ResourceFilter]
    public class AttendenceController : ControllerBase
    {
        private readonly IAttendenceService _service;

        public AttendenceController(IAttendenceService service)
        {
            _service = service;
        }
        private string GetPhotoPath(int SubjectId, int CourseId, int ClassId, int ScheduleId, int StudentId)
        {
            return $"https://" +
                $"{Request.Host}" +
                "/api/photos/attendence/" +
                $"{SubjectId}.{CourseId}.{ClassId}.{ScheduleId}.{StudentId}";
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
                report.PhotoPath = GetPhotoPath(
                    report.SubjectId, report.CourseId, report.ClassId, report.ScheduleId, report.StudentId
                );
                return Ok(report);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [AuthorizeActionFilter(Role = "Student")]
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
        [Authorize]
        [AuthorizeActionFilter(Role = "Student")]
        [HttpGet("ptivate")]
        public async Task<ActionResult> GetAttendencesForPrivate([FromQuery] string student)
        {
            try
            {
                var list = await _service.GetAttendencesOfSpecifyStudent(student);
                foreach (var item in list)
                    {
                        item.PhotoPath = GetPhotoPath(
                        item.SubjectId, item.CourseId, item.ClassId, item.ScheduleId, item.StudentId
                    );
                }
                return Ok(list);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error is occured while getting attendence data!");
            }
        }
    }
}