namespace ProjectCoffee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DrinkTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GlobalInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeetingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdId = c.Guid(nullable: false),
                        Name = c.String(),
                        WillBeThere = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Drink_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DrinkTypes", t => t.Drink_Id)
                .Index(t => t.Drink_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Drink_Id", "dbo.DrinkTypes");
            DropIndex("dbo.Users", new[] { "Drink_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.GlobalInformations");
            DropTable("dbo.DrinkTypes");
        }
    }
}
