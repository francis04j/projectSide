FROM mcr.microsoft.com/dotnet/sdk:5.0.400-alpine3.13-amd64 AS build-env
WORKDIR /app

COPY *.sln ./
COPY ./Api/*.csproj ./Api/
COPY ./Application/*.csproj ./Application/
COPY ./Domain/*.csproj ./Domain/
COPY ./ApplicationTest/*.csproj ./ApplicationTest/
COPY ./Frontend/*.csproj ./Frontend/
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out
# runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0.9-alpine3.13-amd64
WORKDIR /app
COPY --from=build-env /app/out .

# you can override this using -e ASPNETCORE_ENVIRONMENT={new_environment} in docker run command
ENV ASPNETCORE_ENVIRONMENT Development
ENTRYPOINT ["dotnet", "Api.dll"]