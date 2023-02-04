using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeredeKal.HotelServices.Infrastructure.Repositories.Abstract
{
	public interface IUnitOfWork : IDisposable
	{
		IHotelRepository HotelRepository { get; }
		IContactRepository ContactRepository { get; }
		void Complete(bool state = true);
		Task CompleteAsync(bool state = true);
	}
}
