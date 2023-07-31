@echo off
call dotnet ef migrations add InitialCreate --context AdionFADbContext -o Persistence\Migrations --project ..\AdionFA.Infrastructure.csproj -v
call dotnet ef database update --context AdionFADbContext -p ..\AdionFA.Infrastructure.csproj -v