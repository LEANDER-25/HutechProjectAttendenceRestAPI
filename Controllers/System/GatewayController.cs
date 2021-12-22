using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RESTAPIRNSQLServer.DTOs.SystemDTOs;
using RESTAPIRNSQLServer.IServices;

namespace RESTAPIRNSQLServer.Controllers.System
{
    [Route("gateway")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly IGatewayService _service;

        public GatewayController(IGatewayService service)
        {
            _service = service;
        }
        [HttpPost("students/login")]
        public async Task<ActionResult> StudentLogIn([FromBody] UserLoginDTO user)
        {
            try
            {                 
                var userWithToken = await _service.StudentLogin(user);
                return Ok(userWithToken);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("teachers/login")]
        public async Task<ActionResult> TeacherLogIn([FromBody] UserLoginDTO user)
        {
            try
            {                 
                var userWithToken = await _service.TeacherLogin(user);
                return Ok(userWithToken);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("student/register")]
        public async Task<ActionResult> StudentRegister([FromBody] StudentRegisterDTO studentRegisterDTO)
        {
            try
            {
                var isRegitered = await _service.StudentRegister(studentRegisterDTO);
                return Ok(new {
                    Message = $"Register successfully: {studentRegisterDTO.Email}"
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("teacher/register")]
        public async Task<ActionResult> TeacherRegister([FromBody] TeacherRegisterDTO teacherRegisterDTO)
        {
            try
            {
                var isRegitered = await _service.TeacherRegister(teacherRegisterDTO);
                return Ok(new {
                    Message = $"Register successfully: {teacherRegisterDTO.Email}"
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}