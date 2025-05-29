# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo el contenido del proyecto (incluye .sln y proyectos compartidos)
COPY . .

# Restaurar dependencias desde la solución
RUN dotnet restore ./docker.sln

# Publicar solo el microservicio ProgramasService
RUN dotnet publish ./ProgramasService/ProgramasService.csproj -c Release -o /out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

EXPOSE 5001
ENTRYPOINT ["dotnet", "ProgramasService.dll"]

