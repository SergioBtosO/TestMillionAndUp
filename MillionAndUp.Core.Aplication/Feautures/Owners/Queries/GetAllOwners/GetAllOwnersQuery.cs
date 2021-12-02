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

namespace MillionAndUp.Core.Application.Feautures.Owners.Queries.GetAllOwners
{
    public class GetAllOwnersQuery : IRequest<PagedResponse<List<OwnerDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime MinBirthday { get; set; }
        public DateTime MaxBirthday { get; set; }

    }

    public class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, PagedResponse<List<OwnerDTO>>>
    {
        private readonly IRepositoryAsync<Owner> _repositoryAsync;
        private readonly IMapper _mapper;
        public GetAllOwnersQueryHandler(IRepositoryAsync<Owner> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<OwnerDTO>>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
        {
            var ownersResult =  await _repositoryAsync.ListAsync(new PagedOwnersSpecification(request.PageNumber,request.PageSize, request.Name, request.Address,request.MinBirthday,request.MaxBirthday));
            var ownersDto = _mapper.Map<List<OwnerDTO>>(ownersResult);

            return new PagedResponse<List<OwnerDTO>>(ownersDto,request.PageNumber,request.PageSize);
        }
    }
}
