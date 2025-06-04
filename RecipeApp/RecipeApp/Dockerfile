FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY ./ .

EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "RecipeApp.dll"]