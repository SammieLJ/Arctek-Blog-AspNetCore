using System.Linq;
using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Blog.Models.Repos;

namespace Blog.Controllers{
    public class PostController : Controller {

        [Route("/post/{postTitle}")]
        public IActionResult Index([FromRoute] string postTitle){
            BlogRepo blogRepo = new BlogRepo();
            // blogRepo.CreateExamplePosts(); // Remove this in production!

            // Find the post by id or title, and only if Public == true
            var post = blogRepo.Posts.Find(p => p.Title.Replace(" ", "-") == postTitle && p.Public == true).FirstOrDefault();
            if (post == null)
                return NotFound();
            return View(post);
        }
    }
}