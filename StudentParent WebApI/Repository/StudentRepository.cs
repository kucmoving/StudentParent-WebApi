using StudentParent_WebApI.Data;
using StudentParent_WebApI.Interface;
using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Repository
{
    public class StudentRepository: IStudentRepository
    {
        private readonly DataContext _dataContext;

        public StudentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }



        public decimal GetStudentRating(int studentId)
        {
            var comment = _dataContext.Comments.Where(s => s.Student.Id == studentId);
            if (comment.Count() < 0)
                return 0;

            return ((decimal)comment.Sum(r => r.Rating) / comment.Count());
        }

        public Student GetStudent(int id)
        {
            return _dataContext.Students.Where(s => s.Id == id).FirstOrDefault();
        }

        public Student GetStudent(string name)
        {
            return _dataContext.Students.Where(s => s.Name == name).FirstOrDefault();
        }

        public ICollection<Student> GetStudents()
        {
            return _dataContext.Students.OrderBy(s => s.Id).ToList();
        }


        public bool StudentExists(int studentId)
        {
            return _dataContext.Students.Any(p => p.Id == studentId);
        }
    }
}
