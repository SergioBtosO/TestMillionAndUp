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

namespace MillionAndUp.Core.Application.Feautures.PropertyTraces.Commands.DeletePropertyTraceCommand
{
    public class DeletePropertyTraceCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeletePropertyTraceCommandHandler : IRequestHandler<DeletePropertyTraceCommand, Response<int>>
    {
        private readonly IRepositoryAsync<PropertyTrace> _repositoryAsync;

        public DeletePropertyTraceCommandHandler(IRepositoryAsync<PropertyTrace> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeletePropertyTraceCommand request, CancellationToken cancellationToken)
        {
            var currentPropertyTrace = await _repositoryAsync.GetByIdAsync(request.Id);

            if (currentPropertyTrace == null)
            {
                throw new KeyNotFoundException($"Property Trace with Id {request.Id} not exist.");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(currentPropertyTrace);
            }

            return new Response<int>(currentPropertyTrace.Id);
        }
    }
}
