namespace ProjectCoffee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCoffeeOptionstoUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CoffeeOptionsJson", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CoffeeOptionsJson");
        }
    }
}
