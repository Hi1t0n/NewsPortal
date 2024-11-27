FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /src

COPY ["UserService/UserService.sln", "UserService/"]
COPY ["UserService/UserService.Domain/UserService.Domain.csproj", "UserService/UserService.Domain/"]
COPY ["UserService/UserService.Infrastructure/UserService.Infrastructure.csproj", "UserService/UserService.Infrastructure/"]
COPY ["UserService/UserService.Host/UserService.Host.csproj", "UserService/UserService.Host/"]
COPY ["SharedLibrarySolution/NewsPortal.SharedLibrary/NewsPortal.SharedLibrary.csproj", "SharedLibrarySolution/NewsPortal.SharedLibrary/"]

RUN dotnet restore "UserService/UserService.sln"

COPY . .

WORKDIR "/src/UserService/UserService.Host"

RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app

COPY --from=build /app ./

EXPOSE 5229
ENTRYPOINT ["dotnet", "UserService.Host.dll"]
