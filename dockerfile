FROM node:14 AS build-stage
WORKDIR /app
COPY client/package*.json ./
RUN npm install
COPY client/ ./
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /aspnetapp
COPY --from=build-stage /app/build ./wwwroot
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out --verbosity normal

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /aspnetapp
COPY --from=build /aspnetapp/out ./
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "FitHub.dll"]
