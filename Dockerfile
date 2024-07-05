FROM mcr.microsoft.com/dotnet/sdk:latest AS build
EXPOSE 8080
EXPOSE 8081

WORKDIR /src

COPY TechNationFinanceiroClient.csproj .
RUN dotnet restore TechNationFinanceiroClient.csproj

COPY . .
RUN dotnet build TechNationFinanceiroClient.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish TechNationFinanceiroClient.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf