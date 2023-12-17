using K_Burns_SpitTree.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace K_Burns_SpitTree.Controllers
{
    [Authorize(Roles = "Admin")]//this allows only registered user as admin role to access the Admin controller
    public class AdminController : Controller
    {

        //create an instance of the database o we can access the tables
        private SpitTreeDbContext db = new SpitTreeDbContext();

        // GET: Admin
        [Authorize(Roles = "Admin")]//only Admins can call the index action
        public ActionResult Index()
        {
            return View();
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //find category by id in Categories table in the database
            Category category = db.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            //send the category to the Details view
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("ViewAllCategories");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewAllCategories");
            }

            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("ViewAllCategories");
        }

        // GET: Posts/Delete/5
        public ActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePostConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("ViewAllPosts");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

//**********************************************************************************************************************************************************
        //GET: Categories
        public ActionResult ViewAllCategories()
        {
            return View(db.Categories.ToList());
        }

//**********************************************************************************************************************************************************
        [Authorize(Roles = "Admin")]
        public ActionResult ViewAllPosts()
        {
            //get all posts from database including their category and the user who created the post
            List<Post> posts = db.Posts.Include(p => p.Category).Include(p => p.User).ToList();

            //send the list to the view named ViewAllPosts
            return View(posts);
        }

//**********************************************************************************************************************************************************
        [Authorize(Roles = "Admin")]
        public ActionResult ViewUsers()
        {
            //get all the registered users from database
            //include their role
            //then order them by their last name
            List<User> users = db.Users.Include(u => u.Roles).OrderBy(u => u.LastName).ToList();

            //send the list to the view and display
            return View(users);
        }
    }
}
