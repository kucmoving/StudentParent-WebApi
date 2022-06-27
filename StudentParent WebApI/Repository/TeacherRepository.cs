using Microsoft.EntityFrameworkCore;
using StudentParent_WebApI.Data;
using StudentParent_WebApI.Interface;
using StudentParent_WebApI.Models;
using System.Linq;

namespace StudentParent_WebApI.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DataContext _dataContext;

        public TeacherRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<Comment> GetCommentsByTeacher(int teacherId)
        {
            return _dataContext.Comments.Where(x => x.Id == teacherId).ToList();
        }

        //be care
        public Teacher GetTeacher(int teacherId)
        {
            return _dataContext.Teachers.Where(x => x.Id == teacherId)
                .Include(x => x.Comments).FirstOrDefault();
        }

        public ICollection<Teacher> GetTeachers()
        {
            return _dataContext.Teachers.OrderBy(x => x.FirstName).ToList();
        }

        public bool TeacherExists(int teacherId)
        {
            return _dataContext.Teachers.Any(x => x.Id == teacherId);
        }
    }
}
