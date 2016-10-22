using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Prediction.Model
{
    public class DataTrain
    {
        public string muagia { get; set; }
        public string round { get; set; }
        public string data { get; set; }

        public DataTrain()
        {

        }

        public DataTrain(string muagiai, string round, string data)
        {
            this.muagia = muagiai;
            this.round = round;
            this.data = data;
        }
    }

    public class DuDoanTiSo
    {
        public string trandau { get; set; }
        public List<DataTrain> data_train { get; set; }
        public string data_test { get; set; }

        public DuDoanTiSo()
        {
            this.data_test = string.Empty;
        }
    }

    public class RootObject
    {
        public List<DuDoanTiSo> DuDoanTiSo { get; set; }
    }
}
