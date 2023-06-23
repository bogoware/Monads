FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["sample/FluentValidationSample/FluentValidationSample.csproj", "sample/FluentValidationSample/"]
RUN dotnet restore "sample/FluentValidationSample/FluentValidationSample.csproj"
COPY . .
WORKDIR "/src/sample/FluentValidationSample"
RUN dotnet build "FluentValidationSample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluentValidationSample.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluentValidationSample.dll"]
