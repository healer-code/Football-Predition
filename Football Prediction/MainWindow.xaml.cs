using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Football_Prediction.Model;
using Newtonsoft.Json;

namespace Football_Prediction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Common.Common.ListTeam.InitListMatch();
            Common.Common.TeamMap.InitTeamMap();

            string json = Utility.Utility.ReadContentFromFile();

            Common.Common.ListDataForMatch = JsonConvert.DeserializeObject<RootObject>(json);



            if (!Common.Common.PrepareMatchs.IsLoaded)
                Common.Common.PrepareMatchs.GetMatchPrepare();

            lbMatch.ItemsSource = Common.Common.PrepareMatchs.Datas;

        }

        private void lbMatch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox != null && listBox.SelectedItem != null)
            {
                Common.Common.PassData = listBox.SelectedItem as PrepareMatch;

                this.Hide();
                //string query = string.Format("name={0}&teamA={1}&teamB={2}&round={3}&date={4}&hour={5}", data.Name, data.NameTeamA, data.NameTeamB, data.Round, data.DateTime, data.HourTime);

                DetailPage detailPage = new DetailPage();
                detailPage.Show();
            }

            listBox.SelectedItem = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AboutPage aboutPage = new AboutPage();
            aboutPage.Show();
        }


    }
}
