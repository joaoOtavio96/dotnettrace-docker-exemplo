#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
RUN dotnet tool install dotnet-trace --tool-path /tools --create-manifest-if-needed
WORKDIR /src
COPY ["DotnetTraceDocker.Api/DotnetTraceDocker.Api.csproj", "DotnetTraceDocker.Api/"]
RUN dotnet restore "DotnetTraceDocker.Api/DotnetTraceDocker.Api.csproj"
COPY . .
WORKDIR "/src/DotnetTraceDocker.Api"
RUN dotnet build "DotnetTraceDocker.Api.csproj" -c Release -o /app/build

FROM build AS publish

ENV DOTNET_PerfMapEnabled=1

RUN dotnet publish "DotnetTraceDocker.Api.csproj" -c Release -o /app/publish --self-contained

FROM base AS final
WORKDIR /app
COPY --from=build /tools /tools
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotnetTraceDocker.Api.dll"]