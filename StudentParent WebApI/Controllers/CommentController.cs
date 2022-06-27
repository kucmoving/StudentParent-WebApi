using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentParent_WebApI.Dto;
using StudentParent_WebApI.Interface;

namespace StudentParent_WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository, IMapper imapper)
        {
            _commentRepository = commentRepository;
            _mapper = imapper;
        }

        [HttpGet]
        public IActionResult GetComments()
        {
            var comments = _mapper.Map<List<CommentDto>>(_commentRepository.GetComments());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(comments);
        }



        [HttpGet("{commentId}")]
        public IActionResult GetComment(int commentId)
        {
            if (!_commentRepository.CommentExists(commentId))
                return NotFound();

            var comment = _mapper.Map<CommentDto>(_commentRepository.GetComment(commentId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(comment);
        }

        [HttpGet("student/{studentId}")]

        public IActionResult GetCommentsByStudentId(int studentId)
        {
            var comment = _mapper.Map<List<CommentDto>>(
                _commentRepository.GetCommentsbyStudentId(studentId));

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(comment);
        }
    }
}
