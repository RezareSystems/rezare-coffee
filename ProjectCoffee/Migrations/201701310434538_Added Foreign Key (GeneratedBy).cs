namespace ProjectCoffee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForeignKeyGeneratedBy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CoffeeReports", "GeneratedBy_Id", "dbo.Users");
            DropIndex("dbo.CoffeeReports", new[] { "GeneratedBy_Id" });
            AlterColumn("dbo.CoffeeReports", "GeneratedBy_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.CoffeeReports", "GeneratedBy_Id");
            AddForeignKey("dbo.CoffeeReports", "GeneratedBy_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CoffeeReports", "GeneratedBy_Id", "dbo.Users");
            DropIndex("dbo.CoffeeReports", new[] { "GeneratedBy_Id" });
            AlterColumn("dbo.CoffeeReports", "GeneratedBy_Id", c => c.Int());
            CreateIndex("dbo.CoffeeReports", "GeneratedBy_Id");
            AddForeignKey("dbo.CoffeeReports", "GeneratedBy_Id", "dbo.Users", "Id");
        }
    }
}
