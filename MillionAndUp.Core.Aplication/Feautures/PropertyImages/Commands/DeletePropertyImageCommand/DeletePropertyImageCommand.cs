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

namespace MillionAndUp.Core.Application.Feautures.PropertyImages.Commands.DeletePropertyImageCommand
{
    public class DeletePropertyImageCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeletePropertyImageCommandHandler : IRequestHandler<DeletePropertyImageCommand, Response<int>>
    {
        private readonly IRepositoryAsync<PropertyImage> _repositoryAsync;

        public DeletePropertyImageCommandHandler(IRepositoryAsync<PropertyImage> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeletePropertyImageCommand request, CancellationToken cancellationToken)
        {
            var currentPropertyImage = await _repositoryAsync.GetByIdAsync(request.Id);

            if (currentPropertyImage == null)
            {
                throw new KeyNotFoundException($"Property Image with Id {request.Id} not exist.");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(currentPropertyImage);
            }

            return new Response<int>(currentPropertyImage.Id);
        }
    }
}
