using StudentParent_WebApI.Data;
using StudentParent_WebApI.Interface;
using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _dataContext;

        public SubjectRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        //nested entity have to use .select 
        //pick the id and select take the value from relationship
        //include also can help

        public ICollection<Student> GetStudentBySubjectId(int subjectId)
        {
            return _dataContext.StudentSubject.Where(x => x.SubjectId == subjectId)
                .Select(c=>c.Student).ToList();
        }

        public Subject GetSubject(int id)
        {
            return _dataContext.Subjects.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<Subject> GetSubjects()
        {
            return _dataContext.Subjects.OrderBy(s=>s.Name).ToList();
        }

        public bool SubjectExists(int id)
        {
            return _dataContext.Subjects.Any(s=>s.Id== id);
        }




        public bool CreateSubject(Subject subject)
        {
            _dataContext.Add(subject);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateSubject(Subject subject)
        {
            _dataContext.Update(subject);
            return Save();
        }

        public bool DeleteSubject(Subject subject)
        {
            _dataContext.Remove(subject);
            return Save();
        }
    }
}
