using AutoMapper;
using StudentParent_WebApI.Dto;
using StudentParent_WebApI.Models;

namespace StudentParent_WebApI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<SchoolClub, SchoolClubDto>().ReverseMap();
            CreateMap<Parent, ParentDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ReverseMap();

        }
    }
}
