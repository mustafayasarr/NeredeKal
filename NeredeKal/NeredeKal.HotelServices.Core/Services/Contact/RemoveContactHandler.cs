using MediatR;
using Microsoft.Extensions.Logging;
using NeredeKal.HotelServices.Domain.Constants;
using NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Contact;
using NeredeKal.HotelServices.Domain.Models.Results;
using NeredeKal.HotelServices.Infrastructure.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Core.Services.Contact
{
	public sealed class RemoveContactHandler : IRequestHandler<RemoveContactCommand, BaseResponseResult>
	{
		private readonly ILogger<RemoveContactHandler> _logger;
		private IUnitOfWork _unitOfWork;
		public RemoveContactHandler(IUnitOfWork unitOfWork, ILogger<RemoveContactHandler> logger)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}
		public async Task<BaseResponseResult> Handle(RemoveContactCommand command, CancellationToken cancellationToken)
		{

			var response = new BaseResponseResult();
			try
			{
				var getEntity = await _unitOfWork.ContactRepository.FirstOrDefaultAsync(x => x.Id == Guid.Parse(command.ContactId) && x.HotelId == Guid.Parse(command.HotelId));
				if (getEntity == null)
				{
					response.Errors.Add(ResponseMessageConstants.NoRecordContact);
					return response;

				}
				_unitOfWork.ContactRepository.Delete(getEntity);
				await _unitOfWork.CompleteAsync();

			}
			catch (Exception ex)
			{
				await _unitOfWork.CompleteAsync(false);
				_logger.LogError(ex, ex.Message);
				response.Errors.Add(ResponseMessageConstants.AnErrorOccurred);
			}
			return response;
		}
	}
}
