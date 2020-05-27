namespace FantasyEuroleague.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserWalletProperties : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Amount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.Players", "UserAccount_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "IdentityPapersNumber", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "PrimaryResidence", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "Street", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "Town", c => c.String());
            AddColumn("dbo.AspNetUsers", "PostalCode", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Players", "UserAccount_Id");
            AddForeignKey("dbo.Players", "UserAccount_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wallets", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Players", "UserAccount_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Wallets", new[] { "Id" });
            DropIndex("dbo.Players", new[] { "UserAccount_Id" });
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "PostalCode");
            DropColumn("dbo.AspNetUsers", "Town");
            DropColumn("dbo.AspNetUsers", "Street");
            DropColumn("dbo.AspNetUsers", "PrimaryResidence");
            DropColumn("dbo.AspNetUsers", "IdentityPapersNumber");
            DropColumn("dbo.AspNetUsers", "DateOfBirth");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.Players", "UserAccount_Id");
            DropTable("dbo.Wallets");
        }
    }
}
