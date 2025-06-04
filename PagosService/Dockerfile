# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto primero (optimiza cache de dependencias)
COPY ./PagosService/*.csproj ./PagosService/
COPY ./Inscripciones.Common/*.csproj ./Inscripciones.Common/

# Restaurar dependencias
WORKDIR /src/PagosService
RUN dotnet restore

# Copiar todo el código fuente
WORKDIR /src
COPY . .

# Publicar la aplicación
WORKDIR /src/PagosService
RUN dotnet publish -c Release -o /app

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

EXPOSE 8080
ENTRYPOINT ["dotnet", "PagosService.dll"]
