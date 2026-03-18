namespace OrganizationMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDocumentSupport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DocumentName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DocumentName");
        }
    }
}
