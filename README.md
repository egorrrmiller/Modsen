
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


## Примечания
1. Из за требований постгреса к указанию тайм-зон при добавлении записей в таблицу была написана такая логика для дефолтного значения при добавлении записи в БД. <br/>
После перехода на SQLite надобности в UtcNow.AddHours(3) нет, однако из за того, что SQLite используется чтобы просто запустить проект без установленного постгреса, убирать я не буду. <br/>
![image](https://user-images.githubusercontent.com/44502536/233639053-3041f862-769d-4fc5-9536-de4241fcb8f2.png) <br/>
В других местах использовался Datetime.Now т.к там не было действий с добавлением записи в БД и указанием времени

2. Регистрация и авторизация была написана без хеширования паролей для упрощения реализации в рамках тестового задания.

3. Автомаппер в основном использовался для сущностей книг из за количества свойств, которые бы пришлось маппить в ручную без него. Для этого были сделаны методы расширения, дабы каждый раз не писать одно и то же. <br/>
Для сущностей юзера от автомаппера решил воздержаться в виду малого количества свойств и количества маппов, которые бы пришлось делать

4. При добавлении новой книги, если найдется книга с таким же ISBN, то выкинет BookExistsException. <br/>
Сделано было для демонстрации миддлвари глобальной обработки ошибок и кастомных исключений. <br/>
В случае возникновения других исключений, они также будут обрабатываться там.

5. Старался придерживаться трехслойной архитектуры. Думаю получилось вполне нормально.

6. Просто покажу как солюшн выглядит в IDE Rider JetBrains. <br/>
![image](https://user-images.githubusercontent.com/44502536/233641501-13d00aa6-fa55-4058-a03b-8694b0df309a.png)

