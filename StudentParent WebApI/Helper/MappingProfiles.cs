using AutoMapper;
using StudentParent_WebApI.Dto;
using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<Subject, SubjectDto>();
            CreateMap<SchoolClub, SchoolClubDto>();
            CreateMap<Parent, ParentDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Teacher, TeacherDto>();

        }
    }
}
