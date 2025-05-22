using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCore_sourcecode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Migration CLI support
            if (args.Length >= 2 && (args[0] == "-migrate" || args[0] == "--migrate"))
            {
                if (args[1] == "CreateExamplePosts")
                {
                    Console.WriteLine("Running migration: CreateExamplePosts...");
                    var repo = new Blog.Models.Repos.BlogRepo();
                    repo.CreateExamplePosts();
                    Console.WriteLine("Migration complete.");
                    return;
                }
                else
                {
                    Console.WriteLine("Unknown migration command. Available:");
                    Console.WriteLine("  CreateExamplePosts");
                    return;
                }
            }
            else if (args.Length == 1 && (args[0] == "-migrate" || args[0] == "--migrate"))
            {
                Console.WriteLine("Usage: dotnet run -- -migrate CreateExamplePosts");
                Console.WriteLine("Available migrations:");
                Console.WriteLine("  CreateExamplePosts");
                return;
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
