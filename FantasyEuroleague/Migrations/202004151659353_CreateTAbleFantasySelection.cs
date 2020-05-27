namespace FantasyEuroleague.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTAbleFantasySelection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FantasySelections",
                c => new
                    {
                        EightPlayerTeamId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EightPlayerTeamId, t.PlayerId })
                .ForeignKey("dbo.EightPlayerTeams", t => t.EightPlayerTeamId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.EightPlayerTeamId)
                .Index(t => t.PlayerId);
            
            AddColumn("dbo.EightPlayerTeams", "TotalBudget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.EightPlayerTeams", "RoundScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.EightPlayerTeams", "TotalScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.EightPlayerTeams", "FantasyScore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EightPlayerTeams", "FantasyScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.FantasySelections", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.FantasySelections", "EightPlayerTeamId", "dbo.EightPlayerTeams");
            DropIndex("dbo.FantasySelections", new[] { "PlayerId" });
            DropIndex("dbo.FantasySelections", new[] { "EightPlayerTeamId" });
            DropColumn("dbo.EightPlayerTeams", "TotalScore");
            DropColumn("dbo.EightPlayerTeams", "RoundScore");
            DropColumn("dbo.EightPlayerTeams", "TotalBudget");
            DropTable("dbo.FantasySelections");
        }
    }
}
