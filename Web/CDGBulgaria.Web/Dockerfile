FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Web/CDGBulgaria.Web/CDGBulgaria.Web.csproj", "Web/CDGBulgaria.Web/"]
COPY ["Services/CDGBulgaria.Services.Mapping/CDGBulgaria.Services.Mapping.csproj", "Services/CDGBulgaria.Services.Mapping/"]
COPY ["Data/CDGBulgaria.Data/CDGBulgaria.Data.csproj", "Data/CDGBulgaria.Data/"]
COPY ["Data/CSGBulgaria.Data.Models/CDGBulgaria.Data.Models.csproj", "Data/CSGBulgaria.Data.Models/"]
COPY ["Web/CDGBulgaria.Web.InputModels/CDGBulgaria.Web.InputModels.csproj", "Web/CDGBulgaria.Web.InputModels/"]
COPY ["Services/CDGBulgaria.Services.Models/CDGBulgaria.Services.Models.csproj", "Services/CDGBulgaria.Services.Models/"]
COPY ["Services/CDGBulgaria.Services/CDGBulgaria.Services.csproj", "Services/CDGBulgaria.Services/"]
COPY ["Web/CDGBulgaria.Web.ViewModels/CDGBulgaria.Web.ViewModels.csproj", "Web/CDGBulgaria.Web.ViewModels/"]
RUN dotnet restore "Web/CDGBulgaria.Web/CDGBulgaria.Web.csproj"
COPY . .
WORKDIR "/src/Web/CDGBulgaria.Web"
RUN dotnet build "CDGBulgaria.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CDGBulgaria.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CDGBulgaria.Web.dll"]