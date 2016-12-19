namespace ProjectCoffee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNickName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "NickName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "NickName");
        }
    }
}
