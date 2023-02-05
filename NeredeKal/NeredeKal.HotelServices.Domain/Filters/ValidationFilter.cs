using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NeredeKal.HotelServices.Domain.Models.Results;

namespace NeredeKal.HotelServices.Domain.Filters
{
	public class ValidationFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (!context.ModelState.IsValid)
			{

				var errorsInModelState = context.ModelState.Where(p => p.Value.Errors.Count > 0)
					.ToDictionary(x => x.Key, x => x.Value.Errors.Select(y => y.ErrorMessage)).ToArray();

				var errorResponse = new BaseResponseResult();

				foreach (var error in errorsInModelState)
				{
					foreach (var subError in error.Value)
					{
						errorResponse.Errors.Add(subError);
					}
				}

				context.Result = new BadRequestObjectResult(errorResponse);
			}
			await next();

		}
	}
}
