FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Shortnr.Api/Shortnr.Api.csproj", "Shortnr.Api/"]
RUN dotnet restore "Shortnr.Api/Shortnr.Api.csproj"
COPY . .
WORKDIR "/src/Shortnr.Api"
RUN dotnet build "Shortnr.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Shortnr.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Shortnr.Api.dll"]