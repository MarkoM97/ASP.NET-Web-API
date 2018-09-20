//using PostsPortal.Models.CAModels;
using PostsPortal.Models;
using PostsPortal.Models.CAModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostsPortal.Models.CAViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public string CreatedString { get; set; }
        public ApplicationUserViewModel Owner { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

    }

    public partial class ApplicationUtils
    {
        public static CommentViewModel CreateCommentViewModel(Comment comment)
        {
            CommentViewModel cvm = new CommentViewModel();

            cvm.Id = comment.Id;
            cvm.Content = comment.Content;
            cvm.Created = comment.Created;
            cvm.CreatedString = String.Format("{0:ddd MMM ddd yyyy}", cvm.Created);
            cvm.Owner = ApplicationUtils.CreateApplicationUserViewModel(comment.Owner);
            cvm.Likes = 0;
            cvm.Dislikes = 0;
            return cvm;

        }
    }
}