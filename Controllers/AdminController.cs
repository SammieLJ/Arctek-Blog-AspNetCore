using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Blog.Models.Repos;
using Blog.Models;

namespace Blog.Controllers{
    [Route("/admin")]
    public class AdminController : Controller{
        private BlogRepo BlogRepo{get;set;}

        // The Controller constructor will be auto called everytime we 
        // navigate to one of the routes/methods in the controller
        public AdminController(){
            BlogRepo = new BlogRepo();
        }
        public IActionResult Index(){
            return View(BlogRepo.Posts.FindAll());
        }

        // GET /admin/new/post (show form)
        [HttpGet("new/post")]
        public IActionResult NewPost() {
            return View("NewPost");
        }

        // Helper: Sanitize and validate post data
        private bool SanitizeAndValidatePost(ref string title, ref string excerpt, ref string content, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(excerpt) || string.IsNullOrWhiteSpace(content))
            {
                error = "Title, Excerpt, and Content are required.";
                return false;
            }
            title = title.Trim();
            excerpt = excerpt.Trim();
            content = content.Trim();
            if (title.Length > 100) title = title.Substring(0, 100);
            if (excerpt.Length > 300) excerpt = excerpt.Substring(0, 300);
            if (content.Length > 10000) content = content.Substring(0, 10000);
            // Basic sanitization: remove dangerous chars
            title = title.Replace("$", "").Replace(";", "");
            excerpt = excerpt.Replace("$", "").Replace(";", "");
            // Add more rules as needed
            return true;
        }

        // POST /admin/new/post (handle form submission)
        [HttpPost("new/post")] 
        public IActionResult CreatePost([FromForm] string Title, [FromForm] string Excerpt, [FromForm] string Content) {
            string error;
            if (!SanitizeAndValidatePost(ref Title, ref Excerpt, ref Content, out error))
            {
                return BadRequest(error);
            }
            try {
                // Create new post object
                var newPost = new Post {
                    Title = Title,
                    Excerpt = Excerpt,
                    Content = Content,
                    CoverImagePath = "", // Placeholder for cover image path
                    Public = true, // Always set to true for new posts
                    Views = 0,
                    Created = DateTime.Now,
                    LastEdited = DateTime.Now
                };

                // Insert into database
                BlogRepo.Posts.Insert(newPost);

                // Ensure the post was actually created
                var createdPost = BlogRepo.Posts.Find(p => p.Title == Title).FirstOrDefault();
                if (createdPost == null) {
                    return StatusCode(500, "Failed to create post");
                }

                // Create indexes if they don't exist
                BlogRepo.Posts.EnsureIndex(p => p.Title, true); // Unique index
                BlogRepo.Posts.EnsureIndex(p => p._id);

                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                // Log the error (you should implement proper logging)
                return StatusCode(500, $"Error creating post: {ex.Message}");
            }
        }

        // GET /admin/edit/post/{id}
        [HttpGet("edit/post/{id}")]
        public IActionResult EditPost(string id)
        {
            // Decode the URL-friendly title back to the original
            var decodedTitle = Uri.UnescapeDataString(id).Replace("-", " ");
            var post = BlogRepo.Posts.Find(p => p.Title == decodedTitle).FirstOrDefault();
            
            if (post == null)
            {
                return NotFound();
            }

            return View("EditPost", post);
        }

        // POST /admin/edit/post/{id}
        [HttpPost("edit/post/{id}")]
        public IActionResult UpdatePost(string id, [FromForm] string Title, [FromForm] string Excerpt, [FromForm] string Content, [FromForm] bool? Public)
        {
            try
            {
                var decodedTitle = Uri.UnescapeDataString(id).Replace("-", " ");
                var post = BlogRepo.Posts.Find(p => p.Title == decodedTitle).FirstOrDefault();
                
                if (post == null)
                {
                    return NotFound();
                }

                // Update post properties
                post.Title = Title;
                post.Excerpt = Excerpt;
                post.Content = Content;
                post.Public = Public ?? false; // If unchecked, set to false
                post.LastEdited = DateTime.Now;

                // Save changes
                BlogRepo.Posts.Update(post);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating post: {ex.Message}");
            }
        }

        // Http DELETE method 
        // Pretty self explanatory if you ask me
        // [FromBody] means that we'll expect a value to be passed in the body 
        // of the request with the key of postTitle
        [HttpDelete("delete/post/{id}")]
        public IActionResult DeletePost(string id)
        {
            try
            {
                // Find post by title (URL-decoded)
                var decodedTitle = Uri.UnescapeDataString(id).Replace("-", " ");
                var post = BlogRepo.Posts.Find(p => p.Title == decodedTitle).FirstOrDefault();
                
                if (post == null)
                {
                    return NotFound();
                }

                // Delete the post
                BlogRepo.Posts.Delete(post._id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting post: {ex.Message}");
            }
        }

        // Http PUT method expected
        // Stands for update in this case 
        [HttpPut, Route("post/update")]
        public IActionResult UpdatePost([FromBody]Post newPost, [FromBody]string postTitle){
            // Updating is a bit more complex because we obviously have to get the original post first
            // and then update it.
            // in this case LiteDB does mose of the work for us
            var postId = BlogRepo.Posts.Find(p => p.Title == postTitle ).FirstOrDefault()._id;
            bool status = BlogRepo.Posts.Update(postId, newPost);
            if(status){
                return StatusCode(200);
            }
            return StatusCode(400);
        } 
    }
}