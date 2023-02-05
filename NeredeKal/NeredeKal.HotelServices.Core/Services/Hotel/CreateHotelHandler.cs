using MediatR;
using Microsoft.Extensions.Logging;
using NeredeKal.HotelServices.Domain.Constants;
using NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Hotel;
using NeredeKal.HotelServices.Domain.Models.Results;
using NeredeKal.HotelServices.Infrastructure.Repositories.Abstract;

namespace NeredeKal.HotelServices.Core.Services.Hotel
{
	public class CreateHotelHandler : IRequestHandler<CreateHotelCommand, BaseResponseResult>
	{
		private readonly ILogger<CreateHotelHandler> _logger;
		private IUnitOfWork _unitOfWork;
		public CreateHotelHandler(IUnitOfWork unitOfWork, ILogger<CreateHotelHandler> logger)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}
		public async Task<BaseResponseResult> Handle(CreateHotelCommand command, CancellationToken cancellationToken)
		{
			var response = new BaseResponseResult();
			try
			{
				var hotelEntity = new Domain.Models.Entities.Hotel(command.AuthorizedName.ToUpper(), command.AuthorizedLastName.ToUpper(), command.CompanyName.ToUpper());
				_unitOfWork.HotelRepository.Add(hotelEntity);
				if (command.Contacts != null && command.Contacts.Count > 0)
				{

					var entityInfoList = command.Contacts.Select(x => new Domain.Models.Entities.Contact(x.ContactType, x.ContactContent.ToUpper(), hotelEntity.Id));

					_unitOfWork.ContactRepository.AddRange(entityInfoList);
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
