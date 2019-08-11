# MittoAPI

- Set DB parms in the Mitto.App2Sms\appsettings.json to hit an existing mysql server instance. System will automatically create DB and Schema.
- Allowed origin for CORS is set in appsettings.json, variable FEURL sets front end url. Currently set to hit Frontend Docker Container URL.

Steps to run API:

- Navigate to folder Mitto.App2Sms.Web
- dotnet restore
- dotnet publish -c Release -o out
- dotnet out\Mitto.App2Sms.Web.dll

Dockerfile is present, but running container is not working at this moment.

Steps to spin docker container:

- Navigate to root folder
- docker image build -t mittoapi .
- docker run -p 5000:80 --rm mittoapi