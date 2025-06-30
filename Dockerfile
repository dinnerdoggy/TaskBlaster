# Use official .NET 8 runtime as base image for final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["TaskBlaster/TaskBlaster.csproj", "TaskBlaster/"]
WORKDIR /src/TaskBlaster

# Restore dependencies
RUN dotnet restore "TaskBlaster.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "TaskBlaster.csproj" -c Release -o /app/build
RUN dotnet publish "TaskBlaster.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TaskBlaster.dll"]
