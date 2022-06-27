using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentParent_WebApI.Dto;
using StudentParent_WebApI.Interface;

namespace StudentParent_WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _mapper.Map<List<StudentDto>>(_studentRepository.GetStudents());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(students);
        }

        [HttpGet("(studentId)")]
        public IActionResult GetStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
                return NotFound();

            var student = _mapper.Map<StudentDto>(_studentRepository.GetStudent(studentId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(student);
        }
        [HttpGet("{studentId}/rating")]
        public IActionResult GetStudentRating(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
                return NotFound();

            var rating = _studentRepository.GetStudentRating(studentId);

            if (!ModelState.IsValid)
                return BadRequest(rating);
            return Ok(rating);

        }
    }
}
