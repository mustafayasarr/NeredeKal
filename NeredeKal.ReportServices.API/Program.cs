using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NeredeKal.ReportServices.API.Bootstrapper;
using NeredeKal.ReportServices.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterConfigurationServices(builder.Configuration);
builder.Services.AddCap(options =>
{
	options.UseEntityFramework<ReportDbContext>();
	options.UsePostgreSql(builder.Configuration.GetConnectionString("DevelopmentDbConnection"));
	options.UseRabbitMQ(a =>
	{
		a.HostName = builder.Configuration.GetSection("RabbitMQHost:HostName").Value;
		a.UserName = builder.Configuration.GetSection("RabbitMQHost:UserName").Value;
		a.Password = builder.Configuration.GetSection("RabbitMQHost:Password").Value;
	});
	options.UseDashboard();

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<ReportDbContext>();
	db.Database.Migrate();
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(
		   Path.Combine(builder.Environment.ContentRootPath, "Reports")),
	RequestPath = "/Reports"
});
app.UseFileServer(new FileServerOptions
{
	FileProvider = new PhysicalFileProvider(
		   Path.Combine(builder.Environment.ContentRootPath, "Reports")),
	RequestPath = "/Reports",
	EnableDirectoryBrowsing = true
});
app.MapControllers();

app.Run();
