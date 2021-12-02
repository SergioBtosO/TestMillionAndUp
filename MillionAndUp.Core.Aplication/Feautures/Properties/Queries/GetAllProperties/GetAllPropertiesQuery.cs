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

namespace MillionAndUp.Core.Application.Feautures.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesQuery : IRequest<PagedResponse<List<PropertyDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CodeInternal { get; set; }
        public int IdOwner { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public int MaxYear { get; set; }
        public int MinYear { get; set; }
    }

    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, PagedResponse<List<PropertyDTO>>>
    {
        private readonly IRepositoryAsync<Property> _repositoryAsync;
        private readonly IMapper _mapper;
        public GetAllPropertiesQueryHandler(IRepositoryAsync<Property> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<PropertyDTO>>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var propertiesResult = await _repositoryAsync.ListAsync(new PagedPropertiesSpecification(request.PageNumber, request.PageSize, request.Name, request.Address, request.CodeInternal,request.IdOwner,request.MinPrice,request.MaxPrice,request.MaxYear,request.MinYear));
            var propertiesDto = _mapper.Map<List<PropertyDTO>>(propertiesResult);

            return new PagedResponse<List<PropertyDTO>>(propertiesDto, request.PageNumber, request.PageSize);
        }
    }
}
