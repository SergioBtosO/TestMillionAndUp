using AutoMapper;
using MillionAndUp.Core.Application.DTOs;
using MillionAndUp.Core.Application.Feautures.Owners.Commands.CreateOwnerCommand;
using MillionAndUp.Core.Application.Feautures.Owners.Commands.UpdateOwnerCommand;
using MillionAndUp.Core.Application.Feautures.Properties.Commands.CreatePropertyCommand;
using MillionAndUp.Core.Application.Feautures.PropertyImages.Commands.CreatePropertyImageCommand;
using MillionAndUp.Core.Application.Feautures.PropertyTraces.Commands.CreatePropertyTraceCommand;
using MillionAndUp.Core.Domain.Entities;

namespace MillionAndUp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            
            #region Commands
            CreateMap<CreateOwnerCommand, Owner>();
            CreateMap<CreatePropertyCommand, Property>();
            CreateMap<CreatePropertyImageCommand, PropertyImage>();
            CreateMap<CreatePropertyTraceCommand, PropertyTrace>();
            #endregion
            #region DTO's
            CreateMap<Owner, OwnerDTO>();
            CreateMap<Property, PropertyDTO>();
            CreateMap<PropertyImage, PropertyImageDTO>();
            CreateMap<PropertyTrace, PropertyTraceDTO>();
            #endregion





        }
    }
}
