using AutoMapper;
using MediatR;
using MillionAndUp.Core.Aplication.Wrappers;
using MillionAndUp.Core.Application.DTOs;
using MillionAndUp.Core.Application.Interfaces;
using MillionAndUp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.Properties.Queries.GetPropertyById
{
    public class GetPropertyByIdQuery : IRequest<Response<PropertyDTO>>
    {
        public int Id { get; set; }

        public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, Response<PropertyDTO>>
        {
            private readonly IRepositoryAsync<Property> _repositoryAsync;
            private readonly IMapper _mapper;
            public GetPropertyByIdQueryHandler(IRepositoryAsync<Property> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<PropertyDTO>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
            {
                var propertyResult = await _repositoryAsync.GetByIdAsync(request.Id);

                if (propertyResult == null)
                {
                    throw new KeyNotFoundException($"Property with Id {request.Id} not exist.");
                }
                else
                {
                    var propertyDtoResult = _mapper.Map<PropertyDTO>(propertyResult);
                    return new Response<PropertyDTO>(propertyDtoResult);
                }
            }
        }


    }
}
