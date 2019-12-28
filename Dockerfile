FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim 
WORKDIR /app
COPY . .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ViberApi.dll