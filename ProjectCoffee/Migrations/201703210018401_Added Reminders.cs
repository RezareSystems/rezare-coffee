namespace ProjectCoffee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReminders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reminders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reminders", "User_Id", "dbo.Users");
            DropIndex("dbo.Reminders", new[] { "User_Id" });
            DropTable("dbo.Reminders");
        }
    }
}
