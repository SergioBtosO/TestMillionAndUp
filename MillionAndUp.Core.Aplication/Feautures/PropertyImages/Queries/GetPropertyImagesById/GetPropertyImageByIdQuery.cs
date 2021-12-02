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

namespace MillionAndUp.Core.Application.Feautures.PropertyImages.Queries.GetPropertyImagesById
{
    public class GetPropertyImageByIdQuery : IRequest<Response<PropertyImageDTO>>
    {
        public int Id { get; set; }

        public class GetPropertyImageByIdQueryHandler : IRequestHandler<GetPropertyImageByIdQuery, Response<PropertyImageDTO>>
        {
            private readonly IRepositoryAsync<PropertyImage> _repositoryAsync;
            private readonly IMapper _mapper;
            public GetPropertyImageByIdQueryHandler(IRepositoryAsync<PropertyImage> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<PropertyImageDTO>> Handle(GetPropertyImageByIdQuery request, CancellationToken cancellationToken)
            {
                var propertyResult = await _repositoryAsync.GetByIdAsync(request.Id);

                if (propertyResult == null)
                {
                    throw new KeyNotFoundException($"Property Image with Id {request.Id} not exist.");
                }
                else
                {
                    var propertyDtoResult = _mapper.Map<PropertyImageDTO>(propertyResult);
                    return new Response<PropertyImageDTO>(propertyDtoResult);
                }
            }
        }


    }
}
