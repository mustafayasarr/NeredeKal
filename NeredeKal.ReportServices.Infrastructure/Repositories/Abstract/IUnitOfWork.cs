namespace NeredeKal.ReportServices.Infrastructure.Repositories.Abstract
{
	public interface IUnitOfWork : IDisposable
	{
		IReportItemRepository ReportItemRepository { get; }

		void Complete(bool state = true);
		Task CompleteAsync(bool state = true);
	}
}
