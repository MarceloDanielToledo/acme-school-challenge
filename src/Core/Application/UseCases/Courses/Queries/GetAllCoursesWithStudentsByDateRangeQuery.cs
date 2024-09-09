using Application.Constant;
using Application.Interfaces;
using Application.UseCases.Courses.Responses;
using Application.UseCases.Courses.Specifications;
using Application.UseCases.CoursesEnrollments.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Courses.Queries
{
    public class GetAllCoursesWithStudentsByDateRangeQuery(DateTime start, DateTime end) : IRequest<Response<List<CourseResponse>>>
    {
        public DateTime Start { get; } = start;
        public DateTime End { get; } = end;
    }
    public class GetAllCoursesWithStudentsByDateRangeQueryHandler(IRepositoryAsync<Course> repositoryAsync, IMapper mapper) : IRequestHandler<GetAllCoursesWithStudentsByDateRangeQuery, Response<List<CourseResponse>>>
    {
        private readonly IRepositoryAsync<Course> _repositoryAsync = repositoryAsync;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<CourseResponse>>> Handle(GetAllCoursesWithStudentsByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var records = await _repositoryAsync.ListAsync(new GetCoursesWithStudentsByDateRangeSpecification(request.Start,request.End), cancellationToken);
            if (records is null || records.Count == 0)
            {
                return Response<List<CourseResponse>>.NotSuccess(ResponseMessages.NotFoundsMessage);
            }
            return Response<List<CourseResponse>>.Success(_mapper.Map<List<CourseResponse>>(records));
        }
    }
}
