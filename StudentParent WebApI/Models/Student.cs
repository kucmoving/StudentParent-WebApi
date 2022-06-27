namespace StudentParent_WebApI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public DateTime BirthDate { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<StudentParent> StudentParents { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }


    }
}
