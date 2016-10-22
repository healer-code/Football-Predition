using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Football_Prediction.Model;


namespace Football_Prediction.Utility
{
    public class Utility
    {
        public static async void GetAllData()
        {


            Dictionary<string, Data> DoAn = new Dictionary<string, Data>();

            for (int mg = 0; mg < Common.Common.muagia.Length; mg++)
            {
                Console.WriteLine("GET DATA" + Common.Common.muagia[mg]);
                string data = string.Empty;


                for (int i = 1; i <= 38; i++)
                {
                    DoAn.Clear();
                    data = await GetDataRound(Common.Common.muagia[mg], i.ToString());


                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(data);


                    var nodeRoot = doc.DocumentNode.SelectSingleNode("//*[@id='site']/div[3]/div[1]/div/div[7]/div/table[1]");

                    var nodeList = nodeRoot.SelectNodes("./tr");

                    nodeList.Remove(0);

                    if (nodeList != null)
                    {
                        // Debug.WriteLine(nodeList.Count);
                        int rank = 1;
                        foreach (var item in nodeList)
                        {
                            var nodeDetailForTeam = item.SelectNodes("./td");

                            string nameTeam = nodeDetailForTeam[2].InnerText.Trim();

                            string rankT = rank.ToString();
                            rank++;
                            string win = nodeDetailForTeam[4].InnerText.Trim();
                            string draw = nodeDetailForTeam[5].InnerText.Trim();
                            string lost = nodeDetailForTeam[6].InnerText.Trim();

                            string nghia = rankT + "," + win + "," + draw + "," + lost;

                            DoAn.Add(nameTeam, new Data(nghia));
                            //Debug.WriteLine(nameTeam + " " + rankT + " " + win + " " + draw + "  " + lost + Environment.NewLine);
                        }
                    }



                    var doiDauRoot = doc.DocumentNode.SelectSingleNode("//*[@id='site']/div[3]/div[1]/div/div[4]/div/table");


                    //Debug.WriteLine(DoAn.Count);
                    var listNodeDoiDau = doiDauRoot.SelectNodes("./tr");

                    Debug.WriteLine(listNodeDoiDau.Count);


                    if (listNodeDoiDau != null)
                    {
                        foreach (var item in listNodeDoiDau)
                        {
                            var listDetail = item.SelectNodes("./td");
                            string teamA = listDetail[2].InnerText.Trim();
                            string teamB = listDetail[4].InnerText.Trim();

                            string ketqua = listDetail[5].InnerText.Trim();

                            string value = string.Empty;
                            if (ketqua == "-:-")
                            {
                                value = "?";
                            }
                            else
                            {
                                int left = int.Parse(ketqua[0].ToString());
                                int right = int.Parse(ketqua[2].ToString());

                                if (left > right)
                                {
                                    value = "1";
                                }
                                else if (left < right)
                                {
                                    value = "0";
                                }
                                else if (left == right)
                                {
                                    value = "0.5";
                                }

                            }

                            Data objTeamA = null;
                            Data objTeamB = null;

                            if (DoAn.ContainsKey(teamA))
                            {
                                objTeamA = DoAn[teamA] as Data;
                            }

                            if (DoAn.ContainsKey(teamB))
                            {
                                objTeamB = DoAn[teamB] as Data;
                            }

                            StringBuilder strBuilder = new StringBuilder();

                            string haha = objTeamA.data + "," + objTeamB.data + "," + value;
                            //Debug.WriteLine(teamA + "-" + teamB);
                            //Debug.WriteLine(haha);


                            Common.Common.Datas.Add(new Match(teamA + "-" + teamB, Common.Common.muagia[mg], i.ToString(), haha));
                            // Debug.Write(teamA + "-" + teamB);

                            if (value == "1")
                                value = "0";
                            else if (value == "0")
                                value = "1";

                            //Debug.WriteLine(teamB + "--" + teamA);
                            //Debug.WriteLine(nguoc);

                            string nguoc = objTeamB.data + "," + objTeamA.data + "," + value;


                            Common.Common.Datas.Add(new Match(teamB + "-" + teamA, Common.Common.muagia[mg], i.ToString(), nguoc));
                            // Debug.Write(teamB + "-" + teamA);

                        }

                    }

                }
            }

        }
        public static async Task<string> GetDataRound(string muagia, string round)
        {
            string data = string.Empty;
            string url = string.Format("http://www.worldfootball.net/schedule/eng-premier-league-{0}-spieltag/{1}/", muagia, round);
            data = await WebHelper.GetStringFromRequest(url);
            return data;
        }

