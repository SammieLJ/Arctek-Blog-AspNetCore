# Arctek-Blog-AspNetCore
Implementation of Arctek Blog tutorial in AspNet Core 3.1

## Prerequisites

You should have installed DotNet Core 3.1 Developer SDK and Runtime. Also check to have Asp.Net Core 3.1 installed. Regardless of OS, code can be executed on Windows OS, MacOS, Linux OS. Just install DotBet Core for your OS and LiteDB.

```
$ dotnet --version
3.1.302
```

Install LiteDB simple SQL database. Like SQLite3 or MongoDB.
```
dotnet add package LiteDB
```
or in NuGet PacketManager
```
Install-Package LiteDB
```
There is also LiteDB Studio:
https://github.com/mbdavid/LiteDB.Studio

More info about LiteDB: https://www.litedb.org/

 
## Arctek tutorial links
Links provided from top do down (Part 1 to 5)
- https://www.arctek.dev/blog/blog-with-csharp-pt1
- https://www.arctek.dev/blog/blog-with-csharp-pt2
- https://www.arctek.dev/blog/blog-with-csharp-pt3
- https://www.arctek.dev/blog/blog-with-csharp-pt4
- https://www.arctek.dev/blog/blog-with-csharp-pt5

## Running code

After you have cloed source code, go to your local folder (in Command Prompt or Terminal) and run:
```
$ dotnet build
$ dotnet run
```

or if you would like to mess with code and see result, use watch:
```
$ dotnet watch run
```

You can skip command line by importing project in Visual Studio.Net 2019 and running application from VS.Net GUI.

Go to http://localhost:5000 or https://localhost:5001 (in browser accept certificate exception, that provider cannot be verified)

For admin dashboard add /admin (http://localhost:5000/admin)

## What is not working?

It is obvious, that author (that is Arctek, not me) didn't finish his project. Still needs some HTML and JavaScript. Admin modul web page doesn't work.

All Web API GET and POST methods do work, so you can use Postman to post, edit, delete posts.

Maybe I will add one of popular JS libraries Angular/React/Vue so that JS lib, can show data and call Web API methods.

## Good example to learn in code

Still it is good example to learn basics of AspNet Core 3.1
I had to do a lot of modifications to the code, that would even compile. (a lot in BlogRepo.cs) There was bug in using LiteDB. There was IOException error, when using LiteDB for more that one time. I fixed with using Connection string (Shared Connections) in appsettings.json There is good example how to read setting string from appsettings.json in BlogRepo.cs, because almost all tutorials were suggesting me, that I need to define Configurator in Startup.cs, then define class model of Connection String in Model folder, use Connection String model and Configurator in every use defined Controller. So that Dependency Injection can happen. :) Way to complicated, what was in Asp.Net MVC 4.7
