using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentParent_WebApI.Dto;
using StudentParent_WebApI.Interface;

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


    }
}
