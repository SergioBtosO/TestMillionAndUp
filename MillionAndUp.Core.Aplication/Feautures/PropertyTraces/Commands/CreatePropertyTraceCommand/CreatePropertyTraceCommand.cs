using AutoMapper;
using MediatR;
using MillionAndUp.Core.Aplication.Wrappers;
using MillionAndUp.Core.Application.Interfaces;
using MillionAndUp.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.PropertyTraces.Commands.CreatePropertyTraceCommand
{
    public class CreatePropertyTraceCommand : IRequest<Response<int>>
    {
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double Tax { get; set; }
        public int IdProperty { get; set; }
    }

    public class CreatePropertyTraceCommandHandler : IRequestHandler<CreatePropertyTraceCommand, Response<int>>
    {
        private readonly IRepositoryAsync<PropertyTrace> _repositoryAsync;
        private readonly IMapper _mapper;
        public CreatePropertyTraceCommandHandler(IRepositoryAsync<PropertyTrace> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreatePropertyTraceCommand request, CancellationToken cancellationToken)
        {

            var newPropertyTrace = _mapper.Map<PropertyTrace>(request);
            var data = await _repositoryAsync.AddAsync(newPropertyTrace);
            return new Response<int>(data.Id);
        }
    }
}
