using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.Applications.System.FilterPipeLines;
using RESTAPIRNSQLServer.IServices;
using RESTAPIRNSQLServer.Applications.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTAPIRNSQLServer.DTOs.LessonDTOs;

namespace RESTAPIRNSQLServer.Controllers
{
    [Route("api/lessons")]
    [ApiController]
    [ResourceFilter]
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

            //return Ok(
            //    new LessonAll
            //    {
            //        ListDTO = readDTO.ToList()
            //    }
            //);
        }
        //public class LessonAll
        //{
        //    public IList<LessonReadDTO> ListDTO { get; set; }
        //}
        //[Authorize]
        //[AuthorizeActionFilter(Role = "Student")]
        [HttpGet("id={id}")]
        public async Task<ActionResult> GetByID(int id)
        {
            var lesson = await _service.GetByID(id);
            if(lesson == null)
                return NotFound($"lesson with Id = {id}");
            return Ok(lesson);
        }
        [HttpPost("filter")]
        public async Task<ActionResult> FilterLessons([FromBody] FilterLessonItems filter)
        {
            var readDTO = await _service.FilterLessions(filter);
            return Ok(readDTO);
        }
    }
}
