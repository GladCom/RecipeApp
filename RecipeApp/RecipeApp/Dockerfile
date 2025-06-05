FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY ./ .

EXPOSE 5128

ENTRYPOINT ["dotnet", "RecipeApp.dll"]