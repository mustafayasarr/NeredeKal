using ClosedXML.Excel;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NeredeKal.ReportServices.Domain.Constants;
using NeredeKal.ReportServices.Domain.Models.Enums;
using NeredeKal.ReportServices.Domain.Models.Messaging.Commands;
using NeredeKal.ReportServices.Domain.Models.Results;
using NeredeKal.ReportServices.Infrastructure.Repositories.Abstract;

namespace NeredeKal.ReportServices.Core.Services.Report
{
	public class UpdateLocationReportHandler : IRequestHandler<UpdateLocationReportCommand, BaseResponseResult>
	{
		private readonly ILogger<UpdateLocationReportHandler> _logger;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHostingEnvironment _hostingEnvironment;

		public UpdateLocationReportHandler(ILogger<UpdateLocationReportHandler> logger, IUnitOfWork unitOfWork, IHostingEnvironment env)
		{
			_hostingEnvironment = env;
			_logger = logger;
			_unitOfWork = unitOfWork;

		}
		public async Task<BaseResponseResult> Handle(UpdateLocationReportCommand request, CancellationToken cancellationToken)
		{
			var response = new BaseResponseResult();
			try
			{
				var getEntity = await _unitOfWork.ReportItemRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

				if (getEntity != null)
				{
					if (request.Status == ReportStatus.Tamamlandi)
					{
						getEntity.Path = ExcelSave(request);
						getEntity.ReportCreateDate = request.CreatedDate;

					}
				}
				getEntity.ReportName = request.ReportName;
				getEntity.Status = request.Status;
				getEntity.RequestObjectJson = request.JsonRequest;
				getEntity.Message = request.Message;

				await _unitOfWork.ReportItemRepository.UpdateAsync(getEntity);
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
		private string ExcelSave(UpdateLocationReportCommand command)
		{
			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Location Report");
				var currentRow = 1;
				worksheet.Cell(currentRow, 1).Value = "Id";
				worksheet.Cell(currentRow, 2).Value = "Location";
				worksheet.Cell(currentRow, 3).Value = "Contact Count";
				worksheet.Cell(currentRow, 4).Value = "Phone Count";

				worksheet.Row(currentRow).CellsUsed().Style.Font.SetBold();
				worksheet.Row(currentRow).CellsUsed().Style.Font.SetFontSize(12);
				worksheet.Row(currentRow).CellsUsed().Style.Fill.SetBackgroundColor(XLColor.LightGray);

				foreach (var item in command.ReportItems)
				{
					currentRow++;
					worksheet.Cell(currentRow, 1).Value = item.Id;
					worksheet.Cell(currentRow, 2).Value = item.Location;
					worksheet.Cell(currentRow, 3).Value = item.ContactCount;
					worksheet.Cell(currentRow, 4).Value = item.PhoneCount;
				}
				worksheet.Columns().AdjustToContents();

				using var stream = new MemoryStream();
				workbook.SaveAs(stream);

				var fileName = $"{command.ReportName?.ToLower().Replace(" ", string.Empty)}_Report_" + Guid.NewGuid().ToString() + ".xlsx";
				string reportFile = Path.Combine(_hostingEnvironment.ContentRootPath, "Reports", fileName);
				using (var fs = new FileStream(reportFile, FileMode.Create, FileAccess.Write))
				{
					stream.WriteTo(fs);
				}
				return fileName;

			}

		}
	}
}
