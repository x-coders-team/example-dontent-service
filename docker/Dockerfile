# ##################################################### #
# This is Dockerfile use for production                 #
# ##################################################### #
ARG dotnet_version=6.0
ARG project_file_cfproj="./src/CrontabRegistry/Application/Application.csproj"
ARG project_entrypoint_dll="/application/Application.dll"
ARG app_dotnet_environment_name="Production"

FROM mcr.microsoft.com/dotnet/sdk:$dotnet_version as build
	ARG project_file_cfproj
	ARG project_start_directory
	ARG app_dotnet_environment_name

	ENV ASPNETCORE_URLS=http://+:4500
	ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
	ENV ASPNETCORE_ENVIRONMENT=${app_dotnet_environment_name}
	ENV MYSERVICE_PROJECT_FILE_CSPROJ="${project_file_cfproj}"

	WORKDIR /application
	COPY . .

	RUN dotnet restore ${MYSERVICE_PROJECT_FILE_CSPROJ}; \
		dotnet build ${MYSERVICE_PROJECT_FILE_CSPROJ}; \
		dotnet publish ${MYSERVICE_PROJECT_FILE_CSPROJ} -o ./publish;


FROM mcr.microsoft.com/dotnet/aspnet:$dotnet_version as runtime
	ARG project_entrypoint_dll
	ARG app_dotnet_environment_name

	ENV ASPNETCORE_URLS=http://+:4500
	ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
	ENV ASPNETCORE_ENVIRONMENT=${app_dotnet_environment_name}
	ENV MYSERVICE_PROJECT_ENTRYPOINT_DLL=${project_entrypoint_dll}

	WORKDIR /application

	COPY --from=build /application/publish /application

	EXPOSE 4500
	ENTRYPOINT echo $MYSERVICE_PROJECT_ENTRYPOINT_DLL; dotnet $MYSERVICE_PROJECT_ENTRYPOINT_DLL;
