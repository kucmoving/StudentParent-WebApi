using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Interface
{
    public interface ISchoolClubRepository
    {
        ICollection<SchoolClub> GetSchoolClubs();
        SchoolClub GetSchoolClub(int id);
        SchoolClub GetSchoolClubByParentId(int parentId);

        ICollection<Parent> GetParentsFromASchoolClub(int schoolClubId);
        bool SchoolClubExists(int id);
    }
}
