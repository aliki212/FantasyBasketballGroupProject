using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FantasyEuroleague.ViewModels
{
    public class FantasyTeamViewModel
    {
        public int Id { get; set; }

        
        [StringLength(255)]
        public string Name { get; set; }
        public decimal RemainingBudget { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal RoundScore { get; set; }
        public decimal TotalScore { get; set; }
        public List<int> PlayerIds { get; set; }
    }
}