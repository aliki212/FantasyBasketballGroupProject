using FantasyEuroleague.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FantasyEuroleague.Dtos;
using AutoMapper;

namespace FantasyEuroleague.Controllers.API
{
    public class PlayersController : ApiController
    {
        private ApplicationDbContext context;

        public PlayersController()
        {
            context = new ApplicationDbContext();
        }

        // GET: /api/players
        public IEnumerable<PlayerDto> GetPlayers()
        {
            return context.Players
                .Include(p => p.Profile)
                .Include(p => p.Team)
                .Select(Mapper.Map<Player, PlayerDto>);
        }

        //GET: /api/player/id
        public IHttpActionResult GetPlayer(int id)
        {
            var player = context.Players
                .Include(p => p.Profile)
                .Include(p => p.Team)
                .SingleOrDefault(p => p.ID == id);

            if (player == null)
                return NotFound();

            return Ok(Mapper.Map<Player, PlayerDto>(player));
        }


        // POST: /api/players
        [HttpPost]
        public IHttpActionResult CreatePlayer(PlayerDto playerDto)
        {
            if (!ModelState.IsValid)
                return NotFound();

            var player = Mapper.Map<PlayerDto, Player>(playerDto);
            context.Players.Add(player);

            context.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/" + player.ID), playerDto);
        }

