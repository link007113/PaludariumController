#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app

ENV ASPNETCORE_URLS=http://*:7329
ENV ASPNETCORE_ENVIRONMENT=”production”

ENV InstanceName=Docker
ENV Device=Mock
ENV Port=Com05
ENV UseSwagger=true

EXPOSE 7329


FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["PaludariumController.WebApi/PaludariumController.WebApi.csproj", "PaludariumController.WebApi/"]
COPY ["PaludariumController.InfraStructure/PaludariumController.InfraStructure.csproj", "PaludariumController.InfraStructure/"]
COPY ["PaludariumController.Core/PaludariumController.Core.csproj", "PaludariumController.Core/"]
RUN dotnet restore "PaludariumController.WebApi/PaludariumController.WebApi.csproj"
COPY . .
WORKDIR "/src/PaludariumController.WebApi"
RUN dotnet build "PaludariumController.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaludariumController.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaludariumController.WebApi.dll"]