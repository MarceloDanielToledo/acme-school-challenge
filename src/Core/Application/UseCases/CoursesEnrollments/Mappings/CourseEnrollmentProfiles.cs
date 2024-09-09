using Application.UseCases.CoursesEnrollments.Requests;
using Application.UseCases.CoursesEnrollments.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.CoursesEnrollments.Mappings
{
    public class CourseEnrollmentProfiles : Profile
    {
        public CourseEnrollmentProfiles() 
        {
            CreateMap<CreateCourseEnrollmentRequest, CourseEnrollment>().ReverseMap();
            CreateMap<CourseEnrollment, CourseEnrollmentResponse>().ReverseMap();
        }

    }
}
