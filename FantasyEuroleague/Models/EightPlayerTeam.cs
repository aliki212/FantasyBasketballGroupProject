using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using FantasyEuroleague.Interfaces;
using FantasyEuroleague.Enumerations;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace FantasyEuroleague.Models
{
    public class EightPlayerTeam 
    {
        private const decimal initialBudget = 10M;
        private const int count = 8;
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public decimal RemainingBudget { get; private set; }
        public decimal CurrentValue { get; private set; }
        public decimal TotalBudget { get; private set; }
        public decimal  RoundScore { get; private set; }
        public decimal TotalScore { get; private set; }
        
        public List<Player> Players { get; set; }        
        public string UserId { get; set; }
        public UserAccount User { get; set; }
        protected EightPlayerTeam()
        { }

        protected EightPlayerTeam(List<Player> newPlayers, string userId, string name)
        {
            Players = newPlayers;
            UserId = userId;
            CurrentValue =  Players.Sum(p => p.Price);
            TotalBudget = initialBudget;
            RemainingBudget = TotalBudget - CurrentValue;
            Name = name;
        }

        public static EightPlayerTeam CreateTeam(List<Player> players, string userId, string name)
        {
            return  (!EightPlayerTeam.IsValid(players)) ? null : new EightPlayerTeam(players, userId, name);
        }


        //UPDATE AFTER PLAYER CHANGES
        public void UpdateTeam(List<Player> newPlayers)
        {
            Players = (ChangesAreValid(newPlayers)) ? newPlayers : Players;
            CurrentValue = Players.Sum(p => p.Price);
            RemainingBudget = TotalBudget - CurrentValue;
        }


        //UPDATE AFTER ROUND EXECUTION
        public void UpdateTeam(int round = 1)
        {
            CurrentValue = Players.Sum(p => p.Price);
            TotalBudget = RemainingBudget + CurrentValue;
            GetFantasyScore(round);
        }


        //CREATE VALIDATION
        public static bool IsValid(List<Player> players)
        {
            return (players.Distinct().Count() != players.Count()) ? false :
                   (players.Count() != count) ? false :
                   (
                        players.Where(p => p.Profile.Position == Position.Guard).Count() != 3 ||
                        players.Where(p => p.Profile.Position == Position.Forward).Count() != 3 ||
                        players.Where(p => p.Profile.Position == Position.Center).Count() != 2
                   ) ? false :
                   (players.Sum(p => p.Price) <= initialBudget);

        }


        //UPDATE VALIDATION
        public bool ChangesAreValid(List<Player> players)
        {
            int numberOfChanges = 0;
            foreach (var player in players)
            {
                numberOfChanges = (Players.Contains(player)) ? numberOfChanges : numberOfChanges++;
            }

            return (numberOfChanges > 3)? false :
                   (players.Distinct().Count() != players.Count())? false : 
                   (players.Count() != count)? false :          
                   (
                        players.Where(p => p.Profile.Position == Position.Guard).Count() != 3 ||
                        players.Where(p => p.Profile.Position == Position.Forward).Count() != 3 ||
                        players.Where(p => p.Profile.Position == Position.Center).Count() != 2
                   ) ? false :
                   ( players.Sum(p => p.Price) <= TotalBudget );             
            
        }      

        public void GetFantasyScore(int round)
        {
            Players = Players.Where(p => p.PlayerStats.Count() > 0).ToList();
            RoundScore = Players.Sum(p => p.PlayerStats
                                .Where(ps => ps.Game.Round == round)
                                .Select(ps => ps.PIR)
                                .SingleOrDefault());
            TotalScore += RoundScore;            
        }

        

       
    }
}