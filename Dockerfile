# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# ����������Ŀ�ļ��е�������
COPY PackageTrackingAPI.BLL/ PackageTrackingAPI.BLL/
COPY PackageTrackingAPI.DAL/ PackageTrackingAPI.DAL/
COPY PackageTrackingAPI.Mapping/ PackageTrackingAPI.Mapping/
COPY PackageTrackingAPI.Models/ PackageTrackingAPI.Models/
COPY PackageTrackingAPI/ PackageTrackingAPI/

# �л�������Ŀ�ļ���
WORKDIR /src/PackageTrackingAPI

# ��ԭ����
RUN dotnet restore

# ������Ŀ�� /app/out
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "PackageTrackingAPI.dll"]
