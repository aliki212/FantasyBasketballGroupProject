namespace FantasyEuroleague.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectUserToFantasyTeams : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "UserAccount_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Players", new[] { "UserAccount_Id" });
            RenameColumn(table: "dbo.Wallets", name: "Id", newName: "UserAccountId");
            RenameIndex(table: "dbo.Wallets", name: "IX_Id", newName: "IX_UserAccountId");
            AddColumn("dbo.EightPlayerTeams", "Name", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.EightPlayerTeams", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.EightPlayerTeams", "UserId");
            AddForeignKey("dbo.EightPlayerTeams", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Players", "UserAccount_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "UserAccount_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.EightPlayerTeams", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.EightPlayerTeams", new[] { "UserId" });
            DropColumn("dbo.EightPlayerTeams", "UserId");
            DropColumn("dbo.EightPlayerTeams", "Name");
            RenameIndex(table: "dbo.Wallets", name: "IX_UserAccountId", newName: "IX_Id");
            RenameColumn(table: "dbo.Wallets", name: "UserAccountId", newName: "Id");
            CreateIndex("dbo.Players", "UserAccount_Id");
            AddForeignKey("dbo.Players", "UserAccount_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
