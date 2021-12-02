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

namespace MillionAndUp.Core.Application.Feautures.PropertyImages.Commands.UpdatePropertyImageCommand
{
    public class UpdatePropertyImageCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public byte[] File { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
    }

    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyImageCommand, Response<int>>
    {
        private readonly IRepositoryAsync<PropertyImage> _repositoryAsync;
        private readonly IMapper _mapper;
        public UpdatePropertyCommandHandler(IRepositoryAsync<PropertyImage> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(UpdatePropertyImageCommand request, CancellationToken cancellationToken)
        {
            var currentPropertyImage = await _repositoryAsync.GetByIdAsync(request.Id);

            if (currentPropertyImage == null)
            {
                throw new KeyNotFoundException($"Property Image with Id {request.Id} not exist.");
            }
            else
            {
                currentPropertyImage.Title = request.Title;
                currentPropertyImage.File = request.File;
                currentPropertyImage.Description = request.Description;
                currentPropertyImage.Enabled = request.Enabled;

                await _repositoryAsync.UpdateAsync(currentPropertyImage);
            }

            return new Response<int>(currentPropertyImage.Id);
        }
    }

 }
