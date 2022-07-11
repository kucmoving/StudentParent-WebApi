using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Interface
{
    public interface ICommentRepository
    {
        ICollection<Comment> GetComments();
        Comment GetComment(int commentId);
        ICollection<Comment> GetCommentsbyStudentId(int studentId);

        bool CommentExists(int reviewId);

        //create
        bool CreateComment(Comment comment); //signautre
        bool Save();

        bool UpdateComment(Comment comment);

        bool DeleteComment(Comment comment);
        bool DeleteComments(List<Comment> Comments);

    }
}
