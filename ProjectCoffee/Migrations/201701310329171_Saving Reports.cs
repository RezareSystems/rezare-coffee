namespace ProjectCoffee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SavingReports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CoffeeReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GeneratedOn = c.DateTime(nullable: false),
                        GeneratedFor = c.DateTime(nullable: false),
                        ReportJson = c.String(),
                        GeneratedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.GeneratedBy_Id)
                .Index(t => t.GeneratedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CoffeeReports", "GeneratedBy_Id", "dbo.Users");
            DropIndex("dbo.CoffeeReports", new[] { "GeneratedBy_Id" });
            DropTable("dbo.CoffeeReports");
        }
    }
}
