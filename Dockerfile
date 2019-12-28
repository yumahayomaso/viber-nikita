FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ViberApi/ViberApi.csproj", "ViberApi/"]
RUN dotnet restore "ViberApi/ViberApi.csproj"
COPY . .
WORKDIR "/src/ViberApi"
RUN dotnet build "ViberApi/ViberApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ViberApi/ViberApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ViberApi.dll"]