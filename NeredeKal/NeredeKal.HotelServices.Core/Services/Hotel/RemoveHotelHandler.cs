using MediatR;
using Microsoft.Extensions.Logging;
using NeredeKal.HotelServices.Core.Services.Contact;
using NeredeKal.HotelServices.Domain.Constants;
using NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Contact;
using NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Hotel;
using NeredeKal.HotelServices.Domain.Models.Results;
using NeredeKal.HotelServices.Infrastructure.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NeredeKal.HotelServices.Core.Services.Hotel
{
	public sealed class RemoveHotelHandler : IRequestHandler<RemoveHotelCommand, BaseResponseResult>
	{
		private readonly ILogger<RemoveHotelHandler> _logger;
		private IUnitOfWork _unitOfWork;
		public RemoveHotelHandler(IUnitOfWork unitOfWork, ILogger<RemoveHotelHandler> logger)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}
		public async Task<BaseResponseResult> Handle(RemoveHotelCommand command, CancellationToken cancellationToken)
		{
			var response = new BaseResponseResult();
			try
			{
				var getHotel = await _unitOfWork.HotelRepository.FirstOrDefaultAsync(x => x.Id == Guid.Parse(command.HotelId));
				if (getHotel == null)
				{
					response.Errors.Add(ResponseMessageConstants.NoRecordHotel);
					return response;

				}
				_unitOfWork.HotelRepository.Delete(getHotel);

				var getContacts = await _unitOfWork.ContactRepository.ToListAsync(x => x.HotelId == Guid.Parse(command.HotelId));
				if (getContacts != null && getContacts.Count > 0)
				{
					_unitOfWork.ContactRepository.DeleteRange(getContacts);
				}

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
