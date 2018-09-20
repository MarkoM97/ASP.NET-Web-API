using PostsPortal.Models;
using PostsPortal.Models.CAModels;
using PostsPortal.Models.CAViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PostsPortal.Controllers
{
    public class PostController : ApiController
    {
        public IHttpActionResult Get()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<PostViewModel> posts = new List<PostViewModel>();
                foreach(var x in db.Posts.ToList())
                {
                    

                    PostViewModel pvm = ApplicationUtils.CreatePostViewModel(x);
                    pvm.Likes = 0;
                    pvm.Dislikes = 0;
                    pvm.Likes = db.Database.SqlQuery<int>("SELECT COUNT(*) FROM Users_Posts_Like WHERE Post_id = {0}", pvm.Id).First();
                    pvm.Dislikes = db.Database.SqlQuery<int>("SELECT COUNT(*) FROM Users_Posts_Dislike WHERE Post_id = {0}", pvm.Id).First();
                    posts.Add(pvm);
                    
                }
                return Ok( new { Posts = posts });
            }
        }

        //[HttpPost]
        //public IHttpActionResult Create([FromBody]string name, [FromBody]HttpPostedFileBase imageFile)
        //{

        //}
        //[HttpPost]
        //public HttpResponseMessage Create()
        //{
        //    var exMessage = string.Empty;
        //    try
        //    {
        //        string uploadPath = "/Content/PostPhotos/32.png";
        //        HttpPostedFile file = null;
        //        if (HttpContext.Current.Request.Files.Count > 0)
        //        {
        //            file = HttpContext.Current.Request.Files.Get("imageFile");
        //        }
        //        // Check if we have a file
        //        if (null == file)
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, new
        //            {
        //                error = true,
        //                message = "Image file not found xoxo"
        //            });

        //        // Make sure the file has content
        //        if (!(file.ContentLength > 0))
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, new
        //            {
        //                error = true,
        //                message = "Image file not found"
        //            });

        //        //int productId = int.Parse(HttpContext.Current.Request.Params.Get("productId"));
        //        string description = HttpContext.Current.Request.Params.Get("postName");



        //if (!Directory.Exists(HttpContext.Current.Server.MapPath(uploadPath)))
        //        {
        //            // If it doesn't exist, create the directory
        //            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(uploadPath));
        //        }

        //        //Upload File
        //        file.SaveAs(HttpContext.Current.Server.MapPath($"{uploadPath}/{file.FileName}"));

        //    }
        //    catch (Exception ex)
        //    {
        //        exMessage = ex.Message;
        //    }
        //    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = true, message = exMessage == string.Empty ? "An unknown error occured" : exMessage });
        //}


        [HttpPost]
        public HttpResponseMessage Create()
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                var postName = httpRequest.Params["postName"];
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {
                        Post post = new Post();
                        post.Created = DateTime.Now;
                        db.Posts.Add(post);
                        db.SaveChanges();

                        var postedFile = httpRequest.Files[file];
                        string extension = postedFile.FileName.Split('.')[1];
                        var filePath = HttpContext.Current.Server.MapPath("/Content/PostPhotos/" + post.Id + "." + extension);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);

                        

                        
                        post.Name = postName;
                        post.Icon = post.Id + "." + extension;
                        var user = db.Users.Find(User.Identity.GetUserId());
                        post.Owner = user;
                        

                        user.Posts.Add(post);
                        //db.Posts.Add(post);
                        db.SaveChanges();


                    }
                    result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                return result;
            }
        }


        [HttpPost]
        public IHttpActionResult SubmitLikeDislike(int postId,bool isLike) {
            using(ApplicationDbContext db = new ApplicationDbContext()) {
                var user = db.Users.Find(User.Identity.GetUserId());
                if(isLike) user.LikedPosts.Add(db.Posts.Find(postId)); else user.DislikedPosts.Add(db.Posts.Find(postId));
                db.SaveChanges();
                return Ok(new { message = "ok"});
            }
        }

        [HttpPost]
        public IHttpActionResult RemoveLikeDislike(int postId, bool wasLike)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user.LikedPosts.Contains(db.Posts.Find(postId)))
                {
                    if (wasLike) user.LikedPosts.Remove(db.Posts.Find(postId)); else user.DislikedPosts.Remove(db.Posts.Find(postId));
                }
                db.SaveChanges();
                return Ok(new { message = "ok" });
            }
            
        }

    }
}
