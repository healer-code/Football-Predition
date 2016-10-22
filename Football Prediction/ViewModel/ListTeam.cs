using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Prediction.ViewModel
{
    public class ListTeam
    {
        public List<string> Matchs = new List<string>();

        public ListTeam()
        {

        }

       
        string[] listName = new string[20]{"AFC Bournemouth", "Arsenal FC",
                "Aston Villa","Chelsea FC","Crystal Palace", "Everton FC",
                "Leicester City", "Liverpool FC", "Manchester City", "Manchester United", 
                "Newcastle United" , "Norwich City","Southampton FC", "Stoke City", "Sunderland AFC",
                "Swansea City", "Tottenham Hotspur", 
                "Watford FC", "West Bromwich Albion", "West Ham United"};


        public void InitListMatch()
        {
            for (int i = 0; i < listName.Length; i++)
            {
                for (int j = 0; j < listName.Length; j++)
                {
                    if (listName[i] != listName[j])
                    {
                        Matchs.Add(listName[i] + "-" + listName[j]);
                    }
                }
            }
        }
    }
    
}
