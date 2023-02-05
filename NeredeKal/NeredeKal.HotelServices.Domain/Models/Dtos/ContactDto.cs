using FluentValidation;
using NeredeKal.HotelServices.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Domain.Models.Dtos
{
	public class ContactDto
	{
		public ContactDto(Guid contactId, ContactType contactType, string contactContent, DateTime createdDate)
		{
			Id = contactId;
			ContactContent = contactContent;
			ContactType = contactType;
			CreatedDate = createdDate;
		}
		public ContactDto()
		{

		}

		public Guid Id { get; set; }
		public ContactType ContactType { get; set; }
		public string ContactContent { get; set; }
		public DateTime CreatedDate { get; set; }

	}
	public class ContactInformationDtoValidator : AbstractValidator<ContactDto>
	{
		public ContactInformationDtoValidator()
		{
			RuleFor(x => x.ContactContent).NotEmpty().WithMessage("Lütfen ContactContent giriniz.");
			RuleFor(x => x.ContactType).NotNull().WithMessage("Lütfen ContactType giriniz.");
		}
	}
}
