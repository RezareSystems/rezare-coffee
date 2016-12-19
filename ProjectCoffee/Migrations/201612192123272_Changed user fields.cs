namespace ProjectCoffee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeduserfields : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Users", "NickName", "FirstName");
            RenameColumn("dbo.Users", "Name", "LastName");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Users", "FirstName", "NickName");
            RenameColumn("dbo.Users", "LastName", "Name");
        }
    }
}
