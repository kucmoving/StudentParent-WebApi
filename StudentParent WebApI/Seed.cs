using StudentParent_WebApI.Data;
using StudentParent_WebApI.Models;

namespace StudentParent_WebApI
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.StudentParents.Any())
            {
                var StudentParents = new List<StudentParent>()
                {
                    new StudentParent()
                    {
                        Student = new Student()
                        {
                            Name = "Ken",
                            BirthDate = new DateTime(2007,12,30),
                            StudentSubjects = new List<StudentSubject>()
                            {
                                new StudentSubject { Subject = new Subject() { Name = "History"}}
                            },
                            Comments = new List<Comment>()
                            {
                                new Comment { Title="Ken",Content = "he always for get to bring his homework", Rating = 1,
                                Teacher = new Teacher(){ FirstName = "Carol", LastName = "Smith" } },
                                new Comment { Title="Pikachu",Content = "just a sleeping baby in my lecture", Rating = 2,
                                Teacher = new Teacher(){ FirstName = "James", LastName = "White" } },
                                new Comment { Title="Pikachu",Content = "absent all the day", Rating = 1,
                                Teacher = new Teacher(){ FirstName = "Paul", LastName = "Lee" } },
                            }
                        },
                        Parent   = new Parent()
                        {
                            FirstName = "Amy",
                            LastName = "Takahashi",
                            Phone = "9574 2158",
                            SchoolClub  = new SchoolClub()
                            {
                                Name = "Cooking Club"
                            }
                        }
                    },
                    new StudentParent()
                    {
                        Student = new Student()
                        {
                            Name = "John",
                            BirthDate = new DateTime(2006,5,1),
                            StudentSubjects = new List<StudentSubject>()
                            {
                                new StudentSubject { Subject = new Subject() { Name = "Geography"}}
                            },
                            Comments = new List<Comment>()
                            {
                                new Comment { Title="John",Content = "super talent in my subject", Rating = 5,
                                Teacher = new Teacher(){ FirstName = "Carol", LastName = "Smith" } },
                                new Comment { Title="John",Content = "a great school helper", Rating = 5,
                                Teacher = new Teacher(){ FirstName = "James", LastName = "White" } },
                                new Comment { Title="John",Content = "just quiet in my lesson", Rating = 3,
                                Teacher = new Teacher(){ FirstName = "Paul", LastName = "Lee" } },
                            }
                        },
                        Parent   = new Parent()
                        {
                            FirstName = "Fanny",
                            LastName = "Kennedy",
                            Phone = "9584 1546",
                            SchoolClub  = new SchoolClub()
                            {
                                Name = "Dancing"
                            }
                        }
                    },
                    new StudentParent()
                    {
                        Student = new Student()
                        {
                            Name = "Ben",
                            BirthDate = new DateTime(2008,1,1),
                            StudentSubjects = new List<StudentSubject>()
                            {
                                new StudentSubject { Subject = new Subject() { Name = "Math"}}
                            },
                            Comments = new List<Comment>()
                            {
                                new Comment { Title="Ben",Content = "He is interested in my personal life...", Rating = 3,
                                Teacher = new Teacher(){ FirstName = "Carol", LastName = "Smith" } },
                                new Comment { Title="Ben",Content = "He has a lot of friend in school", Rating = 4,
                                Teacher = new Teacher(){ FirstName = "James", LastName = "White" } },
                                new Comment { Title="Ben",Content = "he loves joking.", Rating = 3,
                                Teacher = new Teacher(){ FirstName = "Paul", LastName = "Lee" } },
                            }
                        },
                        Parent   = new Parent()
                        {
                            FirstName = "Tim",
                            LastName = "Wong",
                            Phone = "6587 7813",
                            SchoolClub  = new SchoolClub()
                            {
                                Name = "Promoting"
                            }
                        }
                    }
                };
                dataContext.StudentParents.AddRange(StudentParents);
                dataContext.SaveChanges();
            }
        }
    }
}