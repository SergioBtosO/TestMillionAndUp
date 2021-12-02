using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MillionAndUp.Core.Application.Feautures.Properties.Queries.GetPropertyById;
using MillionAndUp.Core.Application.Feautures.PropertyTraces.Commands.CreatePropertyTraceCommand;
using MillionAndUp.Core.Application.Feautures.PropertyTraces.Commands.DeletePropertyTraceCommand;
using MillionAndUp.Core.Application.Feautures.PropertyTraces.Commands.UpdatePropertyTraceCommand;
using MillionAndUp.Core.Application.Feautures.PropertyTraces.Queries.GetAllPropertyTraces;
using MillionAndUp.Core.Application.Feautures.PropertyTraces.Queries.GetPropertyTraceById;
using System.Threading.Tasks;

namespace MillionAndUp.Presentation.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PropertyTraceController : BaseAPIController
    {
        //GET: api/<controller>
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] GetAllPropertyTracesParameters parameters)
        {
            return Ok(await Mediator.Send(new GetAllPropertyTracesQuery
            {
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                Name = parameters.Name,
                IdProperty = parameters.IdProperty,
                MinDateSale = parameters.MinDateSale,
                MaxDateSale = parameters.MaxDateSale,
                MinValue = parameters.MinValue,
                MaxValue = parameters.MaxValue,
                MinTax = parameters.MinTax,
                MaxTax = parameters.MaxTax
            }));
        }

        //GET: api/<controller>/4
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPropertyTraceByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(CreatePropertyTraceCommand command)
        {
            if (await Mediator.Send(new GetPropertyByIdQuery { Id = command.IdProperty}) != null)
                return Ok(await Mediator.Send(command));
            return BadRequest();
        }

        //PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, UpdatePropertyTraceCommand command)
        {
            return id == command.Id ? Ok(await Mediator.Send(command)) : BadRequest();
        }

        //Delete api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id)
        {
            return Ok(await Mediator.Send(new DeletePropertyTraceCommand { Id = id }));
        }
    }
}
