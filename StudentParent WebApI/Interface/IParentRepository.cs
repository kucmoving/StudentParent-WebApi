using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Interface
{
    public interface IParentRepository
    {
        ICollection<Parent> GetParents();
        Parent GetParent(int parentId);

        ICollection<Parent> GetParentByStudentId(int studentId);
        ICollection<Student> GetStudentByParentId(int parentId);
        bool ParentExists(int parentId);
    }
}
