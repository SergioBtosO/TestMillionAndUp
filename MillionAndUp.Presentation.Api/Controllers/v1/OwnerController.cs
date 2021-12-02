using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MillionAndUp.Core.Application.Feautures.Owners.Commands.CreateOwnerCommand;
using MillionAndUp.Core.Application.Feautures.Owners.Commands.DeleteOwnerCommand;
using MillionAndUp.Core.Application.Feautures.Owners.Commands.UpdateOwnerCommand;
using MillionAndUp.Core.Application.Feautures.Owners.Queries.GetAllOwners;
using MillionAndUp.Core.Application.Feautures.Owners.Queries.GetOwnerById;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MillionAndUp.Presentation.Api.Controllers.v1
{

    [ApiVersion("1.0")]
    public class OwnerController : BaseAPIController
    {

        //GET: api/<controller>
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] GetAllOwnersParameters parameters)
        {
            return Ok(await Mediator.Send(new GetAllOwnersQuery {
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize, 
                Name = parameters.Name,
                Address = parameters.Address,
                MinBirthday = parameters.MinBirthday,
                MaxBirthday = parameters.MaxBirthday,
            }));
        }


        //GET: api/<controller>/4
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetOwnerByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost]
         [Authorize(Roles = "Admin")]
        public async Task <IActionResult> Post(CreateOwnerCommand command)
        {   
            return Ok (await Mediator.Send(command));
        }

        //PUT api/<controller>/5
        [HttpPut ("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id,  UpdateOwnerCommand command)
        {
            return id== command.Id ? Ok(await Mediator.Send(command)) : BadRequest();
        }

        //Delete api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id)
        {
            return Ok(await Mediator.Send(new DeleteOwnerCommand { Id = id }));
        }
    }
}
