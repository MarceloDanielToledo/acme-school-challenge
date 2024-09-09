using Application.UseCases.Courses.Requests;
using Application.UseCases.Courses.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.Courses.Mappings
{
    public class CourseProfile : Profile
    {
        public CourseProfile() 
        {
            CreateMap<CreateCourseRequest, Course>().ReverseMap();
            CreateMap<Course, CourseResponse>().ReverseMap();
        }
    }
}
