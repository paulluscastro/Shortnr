FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Shortnr/Shortnr.csproj Shortnr/
RUN dotnet restore Shortnr/Shortnr.csproj
COPY . .
WORKDIR /src/Shortnr
RUN dotnet build Shortnr.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Shortnr.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Shortnr.dll"]
