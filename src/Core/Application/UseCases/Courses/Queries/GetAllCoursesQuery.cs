using Application.Constant;
using Application.Interfaces;
using Application.UseCases.Courses.Responses;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Courses.Queries
{
    public class GetAllCoursesQuery : IRequest<Response<List<CourseResponse>>>
    {

    }
    public class GetAllCoursesQueryHandler(IRepositoryAsync<Course> repositoryAsync, IMapper mapper) : IRequestHandler<GetAllCoursesQuery, Response<List<CourseResponse>>>
    {
        private readonly IRepositoryAsync<Course> _repositoryAsync = repositoryAsync;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<CourseResponse>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var records = await _repositoryAsync.ListAsync(cancellationToken);
            if (records is null || records.Count == 0)
            {
                return Response<List<CourseResponse>>.NotSuccess(ResponseMessages.NotFoundsMessage);
            }
            return Response<List<CourseResponse>>.Success(_mapper.Map<List<CourseResponse>>(records));
        }
    }
}
