namespace FA.JustBlog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Common.Category",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        UrlSlug = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 500),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.Posts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 255),
                        ShortDescription = c.String(nullable: false, maxLength: 1000),
                        ImageUrl = c.String(maxLength: 255),
                        PostContent = c.String(nullable: false),
                        UrlSlug = c.String(nullable: false, maxLength: 255),
                        Published = c.Boolean(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        RateCount = c.Int(nullable: false),
                        TotalRate = c.Int(nullable: false),
                        PublishedDate = c.DateTime(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Common.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "common.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        UrlSlug = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 500),
                        Count = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.PostTags",
                c => new
                    {
                        PostId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId })
                .ForeignKey("common.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("common.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("common.PostTags", "TagId", "common.Tags");
            DropForeignKey("common.PostTags", "PostId", "common.Posts");
            DropForeignKey("common.Posts", "CategoryId", "Common.Category");
            DropIndex("common.PostTags", new[] { "TagId" });
            DropIndex("common.PostTags", new[] { "PostId" });
            DropIndex("common.Posts", new[] { "CategoryId" });
            DropTable("common.PostTags");
            DropTable("common.Tags");
            DropTable("common.Posts");
            DropTable("Common.Category");
        }
    }
}
