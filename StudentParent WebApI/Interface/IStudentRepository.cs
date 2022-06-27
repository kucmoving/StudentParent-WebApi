using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Interface
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudents();
        Student GetStudent(int id);
        Student GetStudent(string name);
        decimal GetStudentRating(int studentId);
        bool StudentExists(int studentId);
    }
}
