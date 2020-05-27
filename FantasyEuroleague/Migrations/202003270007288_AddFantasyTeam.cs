namespace FantasyEuroleague.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFantasyTeam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EightPlayerTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RemainingBudget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FantasyScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EightPlayerTeams");
        }
    }
}
