using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using FantasyEuroleague.Enumerations;

namespace FantasyEuroleague.Models
{
    public class Player
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get { return Firstname + " " + Lastname; } }
        public Profile Profile { get; set; }
        public int TeamID { get; set; }
        public Team Team { get; set; }        
        public ICollection<EightPlayerTeam> FantasyTeams{ get; set; }
        public IEnumerable<int> Form
        {
            get
            {
                return PlayerStats
                    .Where(ps => ps.Dnp == false)
                    .OrderByDescending(ps => ps.GameID)
                    .Select(ps => ps.PIR)
                    .Take((GamesPLayed < formCount) ? GamesPLayed : formCount);
            }
        }
        private const int formCount = 4;
        public decimal FantasyPoints { get; set; }
        public decimal Price { get; set; }
        public int GamesPLayed { get { return PlayerStats.Where(ps => ps.Dnp == false).Count(); } }
        public ICollection<PlayerStat> PlayerStats { get; private set; }

        public Player()
        {            
            PlayerStats = new Collection<PlayerStat>();           
        }

        public void Update (Player player)
        {
            Firstname = player.Firstname;
            Lastname = player.Lastname;
            Profile.Country = player.Profile.Country;
            Profile.Position = player.Profile.Position;
            TeamID = player.TeamID;
            FantasyPoints = player.FantasyPoints;
            Price = player.Price;
        }

        public void AddStatsToPlayer(PlayerStat playerStat)
        {
            PlayerStats.Add(playerStat);           
            UpdateFantasyPoints();
            UpdatePrice();
        }

       

        public void UpdateFantasyPoints()
        {
            //Gets an average of Form values
            //factor is incremented by one for latest values
            var factor = 1;
            var sum = 0;
            var points = 0;
            foreach (var index in Form)
            {                 
                points += index * factor; 
                sum += factor;                
                factor++;
            }

            FantasyPoints = points / sum;            
        }

        public void UpdatePrice()
        {
            var newPrice = FantasyPoints / 10;
            var minPrice = Price - 0.2M * Price;
            var maxPrice = Price + 0.2M * Price;
            switch (newPrice)
            {
                case decimal np when (np < minPrice):
                    Price = minPrice;
                    break;
                case decimal np when (np > maxPrice):
                    Price = maxPrice;
                    break;
                case decimal np when (np < maxPrice && np > minPrice):
                    Price = newPrice;
                    break;
                default:
                    Price = 0;
                    break;
            }
        }

        public int GetLevel()
        {
            int level = 0;

            switch (Price)
            {
                case decimal p when (p < 0.2M):
                    level = 0;
                    break;
                case decimal p when (p < 0.5M):
                    level = 2;
                    break;
                case decimal p when (p < 1.0M):
                    level = 4;
                    break;
                case decimal p when (p < 1.5M):
                    level = 6;
                    break;
                case decimal p when (p >= 1.5M):
                    level = 8;
                    break;
                default:
                    break;
            }
            return level;
        }

        public void RandomPlayerStats(Game game)
        {
            Random random = new Random();
            int level = GetLevel();
            switch(Profile.Position)
            {
                case Position.Guard:
                    PlayerStats.Add(PlayerStat.RandomGuardStats(this, game, random, level));
                    break;
                case Position.Forward:
                    PlayerStats.Add(PlayerStat.RandomForwardStats(this, game, random, level));
                    break;
                case Position.Center:
                    PlayerStats.Add(PlayerStat.RandomCenterStats(this, game, random, level));
                    break;
                default:
                    break;
            }
           
        }

        

    }
}