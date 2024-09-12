using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EOkulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermController : ControllerBase
    {
        private readonly IStudentNoteService _studentNoteService;
        public TermController(IStudentNoteService studentNoteService)
        {
            _studentNoteService = studentNoteService;
        }
        [HttpPost]
        [Route("AddToFirstTerm")]
        public async Task<IActionResult> AddToFirstTerm(AddNoteDto studentNote)
        {
            var result = await _studentNoteService.AddToFirstTerm(studentNote);
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
        public async Task<IActionResult> AddToSecondTerm(AddNoteDto studentNote)
        {
            var result = await  _studentNoteService.AddToSecondTerm(studentNote);
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
        [HttpPost]
        [Route("GetAllByTerms")]
        public IActionResult GetAllByTerms(int term)
        {
            var result = _studentNoteService.GetAllNotesByTerm(term);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost]
        [Route("Get All Notes By Term and Name")]
        public IActionResult GetAllNotesByTermAndName(int term, string name)
        {
            var result = _studentNoteService.GetAllNotesByTermAndName(name.ToUpper(), term);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }
    }
}
