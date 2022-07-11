using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentParent_WebApI.Dto;
using StudentParent_WebApI.Interface;
using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentRepository _parentRepository;
        private readonly IMapper _mapper;
        private readonly ISchoolClubRepository _schoolClubRepository;
        private readonly ICommentRepository _commentRepository;

        public ParentController(IParentRepository parentRepository, IMapper mapper
            , ISchoolClubRepository schoolClubRepository)
        {
            _parentRepository = parentRepository;
            _mapper = mapper;
            _schoolClubRepository = schoolClubRepository;
        }


        [HttpGet]
        public IActionResult GetParents()
        {
            var parents = _mapper.Map<List<ParentDto>>(_parentRepository.GetParents());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(parents);
        }

        [HttpGet("{parentId}")]
        public IActionResult GetParent(int parentId)
        {
            if (!_parentRepository.ParentExists(parentId))
                return NotFound();

            var parent = _mapper.Map<ParentDto>(_parentRepository.GetParent(parentId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(parent);
        }


        //get student by parent (many 2many relationship)

        [HttpGet("{parentId}/student")]

        public IActionResult GetStudentByParentId(int parentId)
        {
            if (!_parentRepository.ParentExists(parentId))
            {
                return NotFound();
            }
            var parent = _mapper.Map<List<StudentDto>>(
                _parentRepository.GetStudentByParentId(parentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(parent);
        }


        [HttpPost]
        public IActionResult CreateParent([FromQuery] int parentId, [FromBody] ParentDto parentCreate) //take the id from link
        {
            if (parentCreate == null)
                return BadRequest(ModelState);
            var subject = _parentRepository.GetParents()
                .Where(X => X.LastName.Trim().ToUpper() == parentCreate.LastName.TrimEnd()
                .ToUpper()).FirstOrDefault();
            if (subject != null)
            {
                ModelState.AddModelError("", "Parent already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var parentMap = _mapper.Map<Parent>(parentCreate);

            parentMap.SchoolClub = _schoolClubRepository.GetSchoolClub(parentId); //put id in parentMap (buy input)

            if (!_parentRepository.CreateParent(parentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }


        [HttpPut("{parentId}")]
        public IActionResult UpdateParent(int parentId, [FromBody] ParentDto updatedParent)
        {
            if (updatedParent == null)
                return BadRequest(ModelState);

            if (parentId != updatedParent.Id)
                return BadRequest(ModelState);

            if (!_parentRepository.ParentExists(parentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var parentMap = _mapper.Map<Parent>(updatedParent);

            if (!_parentRepository.UpdateParent(parentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating parent");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{parentId}")]
        public IActionResult DeleteParent(int parentId)
        {
            if (!_parentRepository.ParentExists(parentId))
            {
                return NotFound();
            }

            var parentToDelete = _parentRepository.GetParent(parentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_parentRepository.DeleteParent(parentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting parent");
            }
            return NoContent();

        }



    }
}
