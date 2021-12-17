using Microsoft.AspNetCore.Mvc;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPIRNSQLServer.Controllers
{
    [Route("api/lessons")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _service;

        public LessonController(ILessonService service)
        {
            _service = service;
        }
        [HttpGet("all")]
        public async Task<ActionResult> GetAllLesson()
        {
            var readDTO = await _service.GetAll();
            return Ok(readDTO);
        }
        [HttpPost("filter")]
        public async Task<ActionResult> FilterLessons([FromBody] FilterLessonItems filter)
        {
            var readDTO = await _service.FilterLessions(filter);
            return Ok(readDTO);
        }
    }
}
