using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FantasyEuroleague.Interfaces;

namespace FantasyEuroleague.Models
{
    public class PlayerStat:IStat
    {
        public int GameID { get; set; }
        public int PlayerID { get; set; }
        public Game Game { get; set; }
        public Player Player { get; set; }
        public bool Dnp { get; set; } 
        public int TwoPointMade { get; set; }
        public int TwoPointAttempted { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        public decimal TwoPointPercentage { get { return (TwoPointAttempted == 0 ? 0 : (decimal)TwoPointMade / TwoPointAttempted * 100); } }
        public int ThreePointMade { get; set; }
        public int ThreePointAttempted { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        public decimal ThreePointPercentage { get { return (ThreePointAttempted == 0 ? 0 : (decimal)ThreePointMade / ThreePointAttempted * 100); } }
        public int FreeThrowMade { get; set; }
        public int FreeThrowAttempted { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        public decimal FreeThrowPercentage { get { return (FreeThrowAttempted == 0 ? 0 : (decimal)FreeThrowMade / FreeThrowAttempted * 100); } }
        public int Points { get { return TwoPointMade * 2 + ThreePointMade * 3 + FreeThrowMade; } }
        public int Assists { get; set; }
        public int DefensiveRebounds { get; set; }
        public int OffensiveRebounds { get; set; }
        public int Rebounds { get { return DefensiveRebounds + OffensiveRebounds; } }
        public int Steals { get; set; }
        public int Turnovers { get; set; }
        public int BlocksMade { get; set; }
        public int BlocksReceived { get; set; }
        public int FoulsMade { get; set; }
        public int FoulsReceived { get; set; }
        public int ShotsMissed { get { return TwoPointAttempted - TwoPointMade + ThreePointAttempted - ThreePointMade + FreeThrowAttempted - FreeThrowMade; } }
        public int PIR { get { return Points + Rebounds + Assists + BlocksMade + Steals + FoulsReceived - (Turnovers + FoulsMade + BlocksReceived + ShotsMissed); } }

        public PlayerStat()
        {

        }

        //protected PlayerStat()
        //{ }

        protected PlayerStat(Player player, Game game)
        {
            Dnp = true;
            Player = player;
            Game = game;    
        }

        protected PlayerStat(Player player,Game game,int twoPtsAtt,int twoPtsM,int threePtsAtt,int threePtsM,int ftAtt,int ftM,int ass,int defRbs,int offRbs, int st, int blksM,int blksR, int foulCom,int foulRec)
        {
            if (player == null || game == null)
                throw new ArgumentNullException();

            
            Player = player;
            Game = game;
            TwoPointAttempted = twoPtsAtt;
            TwoPointMade = twoPtsM;
            ThreePointAttempted = threePtsAtt;
            ThreePointMade = threePtsM;
            FreeThrowAttempted = ftAtt;
            FreeThrowMade = ftM;
            Assists = ass;
            DefensiveRebounds = defRbs;
            OffensiveRebounds = offRbs;
            Steals = st;
            BlocksMade = blksM;
            BlocksReceived = blksR;
            FoulsMade = foulCom;
            FoulsReceived = foulRec;
        }

        

        public static PlayerStat RandomGuardStats(Player player,Game game, Random r, int level)
        {

            int twoPtsAtt = (level == 0) ? r.Next(2) * r.Next(2) : r.Next(level) + r.Next(level);   
            int twoPtsMade = Math.Max(r.Next(twoPtsAtt + 1),r.Next(twoPtsAtt + 1));
            int threePtsAtt = (level == 0) ? r.Next(2) * r.Next(2) : r.Next(level + 1) + r.Next(level + 1);
            int threePtsMade = Math.Min(r.Next(threePtsAtt + 1), r.Next(threePtsAtt + 1));
            int freeThrowsAtt = (level == 0) ? r.Next(2) * r.Next(2) : r.Next(level);
            int freeThrowsMade = Math.Max(r.Next(freeThrowsAtt + 1), r.Next(freeThrowsAtt + 1));
            int assists = (level == 0) ? r.Next(2) * r.Next(2) : r.Next(level) + r.Next(level);
            int defRebs = (level <= 2) ? r.Next(2) * r.Next(2) : r.Next(level - 2) + r.Next(level - 2);
            int offRebs = (level == 0) ? 0 : r.Next(3);
            int steals = (level <= 2) ? 0 : Math.Min(r.Next(level / 2), r.Next(level / 2));
            int blocksMade = r.Next(2);
            int blocksReceived = (level == 0) ? 0 : Math.Min(r.Next(2), r.Next(2));
            int fouls = r.Next(6);
            int foulsReceived = r.Next(freeThrowsAtt / 2, freeThrowsAtt + 2);

            return new PlayerStat(player,game,twoPtsAtt, twoPtsMade, threePtsAtt, threePtsMade, freeThrowsAtt, freeThrowsMade, 
                                  assists, defRebs, offRebs, steals, blocksMade, blocksReceived, fouls, foulsReceived);
        }

        public static PlayerStat RandomForwardStats(Player player, Game game, Random r, int level)
        {

            int twoPtsAtt = (level == 0) ? r.Next(2) * r.Next(2) : r.Next(level) + r.Next(level);
            int twoPtsMade = Math.Max(r.Next(twoPtsAtt + 1), r.Next(twoPtsAtt + 1));
            int threePtsAtt = (level == 0) ? r.Next(2) * r.Next(2) : r.Next(level) + r.Next(level);
            int threePtsMade = Math.Min(r.Next(threePtsAtt + 1), r.Next(threePtsAtt + 1));
            int freeThrowsAtt = (level == 0) ? r.Next(2) * r.Next(2) : r.Next(level + 4);
            int freeThrowsMade = Math.Max(r.Next(freeThrowsAtt + 1), r.Next(freeThrowsAtt + 1));
            int assists = (level <= 2) ? r.Next(2) * r.Next(2) : r.Next(level/2);
            int defRebs = (level == 0) ? r.Next(2) * r.Next(2) : r.Next(level) + r.Next(level);
            int offRebs = (level == 0) ? r.Next(2) * r.Next(2) : r.Next(level / 2) + r.Next(level / 2);
            int steals = (level <= 2) ? 0 : Math.Min(r.Next(level / 2), r.Next(level / 2));
            int blocksMade = r.Next(2);
            int blocksReceived = (level == 0) ? 0 : Math.Min(r.Next(2), r.Next(2));
            int fouls = r.Next(6);
            int foulsReceived = r.Next(freeThrowsAtt / 2, freeThrowsAtt + 2);

            return new PlayerStat(player, game, twoPtsAtt, twoPtsMade, threePtsAtt, threePtsMade, freeThrowsAtt, freeThrowsMade,
                                  assists, defRebs, offRebs, steals, blocksMade, blocksReceived, fouls, foulsReceived);
        }
        public static PlayerStat RandomCenterStats(Player player, Game game, Random r, int level)
        {

            int twoPtsAtt = (level == 0) ? r.Next(2) * r.Next(2) : r.Next(level + 2) + r.Next(level + 2);
            int twoPtsMade = Math.Max(r.Next(twoPtsAtt + 1), r.Next(twoPtsAtt + 1));
            int threePtsAtt = (level <= 4)? 0 : r.Next(3) * r.Next(3);
            int threePtsMade = Math.Min(r.Next(threePtsAtt + 1), r.Next(threePtsAtt + 1));
            int freeThrowsAtt = (level == 0) ? r.Next(2) * r.Next(2) : r.Next(level);
            int freeThrowsMade = Math.Max(r.Next(freeThrowsAtt + 1), r.Next(freeThrowsAtt + 1)); 
            int assists = (level <= 4) ? r.Next(2) * r.Next(2) : r.Next(level/2 +1);
            int defRebs = (level == 0) ? r.Next(2) * r.Next(2) : r.Next(level + 2) + r.Next(level + 2);
            int offRebs = (level == 0) ? r.Next(2) * r.Next(2) : r.Next(level - 2) + r.Next(level - 2);
            int steals = (level <= 4) ? 0 : Math.Min(r.Next(level / 2), r.Next(level / 2));
            int blocksMade = (level <= 4) ? r.Next(2) * r.Next(2) : r.Next(level / 2);
            int blocksReceived = Math.Min(r.Next(2), r.Next(2));
            int fouls = r.Next(6);
            int foulsReceived = r.Next(freeThrowsAtt / 2, freeThrowsAtt + 2);

            return new PlayerStat(player, game, twoPtsAtt, twoPtsMade, threePtsAtt, threePtsMade, freeThrowsAtt, freeThrowsMade,
                                  assists, defRebs, offRebs, steals, blocksMade, blocksReceived, fouls, foulsReceived);
        }


    }
}