using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FantasyEuroleague.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<PlayerStat> PlayerStats { get; set; }        
        public DbSet<EightPlayerTeam> EightPlayerTeams { get; set; }       
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Game Entity
            modelBuilder.Entity<Game>()
            .HasRequired(t => t.GuestTeam)
            .WithMany(ts => ts.AwayGames)
            .HasForeignKey(g => g.GuestTeamID)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
            .HasRequired(t => t.HomeTeam)
            .WithMany(hg => hg.HomeGames)
            .HasForeignKey(h => h.HomeTeamID)
            .WillCascadeOnDelete(false);

            // Profile Entity
            modelBuilder.Entity<Profile>()
                .HasKey(k => k.PlayerID);

            modelBuilder.Entity<Profile>()
                .HasRequired(pr => pr.Player)
                .WithRequiredDependent(pl => pl.Profile);

            // PlayerStats
            modelBuilder.Entity<PlayerStat>()
                .HasKey(ps => new { ps.PlayerID, ps.GameID });                       

            //EightPlayerTeam
            modelBuilder.Entity<EightPlayerTeam>()
                .HasMany(ept => ept.Players)
                .WithMany(p => p.FantasyTeams)
                .Map(m =>
                {
                    m.ToTable("FantasySelections");
                    m.MapLeftKey("EightPlayerTeamId");
                    m.MapRightKey("PlayerId");
                });

            modelBuilder.Entity<EightPlayerTeam>()
                .HasRequired(ept => ept.User)
                .WithMany(u => u.Teams)
                .HasForeignKey(ept => ept.UserId)
                .WillCascadeOnDelete(false);

            // Profile Entity
            modelBuilder.Entity<Wallet>()
                .HasKey(w => w.UserAccountId);

            modelBuilder.Entity<Wallet>()
                .HasRequired(w => w.UserAccount)
                .WithRequiredDependent(ua => ua.Wallet);





            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext()
            : base("FantasyEuroleagueDBContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}