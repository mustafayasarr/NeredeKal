using FluentValidation;
using MediatR;
using NeredeKal.HotelServices.Domain.Models.Enums;
using NeredeKal.HotelServices.Domain.Models.Results;

namespace NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Contact
{
	public sealed record class CreateContactCommand : IRequest<BaseResponseResult>
	{
		public CreateContactCommand(ContactType contactType, string content, string hotelId)
		{
			ContactType = contactType;
			Content = content;
			HotelId = hotelId;
		}
		public ContactType ContactType { get; set; }
		public string Content { get; set; }
		public string HotelId { get; set; }
	}

	public sealed class CreateContactValidator : AbstractValidator<CreateContactCommand>
	{
		public CreateContactValidator()
		{
			RuleFor(x => x.ContactType)
				.NotEmpty().WithMessage("Lütfen ContactType giriniz.");
			RuleFor(x => x.HotelId).NotEmpty().WithMessage("Lütfen HotelId giriniz.");
		}
	}
}
