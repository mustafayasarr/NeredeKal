using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NeredeKal.ReportServices.API.Controllers
{
	public class BaseApiController : ControllerBase
	{
		public readonly ILogger<BaseApiController> _logger;
		public readonly IMediator _mediator;
		public BaseApiController(ILogger<BaseApiController> logger, IMediator mediator)
		{
			_logger = logger;
			_mediator = mediator;
		}
	}
}
