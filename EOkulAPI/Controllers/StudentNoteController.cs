using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EOkulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentNoteController : ControllerBase
    {
        IStudentNoteService _studentNoteService;
        public StudentNoteController(IStudentNoteService studentNoteService)
        {
            _studentNoteService = studentNoteService;
        }
        [HttpGet("Get All Notes")]
        public IActionResult GetAllNotes() {
            var result = _studentNoteService.GetAllNotes();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("Get By Name")]
        public IActionResult GetNotesByName(string Name) {
            var result = _studentNoteService.GetAllNotesByName(Name);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("Add Notes")]
        public IActionResult AddNote(StudentNote studentNote) { 
            var result= _studentNoteService.AddNote(studentNote);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
