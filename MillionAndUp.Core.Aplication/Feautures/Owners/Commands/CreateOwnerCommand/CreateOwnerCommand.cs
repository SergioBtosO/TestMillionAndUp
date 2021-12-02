using AutoMapper;
using MediatR;
using MillionAndUp.Core.Aplication.Wrappers;
using MillionAndUp.Core.Application.Interfaces;
using MillionAndUp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.Owners.Commands.CreateOwnerCommand
{
    public class CreateOwnerCommand : IRequest <Response<int>>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }

        public DateTime Birthday { get; set; }
    }
    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Owner> _repositoryAsync;
        private readonly IMapper _mapper;
        public CreateOwnerCommandHandler(IRepositoryAsync<Owner> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            var newOwner = _mapper.Map<Owner>(request);
            var data = await _repositoryAsync.AddAsync(newOwner);
            return new Response<int>(data.Id);
        }
    }
}
