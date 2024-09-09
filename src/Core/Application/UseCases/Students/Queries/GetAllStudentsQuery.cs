using Application.Constant;
using Application.Interfaces;
using Application.UseCases.Students.Responses;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Students.Queries
{
    public class GetAllStudentsQuery : IRequest<Response<List<StudentResponse>>>
    {

    }
    public class GetAllStudentsQueryHandler(IRepositoryAsync<Student> repositoryAsync, IMapper mapper) : IRequestHandler<GetAllStudentsQuery, Response<List<StudentResponse>>>
    {
        private readonly IRepositoryAsync<Student> _repositoryAsync = repositoryAsync;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<StudentResponse>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var records = await _repositoryAsync.ListAsync(cancellationToken);
            if (records is null || records.Count == 0)
            {
                return Response<List<StudentResponse>>.NotSuccess(ResponseMessages.NotFoundsMessage);
            }
            return Response<List<StudentResponse>>.Success(_mapper.Map<List<StudentResponse>>(records));
        }
    }
}
