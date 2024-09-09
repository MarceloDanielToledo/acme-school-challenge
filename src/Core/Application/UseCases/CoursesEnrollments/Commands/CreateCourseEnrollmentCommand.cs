using Application.Constant;
using Application.Interfaces;
using Application.UseCases.CoursesEnrollments.Requests;
using Application.UseCases.CoursesEnrollments.Responses;
using Application.UseCases.CoursesEnrollments.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.CoursesEnrollments.Commands
{
    public class CreateCourseEnrollmentCommand(CreateCourseEnrollmentRequest request) : IRequest<Response<CourseEnrollmentResponse>>
    {
        public CreateCourseEnrollmentRequest Request { get; } = request;
    }
    public class CreateCourseEnrollmentCommandHandler(IRepositoryAsync<CourseEnrollment> repositoryAsync, IMapper mapper) : IRequestHandler<CreateCourseEnrollmentCommand, Response<CourseEnrollmentResponse>>
    {
        private readonly IRepositoryAsync<CourseEnrollment> _repositoryAsync = repositoryAsync;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<CourseEnrollmentResponse>> Handle(CreateCourseEnrollmentCommand command, CancellationToken cancellationToken)
        {
            var existingCourseEnrollment = await _repositoryAsync.FirstOrDefaultAsync(new GetCourseEnrollmentByIdCourseAndIdStudentSpecification(command.Request.CourseId, command.Request.StudentId), cancellationToken);
            if (existingCourseEnrollment is not null)
            {
                return Response<CourseEnrollmentResponse>.NotSuccess("El alumno ya se encuentra inscripto al curso ingresado.");
            }
            var newRecordMapped = _mapper.Map<CourseEnrollment>(command.Request);
            var newRecordCreated = await _repositoryAsync.AddAsync(newRecordMapped, cancellationToken);
            return Response<CourseEnrollmentResponse>.Success(_mapper.Map<CourseEnrollmentResponse>(newRecordCreated), ResponseMessages.AddedSuccesfullyMessage);
        }
    }

}
