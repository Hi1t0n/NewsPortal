FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /app

COPY *.sln .

COPY UserService.Host/*.csproj ./UserService.Host/
COPY UserService.Domain/*.csproj ./UserService.Domain/
COPY UserService.Infrastructure/*.csproj ./UserService.Infrastructure/

RUN dotnet restore -r linux-musl-x64

COPY UserService.Host/. ./UserService.Host/
COPY UserService.Domain/. ./UserService.Domain/
COPY UserService.Infrastructure/. ./UserService.Infrastructure/

RUN dotnet build 

RUN dotnet publish -c Release -o /out -r linux-musl-x64

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS publish

WORKDIR /app

COPY --from=build /out .

EXPOSE 5229 
ENTRYPOINT ["./UserService.Host"]
