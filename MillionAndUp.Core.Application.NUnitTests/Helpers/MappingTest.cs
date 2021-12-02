using AutoMapper;
using MillionAndUp.Core.Application.DTOs;
using MillionAndUp.Core.Application.Feautures.Owners.Commands.CreateOwnerCommand;
using MillionAndUp.Core.Application.Feautures.Properties.Commands.CreatePropertyCommand;
using MillionAndUp.Core.Application.Feautures.PropertyImages.Commands.CreatePropertyImageCommand;
using MillionAndUp.Core.Application.Feautures.PropertyTraces.Commands.CreatePropertyTraceCommand;
using MillionAndUp.Core.Domain.Entities;
using MillionAndUp.Infraestructure.Persistence.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Helpers
{
    public class MappingTest: Profile
    {
        public MappingTest()
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
