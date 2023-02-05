using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NeredeKal.HotelServices.Domain.Constants;
using NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Contact;
using NeredeKal.HotelServices.Domain.Models.Results;
using NeredeKal.HotelServices.Infrastructure.Repositories.Abstract;

namespace NeredeKal.HotelServices.Core.Services.Contact
{
	public sealed class CreateContactHandler : IRequestHandler<CreateContactCommand, BaseResponseResult>
	{
		private IUnitOfWork _unitOfWork;
		private readonly ILogger<CreateContactHandler> _logger;
		public CreateContactHandler(IUnitOfWork unitOfWork, ILogger<CreateContactHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}
		public async Task<BaseResponseResult> Handle(CreateContactCommand command, CancellationToken cancellationToken)
		{
			var response = new BaseResponseResult();
			try
			{
				var getHotel = await _unitOfWork.HotelRepository.Table.Include(x => x.Contacts).FirstOrDefaultAsync(x => x.Id == Guid.Parse(command.HotelId));
				if (getHotel == null)
				{
					response.Errors.Add(ResponseMessageConstants.NoRecordContact);
					return response;

				}
				if (getHotel.Contacts.FirstOrDefault(x => x.HotelId == Guid.Parse(command.HotelId) && x.Content == command.Content && x.ContactType == command.ContactType) != null)
				{
					response.Errors.Add(ResponseMessageConstants.AllreadyRecordData);
					return response;

				}
				_unitOfWork.ContactRepository.Add(new Domain.Models.Entities.Contact(command.ContactType, command.Content.ToUpper(), Guid.Parse(command.HotelId)));
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
