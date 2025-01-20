FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build

WORKDIR /src

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

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS publish

WORKDIR /app

COPY --from=build /out .

EXPOSE 5160

ENTRYPOINT ["./UserService.Host"]