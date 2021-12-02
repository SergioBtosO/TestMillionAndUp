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

namespace MillionAndUp.Core.Application.Feautures.Properties.Commands.DeletePropertyCommand
{
    public class DeletePropertyCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Property> _repositoryAsync;

        public DeletePropertyCommandHandler(IRepositoryAsync<Property> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            var currentProperty = await _repositoryAsync.GetByIdAsync(request.Id);

            if (currentProperty == null)
            {
                throw new KeyNotFoundException($"Property with Id {request.Id} not exist.");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(currentProperty);
            }

            return new Response<int>(currentProperty.Id);
        }
    }
}
