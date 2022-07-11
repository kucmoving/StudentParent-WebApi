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
    public class SchoolClubController : ControllerBase
    {
        private readonly ISchoolClubRepository _schoolClubRepository;
        private readonly IMapper _mapper;

        public SchoolClubController(ISchoolClubRepository schoolClubRepository, IMapper mapper)
        {
            _schoolClubRepository = schoolClubRepository;
            _mapper = mapper;
        }



        [HttpGet]
        public IActionResult GetSchoolClubs()
        {
            var schoolClubs = _mapper.Map<List<SchoolClubDto>>(
                _schoolClubRepository.GetSchoolClubs());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(schoolClubs);
        }


        [HttpGet("{schoolClubId}")]
        public IActionResult GetSchoolClub(int schoolClubId)
        {
            if (!_schoolClubRepository.SchoolClubExists(schoolClubId))
                return NotFound();

            var subject = _mapper.Map<SchoolClubDto>(_schoolClubRepository.GetSchoolClub(schoolClubId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(subject);
        }


        [HttpGet("parents/{parentId}")]
        public IActionResult GetSchoolClubByParentId(int parentId)
        {
            var schoolClub = _mapper.Map<SchoolClubDto>(
                _schoolClubRepository.GetSchoolClubByParentId(parentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(schoolClub);
        }

        [HttpPost]
        public IActionResult CreateSchoolClub([FromBody] SchoolClubDto schoolClubCreate)
        {
            if (schoolClubCreate == null)
                return BadRequest(ModelState);
            var schoolClub = _schoolClubRepository.GetSchoolClubs()
                .Where(X => X.Name.Trim().ToUpper() == schoolClubCreate.Name.TrimEnd()
                .ToUpper()).FirstOrDefault();
            if (schoolClub != null)
            {
                ModelState.AddModelError("", "SchoolClub already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var schoolClubMap = _mapper.Map<SchoolClub>(schoolClubCreate);
            if (!_schoolClubRepository.CreateSchoolClub(schoolClubMap))
            {
                ModelState.AddModelError("", "Something went wrong while saveing");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpPut("{schoolBodyId}")]
        public IActionResult UpdateSchoolClub(int schoolClubId, [FromBody] SchoolClubDto updatedSchoolClub)
        {
            if (updatedSchoolClub == null)
                return BadRequest(ModelState);

            if (schoolClubId != updatedSchoolClub.Id)
                return BadRequest(ModelState);

            if (!_schoolClubRepository.SchoolClubExists(schoolClubId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var schoolClubMap = _mapper.Map<SchoolClub>(updatedSchoolClub);

            if (!_schoolClubRepository.UpdateSchoolClub(schoolClubMap))
            {
                ModelState.AddModelError("", "Something went wrong updating schoolCLub");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{schoolClubId}")]
        public IActionResult DeleteSchoolClub(int schoolClubId)
        {
            if (!_schoolClubRepository.SchoolClubExists(schoolClubId))
            {
                return NotFound();
            }

            var schoolClubToDelete = _schoolClubRepository.GetSchoolClub(schoolClubId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_schoolClubRepository.DeleteSchoolClub(schoolClubToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting subject");
            }
            return NoContent();

        }

    }
}


