# Arctek-Blog-AspNetCore
Implementation of Arctek Blog tutorial in AspNet Core 3.1

## Prerequisites

Originally made for DotNet Core 3.1 Runtime now is **updated to DotNet 8.0**. Blog example uses DotNet built in embeded Kestrel web server and can be executed on Windows OS, MacOS, Linux OS. Make sure that you have installed DotBet 8.0 or higher and LiteDB.

```
$ dotnet --version
8.0.7
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

You can find and open databse with LiteDB.exe, database is located in subfolder data, named Blog.db

LiteDB SQL is fine for returning results in JSON format. Be wary that SQL syntax is little bit different that usual SQL statements. For example, simbol $ is used instead of * and id starts as _id

```
select $ from posts where _id = 9;
```
 
## Arctek original tutorial links
Links provided from top do down (Part 1 to 5)
- https://www.arctek.dev/blog/blog-with-csharp-pt1
- https://www.arctek.dev/blog/blog-with-csharp-pt2
- https://www.arctek.dev/blog/blog-with-csharp-pt3
- https://www.arctek.dev/blog/blog-with-csharp-pt4
- https://www.arctek.dev/blog/blog-with-csharp-pt5

## Running code

After you have cloned source code, go to your local folder (in Command Prompt or Terminal) and run:
```
$ dotnet build
$ dotnet run
```

or if you would like to try new stuff with code and see result, use watch (hot reload):
```
$ dotnet watch run
```

You can skip command line by importing project in Visual Studio.Net 2022 and running application from VS.Net GUI.

Go to http://localhost:5000 or https://localhost:5001 (in browser accept certificate exception, that provider cannot be verified)

For admin dashboard add /admin to url (http://localhost:5000/admin) and you will be able to manage (edit, delete), create new Post.

## Migrate DB with example posts

Migration will delete all previous posts and fill with example posts.
```
$ dotnet run -- -migrate CreateExamplePosts
```

## Validation and Sanitization of NoSQL data

Added validation and sanitization, when adding NoSQL data.

**User roles were not implemented!**

## What is not working?

It is obvious, that author (that is Arctek, not me) didn't finish his project. Still needed some HTML and JavaScript. Admin modul web page doesn't work. All Web API GET and POST methods do work, so you can use Postman to post, edit, delete posts.

That was in **2022**

Fast forward to **2025**, and I have updated code base with missing features. Added Vue3.js to frontend (no Node.js needed for running) and Bootstrap (works nicely with Razor templates) Copilot didn't advice me to use ShadCN

## Good example to learn in code

Still it is good example to learn basics of AspNet Core

I had to do a lot of modifications to the code, that would even compile. (a lot in BlogRepo.cs) But still nice example for beginners to take a look at this Blog application and see how it's build using modern web techniques. Added Posts pagination and re-hauled Admin dashboard. If you have any suggestions or would like to contribute to code base, feel free, it is open source.