        public static string MuaGiai(int item)
        {
            //760, 1520, 2280, 3040, 3800, 4560 
            if (item == 760)
                return "2010-2011";
            if (item == 1520)
                return "2011-2012";
            if (item == 2280)
                return "2012-2013";
            if (item == 3040)
                return "2013-2014";
            if (item == 3800)
                return "2014-2015";
            if (item == 4560)
                return "2015-2016";
            return "error";
        }

        public static string GetOutput(List<Match> jList, int index, string roundNow, string input)
        {
            if (roundNow == "1")
                return "0,0,0,0,0,0,0,0," + GetLastDigit(jList[index].Data);

            string resultOutput = string.Empty;
            string leftTeam = string.Empty, rightTeam = string.Empty;
            GetNameTeams(input, ref leftTeam, ref rightTeam);

            string round = (int.Parse(roundNow) - 1).ToString();

            string _leftTeam = string.Empty, none = string.Empty;

            int indexTemp = 0;
            bool gotLeft = false, gotRight = false;
            for (int i = index; i >= 0; i--)
            {
                if (jList[i].Round == round)
                {
                    GetNameTeams(jList[i].Name, ref _leftTeam, ref none);
                    if (!gotLeft && leftTeam == _leftTeam)
                    {
                        //resultOutput = jList[i].Data.Substring(0, 7);
                        resultOutput = GetFirst4Ditgit(jList[i].Data);
                        gotLeft = true;
                    }
                    else if (!gotRight && rightTeam == _leftTeam)
                    {
                        indexTemp = i;
                        gotRight = true;
                    }

                    if (gotLeft && gotRight)
                        break;
                }
            }

            //resultOutput += jList[indexTemp].Data.Substring(0, 7);
            resultOutput += GetFirst4Ditgit(jList[indexTemp].Data);

            resultOutput += GetLastDigit(jList[index].Data);

            return resultOutput;
        }

        public static string GetLastDigit(string data)
        {
            data = data.Trim();
            if (data[data.Length - 1] == '5')
                return "0.5";

            return data[data.Length - 1].ToString();
        }

        public static string GetFirst4Ditgit(string data)
        {
            string result = string.Empty;

            int count = 0;
            int i = 0;
            while (count < 4)
            {
                if (data[i] != ',')
                    result += data[i];
                else
                {
                    result += ',';
                    count++;
                }

                i++;
            }

            return result;
        }

        public static void GetNameTeams(string input, ref string leftTeam, ref string rightTeam)
        {
            int i = 0;
            leftTeam = rightTeam = string.Empty;
            while (input[i] != '-')
            {
                leftTeam += input[i];
                i++;
            }
            i++;
            while (i < input.Length)
            {
                rightTeam += input[i];
                i++;
            }
        }

        public static void FindIndex(int indexBegin, int indexFinish, List<Match> jList, string input, ref int index1, ref int index2)
        {
            index1 = index2 = -1;
            int count = 0;
            string l, r;
            l = r = string.Empty;
            GetNameTeams(input, ref l, ref r);
            string daoNguoc = r + '-' + l;
            string round = string.Empty;
            for (int i = indexBegin; i <= indexFinish; i++)
            {
                if (jList[i].Name == input)
                {
                    index1 = i;
                    round = jList[i].Round;
                    break;
                }
            }

            for (int i = index1 + 1; i <= indexFinish; i++)
            {
                if (jList[i].Name == input)
                {
                    if (jList[i].Round != round)
                    {
                        index2 = i;
                        break;
                    }
                }
            }
        }

