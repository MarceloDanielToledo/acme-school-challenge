using Application.Constant;
using Application.Interfaces;
using Application.UseCases.CoursesEnrollments.Responses;
using Application.UseCases.CoursesEnrollments.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.CoursesEnrollments.Queries
{
    public class GetCoursesEnrollmentsByIdStudentQuery(int studentId) : IRequest<Response<List<CourseEnrollmentResponse>>>
    {
        public int StudentId { get; } = studentId;
    }
    public class GetCoursesEnrollmentsByIdStudentQueryHandler(IRepositoryAsync<CourseEnrollment> repositoryAsync, IMapper mapper) : IRequestHandler<GetCoursesEnrollmentsByIdStudentQuery, Response<List<CourseEnrollmentResponse>>>
    {
        private readonly IRepositoryAsync<CourseEnrollment> _repositoryAsync = repositoryAsync;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<CourseEnrollmentResponse>>> Handle(GetCoursesEnrollmentsByIdStudentQuery request, CancellationToken cancellationToken)
        {
            var records = await _repositoryAsync.ListAsync(new GetCourseEnrollmentByIdStudentSpecification(request.StudentId), cancellationToken);
            if (records is null || records.Count == 0)
            {
                return Response<List<CourseEnrollmentResponse>>.NotSuccess(ResponseMessages.NotFoundsMessage);
            }
            return Response<List<CourseEnrollmentResponse>>.Success(_mapper.Map<List<CourseEnrollmentResponse>>(records));
        }
    }
}
