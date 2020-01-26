using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FriendshipFinder.Models;
using System.Data.Entity;
using FriendshipFinder.Models.ViewModel;

namespace FriendshipFinder.Controllers
{
    public class HomeController : Controller
    {
        GerardJennyEntities db;
        public ActionResult Index()
        {
            if (Session["Login"] != null)
            {
                using (db = new GerardJennyEntities())
                {
                    var num = from p in db.Posts
                              join u in db.Users
                              on p.UserID equals u.ID into bases
                              from sb in bases.DefaultIfEmpty()
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
                        if(timeline.Photo==vidName + ",")
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
                return Json(num.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RecentPosts(int id)
        {
            IEnumerable<Post> post;
            using (db = new GerardJennyEntities())
            {
                post = (from Post in db.Posts where Post.UserID == id orderby Post.PostID descending select Post).ToList();
            }
            return View("_RecentAds", post);
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
                                      ProfilePicture = sb.ProfilePicture,
                                  };
                return PartialView("_Comment", postComment.ToList());
            }
        }
        [HttpPost]
        public ActionResult Comment(int Commentpost, int CommentUser, string description)
        {
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
                return Json(num.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}