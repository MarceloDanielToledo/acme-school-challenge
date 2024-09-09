using Application.UseCases.Students.Requests;
using Application.UseCases.Students.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.Students.Mappings
{
    public class StudentProfiles : Profile
    {
        public StudentProfiles() 
        {
            CreateMap<CreateStudentRequest, Student>().ReverseMap();
            CreateMap<Student, StudentResponse>().ReverseMap();
        }

    }
}
