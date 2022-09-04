#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["dotnet6-api-sample/dotnet6-api-sample.csproj", "dotnet6-api-sample/"]
RUN dotnet restore "dotnet6-api-sample/dotnet6-api-sample.csproj"
COPY . .
WORKDIR "/src/dotnet6-api-sample"
RUN dotnet build "dotnet6-api-sample.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/dotnet6-api-sample"
RUN dotnet publish "dotnet6-api-sample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dotnet6-api-sample.dll"]