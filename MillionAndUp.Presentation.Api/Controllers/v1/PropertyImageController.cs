using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MillionAndUp.Core.Application.Feautures.Properties.Queries.GetPropertyById;
using MillionAndUp.Core.Application.Feautures.PropertyImages.Commands.CreatePropertyImageCommand;
using MillionAndUp.Core.Application.Feautures.PropertyImages.Commands.DeletePropertyImageCommand;
using MillionAndUp.Core.Application.Feautures.PropertyImages.Commands.UpdatePropertyImageCommand;
using MillionAndUp.Core.Application.Feautures.PropertyImages.Queries.GetAllPropertyImages;
using MillionAndUp.Core.Application.Feautures.PropertyImages.Queries.GetPropertyImagesById;
using System.IO;
using System.Threading.Tasks;

namespace MillionAndUp.Presentation.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PropertyImageController : BaseAPIController
    {

        //GET: api/<controller>
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] GetAllPropertyImagesParameters parameters)
        {
            return Ok(await Mediator.Send(new GetAllPropertyImagesQuery
            {
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                Tittle = parameters.Tittle,
                Description = parameters.Description,
                IdProperty = parameters.IdProperty,
                Enabled = parameters.Enabled
            }));
        }

        //GET: api/<controller>/4
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPropertyImageByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(CreatePropertyImageCommand command)
        {
            

            if (await Mediator.Send(new GetPropertyByIdQuery { Id = command.IdProperty }) != null)
                return Ok(await Mediator.Send(command));
            return BadRequest();
        }

        //PUT api/<controller>/5
       
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromForm] UpdatePropertyImageCommand command)
        {
            return id == command.Id ? Ok(await Mediator.Send(command)) : BadRequest();
        }

        //Delete api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id)
        {
            return Ok(await Mediator.Send(new DeletePropertyImageCommand { Id = id }));
        }
    }
}
