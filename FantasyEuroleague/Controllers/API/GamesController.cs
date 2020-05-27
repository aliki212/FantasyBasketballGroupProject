using FantasyEuroleague.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FantasyEuroleague.Controllers.API
{
    public class GamesController : ApiController
    {

        private ApplicationDbContext context;

        public GamesController()
        {
            context = new ApplicationDbContext();
        }

        // /api/games
        [Route("api/players/createallgames")]
        [HttpPost]
        public IHttpActionResult CreateAllGames(List<string[]> games)
        {
            var gamesCount = games.Count(); //180

            for (int i = 0; i < gamesCount; i++)
            {
                var currentgame = games[i];
                var team1Name = currentgame[0];
                var team1 = context.Teams
                .SingleOrDefault(t => t.Name == team1Name);


                var team2Name = currentgame[1];
                var team2 = context.Teams
                .SingleOrDefault(t => t.Name == team2Name);

                //same logic as in calculated property 
                //var currentRound = z % 9 != 0 ? (z / 9 ): ((z / 9) - 1);
                //Did not work - went with long version:

                int z = i + 1;
                var currentRound = 0;
                if (z <= 9)
                #region
                { currentRound = 1; }
                else if (9 < z && z <= 18)
                { currentRound = 2; }
                else if (18 < z && z <= 27)
                { currentRound = 3; }
                else if (27 < z && z <= 36)
                { currentRound = 4; }
                else if (36 < z && z <= 45)
                { currentRound = 5; }
                else if (45 < z && z <= 54)
                { currentRound = 6; }
                else if (54 < z && z <= 63)
                { currentRound = 7; }
                else if (63 < z && z <= 72)
                { currentRound = 8; }
                else if (72 < z && z <= 81)
                { currentRound = 9; }
                else if (81 < z && z <= 90)
                { currentRound = 10; }
                else if (90 < z && z <= 99)
                { currentRound = 11; }
                else if (99 < z && z <= 108)
                { currentRound = 12; }
                else if (108 < z && z <= 117)
                { currentRound = 13; }
                else if (117 < z && z <= 126)
                { currentRound = 14; }
                else if (126 < z && z <= 135)
                { currentRound = 15; }
                else if (135 < z && z <= 144)
                { currentRound = 16; }
                else if (144 < z && z <= 153)
                { currentRound = 17; }
                else if (153 < z && z <= 162)
                { currentRound = 18; }
                else if (162 < z && z <= 171)
                { currentRound = 19; }
                else if (171 < z && z <= 180)
                { currentRound = 20; }
                #endregion

                var gameIn = new Game
                {
                    IsFinished = false,
                    HomeTeamID = team1.ID,
                    GuestTeamID = team2.ID,
                    Round = currentRound,
                   Season = GetSeason()
                };
                context.Games.Add(gameIn);
                context.SaveChanges();
            }//end of for

            return Ok();
        }

        public string GetSeason()
        {
            string season = "";
            //step 1 : get this year from DateTime.Now/year of game import to db
            int thisyear = int.Parse(DateTime.Now.ToString("yyyy"));
            //step 2 : if today's month is between january up to july : make it this year-lastyear eg:2019-2020
            int thismonth = int.Parse(DateTime.Now.Month.ToString());
            if (thismonth <= 7)
            { season = ((thisyear - 1) + "-" + thisyear).ToString(); }
            //else it is August up to december => 2020-2021 season
            else
            { season = (thisyear + "-" + (thisyear + 1)).ToString(); }
            return season;
        }



    }
}
