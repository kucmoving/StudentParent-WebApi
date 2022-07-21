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
    public class CommentController : ControllerBase // inhertid from contollbase 
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public CommentController(ICommentRepository commentRepository, IMapper imapper,
            IStudentRepository studentRepository, ITeacherRepository teacherRepository)
        {
            _commentRepository = commentRepository;
            _mapper = imapper;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        [HttpGet]
        public IActionResult GetComments()
        {
            //var getComments = _commentRepository.GetComments();
            //which recommand ? easy to understand or clear is important?
            var comments = _mapper.Map<List<CommentDto>>(_commentRepository.GetComments());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(comments);   //inherted from Controlbase
        }



        [HttpGet("{commentId}")]
        public IActionResult GetComment(int commentId)
        {
            if (!_commentRepository.CommentExists(commentId))
                return NotFound(); //inherted from Controlbase

            var comment = _mapper.Map<CommentDto>(_commentRepository.GetComment(commentId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(comment); //inherted from Controlbase
        }

        [HttpGet("student/{studentId}")]

        public IActionResult GetCommentsByStudentId(int studentId)
        {
            var comment = _mapper.Map<List<CommentDto>>(
                _commentRepository.GetCommentsbyStudentId(studentId));

            if (!ModelState.IsValid)
                return BadRequest(); 
            return Ok(comment); //inherted from Controlbase
        }


        [HttpPost]
        //get value from query string  //from body = get value from request body
        //can del FromQuery, FromBody!
        public IActionResult CreateComment([FromQuery] int teacherId, int studentId,
            [FromBody] CommentDto commentCreate) 
        {

            if (commentCreate == null)
                return BadRequest(ModelState);

            var comments = _commentRepository.GetComments()
                .Where(X => X.Title.Trim().ToUpper() == commentCreate.Title.TrimEnd()
                .ToUpper()).FirstOrDefault(); //remove space and become upper

            if (comments != null)
            {
                ModelState.AddModelError("", "Comment already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentMap = _mapper.Map<Comment>(commentCreate);

            commentMap.Student = _studentRepository.GetStudent(studentId);
            commentMap.Teacher = _teacherRepository.GetTeacher(teacherId);

            if (!_commentRepository.CreateComment(commentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }


        [HttpPut("{commentId}")] //FormBody can be del
        public IActionResult UpdateComment(int commentId, [FromBody] CommentDto updatedComment)
        {
            if (updatedComment == null)
                return BadRequest(ModelState);

            //commentId, UpdateId must be the same 
            if (commentId != updatedComment.Id)
                return BadRequest(ModelState);

            if (!_commentRepository.CommentExists(commentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            //mapper with models / entities
            var commentMap = _mapper.Map<Comment>(updatedComment);

            if (!_commentRepository.UpdateComment(commentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Comment");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{commentId}")]
        public IActionResult DeleteComment(int commentId)
        {
            if (!_commentRepository.CommentExists(commentId))
            {
                return NotFound();
            }

            var commentToDelete = _commentRepository.GetComment(commentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_commentRepository.DeleteComment(commentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting parent");
            }
            return NoContent();

        }

    }
}
