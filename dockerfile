FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app
EXPOSE 80

# copy everything and build the project
COPY . ./
RUN dotnet restore Mitto.App2Sms.Web/*.csproj
RUN dotnet publish Mitto.App2Sms.Web/*.csproj -c Release -o out

# build runtime image
FROM microsoft/dotnet:2.1.1-runtime
WORKDIR /app
COPY --from=build-env /app/Mitto.App2Sms.Web/out ./
ENTRYPOINT ["dotnet", "Mitto.App2Sms.Web.dll"]