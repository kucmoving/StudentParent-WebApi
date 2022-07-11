using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentParent_WebApI.Dto;
using StudentParent_WebApI.Interface;
using StudentParent_WebApI.Models;

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


        [HttpPost]
        public IActionResult CreateSubject([FromBody] SubjectDto subjectCreate)
        {
            if (subjectCreate == null)
                return BadRequest(ModelState);
            var subject = _subjectRepository.GetSubjects()
                .Where(X => X.Name.Trim().ToUpper() == subjectCreate.Name.TrimEnd()
                .ToUpper()).FirstOrDefault();
            if(subject != null)
            {
                ModelState.AddModelError("", "Subject already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var subjectMap = _mapper.Map<Subject>(subjectCreate);
            if (!_subjectRepository.CreateSubject(subjectMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpPut("{subjectId}")]
        public IActionResult UpdateSubject(int subjectId, [FromBody] SubjectDto updatedSubject)
        {
            if (updatedSubject == null)
                return BadRequest(ModelState);
            
            if (subjectId != updatedSubject.Id)
                return BadRequest(ModelState);

            if (!_subjectRepository.SubjectExists(subjectId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var subjectMap = _mapper.Map<Subject>(updatedSubject);

            if (!_subjectRepository.UpdateSubject(subjectMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{subjectId}")]
        public IActionResult DeleteSubject(int subjectId)
        {
            if (!_subjectRepository.SubjectExists(subjectId))
            {
                return NotFound();
            }

            var subjectToDelete = _subjectRepository.GetSubject(subjectId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_subjectRepository.DeleteSubject(subjectToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting subject");
            }
            return NoContent();

        }


    }
}
