# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything into the container's /src directory
COPY . .

# Restore dependencies for the CatThreadService project
RUN dotnet restore "CatThreadService.csproj"

# Build the app
RUN dotnet build "CatThreadService.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "CatThreadService.csproj" -c Release -o /app/publish

# Final stage - build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatThreadService.dll"]
