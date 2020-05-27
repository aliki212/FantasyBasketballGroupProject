using FantasyEuroleague.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FantasyEuroleague.Dtos
{
    public class FantasyTeamDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public decimal RemainingBudget { get; private set; }
        public decimal CurrentValue { get; private set; }
        public decimal RoundScore { get; private set; }
        public decimal TotalScore { get; private set; }
        public List<int> PlayerIds { get; set; }

        public FantasyTeamDto()
        {
            PlayerIds = new List<int>();
        }
    }
}