using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostsPortal.Models.CAModels
{
    public class Comment
    {

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual Post Post { get; set; }
        //public virtual ICollection<ApplicationUser> LikeUsers { get; set; }
        //public virtual ICollection<ApplicationUser> DislikeUsers { get; set; }

    }
}