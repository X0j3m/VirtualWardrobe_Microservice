# Zobacz https://aka.ms/customizecontainer, aby dowiedzieć się, jak dostosować kontener debugowania i jak program Visual Studio używa tego pliku Dockerfile do kompilowania obrazów w celu szybszego debugowania.

# Ten etap jest używany podczas uruchamiania z programu VS w trybie szybkim (domyślnie dla konfiguracji debugowania)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Ten etap służy do kompilowania projektu usługi
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VirtualWardrobe_Clothes/VirtualWardrobe_Clothes.csproj", "VirtualWardrobe_Clothes/"]
RUN dotnet restore "./VirtualWardrobe_Clothes/VirtualWardrobe_Clothes.csproj"
COPY . .
WORKDIR "/src/VirtualWardrobe_Clothes"
RUN dotnet build "./VirtualWardrobe_Clothes.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Ten etap służy do publikowania projektu usługi do skopiowania do etapu końcowego
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VirtualWardrobe_Clothes.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Ten etap jest używany w środowisku produkcyjnym lub w przypadku uruchamiania z programu VS w trybie regularnym (domyślnie, gdy nie jest używana konfiguracja debugowania)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VirtualWardrobe_Clothes.dll"]