#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["BankDemo.AccountService/BankDemo.AccountService.csproj", "BankDemo.AccountService/"]
#COPY ["BankDemo.DataRepository/BankDemo.DataRepository.csproj", "BankDemo.DataRepository/"]
#COPY ["BankDemo.DataModel/BankDemo.DataModel.csproj", "BankDemo.DataModel/"]
#COPY ["BankDemo.Common/BankDemo.Common.csproj", "BankDemo.Common/"]
#RUN dotnet restore "BankDemo.AccountService/BankDemo.AccountService.csproj"
#COPY . .
#WORKDIR "/src/BankDemo.AccountService"
#RUN dotnet build "BankDemo.AccountService.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "BankDemo.AccountService.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "BankDemo.AccountService.dll"]


FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
USER ContainerAdministrator
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
# COPY ["FinServ/FinServ.csproj", "FinServ/"]
# RUN dotnet restore "FinServ/FinServ.csproj"
# COPY . .
# WORKDIR "/src/FinServ"
# RUN dotnet build "FinServ.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "FinServ.csproj" -c Release -o /app/publish

# FROM ba




# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "FinServ.dll"]

# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BankDemo.AccountService.dll"]