namespace StudentParent_WebApI.Models
{
    public class SchoolClub
    {
        public int Id { get; set; } 
        public string Name { get; set; }   
        
        public ICollection<Parent> Parents { get; set; }
    }
}
