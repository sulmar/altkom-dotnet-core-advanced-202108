#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Altkom.Shop.WebApi/Altkom.Shop.WebApi.csproj", "Altkom.Shop.WebApi/"]
COPY ["Altkom.Shop.Models/Altkom.Shop.Models.csproj", "Altkom.Shop.Models/"]
COPY ["Altkom.Shop.FakeServices/Altkom.Shop.FakeServices.csproj", "Altkom.Shop.FakeServices/"]
COPY ["Altkom.Shop.IServices/Altkom.Shop.IServices.csproj", "Altkom.Shop.IServices/"]
COPY ["Altkom.Shop.Fakers/Altkom.Shop.Fakers.csproj", "Altkom.Shop.Fakers/"]
RUN dotnet restore "Altkom.Shop.WebApi/Altkom.Shop.WebApi.csproj"
COPY . .
WORKDIR "/src/Altkom.Shop.WebApi"
RUN dotnet build "Altkom.Shop.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Altkom.Shop.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Altkom.Shop.WebApi.dll"]