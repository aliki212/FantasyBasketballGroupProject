using FantasyEuroleague.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FantasyEuroleague.Controllers.API
{
    public class EightPlayerTeamsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CreateNewTeam(EightPlayerTeamDto teamDto)
        {

            return Ok();
        }
    }
}
