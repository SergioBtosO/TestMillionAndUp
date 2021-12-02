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

namespace MillionAndUp.Core.Application.Feautures.Owners.Commands.DeleteOwnerCommand
{
    public class DeleteOwnerCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Owner> _repositoryAsync;

        public DeleteOwnerCommandHandler(IRepositoryAsync<Owner> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
        {
            var currentOwner = await _repositoryAsync.GetByIdAsync(request.Id);

            if (currentOwner == null)
            {
                throw new KeyNotFoundException($"Owner with Id {request.Id} not exist.");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(currentOwner);
            }

            return new Response<int>(currentOwner.Id);
        }
    }
}
