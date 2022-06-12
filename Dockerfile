FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . ./

RUN dotnet restore ./VideoHostingBackend.sln
RUN dotnet publish -c Release -o build

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/build ./
EXPOSE 80
ENTRYPOINT ["dotnet", "VideoHostingBackend.dll"]