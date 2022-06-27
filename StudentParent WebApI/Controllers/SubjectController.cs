using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentParent_WebApI.Dto;
using StudentParent_WebApI.Interface;

namespace StudentParent_WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectRepository subjectRepository,IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetSubjects()
        {
            var subjects = _mapper.Map<List<SubjectDto>>(_subjectRepository.GetSubjects());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(subjects);
        }

        [HttpGet("{subjectId}")]
        public IActionResult GetSubject(int subjectId)
        {
            if (!_subjectRepository.SubjectExists(subjectId))
                return NotFound();

            var subject = _mapper.Map<SubjectDto>(_subjectRepository.GetSubject(subjectId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(subject);
        }

        [HttpGet("student/{subjectId}")] // deal with nested
        public IActionResult GetStudentBySubjectId(int subjectId)
        {
            var students = _mapper.Map<List<StudentDto>>(
                _subjectRepository.GetStudentBySubjectId(subjectId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(students);

        }
    }

}
