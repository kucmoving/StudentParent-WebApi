using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Interface
{
    public interface ISubjectRepository
    {
        ICollection<Subject> GetSubjects();

        Subject GetSubject(int id);
        ICollection<Student> GetStudentBySubjectId(int subjectId);
        bool SubjectExists(int id);

    }
}
