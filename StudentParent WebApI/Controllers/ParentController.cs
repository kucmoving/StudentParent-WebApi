using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentParent_WebApI.Dto;
using StudentParent_WebApI.Interface;

namespace StudentParent_WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentRepository _parentRepository;
        private readonly IMapper _mapper;

        public ParentController(IParentRepository parentRepository, IMapper mapper)
        {
            _parentRepository = parentRepository;
            _mapper = mapper;
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
        public IActionResult GetOwner(int parentId)
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


    }
}
