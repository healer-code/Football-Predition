using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Prediction.Model
{
    public class DetailMatch
    {
        public string rankA { get; set; }
        public string  winA { get; set; }
        public string drawA { get; set; }
        public string lostA { get; set; }
        public string rankB { get; set; }
        public string winB { get; set; }
        public string drawB { get; set; }
        public string lostB { get; set; }
        public string result { get; set; }
        public string Round { get; set; }

        public string Muagiai { get; set; }

        public DetailMatch()
        {

        }

        public DetailMatch(string rankA, string winA, string drawA, string lostA, string rankB, string winB, string drawB, string lostB, string result, string round, string muagiai)
        {
            this.rankA = rankA;
            this.winA = winA;
            this.drawA = drawA;
            this.lostA = lostA;

            this.rankB = rankB;
            this.winB = winB;
            this.drawB = drawB;
            this.lostB = lostB;

            this.result = result;

            this.Round = round;
            this.Muagiai = muagiai;
        }
    }
}
