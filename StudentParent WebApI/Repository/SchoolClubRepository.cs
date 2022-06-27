using StudentParent_WebApI.Data;
using StudentParent_WebApI.Interface;
using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Repository
{
    public class SchoolClubRepository : ISchoolClubRepository
    {
        private readonly DataContext _dataContext;

        public SchoolClubRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //nested  single
        public SchoolClub GetSchoolClubByParentId(int parentId)
        {
            return _dataContext.Parents.Where(x => x.Id == parentId)
                .Select(c => c.SchoolClub).FirstOrDefault();
        }


        //nested list
        public ICollection<Parent> GetParentsFromASchoolClub(int schoolClubId)
        {
            return _dataContext.Parents.Where(x=>x.SchoolClub.Id == schoolClubId)
                .ToList();
        }

        public SchoolClub GetSchoolClub(int id)
        {
            return _dataContext.SchoolClubs.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<SchoolClub> GetSchoolClubs()
        {
            return _dataContext.SchoolClubs.OrderBy(x => x.Name).ToList();
        }



        public bool SchoolClubExists(int id)
        {
            return _dataContext.SchoolClubs.Any(x => x.Id == id);
        }
    }
}
