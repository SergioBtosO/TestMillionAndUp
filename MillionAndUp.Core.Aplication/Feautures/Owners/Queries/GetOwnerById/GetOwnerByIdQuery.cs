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

namespace MillionAndUp.Core.Application.Feautures.Owners.Queries.GetOwnerById
{
    public class GetOwnerByIdQuery : IRequest<Response<OwnerDTO>>
    {
        public int Id { get; set; }
        public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, Response<OwnerDTO>>
        {
            private readonly IRepositoryAsync<Owner> _repositoryAsync;
            private readonly IMapper _mapper;
            public GetOwnerByIdQueryHandler(IRepositoryAsync<Owner> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<OwnerDTO>> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
            {
                var owrnerResult = await _repositoryAsync.GetByIdAsync(request.Id);

                if (owrnerResult == null)
                {
                    throw new KeyNotFoundException($"Owner with Id {request.Id} not exist.");
                }
                else
                {
                    var ownerDtoResult = _mapper.Map<OwnerDTO>(owrnerResult);
                    return new Response<OwnerDTO>(ownerDtoResult);
                }
            }
        }

    }
}
