namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceBlogs", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.ServiceBlogs", "ServiceId", "dbo.Services");
            DropIndex("dbo.ServiceBlogs", new[] { "ServiceId" });
            DropIndex("dbo.ServiceBlogs", new[] { "BlogId" });
            AddColumn("dbo.Blogs", "ServiceId", c => c.Guid());
            CreateIndex("dbo.Blogs", "ServiceId");
            AddForeignKey("dbo.Blogs", "ServiceId", "dbo.Services", "Id");
            DropTable("dbo.ServiceBlogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceBlogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ServiceId = c.Guid(nullable: false),
                        BlogId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Blogs", "ServiceId", "dbo.Services");
            DropIndex("dbo.Blogs", new[] { "ServiceId" });
            DropColumn("dbo.Blogs", "ServiceId");
            CreateIndex("dbo.ServiceBlogs", "BlogId");
            CreateIndex("dbo.ServiceBlogs", "ServiceId");
            AddForeignKey("dbo.ServiceBlogs", "ServiceId", "dbo.Services", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ServiceBlogs", "BlogId", "dbo.Blogs", "Id", cascadeDelete: true);
        }
    }
}
