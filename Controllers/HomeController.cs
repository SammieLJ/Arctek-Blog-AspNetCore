using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Blog.Models.Repos;
using System.Linq;

namespace Blog.Controllers{
    // the slash means that this will be the default controller
    // Basically if the URL does not provide any additional routes 
    // this controller will be called.
    [Route("/")]

    // the standard practice is to name your controllers
    // HomeController or PostController or UserController
    // Either way just make sure they end with Controller
    // or don't... your business really
    // but it does make it a lot easier since ASP.NET does some magic...that we'll show later
    public class HomeController : Controller{

        // the default action called is index and takes no parameters
        public IActionResult Index(){
            BlogRepo blogRepo = new BlogRepo();
            //return Content("Home controller called!");
            //blogRepo.CreateExamplePosts();
            
            //var allPosts = blogRepo.Posts.FindAll().ToList();
            //Console.WriteLine($"Total posts: {allPosts.Count}"); // Check count
            //return View(allPosts);

            return View(blogRepo.Posts.Find(
                p => p.Public == true &&
                p.Created <= DateTime.Now &&
                p.Deleted == false)
            );
        }
    }
}