namespace FantasyEuroleague.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlayerProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "FantasyPoints", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Players", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "Price");
            DropColumn("dbo.Players", "FantasyPoints");
        }
    }
}
