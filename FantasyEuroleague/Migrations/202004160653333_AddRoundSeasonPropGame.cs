namespace FantasyEuroleague.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoundSeasonPropGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Round", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "Season", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Season");
            DropColumn("dbo.Games", "Round");
        }
    }
}
