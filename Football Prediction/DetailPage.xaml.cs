using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Football_Prediction.Model;

namespace Football_Prediction
{
    /// <summary>
    /// Interaction logic for DetailPage.xaml
    /// </summary>
    public partial class DetailPage : Window
    {
        const string WIN = "WIN";
        const string LOST = "LOST";
        private System.ComponentModel.BackgroundWorker BackgroundWorker = new System.ComponentModel.BackgroundWorker();
        DuDoanTiSo model = null;
        int countRow = 0;
        double[][] input_patterns = null;
        double[][] ideal_output = null;
        string teamA = string.Empty;
        string teamB = string.Empty;
        double resultFinal = 0;

        public DetailPage()
        {
            InitializeComponent();
            Loaded += DetailPage_Loaded;
        }

        void DetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            string name = Common.Common.PassData.Name;
            teamA = Common.Common.PassData.NameTeamA;
            teamB = Common.Common.PassData.NameTeamB;

            string round = Common.Common.PassData.Round;
            string date = Common.Common.PassData.DateTime;
            string hour = Common.Common.PassData.HourTime;

            string srcPathA = Common.Common.TeamMap.KeyMap[teamA];
            string srcPathB = Common.Common.TeamMap.KeyMap[teamB];

            BitmapImage bmA = new BitmapImage(new Uri(srcPathA, UriKind.RelativeOrAbsolute));
            BitmapImage bmB = new BitmapImage(new Uri(srcPathB, UriKind.RelativeOrAbsolute));


            txtTeamA.Text = teamA;
            txtTeamB.Text = teamB;
            txtTime.Text = hour;
            txtDate.Text = round;
            srcA.Source = bmA;
            srcB.Source = bmB;

            ObservableCollection<DetailMatch> datas = new ObservableCollection<DetailMatch>();


            foreach (var item in Common.Common.ListDataForMatch.DuDoanTiSo)
            {
                if (item.trandau == name)
                {
                    model = item;
                    break;
                }
            }

            if (model != null)
            {
                foreach (var item in model.data_train)
                {

                    var temp = item.data.Split(',').ToArray();

                    string rankTeamA = temp[0];
                    string nWinA = temp[1];
                    string nDrawA = temp[2];
                    string nLostA = temp[3];

                    string rankTeamB = temp[4];
                    string nWinB = temp[5];
                    string nDrawB = temp[6];
                    string nLostB = temp[7];
                    string result = temp[8];

                    string roundMatch = item.round;
                    string muagiai = item.muagia;

                    datas.Add(new DetailMatch(rankTeamA, nWinA, nDrawA, nLostA, rankTeamB, nWinB, nDrawB, nLostB, result, roundMatch, muagiai));


                }
            }
            lbDetailMatchs.ItemsSource = datas;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MainWindow mainWindows = new MainWindow();
            mainWindows.Show();

        }

        private void btnTrain_Click(object sender, RoutedEventArgs e)
        {
            if (model != null)
            {
                countRow = model.data_train.Count;
                input_patterns = new double[countRow][];
                ideal_output = new double[countRow][];
                for (int i = 0; i < countRow; i++)
                {
                    input_patterns[i] = new double[8];
                    ideal_output[i] = new double[1];
                }


                for (int i = 0; i < countRow; i++)
                {
                    var temp = model.data_train[i].data.Split(',').ToArray();


                    for (int j = 0; j < 8; j++)
                    {
                        input_patterns[i][j] = double.Parse(temp[j]);
                        ideal_output[i][0] = double.Parse(temp[temp.Length - 1]);
                    }
                }
            }

            var data_test = model.data_test.Split(',').ToArray();

            int rankTeamA = int.Parse(data_test[0]);
            int nWinA = int.Parse(data_test[1]);
            int nDrawA = int.Parse(data_test[2]);
            int nLostA = int.Parse(data_test[3]);

            int rankTeamB = int.Parse(data_test[4]);
            int nWinB = int.Parse(data_test[5]);
            int nDrawB = int.Parse(data_test[6]);
            int nLostB = int.Parse(data_test[7]);

            ANN_BackPropagation neutron_network = new ANN_BackPropagation(0.7, 100000, 8, 8, 1);
            //Console.WriteLine("Training network. Please wait...");


            for (int i = 0; i < neutron_network.Time_Training; i++)
                neutron_network.TrainingData(input_patterns, ideal_output);


            for (int i = 0; i < input_patterns.Length; i++)
            {
                double[] out_running = neutron_network.TestingData(input_patterns[i]);
                Console.WriteLine("result {0:0.0000}", out_running[0]);
            }

            Console.WriteLine("SIMILAR DATA TEST");
            double[] result = neutron_network.TestingData(19,3,3,12,8,7,6,5);
            Console.WriteLine("Result predition Team A percent win  {0:0.000}", result[0]);

            txtWeight.Text = result[0].ToString();
            resultFinal = result[0];
        }

        private void btnDuDoan_Click(object sender, RoutedEventArgs e)
        {
            if ((resultFinal - 0.5 < 0.05 && resultFinal - 0.5 >= 0) || (resultFinal == 0.5))
            {
                txtPredictionWin.Text = "DRAW";
            }
            else if (resultFinal > 0.5)
            {
                txtPredictionWin.Text = teamA + " " + WIN;
                txtPredictionLost.Text = teamB + " " + LOST;
            }

            else if (resultFinal < 0.5)
            {
                txtPredictionWin.Text = teamB + " " + WIN;
                txtPredictionLost.Text = teamA + " " + LOST;
            }
        }
    }
}
