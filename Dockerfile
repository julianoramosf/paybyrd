FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /Paybyrd.Proof.WebApi
COPY . .
RUN dotnet restore
RUN dotnet publish -o /app/paybyrd

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/paybyrd /app
EXPOSE 8001
ENTRYPOINT [ "dotnet", "/app/Paybyrd.Proof.WebApi.dll" ]