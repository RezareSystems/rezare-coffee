namespace ProjectCoffee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAdIdinusertoGuid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Guid", c => c.Guid(nullable: false));
            DropColumn("dbo.Users", "AdId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "AdId", c => c.Guid(nullable: false));
            DropColumn("dbo.Users", "Guid");
        }
    }
}
