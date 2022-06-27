using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentParent_WebApI.Dto;
using StudentParent_WebApI.Interface;

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
            var schoolClubs = _mapper.Map<List<SchoolClubDto>>(_schoolClubRepository.GetSchoolClubs());
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


        [HttpGet("(parents)/{parentId}")]
        public IActionResult GetSchoolClubByParentId(int parentId)
        {
            var schoolClub = _mapper.Map<SchoolClubDto>(
                _schoolClubRepository.GetSchoolClubByParentId(parentId));

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(schoolClub);
        }   
    }
}


