using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FantasyEuroleague.Models
{
    public class Game
    {
        const int numOfGamesPerRound = 9;
        public int ID { get; set; }
        //public int Round { get { return (ID % numOfGamesPerRound != 0)?  ID / numOfGamesPerRound : (ID / numOfGamesPerRound) -1  ; } }
        public int Round { get; set; }
        public string Season { get; set; }
        public bool  IsFinished { get; set; }
        public int HomeTeamID { get; set; }
        public Team HomeTeam { get; set; }
        public int GuestTeamID { get; set; }
        public Team GuestTeam { get; set; }
        


        public Game()
        {
            
        }

        public void RandomGameStats()
        {
            HomeTeam.RandomTeamStats(this);
            GuestTeam.RandomTeamStats(this);
        }
    }
}