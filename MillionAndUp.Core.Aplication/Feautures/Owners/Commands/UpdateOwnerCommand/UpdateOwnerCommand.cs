using AutoMapper;
using MediatR;
using MillionAndUp.Core.Aplication.Wrappers;
using MillionAndUp.Core.Application.Exceptions;
using MillionAndUp.Core.Application.Interfaces;
using MillionAndUp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.Owners.Commands.UpdateOwnerCommand
{
    public class UpdateOwnerCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Owner> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateOwnerCommandHandler(IRepositoryAsync<Owner> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            var currentOwner = await _repositoryAsync.GetByIdAsync(request.Id);

            if(currentOwner == null)
            {
                throw new KeyNotFoundException($"Owner with Id {request.Id} not exist.");
            }
            else
            {
                currentOwner.Name = request.Name;
                currentOwner.Address = request.Address;
                currentOwner.Photo = request.Photo;
                currentOwner.Birthday = request.Birthday;

                await _repositoryAsync.UpdateAsync(currentOwner);
            }

            return new Response<int>(currentOwner.Id);

        }
    }

}
