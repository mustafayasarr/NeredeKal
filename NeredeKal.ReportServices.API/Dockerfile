#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NeredeKal.ReportServices.API/NeredeKal.ReportServices.API.csproj", "NeredeKal.ReportServices.API/"]
COPY ["NeredeKal.ReportServices.Core/NeredeKal.ReportServices.Core.csproj", "NeredeKal.ReportServices.Core/"]
COPY ["NeredeKal.ReportServices.Domain/NeredeKal.ReportServices.Domain.csproj", "NeredeKal.ReportServices.Domain/"]
COPY ["NeredeKal.ReportServices.Infrastructure/NeredeKal.ReportServices.Infrastructure.csproj", "NeredeKal.ReportServices.Infrastructure/"]
RUN dotnet restore "NeredeKal.ReportServices.API/NeredeKal.ReportServices.API.csproj"
COPY . .
WORKDIR "/src/NeredeKal.ReportServices.API"
RUN dotnet build "NeredeKal.ReportServices.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NeredeKal.ReportServices.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NeredeKal.ReportServices.API.dll"]