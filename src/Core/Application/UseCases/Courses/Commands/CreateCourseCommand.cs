using Application.Constant;
using Application.Interfaces;
using Application.UseCases.Courses.Requests;
using Application.UseCases.Courses.Responses;
using Application.UseCases.Courses.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Courses.Commands
{
    public class CreateCourseCommand(CreateCourseRequest request) : IRequest<Response<CourseResponse>>
    {
        public CreateCourseRequest Request { get; } = request;
    }

    public class CreateCourseCommandHandler(IRepositoryAsync<Course> repositoryAsync, IMapper mapper) : IRequestHandler<CreateCourseCommand, Response<CourseResponse>>
    {
        private readonly IRepositoryAsync<Course> _repositoryAsync = repositoryAsync;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<CourseResponse>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
        {
            var existingCourseByName = await _repositoryAsync.FirstOrDefaultAsync(new GetCourseByNameSpecification(command.Request.Name), cancellationToken);
            if (existingCourseByName is not null)
            {
                return Response<CourseResponse>.NotSuccess("Ya existe un curso con ese nombre");
            }
            var newRecordMapped = _mapper.Map<Course>(command.Request);
            var newRecordCreated = await _repositoryAsync.AddAsync(newRecordMapped,cancellationToken);
            return Response<CourseResponse>.Success(_mapper.Map<CourseResponse>(newRecordCreated), ResponseMessages.AddedSuccesfullyMessage);
        }
    }
}
