FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS runtime

WORKDIR /app

COPY --from=build /out ./

ENV WAAS_CONFIG_KEY="<INSERT HERE>"
ENV PROJECT_ACCESS_KEY="<INSERT HERE>"
ENV EOA_PRIVATE_KEY="<INSERT HERE>"

EXPOSE 80

ENTRYPOINT ["dotnet", "SequenceDotNetBoilerplate.dll"]