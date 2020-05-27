using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FantasyEuroleague.Models
{
    public class Team
    {
        public int ID { get; set; }

        [Display(Name = "Team Name")]
        public string Name { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<Game> HomeGames { get; set; }
        public ICollection<Game> AwayGames { get; set; }

        public Team()
        {
            Players = new Collection<Player>();
            HomeGames = new Collection<Game>();
            AwayGames = new Collection<Game>();
        }

        public void RandomTeamStats(Game game)
        {
            foreach (var player in Players)
            {
                player.RandomPlayerStats(game);
            }
        }
    }
}