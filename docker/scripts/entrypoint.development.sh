#!/bin/bash
source /scripts/commons-functions-display.sh

if dotnet restore ${MYSERVICE_PROJECT_FILE_CSPROJ}; then
	print_messange_log "ok" "Restore project ${MYSERVICE_PROJECT_FILE_CSPROJ}"
else
	print_messange_log "error" "Restore project ${MYSERVICE_PROJECT_FILE_CSPROJ}"
fi

if dotnet publish ${MYSERVICE_PROJECT_FILE_CSPROJ} -o ./publish;  then
	print_messange_log "ok" "Publish project ${MYSERVICE_PROJECT_FILE_CSPROJ}"
else
	print_messange_log "error" "Publish project ${MYSERVICE_PROJECT_FILE_CSPROJ}"
fi

if dotnet user-secrets set "CrontabRegistryDatabaseOptions:ConnectionString" "${DOTENT_APP_CrontabRegistryDatabaseOptions__ConnectionString}" \
	--project ${MYSERVICE_PROJECT_FILE_CSPROJ} > /dev/null; then
	print_messange_log "ok" "Set secrets for CrontabRegistryDatabaseOptions:ConnectionString"
else
	print_messange_log "error" "Set secrets for CrontabRegistryDatabaseOptions:ConnectionString"
fi

dotnet run --no-launch-profile --project "${MYSERVICE_PROJECT_START_DIRECTORY}";
