FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY App2Sms.sln ./
COPY Mitto.App2Sms.ServiceModel/*.csproj ./Mitto.App2Sms.ServiceModel/
COPY Mitto.App2Sms.BussinesLogic/*.csproj ./Mitto.App2Sms.BussinesLogic/
COPY Mitto.App2Sms.ServiceInterface/*.csproj ./Mitto.App2Sms.ServiceInterface/
COPY Mitto.App2Sms/*.csproj ./Mitto.App2Sms/


RUN dotnet restore
COPY . .
WORKDIR /src/Mitto.App2Sms.ServiceModel
RUN dotnet build -c Release -o /app

WORKDIR /src/Mitto.App2Sms.BussinesLogic
RUN dotnet build -c Release -o /app

WORKDIR /src/Mitto.App2Sms.ServiceInterface
RUN dotnet build -c Release -o /app

WORKDIR /src/Mitto.App2Sms
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Mitto.App2Sms.Web.dll"]