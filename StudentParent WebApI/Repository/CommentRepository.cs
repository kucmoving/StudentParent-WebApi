using StudentParent_WebApI.Data;
using StudentParent_WebApI.Interface;
using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _dataContext;

        public CommentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CommentExists(int commentId)
        {
            return _dataContext.Comments.Any(x => x.Id == commentId);
        }

        public Comment GetComment(int commentId)
        {
            return _dataContext.Comments.Where(x => x.Id == commentId).FirstOrDefault();
        }

        public ICollection<Comment> GetComments()
        {
            return _dataContext.Comments.OrderBy(x => x.Title).ToList();
        }

        public ICollection<Comment> GetCommentsbyStudentId(int studentId)
        {
            return _dataContext.Comments.Where(x => x.Student.Id == studentId).ToList();

        }
    }
}
