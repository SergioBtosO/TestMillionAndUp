using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MillionAndUp.Core.Application.Feautures.Owners.Queries.GetOwnerById;
using MillionAndUp.Core.Application.Feautures.Properties.Commands.CreatePropertyCommand;
using MillionAndUp.Core.Application.Feautures.Properties.Commands.DeletePropertyCommand;
using MillionAndUp.Core.Application.Feautures.Properties.Commands.UpdatePropertyCommand;
using MillionAndUp.Core.Application.Feautures.Properties.Queries.GetAllProperties;
using MillionAndUp.Core.Application.Feautures.Properties.Queries.GetPropertyById;
using System.Threading.Tasks;

namespace MillionAndUp.Presentation.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PropertyController : BaseAPIController
    {

        //GET: api/<controller>
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] GetAllPropertiesParameters parameters)
        {
            return Ok(await Mediator.Send(new GetAllPropertiesQuery
            {
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                Name = parameters.Name,
                Address = parameters.Address,
                CodeInternal = parameters.CodeInternal,
                IdOwner = parameters.IdOwner,
                MinPrice = parameters.MinPrice,
                MaxPrice = parameters.MaxPrice,
                MaxYear = parameters.MaxYear,
                MinYear = parameters.MinYear
            }));
        }

        //GET: api/<controller>/4
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPropertyByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(CreatePropertyCommand command)
        {
            if(await Mediator.Send(new GetOwnerByIdQuery { Id = command.IdOwner }) != null)
              return Ok(await Mediator.Send(command));
            return BadRequest();
        }

        //PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, UpdatePropertyCommand command)
        {
            return id == command.Id ? Ok(await Mediator.Send(command)) : BadRequest();
        }

        //Delete api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id)
        {
            return Ok(await Mediator.Send(new DeletePropertyCommand { Id = id }));
        }
    }
}
