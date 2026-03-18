namespace OrganizationMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMultiDocumentSupport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDocuments",
                c => new
                    {
                        UserDocumentId = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 255),
                        FilePath = c.String(nullable: false, maxLength: 500),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserDocumentId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Users", "DocumentName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "DocumentName", c => c.String());
            DropForeignKey("dbo.UserDocuments", "UserId", "dbo.Users");
            DropIndex("dbo.UserDocuments", new[] { "UserId" });
            DropTable("dbo.UserDocuments");
        }
    }
}
