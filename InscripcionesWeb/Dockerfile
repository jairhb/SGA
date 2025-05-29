# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY InscripcionesWeb/InscripcionesWeb.csproj InscripcionesWeb/
COPY Inscripciones.Common/Inscripciones.Common.csproj Inscripciones.Common/

RUN dotnet restore InscripcionesWeb/InscripcionesWeb.csproj

COPY InscripcionesWeb/ InscripcionesWeb/
COPY Inscripciones.Common/ Inscripciones.Common/

WORKDIR /src/InscripcionesWeb
RUN dotnet publish -c Release -o /app

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

EXPOSE 8080
ENTRYPOINT ["dotnet", "InscripcionesWeb.dll"]



