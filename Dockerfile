# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["TaskBlaster/TaskBlaster.csproj", "TaskBlaster/"]
RUN dotnet restore "TaskBlaster/TaskBlaster.csproj"

COPY ["TaskBlaster", "TaskBlaster/"]
WORKDIR /src/TaskBlaster
RUN dotnet build "TaskBlaster.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build as publish
RUN dotnet publish "TaskBlaster.csproj" -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=80
EXPOSE 80
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskBlaster.dll"]
