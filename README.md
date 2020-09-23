# Сайт туроператора
## Установка и запуск
```
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```
## Удаление миграций
```
dotnet ef migrations remove
```