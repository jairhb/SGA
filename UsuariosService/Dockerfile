# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./UsuariosService/*.csproj ./UsuariosService/
COPY ./Inscripciones.Common/*.csproj ./Inscripciones.Common/
RUN dotnet restore ./UsuariosService/UsuariosService.csproj

COPY . .
WORKDIR /src/UsuariosService
RUN dotnet publish UsuariosService.csproj -c Release -o /app

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

EXPOSE 8080
ENTRYPOINT ["dotnet", "UsuariosService.dll"]
