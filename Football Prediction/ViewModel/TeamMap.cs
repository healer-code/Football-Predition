using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Prediction.ViewModel
{
    public class TeamMap
    {
        private const string PATH = "/Assets/Images/";
        public Dictionary<string, string> KeyMap = new Dictionary<string, string>();
        public TeamMap()
        {

        }

        public void InitTeamMap()
        {
            KeyMap["Leicester City"] = PATH + "Leicester City.ico";
            KeyMap["Arsenal FC"] = PATH + "Arsenal.ico";
            KeyMap["Manchester City"] = PATH + "Manchester City.ico";
            KeyMap["Tottenham Hotspur"] = PATH + "Tottenham Hotspur.ico";
            KeyMap["Manchester United"] = PATH + "Manchester United.ico";
            KeyMap["Crystal Palace"] = PATH + "Crystal Palace.png";
            KeyMap["Watford FC"] = PATH + "Watford FC.ico";
            KeyMap["West Ham United"] = PATH + "West Ham United.ico";
            KeyMap["Liverpool FC"] = PATH + "Liverpool FC.ico";
            KeyMap["Everton FC"] = PATH + "Everton.ico";
            KeyMap["Stoke City"] = PATH + "Stoke City.ico";
            KeyMap["Southampton FC"] = PATH + "Southampton FC.ico";
            KeyMap["West Bromwich Albion"] = PATH + "West Bromwich Albion.ico";
            KeyMap["AFC Bournemouth"] = PATH + "AFC-Bournemouth.png";
            KeyMap["Chelsea FC"] = PATH + "Chelsea.ico";
            KeyMap["Norwich City"] = PATH + "Norwich City.ico";
            KeyMap["Newcastle United"] = PATH + "Newcastle United.ico";
            KeyMap["Swansea City"] = PATH + "Swansea City.ico";
            KeyMap["Sunderland AFC"] = PATH + "Sunderland.ico";
            KeyMap["Aston Villa"] = PATH + "Aston Villa.ico";
        }
    }
}
