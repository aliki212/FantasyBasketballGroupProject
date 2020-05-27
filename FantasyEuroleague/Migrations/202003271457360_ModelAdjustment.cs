namespace FantasyEuroleague.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelAdjustment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamStats", "GameID", "dbo.Games");
            DropForeignKey("dbo.TeamStats", "TeamID", "dbo.Teams");
            DropIndex("dbo.TeamStats", new[] { "TeamID" });
            DropIndex("dbo.TeamStats", new[] { "GameID" });
            AddColumn("dbo.Games", "IsFinished", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlayerStats", "Dnp", c => c.Boolean(nullable: false));
            DropTable("dbo.TeamStats");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeamStats",
                c => new
                    {
                        TeamID = c.Int(nullable: false),
                        GameID = c.Int(nullable: false),
                        TwoPointMade = c.Int(nullable: false),
                        TwoPointAttempted = c.Int(nullable: false),
                        ThreePointMade = c.Int(nullable: false),
                        ThreePointAttempted = c.Int(nullable: false),
                        FreeThrowMade = c.Int(nullable: false),
                        FreeThrowAttempted = c.Int(nullable: false),
                        Assists = c.Int(nullable: false),
                        DefensiveRebounds = c.Int(nullable: false),
                        OffensiveRebounds = c.Int(nullable: false),
                        Steals = c.Int(nullable: false),
                        Turnovers = c.Int(nullable: false),
                        BlocksMade = c.Int(nullable: false),
                        BlocksReceived = c.Int(nullable: false),
                        FoulsMade = c.Int(nullable: false),
                        FoulsReceived = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamID, t.GameID });
            
            DropColumn("dbo.PlayerStats", "Dnp");
            DropColumn("dbo.Games", "IsFinished");
            CreateIndex("dbo.TeamStats", "GameID");
            CreateIndex("dbo.TeamStats", "TeamID");
            AddForeignKey("dbo.TeamStats", "TeamID", "dbo.Teams", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TeamStats", "GameID", "dbo.Games", "ID", cascadeDelete: true);
        }
    }
}
