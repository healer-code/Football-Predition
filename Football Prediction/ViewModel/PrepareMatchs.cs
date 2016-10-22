using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Football_Prediction.Model;

namespace Football_Prediction.ViewModel
{
    public class PrepareMatchs
    {
        public bool IsLoaded = false;
        public PrepareMatchs()
        {

        }

        public ObservableCollection<PrepareMatch> Datas = new ObservableCollection<PrepareMatch>();
        public async  void GetMatchPrepare()
        {
            bool isGet = false;
            for (int i = 18; i <= 38; i++)
            {
                if (isGet)
                {
                    break;
                }

                string data =  await Utility.Utility.GetDataRound("2015-2016", i.ToString());
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(data);

                var doiDauRoot = doc.DocumentNode.SelectSingleNode("//*[@id='site']/div[3]/div[1]/div/div[4]/div/table");

                //Debug.WriteLine(DoAn.Count);
                var listNodeDoiDau = doiDauRoot.SelectNodes("./tr");

                string round = "Round " + i.ToString() + ", 2015-2016";

                string ngaydau = string.Empty;
                if (listNodeDoiDau != null)
                {
                    foreach (var item in listNodeDoiDau)
                    {
                        var list_ds = item.SelectNodes("./td");

                        string ketqua = list_ds[5].InnerText.Trim();

                        if (ketqua == "-:-")
                        {
                            isGet = true;
                            string temp = list_ds[0].InnerText.Trim();
                            if (temp != "")
                                ngaydau = temp;

                            string hour = list_ds[1].InnerText.Trim();
                            string teamA = list_ds[2].InnerText.Trim();
                            string teamB = list_ds[4].InnerText.Trim();

                            Datas.Add(new PrepareMatch(teamA, teamB, ngaydau, hour, round));

                        }
                    }
                }
            }
            IsLoaded = true;
        }
    }
}
