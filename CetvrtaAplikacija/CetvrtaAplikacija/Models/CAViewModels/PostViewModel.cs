using PostsPortal.Models.CAModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostsPortal.Models.CAViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public DateTime Created { get; set; }
        public string CreatedString { get; set; }
        public ApplicationUserViewModel Owner { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }

    public partial class ApplicationUtils
    {
        public static PostViewModel CreatePostViewModel(Post post)
        {
            PostViewModel pvm = new PostViewModel();
            pvm.Id = post.Id;
            pvm.Name = post.Name;
            pvm.Icon = post.Icon;
            pvm.Created = post.Created;
            pvm.CreatedString = String.Format("{0:ddd MMM ddd yyyy}", pvm.Created);
            pvm.Owner = CreateApplicationUserViewModel(post.Owner);
            return pvm;
        }
    }
}