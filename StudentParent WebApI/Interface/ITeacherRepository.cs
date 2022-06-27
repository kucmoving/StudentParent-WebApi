using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Interface
{
    public interface ITeacherRepository
    {
        ICollection<Teacher> GetTeachers();
       Teacher GetTeacher(int teacherId);
        ICollection<Comment> GetCommentsByTeacher(int teacherId);
        bool TeacherExists(int teacherId);
    }
}
