using PostsPortal.Models;
using PostsPortal.Models.CAModels;
using PostsPortal.Models.CAViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PostsPortal.Controllers
{
    public class CommentController : ApiController
    {


        public IHttpActionResult Get()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<CommentViewModel> comments = new List<CommentViewModel>();
                foreach (var x in db.Comments.ToList())
                {
                    CommentViewModel cvm = ApplicationUtils.CreateCommentViewModel(x);
                    cvm.Likes = db.Database.SqlQuery<int>("SELECT COUNT(*) FROM Users_Comments_Like WHERE Comment_id = {0}", cvm.Id).First();
                    cvm.Dislikes = db.Database.SqlQuery<int>("SELECT COUNT(*) FROM Users_Comments_Dislike WHERE Comment_id = {0}", cvm.Id).First();

                    comments.Add(cvm);
                }
                return Ok(new { Comments = comments });
            }
        }

        public IHttpActionResult Get(int postId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<CommentViewModel> comments = new List<CommentViewModel>();
                foreach (var x in db.Posts.Find(postId).Comments)
                {
                    CommentViewModel cvm = ApplicationUtils.CreateCommentViewModel(x);
                    cvm.Likes = db.Database.SqlQuery<int>("SELECT COUNT(*) FROM Users_Comments_Like WHERE Comment_id = {0}", cvm.Id).First();
                    cvm.Dislikes = db.Database.SqlQuery<int>("SELECT COUNT(*) FROM Users_Comments_Dislike WHERE Comment_id = {0}", cvm.Id).First();

                    comments.Add(cvm);
                }
                return Ok(new { Comments = comments });
            }
        }

        [HttpPost]
        public IHttpActionResult Create()
        {
            int postId = int.Parse(HttpContext.Current.Request.Params["postId"]);
            var content = HttpContext.Current.Request.Params["content"];

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Comment c = new Comment();
                c.Content = content;
                c.Created = DateTime.Now;
                c.Post = db.Posts.Find(postId);
                c.Owner = db.Users.Find(User.Identity.GetUserId());
                db.Comments.Add(c);
                db.SaveChanges();
                return Ok(new { message = "ok"});
            }
            
        }


        [HttpPost]
        public IHttpActionResult SubmitLikeDislike(int commentId, bool isLike)
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (isLike) user.LikedComments.Add(db.Comments.Find(commentId)); else user.DislikedComments.Add(db.Comments.Find(commentId));
                db.SaveChanges();
                return Ok(new { message = "ok" });
            }
        }
        
        [HttpPost]
        public IHttpActionResult RemoveLikeDislike(int commentId, bool wasLike)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user.LikedComments.Contains(db.Comments.Find(commentId)))
                {
                    if (wasLike) user.LikedComments.Remove(db.Comments.Find(commentId)); else user.DislikedComments.Remove(db.Comments.Find(commentId));
                }
                db.SaveChanges();
                return Ok(new { message = "ok" });
            }

        }
    }
}
