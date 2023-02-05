using FluentValidation;
using MediatR;
using NeredeKal.HotelServices.Domain.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Domain.Models.Messaging.Commands.Hotel
{
	public sealed record class RemoveHotelCommand : IRequest<BaseResponseResult>
	{
		public string HotelId { get; set; }
	}
	public class RemoveContactCommandValidator : AbstractValidator<RemoveHotelCommand>
	{
		public RemoveContactCommandValidator()
		{
			RuleFor(x => x.HotelId).NotEmpty().WithMessage("Lütfen HotelId giriniz.");
		}
	}
}
