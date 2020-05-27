using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FantasyEuroleague.Dtos
{
    public class EightPlayerTeamDto
    {
        public string Name { get; set; }
        public List<int> PlayerIds { get; set; }
    }
}