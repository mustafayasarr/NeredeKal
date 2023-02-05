using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Contact;
using NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Hotel;
using NeredeKal.HotelServices.Domain.Models.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace NeredeKal.HotelServices.API.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	[Produces("application/json")]
	public class HotelController : BaseApiController
	{
		public HotelController(ILogger<HotelController> logger, IMediator mediator) : base(logger, mediator)
		{
		}

		[HttpPost]
		[SwaggerOperation(Summary = "Hotel create işlemini gerçekleştirir")]
		[ProducesResponseType(typeof(BaseResponseResult), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(BaseResponseResult), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResponseResult>> CreateHotel(CreateHotelCommand command)
		{
			var response = await _mediator.Send(command);

			if (response.HasError)
			{
				return BadRequest(response);
			}

			return Ok(response);

		}

		[HttpDelete]
		[SwaggerOperation(Summary = "Hotel delete işlemini gerçekleştirir")]
		[ProducesResponseType(typeof(BaseResponseResult), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(BaseResponseResult), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResponseResult>> RemoveHotel(RemoveHotelCommand command)
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
