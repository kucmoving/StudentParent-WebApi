namespace StudentParent_WebApI.Models
{
    public class Parent
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Phone { get; set; }
        public SchoolClub SchoolClub { get; set; }
        public ICollection<StudentParent> StudentParents { get; set; }
    }
}
