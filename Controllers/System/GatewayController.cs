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
        [HttpPost("students")]
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
        [HttpPost("teachers")]
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
    }
}