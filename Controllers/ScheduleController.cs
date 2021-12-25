using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.Applications.System.FilterPipeLines;
using RESTAPIRNSQLServer.IServices;
using RESTAPIRNSQLServer.Services;

namespace RESTAPIRNSQLServer.Controllers
{
    [Route("api/schedules")]
    [ApiController]
    [ResourceFilter]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;

        public ScheduleController(IScheduleService service)
        {
            _service = service;
        }
        [HttpGet("all")]
        public async Task<ActionResult> GetAll([FromQuery] PaginationOption option)
        {
            var scheduleList = await _service.GetAll(option);
            return Ok(scheduleList);
        }
        [HttpGet("detail")]
        public async Task<ActionResult> GetDetail([FromQuery] FilterScheduleItems filter)
        {
            var detail = await _service.GetByID(filter);
            if(detail == null)
            {
                return NotFound();
            }
            return Ok(detail);
        }
        [HttpGet("detail/student")]
        public async Task<ActionResult> GetByStudent([FromQuery] string studentCode)
        {
            var detail = await _service.GetByStudent(studentCode);
            if(detail == null || detail.Count() == 0)
            {
                return NotFound();
            }
            return Ok(detail);
        }
    }
}