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

namespace MillionAndUp.Core.Application.Feautures.Properties.Commands.UpdatePropertyCommand
{
    public class UpdatePropertyCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
    }

    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Property> _repositoryAsync;
        private readonly IMapper _mapper;
        public UpdatePropertyCommandHandler(IRepositoryAsync<Property> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var currentProperty = await _repositoryAsync.GetByIdAsync(request.Id);

            if (currentProperty == null)
            {
                throw new KeyNotFoundException($"Property with Id {request.Id} not exist.");
            }
            else
            {
                currentProperty.Name = request.Name;
                currentProperty.Address = request.Address;
                currentProperty.Price = request.Price;
                currentProperty.CodeInternal = request.CodeInternal;
                currentProperty.Year = request.Year;
                currentProperty.IdOwner = request.IdOwner;

                await _repositoryAsync.UpdateAsync(currentProperty);
            }

            return new Response<int>(currentProperty.Id);
        }
    }
}
