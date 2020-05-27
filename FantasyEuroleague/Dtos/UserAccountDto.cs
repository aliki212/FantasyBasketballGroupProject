using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FantasyEuroleague.Dtos
{
    public class UserAccountDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }
        public ICollection<FantasyTeamDto> Teams { get; set; }

        public UserAccountDto()
        {
            Teams = new Collection<FantasyTeamDto>();
        }
    }
}