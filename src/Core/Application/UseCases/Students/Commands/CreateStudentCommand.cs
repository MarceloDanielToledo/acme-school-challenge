using Application.Constant;
using Application.Interfaces;
using Application.UseCases.Students.Requests;
using Application.UseCases.Students.Responses;
using Application.UseCases.Students.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Students.Commands
{
    public class CreateStudentCommand(CreateStudentRequest request) : IRequest<Response<StudentResponse>>
    {
        public CreateStudentRequest Request { get; } = request;
    }
    public class CreateStudentCommandHandler(IRepositoryAsync<Student> repositoryAsync, IMapper mapper) : IRequestHandler<CreateStudentCommand, Response<StudentResponse>>
    {
        private readonly IRepositoryAsync<Student> _repositoryAsync = repositoryAsync;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<StudentResponse>> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            var existingStudentByEmail = await _repositoryAsync.FirstOrDefaultAsync(new GetStudentByEmailSpecification(command.Request.Email), cancellationToken);
            if (existingStudentByEmail is not null) 
            {
                return Response<StudentResponse>.NotSuccess("Ya existe un estudiante con el email ingresado.");
            }
            var newRecordMapped = _mapper.Map<Student>(command.Request);
            var newRecordCreated = await _repositoryAsync.AddAsync(newRecordMapped, cancellationToken);
            return Response<StudentResponse>.Success(_mapper.Map<StudentResponse>(newRecordCreated), ResponseMessages.AddedSuccesfullyMessage);
        }
    }
}
