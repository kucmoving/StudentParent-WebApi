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


        //create
        bool CreateSchoolClub(SchoolClub schoolclub); //signautre
        bool Save();


        //update

        bool UpdateSchoolClub(SchoolClub schoolclub);
        bool DeleteSchoolClub(SchoolClub schoolClub);

    }
}
