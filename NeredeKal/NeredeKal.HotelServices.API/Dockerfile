#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NeredeKal.HotelServices.API/NeredeKal.HotelServices.API.csproj", "NeredeKal.HotelServices.API/"]
COPY ["NeredeKal.HotelServices.Core/NeredeKal.HotelServices.Core.csproj", "NeredeKal.HotelServices.Core/"]
COPY ["NeredeKal.HotelServices.Domain/NeredeKal.HotelServices.Domain.csproj", "NeredeKal.HotelServices.Domain/"]
COPY ["NeredeKal.HotelServices.Infrastructure/NeredeKal.HotelServices.Infrastructure.csproj", "NeredeKal.HotelServices.Infrastructure/"]
RUN dotnet restore "NeredeKal.HotelServices.API/NeredeKal.HotelServices.API.csproj"
COPY . .
WORKDIR "/src/NeredeKal.HotelServices.API"
RUN dotnet build "NeredeKal.HotelServices.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NeredeKal.HotelServices.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NeredeKal.HotelServices.API.dll"]