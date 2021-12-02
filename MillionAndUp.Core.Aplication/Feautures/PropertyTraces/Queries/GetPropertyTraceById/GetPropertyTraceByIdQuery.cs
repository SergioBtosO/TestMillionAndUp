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

namespace MillionAndUp.Core.Application.Feautures.PropertyTraces.Queries.GetPropertyTraceById
{
    public class GetPropertyTraceByIdQuery : IRequest<Response<PropertyTraceDTO>>
    {
        public int Id { get; set; }

        public class GetPropertyTraceByIdQueryHandler : IRequestHandler<GetPropertyTraceByIdQuery, Response<PropertyTraceDTO>>
        {
            private readonly IRepositoryAsync<PropertyTrace> _repositoryAsync;
            private readonly IMapper _mapper;
            public GetPropertyTraceByIdQueryHandler(IRepositoryAsync<PropertyTrace> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<PropertyTraceDTO>> Handle(GetPropertyTraceByIdQuery request, CancellationToken cancellationToken)
            {
                var propertyTraceResult = await _repositoryAsync.GetByIdAsync(request.Id);

                if (propertyTraceResult == null)
                {
                    throw new KeyNotFoundException($"Property Trace with Id {request.Id} not exist.");
                }
                else
                {
                    var propertyTraceDtoResult = _mapper.Map<PropertyTraceDTO>(propertyTraceResult);
                    return new Response<PropertyTraceDTO>(propertyTraceDtoResult);
                }
            }
        }


    }
}
