# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Restore
COPY ["src/TaskBlaster/TaskBlaster.csproj", "TaskBlaster/"]
RUN dotnet restore 'TaskBlaster/TaskBlaster.csproj'

# Build
COPY ["src/TaskBlaster", "TaskBlaster/"]
WORKDIR /src/TaskBlaster
RUN dotnet build 'TaskBlaster.csproj' -c Release -o /app/build



# Stage 2: Publish Stage
FROM build as publish
RUN dotnet publish 'TaskBlaster.csproj' -c Release -o /app/publish



# Stage 3: Run Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=80
EXPOSE 80
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "TaskBlaster.dll" ]