using AutoMapper;
using MediatR;
using MillionAndUp.Core.Application.DTOs;
using MillionAndUp.Core.Application.Interfaces;
using MillionAndUp.Core.Application.Specifications;
using MillionAndUp.Core.Application.Wrappers;
using MillionAndUp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.PropertyTraces.Queries.GetAllPropertyTraces
{
    public class GetAllPropertyTracesQuery : IRequest<PagedResponse<List<PropertyTraceDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
        public int IdProperty { get; set; }
        public DateTime MinDateSale { get; set; }
        public DateTime MaxDateSale { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double MinTax { get; set; }
        public double MaxTax { get; set; }
    }
    public class GetAllPropertyTracesQueryHandler : IRequestHandler<GetAllPropertyTracesQuery, PagedResponse<List<PropertyTraceDTO>>>
    {
        private readonly IRepositoryAsync<PropertyTrace> _repositoryAsync;
        private readonly IMapper _mapper;
        public GetAllPropertyTracesQueryHandler(IRepositoryAsync<PropertyTrace> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<PropertyTraceDTO>>> Handle(GetAllPropertyTracesQuery request, CancellationToken cancellationToken)
        {
            var propertysTraceResult = await _repositoryAsync.ListAsync(new PagedPropertyTraceSpecification(request.PageNumber, request.PageSize, request.Name,request.IdProperty,request.MinDateSale,request.MaxDateSale,request.MinValue,request.MaxValue,request.MinTax,request.MaxTax));
            var propertyTraceDto = _mapper.Map<List<PropertyTraceDTO>>(propertysTraceResult);

            return new PagedResponse<List<PropertyTraceDTO>>(propertyTraceDto, request.PageNumber, request.PageSize);
        }
    }
}
