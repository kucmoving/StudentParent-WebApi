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

        //create
        bool CreateStudent(int parentId, int subjectId, Student student); //signautre
        bool Save();

        bool UpdateStudent(int parentId, int subjectId, Student student);
        bool DeleteStudent(Student student);


    }
}
