
# Название и описание

Тестовое задание для компании Modsen с простыми круд операциями и авторизацией через Jwt

## Стек:
- .NET 7
- ASP.NET Core Web Api
- PostgreSQL
- AutoMapper
- Swagger
> Постгрес был изменен на SQLite для удоства развертывания и тестирования апи

## Для запуска

Клонировать репозиторий

```bash
  git clone https://github.com/egorrrmiller/Modsen
```

Перейти в папку с проектом

```bash
  cd Modsen/Modsen.Web
```

Запустить проект

```bash
  dotnet run --project .\Modsen.Web\Modsen.Web.csproj --launch-profile https
```

После запуска переходим по ссылке на страницу сваггера

<https://localhost:7221/swagger/index.html>


## Операции

Для неавторизованных пользователей
- Регистрация и авторизация

Для авторизованных пользователей
- Добавление новых книг
- Вывод списка всех книг, либо поиск по id/isbn
- Удаление и обновление книги


