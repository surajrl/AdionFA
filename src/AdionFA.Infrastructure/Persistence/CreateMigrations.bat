@echo off
dotnet ef migrations add InitialCreate --context AdionFADbContext -o Persistence\Migrations --project ..\AdionFA.Infrastructure.csproj -v