using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostsPortal.Models.CAModels
{
    public class Post
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public DateTime Created { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual List<Comment> Comments { get; set; }
        //public virtual ICollection<ApplicationUser> LikeUsers { get; set; }
        //public virtual ICollection<ApplicationUser> DislikeUsers { get; set; }

    }
}