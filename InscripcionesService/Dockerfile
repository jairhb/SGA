# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de solución y proyectos
COPY *.sln ./
COPY Inscripciones.Common/Inscripciones.Common.csproj Inscripciones.Common/
COPY InscripcionesService/InscripcionesService.csproj InscripcionesService/

# Restaurar dependencias
RUN dotnet restore InscripcionesService/InscripcionesService.csproj

# Copiar todo el código fuente del proyecto
COPY Inscripciones.Common/ Inscripciones.Common/
COPY InscripcionesService/ InscripcionesService/

# Publicar
WORKDIR /src/InscripcionesService
RUN dotnet publish -c Release -o /app

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

EXPOSE 8080
ENTRYPOINT ["dotnet", "InscripcionesService.dll"]


