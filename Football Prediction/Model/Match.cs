using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Prediction.Model
{

   public class Match
    {
        public string MuaGia { get; set; }
        public string Round { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }


        public Match()
        {

        }

        public Match(string name, string muagia, string round, string data)
        {
            this.Name = name;
            this.MuaGia = muagia;
            this.Round = round;
            this.Data = data;

        }
    }
}
