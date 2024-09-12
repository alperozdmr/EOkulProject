using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace EOkulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentClassController : Controller
    {
        private readonly IStudentClassService _studentClassService;
        public StudentClassController(IStudentClassService studentClassService)
        {
            _studentClassService = studentClassService;
        }
        [HttpPost]
        [Route("AddClass")]
        public async Task<IActionResult> AddClass(StudentClass studentClass)
        {
            
            var result = await _studentClassService.AddClass(studentClass);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost]
        [Route("AddClasses")]
        public async Task<IActionResult> AddRangeClass(List<StudentClass> studentClass)
        {
            var result = await _studentClassService.AddRangeClass(studentClass);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost]
        [Route("Get Class Id ")]
        public async Task<IActionResult> GetClass(string className)
        {
            var result = await _studentClassService.GetClass(className);
            if (result.Success) { 
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
