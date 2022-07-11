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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetTeachers()
        {
            var teachers = _mapper.Map<List<TeacherDto>>(_teacherRepository.GetTeachers());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(teachers);
        }

        [HttpGet("{teacherId}")]
        public IActionResult GetTeacher(int teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
                return NotFound();

            var teacher = _mapper.Map<TeacherDto>(_teacherRepository.GetTeacher(teacherId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(teacher);
        }

        [HttpGet("{teacherId}/comments")]
        public IActionResult GetCommentsByTeacher(int teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
                return NotFound();

            var comments = _mapper.Map<List<CommentDto>>(
                _teacherRepository.GetCommentsByTeacher(teacherId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(comments);


        }

        [HttpPost]
        public IActionResult CreateTeacher([FromBody] TeacherDto teacherCreate)
        {
            if (teacherCreate == null)
                return BadRequest(ModelState);
            var teacher = _teacherRepository.GetTeachers()
                .Where(X => X.LastName.Trim().ToUpper() == teacherCreate.LastName.TrimEnd()
                .ToUpper()).FirstOrDefault();
            if (teacher != null)
            {
                ModelState.AddModelError("", "SchoolClub already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var teacherMap = _mapper.Map<Teacher>(teacherCreate);
            if (!_teacherRepository.CreateTeacher(teacherMap))
            {
                ModelState.AddModelError("", "Something went wrong while saveing");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }


        [HttpPut("{teacherId}")]
        public IActionResult UpdateTeacher(int teacherId, [FromBody] TeacherDto updatedTeacher)
        {
            if (updatedTeacher == null)
                return BadRequest(ModelState);

            if (teacherId != updatedTeacher.Id)
                return BadRequest(ModelState);

            if (!_teacherRepository.TeacherExists(teacherId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var teacherMap = _mapper.Map<Teacher>(updatedTeacher);

            if (!_teacherRepository.UpdateTeacher(teacherMap))
            {
                ModelState.AddModelError("", "Something went wrong updating teacher");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{teacherId}")]
        public IActionResult DeleteTeacher(int teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
            {
                return NotFound();
            }

            var teacherToDelete = _teacherRepository.GetTeacher(teacherId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_teacherRepository.DeleteTeacher(teacherToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting teacher");
            }
            return NoContent();




        }
    }
}
