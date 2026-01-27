FROM mcr.microsoft.com/dotnet/sdk:10 AS build-env
WORKDIR /App
COPY . ./
RUN dotnet restore \
	&& dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "out.dll"]
