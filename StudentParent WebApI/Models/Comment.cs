namespace StudentParent_WebApI.Models
{
    public class Comment
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

        public Teacher Teacher { get; set; }
        public Student Student { get; set; }

    }
}
