namespace ProjectCoffee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForeignKeyAssociationtoUsersTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Users", name: "Drink_Id", newName: "DrinkId");
            RenameIndex(table: "dbo.Users", name: "IX_Drink_Id", newName: "IX_DrinkId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Users", name: "IX_DrinkId", newName: "IX_Drink_Id");
            RenameColumn(table: "dbo.Users", name: "DrinkId", newName: "Drink_Id");
        }
    }
}
