# MittoAPI

- Set DB parms in the Mitto.App2Sms\appsettings.json to hit an existing mysql server instance. System will automatically create DB and Schema.
- Allowed origin for CORS is set in appsettings.json, variable FEURL sets front end url. Currently set to hit Frontend Docker Container URL.

Steps to run API:

- Navigate to folder Mitto.App2Sms.Web
- dotnet restore
- dotnet publish -c Release -o out
- dotnet out\Mitto.App2Sms.Web.dll

Dockerization of the API is not finished. Please use non-docker deploy for testing purposes.

Things to be added:

- Turn off sql_mode=only_full_group_by" in the docker-compose for the mysql docker image. Due to this issue API statistics end point do not return query result.
- Frontend project should be added to docker-compose

Steps to spin docker container:

- Navigate to root folder
- docker-compose up