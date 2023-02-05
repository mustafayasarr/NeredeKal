using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Contact;
using NeredeKal.HotelServices.Domain.Models.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace NeredeKal.HotelServices.API.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	[Produces("application/json")]
	public class ContactController : BaseApiController
	{
		public ContactController(ILogger<ContactController> logger, IMediator mediator) : base(logger, mediator)
		{
		}

		[HttpPost]
		[SwaggerOperation(Summary = "Contact create işlemini gerçekleştirir")]
		[ProducesResponseType(typeof(BaseResponseResult), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(BaseResponseResult), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResponseResult>> CreateContact(CreateContactCommand command)
		{
			var response = await _mediator.Send(command);

			if (response.HasError)
			{
				return BadRequest(response);
			}

			return Ok(response);

		}

		[HttpDelete]
		[SwaggerOperation(Summary = "Contact delete işlemini gerçekleştirir")]
		[ProducesResponseType(typeof(BaseResponseResult), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(BaseResponseResult), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResponseResult>> RemoveContactInformation(RemoveContactCommand command)
		{
			var response = await _mediator.Send(command);

			if (response.HasError)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}

	}
}
