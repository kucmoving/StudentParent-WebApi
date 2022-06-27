using StudentParent_WebApI.Data;
using StudentParent_WebApI.Interface;
using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Repository
{
    public class ParentRepository : IParentRepository
    {
        private readonly DataContext _dataContext;

        public ParentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Parent GetParent(int parentId)
        {
            return _dataContext.Parents.Where(x => x.Id == parentId).FirstOrDefault();
        }

        //many to many by id(query in join tablea)
        public ICollection<Parent> GetParentByStudentId(int studentId)
        {
            return _dataContext.StudentParents.Where(x => x.Student.Id == studentId)
                .Select(p => p.Parent).ToList();
        }

        public ICollection<Student> GetStudentByParentId(int parentId)
        {
            return _dataContext.StudentParents.Where(s => s.Parent.Id == parentId)
                .Select(s => s.Student).ToList();
        }


        public ICollection<Parent> GetParents()
        {
            return _dataContext.Parents.OrderBy(x => x.FirstName).ToList();
        }


        public bool ParentExists(int parentId)
        {
            return _dataContext.Parents.Any(x => x.Id == parentId);
        }
    }
}
