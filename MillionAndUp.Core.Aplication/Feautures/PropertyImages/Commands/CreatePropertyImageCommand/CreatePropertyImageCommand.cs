using AutoMapper;
using MediatR;
using MillionAndUp.Core.Aplication.Wrappers;
using MillionAndUp.Core.Application.Interfaces;
using MillionAndUp.Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.PropertyImages.Commands.CreatePropertyImageCommand
{
    public class CreatePropertyImageCommand : IRequest<Response<int>>
    {
        public bool Enabled { get; set; }
        public byte[] File { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdProperty { get; set; }
    }
    public class CreatePropertyImageCommandHandler : IRequestHandler<CreatePropertyImageCommand, Response<int>>
    {
        private readonly IRepositoryAsync<PropertyImage> _repositoryAsync;
        private readonly IMapper _mapper;
        public CreatePropertyImageCommandHandler(IRepositoryAsync<PropertyImage> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreatePropertyImageCommand request, CancellationToken cancellationToken)
        {

            var newPropertyImage = _mapper.Map<PropertyImage>(request);
            var data = await _repositoryAsync.AddAsync(newPropertyImage);
            return new Response<int>(data.Id);
        }
    }
}
