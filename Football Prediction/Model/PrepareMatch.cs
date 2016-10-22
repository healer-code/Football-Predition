using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Prediction.Model
{
    public class PrepareMatch
    {
        public string Name { get; set; }
        public string NameTeamA { get; set; }
        public string NameTeamB { get; set; }
        public string ThumbTeamA { get; set; }
        public string ThumbTeamB { get; set; }
        public string DateTime { get; set; }
        public string HourTime { get; set; }

        public string Round { get; set; }

        public PrepareMatch()
        {

        }

        public PrepareMatch(string teamA, string teamB, string datetime, string hourtime, string round)
        {
            this.Name = teamA + "-" + teamB;
            this.NameTeamA = teamA;
            this.ThumbTeamA = Common.Common.TeamMap.KeyMap[teamA];
            this.ThumbTeamB = Common.Common.TeamMap.KeyMap[teamB];
            this.NameTeamB = teamB;
            this.DateTime = datetime;
            this.HourTime = hourtime;
            this.Round = round;
            
        }
    }
}
