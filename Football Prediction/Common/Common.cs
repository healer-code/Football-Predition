using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Football_Prediction.Model;
using Football_Prediction.ViewModel;

namespace Football_Prediction.Common
{
    public class Common
    {

        public static RootObject ListDataForMatch = new RootObject();
        public static ListTeam ListTeam = new ListTeam();
        public static List<Match> Datas = new List<Match>();
        public static string[] muagia = new string[6] { "2010-2011", "2011-2012", "2012-2013", "2013-2014", "2014-2015", "2015-2016" };
        public static TeamMap TeamMap = new TeamMap();
        public static PrepareMatchs PrepareMatchs = new PrepareMatchs();
        public static PrepareMatch PassData;
      
    }
}
