using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Interface
{
    public interface ISubjectRepository
    {
        ICollection<Subject> GetSubjects();

        Subject GetSubject(int id);
        ICollection<Student> GetStudentBySubjectId(int subjectId);
        bool SubjectExists(int id);

        //create
        bool CreateSubject(Subject subject); //signautre
        bool Save();

        //update

        bool UpdateSubject(Subject subject);

        bool DeleteSubject(Subject subject);

    }
}
