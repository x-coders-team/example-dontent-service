# Crontab registry


# Index of content
* [Project directory structure](#project-directory-structure)
* [Code standards](#code-starndards)
* [Testing](#testing)
* [Docker](#docker)
  * [Build image](#build-image)
    * [Build image for Production](#build-image-for-production)
    * [Build image for Development](#build-image-for-development)
  * [Container create](#container-create)
    * [Container create for Production](#container-create-for-production)
    * [Container create for Development](#container-create-for-development)
  * [Container start](#container-start)
  * [Container restart](#container-restart)
  * [Container destroy](#container-destroy)
  * [Container Stop & Destroy](#container-stop--destroy)
  * [Container run bash](#container-run-bash)
  * [Logs for container](#logs-for-container)
  * [Set secret on container](#set-secret-on-container)
  * [Full commands](#full-commands)
    * [Rebuild for development env](#rebuild-for-development-env)
  * [Environments variables](#environments-variables)
  * [Secrets in project](#secrets-in-project)
* [How to setup local environment](#how-to-setup-local-environment)


---


# Project directory structure
In this section outlines the project's directory structure and its key components:

+ __root__ - Root directory
  + __.github__ - Directory containing configurations and workflows related to GitHub
    + __workflows__ - Directory containing files related to GitHub Actions
  + __doc__ - Directory containing additional documentation (.md files) for the project
  + __docker__ - Directory containing configurations, additional files needed to build the image, and Dockerfile
    + __scripts__ - Directory containing shell scripts required to build the image or run the container (e.g., entrypoint)
  + __src__ - Directory containing the source code for the service or application
  + __tests__ - Directory containing unit and integration tests necessary for verification



# Code starndards
This section outlines the code standards followed in the repository, covering topics such as naming rules, naming conventions, and general code conventions. For more in-depth information, please refer to the dedicated document on code standards, accessible [here](doc/CODE-STANDARDS.md).


# Testing
Almost all public methods should be covered by unit tests, and endpoints must be tested by integration tests. All tests related to unit testing should be placed in `./test/Unit`, and those related to integration testing should be placed in `./test/Integration`. Before creating a Docker image ready to deploy on a specific server, the pipeline should run tests before the building process. This prevents deploying an unstable service or application. You can run unit tests locally via an IDE or manually using the command line:

__Run all tests in repository__
```shell
dotnet test
```

__Run only integration tests__
```shell
dotnet test ./tests/Integration/Integration.csproj
```

__Run only unit tests__
```shell
dotnet test ./tests/Integration/Unit.csproj
```


# Docker 
This section provides insights into fundamental Docker commands for efficient container management.

## Build image
Utilize a Dockerfile to define application configurations and dependencies, creating portable images for seamless deployment across diverse environments.

### Build image for Production
```shell
docker image build -f ./docker/Dockerfile -t crontab-registry:production .;
```


### Build image for Development
```shell
docker image build -f ./docker/Dockerfile.development -t crontab-registry:development .;
```


## Container create
This section details the usage of the docker container create command with specific parameters for creating a container named "crontab-registry."

### Container create for Production
```shell
docker container create -it -p 18181:4500 --env-file=./.env -l com.salamonrafal.repository="crontab-registry" -l com.salamonrafal.environment="production" --name "crontab-registry" --restart always crontab-registry:production
```

### Container create for Development
```shell
docker container create -it -p 18181:4500 --env-file=./.env -l com.salamonrafal.repository="crontab-registry" -l com.salamonrafal.environment="development" --name "crontab-registry" --restart always crontab-registry:development
```


## Container start
This section outlines the usage of the docker container start command, focusing on initiating the "crontab-registry" container.

```shell
docker container start crontab-registry
```


## Container restart
This section provides a concise overview of utilizing the docker container restart command, emphasizing the restart action for the "crontab-registry" container.

```shell
docker container restart crontab-registry
```


## Container stop
This section provides a brief overview of using the docker container stop command, focusing on halting the "crontab-registry" container.

```shell
docker container stop crontab-registry
```


## Container destroy
This section offers a brief overview of using the docker container rm command, focusing on the removal of the "crontab-registry" container.

```shell
docker container rm crontab-registry
```

### Container Stop & Destroy
This section provides a concise overview of using the combination of docker container stop and docker container rm commands to halt and remove the "crontab-registry" container.

```shell
docker container stop crontab-registry && docker container rm crontab-registry
```


## Container run bash
This section provides a brief overview of using the docker exec command with the options -it to access the interactive shell of the "crontab-registry" container.
```shell
docker exec -it crontab-registry bash
```

## Logs for container:
Gain insights into container behavior by leveraging the docker logs command. This tool allows you to retrieve real-time or historical log data, aiding in troubleshooting, debugging, and performance analysis. Understand how to efficiently monitor and interpret the output generated by running containers.

```shell
docker logs crontab-registry
```

## Set secret on container
This section provides a brief overview of the command sequence designed to update configuration settings within the "crontab-registry" container using Docker commands.

```shell
( source '.env' && docker exec -it crontab-registry \
  dotnet user-secrets set "CrontabRegistryDatabaseOptions:ConnectionString" "${CRONTAB_REGISTRY_MONGODB_CS_SECRETS}" \
  --project ./src/CrontabRegistry/Application/Application.csproj && docker container restart crontab-registry );
```

This sequence is designed to dynamically update the MongoDB connection string within the "crontab-registry" container, ensuring seamless integration of configuration changes without disrupting the overall application functionality.

## Full commands

### Rebuild for development env
This section details a command sequence designed to stop and remove the "crontab-registry" container, followed by rebuilding the container with updated configurations.

```shell
docker container stop crontab-registry && docker container rm crontab-registry; \
  (source '.env' \
    && docker image build -f ./docker/Dockerfile.development -t crontab-registry:development . \
    && docker container create -it -p 18181:4500 --env-file=./.env -l com.salamonrafal.repository="crontab-registry" -l com.salamonrafal.environment="development" --name "crontab-registry" --restart always crontab-registry:development \
    && docker container start crontab-registry \
  );
```

This sequence provides a comprehensive approach to update, rebuild, and restart the "crontab-registry" container with the latest configurations.


# Environments variables
* DOTENT_APP_CrontabRegistryDatabaseOptions__ConnectionString - You can pass to your application connection string to MongoDb
* ASPNETCORE_ENVIRONMENT - You can set for which environment you deploy code. Options: Development, Production


# Secrets in project 
Never commit variables such as passwords or other sensitive data to the repository. 
You can handle such data in several ways:
* Through environment variables (used in this project for deploying the application to a server), and you can read more about them [here](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0#non-prefixed-environment-variables)
* Using user-secrets in dotnet, which is very useful during the development and testing phases. More information can be found [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=linux)
* By utilizing an application like Vault or a similar tool (the best solution where sensitive data is not stored directly without encryption). You can learn more [here](https://developer.hashicorp.com/vault/docs/what-is-vault)


# How to setup local environment
This section should describe how to set up your local computer to run the service or application on it.

If you need to test your service without the integration process, you can simply use your IDE or a shell command like:

```shell
dotnet run --no-launch-profile --project src/CrontabRegistry/Application/
```

But before running locally, you have to prepare secrets for access to the database. You can do this in your IDE or use the shell command below:

```shell
dotnet user-secrets set "CrontabRegistryDatabaseOptions:ConnectionString" "<CONNECTION_STRING_TO_MONGO_DB>" --project src/CrontabRegistry/Application/Application.csproj
```

Where _**<CONNECTION_STRING_TO_MONGO_DB>**_ is a string containing the address and authentication for the MongoDB server, like: `mongodb://demouser:demopassword@localhost:27017/`