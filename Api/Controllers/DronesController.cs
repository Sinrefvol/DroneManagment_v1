using API.Application.ApiModels;
using API.Application.Commands;
using API.Application.Contracts;
using API.Application.Queries;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DronesController : BaseController
    {
        private IValidator<AddDroneContract> _validator;

        public DronesController(IValidator<AddDroneContract> validator)
        {
            _validator = validator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiDrone), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDroneById([FromRoute] Guid id)
        {
            var command = new GetDroneByIdQuery(id);
            var drone = await DispatchAsync(command);
            if (drone == null)
                return NotFound();
            return Ok(drone);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiDrone), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddDrone([FromBody] AddDroneContract contract)
        {
            ValidationResult result = await _validator.ValidateAsync(contract);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

                var command = new AddDroneCommand(contract.SerialNumber, contract.ModelName);
            var drone = await DispatchAsync(command);
            if (drone == null)
            {
                return BadRequest();
            }
            return Created(nameof(AddDrone), drone);
        }


        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateDrone([FromRoute] Guid id, [FromBody] UpdateDroneContract contract)
        {
            var updatedModel = await DispatchAsync(new UpdateDroneCommand(id, contract.ScheduledMaintainance, contract.DistanceCovered, contract.OnMaintainance));
            if(updatedModel == null)
            {
                return NotFound();
            }
            return NoContent();


        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteDrone([FromRoute] Guid id)
        {
            var deletedDrone = await DispatchAsync(new DeleteDroneCommand(id));
            if(deletedDrone == null)
                return NotFound();

            return NoContent();
        }
    }
}
