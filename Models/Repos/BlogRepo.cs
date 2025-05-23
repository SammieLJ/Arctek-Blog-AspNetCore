
using System.IO;
using System;
using LiteDB;
using System.Collections.Generic; // Required for using collections such as List
using Microsoft.Extensions.Configuration;

namespace Blog.Models.Repos
{
    public class BlogRepo
    {
        public IConfiguration _configuration { get; set; } 

        public LiteDatabase DB { get; set; }
        public LiteCollection<Post> Posts { get; set; }

        public BlogRepo()
        {
            var builder = new ConfigurationBuilder()     
                .AddJsonFile("appSettings.json");   
            _configuration = builder.Build();

            DB = new LiteDatabase(_configuration["LiteDBConn:defaultConnection"]);
            //DB = new LiteDatabase(@"Filename=Data/Blog.db; Connection=shared");
            Posts = (LiteDB.LiteCollection<Post>)DB.GetCollection<Post>("posts");

        }
        public void CreateExamplePosts()
        {
            // Delete all existing posts
            Posts.DeleteAll();
            
            // Create a list of example posts
            List<Post> PostList = new List<Post>(){
                new Post(){
                    Title = "First post",
                    Public = true,
                    CoverImagePath = "", // leave this blank for now
                    Excerpt = "Define the underlying",
                    Content = "Define the underlying principles that drive decisions and strategy for your design language bleeding edge onward and upward"
                    // The above text was generated by Office Ipsum http://officeipsum.com/index.php
                },
                new Post(){
                    Title = "Second post",
                    Public = true,
                    CoverImagePath = "", // still blank
                    Excerpt = "so what's our",
                    Content = "so what's our go to market strategy?. Customer centric all hands on deck yet where the metal hits the meat define"
                },
                new Post(){
                    Title = "Not visible",
                    Public = false,
                    CoverImagePath = "", // blank
                    Excerpt = "not important",
                    Content = "not important, you should not see this post"
                },
                new Post(){
                    Title = "Third post",
                    Public = true,
                    CoverImagePath = "", // blank
                    Excerpt = "the underlying",
                    Content = "the underlying principles that drive decisions and strategy for your design language not the long pole",
                    Deleted = true // this one should also not be visible
                },
                new Post(){
                    Title = "Fourth post",
                    Public = true,
                    CoverImagePath = "", // blank
                    Excerpt = "in the future",
                    Content = "Post scheduling made super easy",
                    Created = DateTime.Now.AddDays(3) // Post scheduling made easy
                },
                new Post(){
                    Title = "Markdown Test",
                    Public = true,
                    CoverImagePath = "", // blank
                    Excerpt = "Let's see what markdown can do",
                    Content =   "# Hello world\n"+ 
                                "**Lorem** ipsum dolor sit"+ 
                                "amet, *consectetur* adipiscing elit. Sed"+ 
                                "eu est nec metus luctus tempus. Pellentesque"+ 
                                "at elementum sapien, ac faucibus sem"+
                                "![surprise](https://media.giphy.com/media/fdyZ3qI0GVZC0/giphy.gif)"
                }
            };
            
            try
            {
                // This will ensure that the title of the post is unique
                // since we're going to be using it later in the URL
                Posts.EnsureIndex(p => p.Title, true);

                Posts.EnsureIndex(p => p._id, true);
                // Finally let's insert all these posts into the database
                //Posts.InsertBulk(PostList); // Obsolete
                Posts.Insert(PostList);
            }
            catch (LiteDB.LiteException ex)
            {
                // we'll simply ignore any exceptions that might happen here for now
                Console.WriteLine(ex.StackTrace.ToString());
            }
            catch (IOException iex)
            {
                Console.WriteLine(iex.StackTrace.ToString());
            }
        }// method: CreateExamplePosts
    }
}