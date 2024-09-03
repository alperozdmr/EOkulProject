using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EOkulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermController : ControllerBase
    {
        IStudentNoteService _studentNoteService;
        public TermController(IStudentNoteService studentNoteService)
        {
            _studentNoteService = studentNoteService;
        }
        [HttpPost]
        [Route("AddToFirstTerm")]
        public IActionResult AddToFirstTerm(StudentNote studentNote)
        {
            studentNote.WhichTerm = 1;
            var result = _studentNoteService.AddNote(studentNote);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost]
        [Route("GetNameByFirstTerm")]
        public IActionResult GetNameByFirstTerm(string Name)
        {
            var result = _studentNoteService.GetAllNotesByName(Name.ToUpper());
            var data = result.Data.Where(x => x.WhichTerm == 1).ToList(); 
            if (result.Success)
            {
                return Ok(data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost]
        [Route("AddToSecondTerm")]
        public IActionResult AddToSecondTerm(StudentNote studentNote)
        {
            studentNote.WhichTerm = 2;
            var result = _studentNoteService.AddNote(studentNote);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost]
        [Route("GetNameBySecondTerm")]
        public IActionResult GetNameBySecondTerm(string Name)
        {
            var result = _studentNoteService.GetAllNotesByName(Name.ToUpper());
            var data = result.Data.Where(x => x.WhichTerm == 2).ToList();
            if (result.Success)
            {
                return Ok(data);
            }
            return BadRequest(result.Message);
        }
        //[HttpPost]
        //[Route("GetAllByTerms")]
        //public IActionResult GetAllByTerms(int term)
        //{
        //    var result = _studentNoteService.GetAllNotesByTerm(term);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result.Message);
        //}
    }
}
