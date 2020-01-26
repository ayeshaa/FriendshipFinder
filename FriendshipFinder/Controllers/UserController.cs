using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendshipFinder.Models;

namespace FriendshipFinder.Controllers
{
    public class UserController : Controller
    {
        GerardJennyEntities db;
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            using (db=new GerardJennyEntities())
            {
                User user = new User();
                var Username = form["username"];
                var Password = form["password"];
                var queryId = (from User in db.Users where User.Email == Username && User.Password == Password select User.ID).FirstOrDefault();
                var queryName = (from User in db.Users where User.Email == Username && User.Password == Password select User.Name).FirstOrDefault();
                var queryImage = (from User in db.Users where User.Email == Username && User.Password == Password select User.ProfilePicture).FirstOrDefault();
                if (queryId != 0)
                {
                    Session["Login"] = queryId;
                    Session["Name"] = queryName;
                    Session["ProfileImage"] = queryImage;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["LoginError"]="Username or Password is incorrect!";
                }
            }
                return View();
        }
        public ActionResult Logout()
        {
            if (Session["Login"] != null)
            {
                Session.Remove("Login");
            }
            return RedirectToAction("Index", "User");
        }
    }
}