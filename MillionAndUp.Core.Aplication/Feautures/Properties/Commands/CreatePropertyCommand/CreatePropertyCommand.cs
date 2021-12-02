using AutoMapper;
using MediatR;
using MillionAndUp.Core.Aplication.Wrappers;
using MillionAndUp.Core.Application.Interfaces;
using MillionAndUp.Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.Properties.Commands.CreatePropertyCommand
{
    public class CreatePropertyCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
    }
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Property> _repositoryAsync;
        private readonly IMapper _mapper;
        public CreatePropertyCommandHandler(IRepositoryAsync<Property> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {

            var newProperty = _mapper.Map<Property>(request);
            var data = await _repositoryAsync.AddAsync(newProperty);
            return new Response<int>(data.Id);
        }
    }
}
