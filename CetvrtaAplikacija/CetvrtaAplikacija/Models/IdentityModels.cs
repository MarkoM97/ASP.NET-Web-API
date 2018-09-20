using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using PostsPortal.Models.CAModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace PostsPortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {


        public ApplicationUser()
        {
            this.LikedPosts = new HashSet<Post>();
            this.DislikedPosts = new HashSet<Post>();
            this.LikedComments = new HashSet<Comment>();
            this.DislikedComments = new HashSet<Comment>();
            this.Icon = "Avatar.png";
        }

        public DateTime Created { get; set; }
        public bool Banned { get; set; }
        public string Icon { get; set; }
        public string ApplicationUsername { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Post> LikedPosts { get; set; }
        public virtual ICollection<Post> DislikedPosts { get; set; }

        public virtual ICollection<Comment> LikedComments { get; set; }
        public virtual ICollection<Comment> DislikedComments { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(): base("DefaultConnection", throwIfV1Schema: false){}
        public static ApplicationDbContext Create(){return new ApplicationDbContext();}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasMany(a => a.LikedPosts).WithMany().Map(x =>
                                                  {
                                                      x.MapLeftKey("User_id");
                                                      x.MapRightKey("Post_id");
                                                      x.ToTable("Users_Posts_Like");
                                                  });

            modelBuilder.Entity<ApplicationUser>().HasMany(a => a.DislikedPosts)
                                                    .WithMany()
                                                       .Map(x =>
                                                       {
                                                           x.MapLeftKey("User_id");
                                                           x.MapRightKey("Post_id");
                                                           x.ToTable("Users_Posts_Dislike");
                                                       });


            modelBuilder.Entity<ApplicationUser>().HasMany(a => a.LikedComments)
                                                    .WithMany()
                                                       .Map(x =>
                                                       {
                                                           x.MapLeftKey("User_id");
                                                           x.MapRightKey("Comment_id");
                                                           x.ToTable("Users_Comments_Like");
                                                       });


            modelBuilder.Entity<ApplicationUser>().HasMany(a => a.DislikedComments)
                                                    .WithMany()
                                                       .Map(x =>
                                                       {
                                                           x.MapLeftKey("User_id");
                                                           x.MapRightKey("Comment_id");
                                                           x.ToTable("Users_Comments_Dislike");
                                                       });
            //modelBuilder.Entity<IdentityUser>().ToTable("Users");
            //modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            //modelBuilder.Entity<IdentityUserLogin>().ToTable("Logins");
            //modelBuilder.Entity<IdentityUserRole>().ToTable("UsersRoles");
            //modelBuilder.Entity<IdentityUserClaim>().ToTable("Claims");

        }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        //public virtual DbSet<Post> LikedPosts { get; set; }
        //public virtual DbSet<Post> DislikedPosts { get; set; }

        //public virtual DbSet<Comment> LikedComments { get; set; }
        //public virtual DbSet<Comment> DislikedComments { get; set; }
    }
}