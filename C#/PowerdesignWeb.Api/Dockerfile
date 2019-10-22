FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["PowerdesignWeb.Api/PowerdesignWeb.Api.csproj", "PowerdesignWeb.Api/"]
RUN dotnet restore "PowerdesignWeb.Api/PowerdesignWeb.Api.csproj"
COPY . .
WORKDIR "/src/PowerdesignWeb.Api"
RUN dotnet build "PowerdesignWeb.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PowerdesignWeb.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PowerdesignWeb.Api.dll"]