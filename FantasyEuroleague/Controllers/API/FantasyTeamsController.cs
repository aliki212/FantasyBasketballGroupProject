using AutoMapper;
using FantasyEuroleague.Dtos;
using FantasyEuroleague.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FantasyEuroleague.Controllers.API
{
    public class FantasyTeamsController : ApiController
    {
        private ApplicationDbContext context;

        public FantasyTeamsController()
        {
            context = new ApplicationDbContext();
        }


        //Get / api / teams
        public IEnumerable<FantasyTeamDto> GetTeams(string userId)
        {
            var teams = context.EightPlayerTeams
                   .Include(ept => ept.User)
                   .Select(Mapper.Map<EightPlayerTeam, FantasyTeamDto>);

            //if (!String.IsNullOrEmpty(userId))
            //    teams = teams.Where(t => t.UserAccount.Id == userId);

            return teams.ToList();
        }

        

        //public IEnumerable<FantasyTeamDto> GetMyTeams()
        //{
        //    var userId = User.Identity.GetUserId();
        //    return GetTeams(userId);
        //}

        //Get / api / team / id
        //public IHttpActionResult GetTeam(int id)
        //{
        //    var team = context.EightPlayerTeams
        //        .Include(ept => ept.Players)
        //        .SingleOrDefault(ept => ept.Id == id);

        //    if (team == null)
        //        return NotFound();

        //    return Ok(Mapper.Map<EightPlayerTeam, FantasyTeamDto>(team));
        //}




        ////POST: /api/teams
        
        [HttpPost]
        public IHttpActionResult CreateFantasyTeam(FantasyTeamDto fantasyTeamDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var userId = User.Identity.GetUserId();
            var players = new List<Player>();

            foreach (var Id in fantasyTeamDto.PlayerIds)
            {
                var player = context.Players
                    .Include(p => p.Profile)
                    .SingleOrDefault(p => p.ID == Id);
                players.Add(player);
            }

            var team = EightPlayerTeam.CreateTeam(players, userId, fantasyTeamDto.Name);

            if (team == null)
                return BadRequest();

            context.EightPlayerTeams.Add(team);

            context.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/" + team.Id), fantasyTeamDto);
        }


        // PUT: /api/teams/id




        // DELETE: /api/players/id
        [HttpDelete]
        public void DeleteTeam(int id)
        {
            var team = context.EightPlayerTeams.
                Include(ept => ept.Players)
                .SingleOrDefault(ept => ept.Id == id);

            if (team == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            context.EightPlayerTeams.Remove(team);
            context.SaveChanges();
        }



    }
}
