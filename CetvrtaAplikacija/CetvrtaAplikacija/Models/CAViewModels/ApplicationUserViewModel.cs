using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostsPortal.Models.CAViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Icon { get; set; }
        public List<int> postLikes { get; set; }
        public List<int> postDislikes { get; set; }
    }



    public partial class ApplicationUtils
    {
        public static ApplicationUserViewModel CreateApplicationUserViewModel(ApplicationUser user)
        {
            //auto-mapper
            //es driven develop
            //DEPENDENCY INJC
            //
            ApplicationUserViewModel avm = new ApplicationUserViewModel();
            avm.Id = user.Id;
            avm.Email = user.Email;
            avm.Icon = user.Icon;
            avm.postLikes = new List<int>();
            avm.postDislikes = new List<int>();
            avm.postLikes.Add(0);
            avm.postDislikes.Add(0);
            foreach(var x in user.LikedPosts)
            {
                avm.postLikes.Add(x.Id);
            }

            foreach (var x in user.DislikedPosts)
            {
                avm.postDislikes.Add(x.Id);
            }

            //foreach (var x in user.LikedComments)
            //{
            //    avm.commenLikeDislike.Add(x.Id, true);
            //}

            //foreach (var x in user.DislikedComments)
            //{
            //    avm.commenLikeDislike.Add(x.Id, true);
            //}
            return avm;
        }
    }
}