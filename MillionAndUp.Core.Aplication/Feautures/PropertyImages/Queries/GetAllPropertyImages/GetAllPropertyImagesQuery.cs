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

namespace MillionAndUp.Core.Application.Feautures.PropertyImages.Queries.GetAllPropertyImages
{
    public class GetAllPropertyImagesQuery : IRequest<PagedResponse<List<PropertyImageDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public int IdProperty { get; set; }
        public bool Enabled { get; set; }
    }
    public class GetAllPropertyImagesQueryHandler : IRequestHandler<GetAllPropertyImagesQuery, PagedResponse<List<PropertyImageDTO>>>
    {
        private readonly IRepositoryAsync<PropertyImage> _repositoryAsync;
        private readonly IMapper _mapper;
        public GetAllPropertyImagesQueryHandler(IRepositoryAsync<PropertyImage> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<PropertyImageDTO>>> Handle(GetAllPropertyImagesQuery request, CancellationToken cancellationToken)
        {
            var propertysImagesResult = await _repositoryAsync.ListAsync(new PagedPropertyImagesSpecification(request.PageNumber, request.PageSize, request.Tittle,request.Description,request.IdProperty,request.Enabled));
            var propertyImagesDto = _mapper.Map<List<PropertyImageDTO>>(propertysImagesResult);

            return new PagedResponse<List<PropertyImageDTO>>(propertyImagesDto, request.PageNumber, request.PageSize);
        }
    }

}
