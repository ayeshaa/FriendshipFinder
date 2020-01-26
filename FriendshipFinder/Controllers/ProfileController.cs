using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendshipFinder.Models;
using System.IO;
using FriendshipFinder.Models.ViewModel;

namespace FriendshipFinder.Controllers
{
    public class ProfileController : Controller
    {
        GerardJennyEntities db;
        public ActionResult Index()
        {
            if (Session["Login"] != null)
            {
                int UserId = Convert.ToInt32(Session["Login"]);
                using (db = new GerardJennyEntities())
                {
                    var num = from p in db.Posts
                              join u in db.Users
                              on p.UserID equals u.ID into bases
                              from sb in bases.DefaultIfEmpty()
                              where p.UserID == UserId
                              orderby p.PostID descending
                              select new PostUser
                              {
                                  PostID = p.PostID,
                                  PostedOn = p.PostedOn,
                                  Likes = p.Likes,
                                  Dislikes = p.Dislikes,
                                  Photo = p.Photo,
                                  Description = p.Description,
                                  UserID = p.UserID,
                                  ID = sb.ID,
                                  Name = sb.Name,
                                  ProfilePicture = sb.ProfilePicture,
                                  video=p.video,
                              };
                    return View(num.ToList());
                }
            }
            return RedirectToAction("Index", "User");
        }
        [HttpPost]
        public ActionResult Index(FormCollection form, HttpPostedFileBase videos)
        {
            Post timeline = new Post();
            string fileName = "";
            int UserId = Convert.ToInt32(Session["Login"]);
            using (db = new GerardJennyEntities())
            {
                try
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        if (file != null && file.ContentLength > 0)
                        {
                            fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("/Uploads/Images/"), fileName);
                            timeline.Photo = timeline.Photo + fileName;
                            if (i == Request.Files.Count - 2)
                            {
                                file.SaveAs(path);
                                break;
                            }
                            else
                            {
                                timeline.Photo += ",";
                            }
                            file.SaveAs(path);
                        }
                    }
                    if (videos != null && videos.ContentLength > 0)
                    {
                        var vidName = Path.GetFileName(videos.FileName);
                        var path = Path.Combine(Server.MapPath("/Uploads/Videos/"), vidName);
                        videos.SaveAs(path);
                        if (timeline.Photo == vidName + ",")
                        {
                            timeline.Photo = null;
                        }
                        timeline.video = vidName;
                    }
                    if (ModelState.IsValid)
                    {
                        timeline.Description = form["texts"];
                        timeline.PostedOn = DateTime.Now;
                        timeline.UserID = UserId;
                        db.Posts.Add(timeline);
                        db.SaveChanges();
                    }
                }
                catch
                {
                    ViewData["ERROR"] = "Something went wrong!";
                }
                var num = from p in db.Posts
                          join u in db.Users
                          on p.UserID equals u.ID into bases
                          from sb in bases.DefaultIfEmpty()
                          where p.UserID == UserId
                          orderby p.PostID descending
                          select new PostUser
                          {
                              PostID = p.PostID,
                              PostedOn = p.PostedOn,
                              Likes = p.Likes,
                              Dislikes = p.Dislikes,
                              Photo = p.Photo,
                              video = p.video,
                              Description = p.Description,
                              UserID = p.UserID,
                              ID = sb.ID,
                              Name = sb.Name,
                              ProfilePicture = sb.ProfilePicture,
                          };
                return View(num.ToList());
            }
        }
        public ActionResult About()
        {
            if (Session["Login"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "User");
        }
        private void UpdateLikes(int post)
        {
            var count = (from PostLike in db.PostLikes where PostLike.PostId == post select PostLike).Count();
            Post p = (from x in db.Posts
                      where x.PostID == post
                      select x).First();
            p.Likes = count;
            db.SaveChanges();
        }
        public ActionResult Likes(int post, int user)
        {
            bool query;
            int UserId = Convert.ToInt32(Session["Login"]);
            using (db = new GerardJennyEntities())
            {
                query = (from PostLike in db.PostLikes where PostLike.PostId == post && PostLike.UserId == user select PostLike).Any();
                if (query == false)
                {
                    PostLike postlike = new PostLike();
                    postlike.PostId = post;
                    postlike.UserId = user;
                    db.PostLikes.Add(postlike);
                    db.SaveChanges();
                    UpdateLikes(post);
                }
                else
                {
                    ViewData["Exists"] = "You Already liked this post!";
                }
                var num = from p in db.Posts
                          join u in db.Users
                          on p.UserID equals u.ID into bases
                          from sb in bases.DefaultIfEmpty()
                          where p.UserID == UserId
                          orderby p.PostID descending
                          select new PostUser
                          {
                              PostID = p.PostID,
                              PostedOn = p.PostedOn,
                              Likes = p.Likes,
                              Dislikes = p.Dislikes,
                              Photo = p.Photo,
                              Description = p.Description,
                              UserID = p.UserID,
                              ID = sb.ID,
                              Name = sb.Name,
                              ProfilePicture = sb.ProfilePicture,
                              video = p.video,
                          };
                return Json(num.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        private void UpdateDisLikes(int post)
        {
            var count = (from PostDislike in db.PostDislikes where PostDislike.PostId == post select PostDislike).Count();
            Post p = (from x in db.Posts
                      where x.PostID == post
                      select x).First();
            p.Dislikes = count;
            db.SaveChanges();
        }
        public ActionResult DisLikes(int post, int user)
        {
            bool query;
            int UserId = Convert.ToInt32(Session["Login"]);
            using (db = new GerardJennyEntities())
            {
                query = (from PostDislike in db.PostDislikes where PostDislike.PostId == post && PostDislike.UserId == user select PostDislike).Any();
                if (query == false)
                {
                    PostDislike postdislike = new PostDislike();
                    postdislike.PostId = post;
                    postdislike.UserId = user;
                    db.PostDislikes.Add(postdislike);
                    db.SaveChanges();
                    UpdateDisLikes(post);
                }
                else
                {
                    ViewData["Exists"] = "You Already liked this post!";
                }
                var num = from p in db.Posts
                          join u in db.Users
                          on p.UserID equals u.ID into bases
                          from sb in bases.DefaultIfEmpty()
                          where p.UserID == UserId
                          orderby p.PostID descending
                          select new PostUser
                          {
                              PostID = p.PostID,
                              PostedOn = p.PostedOn,
                              Likes = p.Likes,
                              Dislikes = p.Dislikes,
                              Photo = p.Photo,
                              Description = p.Description,
                              UserID = p.UserID,
                              ID = sb.ID,
                              Name = sb.Name,
                              ProfilePicture = sb.ProfilePicture,
                              video = p.video,
                          };
                return Json(num.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DisplayComment(int id)
        {
            using (db = new GerardJennyEntities())
            {
                var postComment = from c in db.PostComments
                                  join u in db.Users
                                  on c.UserId equals u.ID into bases
                                  from sb in bases.DefaultIfEmpty()
                                  where c.PostId == id
                                  select new CommentUser
                                  {
                                      CommentID = c.ID,
                                      Description = c.Description,
                                      PostId = c.PostId,
                                      UserId = c.UserId,
                                      ID = sb.ID,
                                      Name = sb.Name,
                                  };
                return PartialView("_Comment", postComment.ToList());
            }
        }
        [HttpPost]
        public ActionResult Comment(int Commentpost, int CommentUser, string description)
        {
            int UserId = Convert.ToInt32(Session["Login"]);
            using (db = new GerardJennyEntities())
            {
                PostComment comment = new PostComment();
                comment.Description = description;
                comment.PostId = Convert.ToInt32(Commentpost);
                comment.UserId = Convert.ToInt32(CommentUser);
                db.PostComments.Add(comment);
                db.SaveChanges();
                var num = from p in db.Posts
                          join u in db.Users
                          on p.UserID equals u.ID into bases
                          from sb in bases.DefaultIfEmpty()
                          where p.UserID == UserId
                          orderby p.PostID descending
                          select new PostUser
                          {
                              PostID = p.PostID,
                              PostedOn = p.PostedOn,
                              Likes = p.Likes,
                              Dislikes = p.Dislikes,
                              Photo = p.Photo,
                              Description = p.Description,
                              UserID = p.UserID,
                              ID = sb.ID,
                              Name = sb.Name,
                              ProfilePicture = sb.ProfilePicture,
                              video = p.video,
                          };
                return Json(num.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Introduction()
        {
            if (Session["Login"] != null)
            {
                using (db = new GerardJennyEntities())
                {
                    var num = from w in db.Writings
                              join u in db.Users
                              on w.UserId equals u.ID into bases
                              from sb in bases.DefaultIfEmpty()
                              orderby w.ContentID descending
                              select new ContentUser
                              {
                                  ContentID = w.ContentID,
                                  Description = w.Description,
                                  UserId = w.UserId,
                                  ID = sb.ID,
                                  Name = sb.Name,
                                  ProfilePicture = sb.ProfilePicture,
                              };
                    return View(num.ToList());
                }
            }
            return RedirectToAction("Index", "User");
        }
        [HttpPost]
        public ActionResult Introduction(FormCollection form)
        {
            int UserId = Convert.ToInt32(Session["Login"]);
            using (db = new GerardJennyEntities())
            {
                Writing write = new Writing();
                write.Description = form["texts"];
                write.UserId = UserId;
                db.Writings.Add(write);
                db.SaveChanges();
                var num = from w in db.Writings
                          join u in db.Users
                          on w.UserId equals u.ID into bases
                          from sb in bases.DefaultIfEmpty()
                          orderby w.ContentID descending
                          select new ContentUser
                          {
                              ContentID = w.ContentID,
                              Description = w.Description,
                              UserId = w.UserId,
                              ID = sb.ID,
                              Name = sb.Name,
                              ProfilePicture = sb.ProfilePicture,
                          };
                return View(num.ToList());
            }
        }
    }
}