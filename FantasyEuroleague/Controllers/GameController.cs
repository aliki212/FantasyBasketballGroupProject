using FantasyEuroleague.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace FantasyEuroleague.Controllers
{
    public class GameController : Controller
    {
        private ApplicationDbContext context;

        public GameController()
        {
            context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        //GET: Game
        public ActionResult Index()
        {
            var games = context.Games
                .Include(g => g.HomeTeam)
                .Include(g => g.GuestTeam).ToList();           

            return View(games);
        }   


        public ActionResult Details(int gameId, int teamId)
        {
            var playerStats = context.PlayerStats
                .Include(ps => ps.Game)
                .Include(ps => ps.Player)
                .Where(ps => ps.GameID == gameId && ps.Player.TeamID == teamId)
                .OrderBy(ps => ps.Player.Lastname)
                .ToList();

            return View(playerStats);
        }
        public ActionResult ExecuteGame(int id)
        {
            var game = context.Games
                .Include(g => g.HomeTeam)
                .Include(g => g.HomeTeam.Players)
                .Include(g => g.HomeTeam.Players.Select(p => p.Profile))
                .Include(g => g.GuestTeam)
                .Include(g => g.GuestTeam.Players)
                .Include(g => g.GuestTeam.Players.Select(p => p.Profile))
                .Single(g => g.ID == id);
                
            game.RandomGameStats();
            context.SaveChanges();
            return RedirectToAction("Index","Game");
        }
    }
}