using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Interface
{
    public interface ICommentRepository
    {
        ICollection<Comment> GetComments();
        Comment GetComment(int commentId);
        ICollection<Comment> GetCommentsbyStudentId(int studentId);

        bool CommentExists(int reviewId);

    }
}
