namespace PostsPortal.Migrations.CAMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ajmajtfaktingzaphir : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Created = c.DateTime(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Created = c.DateTime(nullable: false),
                        Banned = c.Boolean(nullable: false),
                        Icon = c.String(),
                        ApplicationUsername = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Icon = c.String(),
                        Created = c.DateTime(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Users_Comments_Dislike",
                c => new
                    {
                        User_id = c.String(nullable: false, maxLength: 128),
                        Comment_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_id, t.Comment_id })
                .ForeignKey("dbo.AspNetUsers", t => t.User_id, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.Comment_id, cascadeDelete: true)
                .Index(t => t.User_id)
                .Index(t => t.Comment_id);
            
            CreateTable(
                "dbo.Users_Posts_Dislike",
                c => new
                    {
                        User_id = c.String(nullable: false, maxLength: 128),
                        Post_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_id, t.Post_id })
                .ForeignKey("dbo.AspNetUsers", t => t.User_id, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_id, cascadeDelete: true)
                .Index(t => t.User_id)
                .Index(t => t.Post_id);
            
            CreateTable(
                "dbo.Users_Comments_Like",
                c => new
                    {
                        User_id = c.String(nullable: false, maxLength: 128),
                        Comment_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_id, t.Comment_id })
                .ForeignKey("dbo.AspNetUsers", t => t.User_id, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.Comment_id, cascadeDelete: true)
                .Index(t => t.User_id)
                .Index(t => t.Comment_id);
            
            CreateTable(
                "dbo.Users_Posts_Like",
                c => new
                    {
                        User_id = c.String(nullable: false, maxLength: 128),
                        Post_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_id, t.Post_id })
                .ForeignKey("dbo.AspNetUsers", t => t.User_id, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_id, cascadeDelete: true)
                .Index(t => t.User_id)
                .Index(t => t.Post_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Users_Posts_Like", "Post_id", "dbo.Posts");
            DropForeignKey("dbo.Users_Posts_Like", "User_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Users_Comments_Like", "Comment_id", "dbo.Comments");
            DropForeignKey("dbo.Users_Comments_Like", "User_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Users_Posts_Dislike", "Post_id", "dbo.Posts");
            DropForeignKey("dbo.Users_Posts_Dislike", "User_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Users_Comments_Dislike", "Comment_id", "dbo.Comments");
            DropForeignKey("dbo.Users_Comments_Dislike", "User_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Users_Posts_Like", new[] { "Post_id" });
            DropIndex("dbo.Users_Posts_Like", new[] { "User_id" });
            DropIndex("dbo.Users_Comments_Like", new[] { "Comment_id" });
            DropIndex("dbo.Users_Comments_Like", new[] { "User_id" });
            DropIndex("dbo.Users_Posts_Dislike", new[] { "Post_id" });
            DropIndex("dbo.Users_Posts_Dislike", new[] { "User_id" });
            DropIndex("dbo.Users_Comments_Dislike", new[] { "Comment_id" });
            DropIndex("dbo.Users_Comments_Dislike", new[] { "User_id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "Owner_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropIndex("dbo.Comments", new[] { "Owner_Id" });
            DropTable("dbo.Users_Posts_Like");
            DropTable("dbo.Users_Comments_Like");
            DropTable("dbo.Users_Posts_Dislike");
            DropTable("dbo.Users_Comments_Dislike");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Posts");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
        }
    }
}
