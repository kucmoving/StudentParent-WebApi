using Microsoft.EntityFrameworkCore;
using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<SchoolClub> SchoolClubs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentParent> StudentParents { get; set; }
        public DbSet<StudentSubject> StudentSubject { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentSubject>()
                .HasKey(ss => new { ss.StudentId, ss.SubjectId });
            modelBuilder.Entity<StudentSubject>() 
                .HasOne(s => s.Student)
                .WithMany(ss => ss.StudentSubjects)
                .HasForeignKey(s => s.StudentId);
            modelBuilder.Entity<StudentSubject>()
                .HasOne(s => s.Subject)
                .WithMany(ss => ss.StudentSubjects)
                .HasForeignKey(s => s.SubjectId);


            modelBuilder.Entity<StudentParent>()
                .HasKey(ss => new { ss.StudentId, ss.ParentId });
            modelBuilder.Entity<StudentParent>()
                .HasOne(sp => sp.Parent)
                .WithMany(p => p.StudentParents)
                .HasForeignKey(sp => sp.ParentId);
            modelBuilder.Entity<StudentParent>()
                .HasOne(sp => sp.Student)
                .WithMany(s => s.StudentParents)
                .HasForeignKey(sp => sp.StudentId);
        }
    }
}