        // PUT: /api/players/id
        [HttpPut]
        public void UpdatePlayer(int id, PlayerDto playerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var playerDb = context.Players
                .Include(p => p.Profile)
                .SingleOrDefault(p => p.ID == id);

            if (playerDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(playerDto, playerDb);
            context.SaveChanges();
        }

        // DELETE: /api/players/id
        [HttpDelete]
        public void DeletePlayer(int id)
        {
            var playerDb = context.Players.
                Include(p => p.Profile)
                .SingleOrDefault(p => p.ID == id);

            if (playerDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            context.Players.Remove(playerDb);
            context.SaveChanges();
        }

        // POST: /api/players - 1) create players
        [Route("api/players/createplayer")]
        [HttpPost]
        public IHttpActionResult Createplayer(List<string> player)
        {
            //000 get the team
            var team = player[0];

            //001 clear the name and surname
            var index = player[1].IndexOf(",");
            var lastname = player[1].Substring(0, index).ToLower();
            var playerlastname = lastname.First().ToString().ToUpper() + lastname.Substring(1); //Made the FirstLetter capital bcs I couldnt find the player- always null
            //the problem is i have different players in the db!
            index++;
            var name = player[1].Substring(index).Trim().ToLower();
            var playername = name.First().ToString().ToUpper() + name.Substring(1);

            //003 Get the position
            var positionNum = Enumerations.Position.Center;
            var position = player[3];

            if (position == Enumerations.Position.Forward.ToString())
            { positionNum = Enumerations.Position.Forward; }
            else if (position == Enumerations.Position.Center.ToString())
            { positionNum = Enumerations.Position.Center; }
            else
            { positionNum = Enumerations.Position.Guard; };
            //004 create player with profile within it 
            var playerIn = new Player
            {
                Firstname = playername,
                Lastname = playerlastname,
                Profile = new Models.Profile
                {
                    Country = player[5],
                    Country3Code = player[7], //need to fill it in!
                    Position = positionNum
                },
                Team = context.Teams.SingleOrDefault(t => t.Name == team),
                FantasyPoints = 100,
                Price = 20
            };
            context.Players.Add(playerIn);
            context.SaveChanges();

            return Ok();
        }



        // POST: /api/players - 2) create players stats  
        [HttpPost]
        [Route("api/players/createplayerstats")]
        public IHttpActionResult CreatePlayerStats(List<string> playerstats)
        {
            // initializing all stat-holding int variables
            int twoPointMadeP, twoPointAttemptedP, threePointMadeP, threePointAttemptedP, freeThrowsAttemptedP, freeThrowsMadeP,
                offensiveReboundsP, defensiveReboundsP, assistsP, stealsP, turnoversP, blocksMadeInFavorP, blocksReceivedAgainstP,
                foulsMadeP, foulsReceivedP = 0;

            // initializing an index to use within the strings
            int index = 0;

            ////firstname & lastname
            
            index = playerstats[1].IndexOf(",");
            var lastname = playerstats[1].Substring(0, index).ToLower();
            var playerlastname = lastname.First().ToString().ToUpper() + lastname.Substring(1); //Made the FirstLetter capital bcs I couldnt find the player- always null

            index++; // to get from index+1 index of the array name:

            var name = playerstats[1].Substring(index).Trim().ToLower();
            var playername = name.First().ToString().ToUpper() + name.Substring(1);

            //get player from db
            var player = context.Players
                .Include(p => p.Profile)
                .Include(p => p.Team)
                .SingleOrDefault(p => p.Firstname == playername && p.Lastname == lastname);
            // Possiblity to substitute above method with var player = ReturnPlayer(playerstats); - but needed to throw exception here 

            if (player == null)
                throw new HttpRequestException();

            int round = int.Parse(playerstats[19]);
            // get Game
            var gameIn = context.Games
                 .Where(g => g.Round == round)
                 .Where(g => g.GuestTeamID == player.TeamID || g.HomeTeamID == player.TeamID)
                 .SingleOrDefault();

            // BIG IF - if element of index [2] = "DNP" => player did not play | no stats
            if (!(playerstats[2] == "DNP"))
            {
                //now going for stats with order of the json:
                #region
                //1// FG2 M/A - element index 4
                if (string.IsNullOrEmpty(playerstats[4]))
                {
                    twoPointMadeP = 0;
                    twoPointAttemptedP = 0;
                }
                else
                {
                    index = playerstats[4].IndexOf("/");
                    twoPointMadeP = int.Parse(playerstats[4].Substring(0, index));
                    index++;
                    twoPointAttemptedP = int.Parse(playerstats[4].Substring(index));
                }


                //2// FG3 M/A - element index 5
                if (string.IsNullOrEmpty(playerstats[5]))
                {
                    threePointMadeP = 0;
                    threePointAttemptedP = 0;
                }
                else
                {
                    index = playerstats[5].IndexOf("/");
                    threePointMadeP = int.Parse(playerstats[5].Substring(0, index));
                    index++;
                    threePointAttemptedP = int.Parse(playerstats[5].Substring(index));
                }

                //3// Free Throws M/A - element index 6
                if (string.IsNullOrEmpty(playerstats[6]))
                {
                    freeThrowsMadeP = 0;
                    freeThrowsAttemptedP = 0;
                }
                else
                {
                    index = playerstats[6].IndexOf("/");
                    freeThrowsMadeP = int.Parse(playerstats[6].Substring(0, index));
                    index++;
                    freeThrowsAttemptedP = int.Parse(playerstats[6].Substring(index));
                }

                //4// Offensive Rebounds - element index 7
                if (string.IsNullOrEmpty(playerstats[7]))
                {
                    offensiveReboundsP = 0;
                }
                else
                {
                    offensiveReboundsP = int.Parse(playerstats[7]);
                }

                //5// Defensive Rebounds - element index 8
                if (string.IsNullOrEmpty(playerstats[8]))
                {
                    defensiveReboundsP = 0;
                }
                else
                {
                    defensiveReboundsP = int.Parse(playerstats[8]);
                }

                //6// Assists - element index 10
                if (string.IsNullOrEmpty(playerstats[10]))
                {
                    assistsP = 0;
                }
                else
                {
                    assistsP = int.Parse(playerstats[10]);
                }

                //7// Steals - element index 11
                if (string.IsNullOrEmpty(playerstats[11]))
                {
                    stealsP = 0;
                }
                else
                {
                    stealsP = int.Parse(playerstats[11]);
                }

                //8// To Turnovers - element index 12
                if (string.IsNullOrEmpty(playerstats[12]))
                {
                    turnoversP = 0;
                }
                else
                {
                    turnoversP = int.Parse(playerstats[12]);
                }

                //9// In Favor Blocks - element index 13
                if (string.IsNullOrEmpty(playerstats[13]))
                {
                    blocksMadeInFavorP = 0;
                }
                else
                {
                    blocksMadeInFavorP = int.Parse(playerstats[13]);
                }

                //10// In Favor Against - element index 14
                if (string.IsNullOrEmpty(playerstats[14]))
                {
                    blocksReceivedAgainstP = 0;
                }
                else
                {
                    blocksReceivedAgainstP = int.Parse(playerstats[14]);
                }

                //11// Fouls Made/Commited - element index 15
                if (string.IsNullOrEmpty(playerstats[15]))
                {
                    foulsMadeP = 0;
                }
                else
                {
                    foulsMadeP = int.Parse(playerstats[15]);
                }

                //12// Fouls Received - element index 16
                if (string.IsNullOrEmpty(playerstats[16]))
                {
                    foulsReceivedP = 0;
                }
                else
                {
                    foulsReceivedP = int.Parse(playerstats[16]);
                }
                #endregion


                // Creating PlayerStat

                var playerstatsIn = new PlayerStat
                {
                    PlayerID = player.ID, // needs the id NOT the object Player = player, - same goes for Game
                    TwoPointMade = twoPointMadeP,
                    TwoPointAttempted = twoPointAttemptedP,
                    ThreePointMade = threePointMadeP,
                    ThreePointAttempted = threePointAttemptedP,
                    FreeThrowMade = freeThrowsMadeP,
                    FreeThrowAttempted = freeThrowsAttemptedP,
                    OffensiveRebounds = offensiveReboundsP,
                    DefensiveRebounds = defensiveReboundsP,
                    Assists = assistsP,
                    Steals = stealsP,
                    Turnovers = turnoversP,
                    BlocksMade = blocksMadeInFavorP,
                    BlocksReceived = blocksReceivedAgainstP,
                    FoulsMade = foulsMadeP,
                    FoulsReceived = foulsReceivedP,
                    Dnp = false,
                    GameID = gameIn.ID
                };
                player.AddStatsToPlayer(playerstatsIn);
               
            }// end of if element 2 ="DNP"
            else
            {
                var playerstatsIn = new PlayerStat
                {
                    PlayerID = player.ID,
                    TwoPointMade = 0,
                    TwoPointAttempted = 0,
                    ThreePointMade = 0,
                    ThreePointAttempted = 0,
                    FreeThrowMade = 0,
                    FreeThrowAttempted = 0,
                    OffensiveRebounds = 0,
                    DefensiveRebounds = 0,
                    Assists = 0,
                    Steals = 0,
                    Turnovers = 0,
                    BlocksMade = 0,
                    BlocksReceived = 0,
                    FoulsMade = 0,
                    FoulsReceived = 0,
                    Dnp = true,
                    GameID = gameIn.ID
                };

                player.AddStatsToPlayer(playerstatsIn);               
            }


            context.SaveChanges();
            return Ok();
        }



        // POST: /api / players / updateteamsdata / {id(round) : int } 3) get the data in the db of user's fantasy team
        [Route("api/players/updateteamsdata/{id:int}")]
        [HttpPost]
        public IHttpActionResult UpdateFTeamsData(int id)
        {
            var teams = context.EightPlayerTeams
                .Include(ept => ept.Players)
                .Include(ept => ept.Players.Select(p => p.PlayerStats))
                .Include(ept => ept.Players.Select(p => p.PlayerStats.Select(ps => ps.Game)))
                .ToList();

            teams.ForEach(t => t.UpdateTeam(id));
            context.SaveChanges();

            return Ok(teams);
        }

    }
}
