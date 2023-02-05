using FluentValidation;
using MediatR;
using NeredeKal.HotelServices.Domain.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Contact
{
	public sealed record class RemoveContactCommand : IRequest<BaseResponseResult>
	{
		public string HotelId { get; set; }

		public string ContactId { get; set; }
	}
	public class RemoveContactValidator : AbstractValidator<RemoveContactCommand>
	{
		public RemoveContactValidator()
		{
			RuleFor(x => x.ContactId)
				.NotNull().NotEmpty().WithMessage("Lütfen ContactId giriniz.");
			RuleFor(x => x.ContactId).NotNull().NotEmpty().WithMessage("Lütfen ContactId giriniz.");
		}
	}
}
