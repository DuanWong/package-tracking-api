# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# 复制所有项目文件夹到容器里
COPY PackageTrackingAPI.BLL/ PackageTrackingAPI.BLL/
COPY PackageTrackingAPI.DAL/ PackageTrackingAPI.DAL/
COPY PackageTrackingAPI.Mapping/ PackageTrackingAPI.Mapping/
COPY PackageTrackingAPI.Models/ PackageTrackingAPI.Models/
COPY PackageTrackingAPI/ PackageTrackingAPI/

# 切换到主项目文件夹
WORKDIR /src/PackageTrackingAPI

# 还原依赖
RUN dotnet restore

# 发布项目到 /app/out
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "PackageTrackingAPI.dll"]
