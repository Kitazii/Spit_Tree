using K_Burns_SpitTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace K_Burns_SpitTree.Controllers
{
    public class HomeController : Controller
    {
        //create an instance of the database context
        private SpitTreeDbContext context = new SpitTreeDbContext();
        public ActionResult Index()
        {
            //get all posts, include the categoryfor each post, include the user who created the post
            //and order the post from the most current to old posts
            var posts = context.Posts.Include(p => p.Category).Include(p => p.User).OrderByDescending(p => p.DatePosted);

            //send the list of categoried over the index page
            //so we can display them
            ViewBag.Categories = context.Categories.ToList();

            //send the posts collection to the view name index
            return View(posts.ToList());
        }
        [HttpPost]
        //this is the action that will process the search form on the index page
        //the name of the string parameter SearchString must be the same
        //with the name of the textbox on the view
        public ActionResult Index(string SearchString)
        {
            var posts = context.Posts.Include(p => p.Category).Include(p => p.User).Where(p => p.Category.Name.Equals(SearchString.Trim())).OrderByDescending(p => p.DatePosted);
            //send the list of categoried over the index page
            //so we can display them
            ViewBag.Categories = context.Categories.ToList();
            if(SearchString.Equals(""))
            {
                return RedirectToAction("Index");
            }
            return View(posts.ToList());
        }

        public ActionResult Details (int id)
        {
            //search the posts table in the database
            //find post by id
            //return post
            Post post = context.Posts.Find(id);

            //using the foreign key UserId from the post instance
            //find the user who created the post
            var user = context.Users.Find(post.UserId);

            //using the foreign key CategoryId from the post
            //find the category that the post belongs to
            var category = context.Categories.Find(post.CategoryId);

            //assign the user to the User navigational property in Post
            post.User = user;

            //assing the category to the Category naviational propert in Post
            post.Category = category;

            //send the post model to the Details View
            return View(post);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}