        public static string ReadContentFromFile()
        {
            string json = string.Empty;
            string path = System.IO.Directory.GetCurrentDirectory();
            path = System.IO.Directory.GetParent(path).FullName;
            path = System.IO.Directory.GetParent(path).FullName;

            string filePath = path + "/Assets/Data/" + "Result_Final.txt";

            using(FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using(StreamReader reader = new StreamReader(fileStream))
                {
                   json =  reader.ReadToEnd();
                }
            }
            

            
            return json;
        }

        public void Test()
        {

            //man united - liver 
            //vi tri, so tran thang hoa thua, so ban thang, thung luoi
            double[][] input_patterns = {
                                           /*
            new double [8]{1, 9, 7, 0, 2, 10, 2, 4},
            new double [8]{1, 21, 10, 3, 3, 18, 10, 6},
            new double [8]{2, 15, 3, 3, 5, 11, 3, 7},
            new double [8]{2, 2, 0, 0, 15, 0, 1, 1},
            new double [8]{1, 27, 3, 4, 4, 18, 9, 7},
            new double [8]{2, 7, 0, 2, 6, 4, 3, 2},
            new double [8]{8, 5, 2, 3, 1, 8, 1, 1},
            new double [8]{7, 12, 5, 8, 2, 17, 4, 4},
            new double [8]{7, 4, 4, 3, 6, 4, 5, 2},
                                            * */
 
            //new double [8]{4, 20, 8, 8, 3, 21, 8, 7},
            /* Sunderland - Liver
            new double [8]{3,3, 2, 1, 1, 5, 0, 1},
            new double [8]{2, 11, 2, 5, 3, 10, 4, 4},
            new double [8]{7, 4, 1, 4, 3, 6, 1, 2},
            new double [8]{3, 20, 4, 10, 6, 16, 9, 9},
            new double [8]{1, 4, 1, 0, 5, 2, 3, 0},
            new double [8]{5, 10, 7, 5, 3, 12, 6, 4},
            new double [8]{1, 11, 2, 3, 3, 10, 3, 3},
            new double [8]{4, 19, 6, 5, 2, 20, 6, 4},
            new double [8]{4, 2, 4, 0, 1, 5, 1, 0},
            new double [8]{2, 20, 7, 6, 1, 24, 7, 2},
             */
                                       };
            double[][] ideal_output ={
                                         /*
            new double[1]{0},
            new double[1]{1},
            new double[1]{1},
            new double[1]{1},
            new double[1]{0},
            new double[1]{1},
            new double[1]{1},
            new double[1]{0.5},
            new double[1]{1},
                                          * */
            /* Sunderland - Liverpool 
            new double[1]{0},
            new double[1]{1},
            new double[1]{1},
            new double[1]{0.5},
            new double[1]{0},
            new double[1]{0},
            new double[1]{0.5},
            new double[1]{0},
            new double[1]{0},
            new double[1]{0.5},
             */
                                      };

            ANN_BackPropagation neutron_network = new ANN_BackPropagation(0.7, 100000, 8, 8, 1);
            //Console.WriteLine("Training network. Please wait...");

            for (int i = 0; i < neutron_network.Time_Training; i++)
                neutron_network.TrainingData(input_patterns, ideal_output);

            Console.WriteLine("Train finished. Press key to continue...");
            // Console.ReadKey(true);

            for (int i = 0; i < input_patterns.Length; i++)
            {
                double[] out_running = neutron_network.TestingData(input_patterns[i]);
                Console.WriteLine("result {0:0.0000}", out_running[0]);
            }

            Console.WriteLine("SIMILAR DATA TEST");
            double[] result = neutron_network.TestingData(2, 20, 7, 6, 1, 24, 7, 2);
            Console.WriteLine("Result predition Team A percent win  {0:0.000}", result[0]);
            //Console.ReadKey(true);
        }





    }
}
