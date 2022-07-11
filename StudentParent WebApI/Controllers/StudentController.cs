using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentParent_WebApI.Dto;
using StudentParent_WebApI.Interface;
using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;

        public StudentController(IStudentRepository studentRepository, IMapper mapper,
            ICommentRepository commentRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _mapper.Map<List<StudentDto>>(_studentRepository.GetStudents());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(students);
        }

        [HttpGet("{studentId}")]
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

        [HttpPost]
        public IActionResult CreateStudent([FromQuery] int parentId, [FromQuery]int subjectId,
            [FromBody] StudentDto studentCreate) //take the id from link
        {
            if (studentCreate == null)
                return BadRequest(ModelState);
            var students = _studentRepository.GetStudents()
                .Where(X => X.Name.Trim().ToUpper() == studentCreate.Name.TrimEnd()
                .ToUpper()).FirstOrDefault();
            if (students != null)
            {
                ModelState.AddModelError("", "Parent already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var studentMap = _mapper.Map<Student>(studentCreate);

            if (!_studentRepository.CreateStudent(parentId, subjectId, studentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }


        [HttpPut("{studentId}")]

        public IActionResult UpdateStudent(int studentId,
    [FromQuery] int parentId, int subjectId,
    [FromBody] StudentDto updatedStudent)
        {
            if (updatedStudent == null)
                return BadRequest(ModelState);

            if (studentId != updatedStudent.Id)
                return BadRequest(ModelState);

            if (!_studentRepository.StudentExists(studentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();


            var studentMap = _mapper.Map<Student>(updatedStudent);

            if (!_studentRepository.UpdateStudent(parentId, subjectId, studentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating student");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        [HttpDelete("{studentId}")]
        public IActionResult DeleteStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
            {
                return NotFound();
            }

            var commentsToDelete = _commentRepository.GetCommentsbyStudentId(studentId);
            var studentToDelete = _studentRepository.GetStudent(studentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_commentRepository.DeleteComments(commentsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            if (!_studentRepository.DeleteStudent(studentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }

    }
}
