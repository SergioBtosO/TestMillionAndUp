using AutoMapper;
using MediatR;
using MillionAndUp.Core.Aplication.Wrappers;
using MillionAndUp.Core.Application.Interfaces;
using MillionAndUp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.PropertyTraces.Commands.UpdatePropertyTraceCommand
{
    public class UpdatePropertyTraceCommand : IRequest<Response<int>>
    {
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double Tax { get; set; }
        public int Id { get; set; }
    }

    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyTraceCommand, Response<int>>
    {
        private readonly IRepositoryAsync<PropertyTrace> _repositoryAsync;
        private readonly IMapper _mapper;
        public UpdatePropertyCommandHandler(IRepositoryAsync<PropertyTrace> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(UpdatePropertyTraceCommand request, CancellationToken cancellationToken)
        {
            var currentPropertyTrace = await _repositoryAsync.GetByIdAsync(request.Id);

            if (currentPropertyTrace == null)
            {
                throw new KeyNotFoundException($"Property Trace with Id {request.Id} not exist.");
            }
            else
            {
                currentPropertyTrace.DateSale = request.DateSale;
                currentPropertyTrace.Name = request.Name;
                currentPropertyTrace.Value = request.Value;
                currentPropertyTrace.Tax = request.Tax;

                await _repositoryAsync.UpdateAsync(currentPropertyTrace);
            }

            return new Response<int>(currentPropertyTrace.Id);
        }
    }
}
