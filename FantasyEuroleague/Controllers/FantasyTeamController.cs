using FantasyEuroleague.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyEuroleague.ViewModels;
using Microsoft.AspNet.Identity;

namespace FantasyEuroleague.Controllers
{
    public class FantasyTeamController : Controller
    {
        private ApplicationDbContext context;
        public FantasyTeamController()
        {
            context = new ApplicationDbContext();
        }

        // GET: FantasyTeam
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        // GET MyTeams
        public ActionResult MyTeams()
        {
            var userId = User.Identity.GetUserId();
            var teams = context.EightPlayerTeams
                .Include(ept => ept.User)
                .Where(ept => ept.UserId == userId)
                .ToList(); 
            return View(teams);
            
        }

        // FantasyTeam/AllTeams
        public ActionResult AllTeams()
        {
            var teams = context.EightPlayerTeams
                .Include(ept => ept.User)
                .OrderByDescending(ept => ept.TotalScore)
                .ToList();
            return View("FantasyTeams",teams);
        }
        
        [Authorize]
        //EDIT GET
        public ActionResult Edit(int Id)
        {
            var team = context.EightPlayerTeams
                .Include(ept => ept.Players)
                .SingleOrDefault(ept => ept.Id == Id);
            var userId = User.Identity.GetUserId();

            if (userId != team.UserId)
                return new HttpUnauthorizedResult();

            if (team == null)
                return HttpNotFound();

            var viewModel = new FantasyTeamViewModel()
            {
                Id = team.Id,
                Name = team.Name,
                RemainingBudget = team.RemainingBudget,
                CurrentValue = team.CurrentValue,
                RoundScore = team.RoundScore,
                TotalScore = team.TotalScore,
                PlayerIds = team.Players.Select(p => p.ID).ToList()
            };

            return View("Update",viewModel);
        }
        [Authorize]
        [HttpPut]
        public ActionResult UpdateFantasyTeam(FantasyTeamViewModel  fantasyTeamViewModel)
        {
            if (!ModelState.IsValid)
                return View("Update", fantasyTeamViewModel);

            var team = context.EightPlayerTeams
                .Include(ept => ept.Players)
                .SingleOrDefault(ept => ept.Id == fantasyTeamViewModel.Id);

            if (team == null)
                return HttpNotFound();

            var players = new List<Player>();
            foreach (var Id in fantasyTeamViewModel.PlayerIds)
            {

                var player = context.Players
                    .Include(p => p.Profile)
                    .SingleOrDefault(p => p.ID == Id);
                players.Add(player);
            }

            team.UpdateTeam(players);
            context.SaveChanges();

            return View();
        }
    }
}