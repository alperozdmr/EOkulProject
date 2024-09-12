using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EOkulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteCalculationController : ControllerBase
    {
        private readonly IStudentClassService _studentClassService;
        private readonly IStudentNoteService _noteService;
        private readonly IUserService<Student> _userService;

        public NoteCalculationController(IStudentClassService studentClassService, IStudentNoteService noteService, IUserService<Student> userService)
        {
            _studentClassService = studentClassService;
            _noteService = noteService;
            _userService = userService;
        }

        [HttpPost]
        [Route("Get All Notes By Class")]
        public async Task<IActionResult> GetAllNotesByClass(string ClassName)
        {
            List<StudentNote> notes = new List<StudentNote>();
            var classId = await _studentClassService.GetClass(ClassName);
            var Students = _userService.Where(x => x.StudentClassId == classId.Data.Id);
            if (Students.Success) {
                for (int i = 0; i < Students.Data.Count(); i++)
                {
                    var note = _noteService.Where(x=>x.StudentId == Students.Data[i].Id);
                    if (note.Success) {
                        notes.AddRange(note.Data);
                    }
                }
                var orderedNotes = notes.OrderByDescending(x => x.WhichTerm);
            return Ok(orderedNotes); 
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("Calculate notes Average of Class")]
        public async Task<float> CalculateNotesAverage(string ClassName , string nameOfLecture)
        {
            List<StudentNote> notes = new List<StudentNote>();
            var classId = await _studentClassService.GetClass(ClassName);
            var Students = _userService.Where(x => x.StudentClassId == classId.Data.Id);
            string lecture = nameOfLecture.ToUpper();
            if (Students.Success) {
                for (int i = 0; i < Students.Data.Count(); i++)
                {
                    var note = _noteService.Where(x => x.StudentId == Students.Data[i].Id);
                    if (note.Success)
                    {
                        notes.AddRange(note.Data);
                    }
                }
            }
            float sum = 0;
            switch (lecture)
            {
                case "MATH":
                    for (int i = 0; i < notes.Count; i++)
                    {
                        sum += notes[i].Math;
                    }
                    break;
                case "PHYSICS":
                    for (int i = 0; i < notes.Count; i++)
                    {
                        sum += notes[i].Physics;
                    }
                    break;
                case "BIOLOGY":
                    for (int i = 0; i < notes.Count; i++)
                    {
                        sum += notes[i].Biology;
                    }
                    break;
                case "CHEMISTRY":
                    for (int i = 0; i < notes.Count; i++)
                    {
                        sum += notes[i].Chemistry;
                    }
                    break;
                case "TURKISH":
                    for (int i = 0; i < notes.Count; i++)
                    {
                        sum += notes[i].Turkish;
                    }
                    break;
                case "HISTORY":
                    for (int i = 0; i < notes.Count; i++)
                    {
                        sum += notes[i].History;
                    }
                    break;
                case "GEOGRAPHY":
                    for (int i = 0; i < notes.Count; i++)
                    {
                        sum += notes[i].Geography;
                    }
                    break;
            }
            return sum/notes.Count;
        }
    }
}
