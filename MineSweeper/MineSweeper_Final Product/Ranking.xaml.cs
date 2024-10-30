using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Data.SqlClient;
using MineSweeper1.DAL;

namespace MineSweeper1
{
    /// <summary>
    /// Ranking.xaml 的交互逻辑
    /// </summary>
    public partial class Ranking : Window
    {
        private PlayerService playerService =new PlayerService();
        Ranking rankingSimple;
        Ranking rankingNormal;
        Ranking rankingHard;
        public Ranking()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;//设置窗口启动位置为屏幕中央
            
        }

        private void ToSimple(object sender, RoutedEventArgs e)
        {   
            RankingWindow?.Close();
            rankingNormal?.Close();
            rankingHard?.Close();
            ConnectToDatabase(this.Simple.Name);
             rankingSimple = new Ranking();
            rankingSimple.RankingBox.ItemsSource = playerService.QueryPlayer("Simple");
            rankingSimple.Show();
        }

        private void ToNarmal(object sender, RoutedEventArgs e)
        {
            RankingWindow?.Close();
            rankingSimple?.Close();
            rankingHard?.Close();
            ConnectToDatabase(this.Normal.Name);
             rankingNormal = new Ranking();
            rankingNormal.RankingBox.ItemsSource = playerService.QueryPlayer("Normal");
            rankingNormal.Show();
        }

        private void ToHard(object sender, RoutedEventArgs e)
        {
            RankingWindow?.Close();
            rankingSimple?.Close();
            rankingNormal?.Close();
            ConnectToDatabase(this.Hard.Name);
             rankingHard = new Ranking();
            rankingHard.RankingBox.ItemsSource = playerService.QueryPlayer("Hard");
            rankingHard.Show();
        }
        private void ConnectToDatabase(string strName)
        {
            SqlConnection conn = null;
            try
            {
                string str = "Data Source=.;Initial Catalog=MineSweeper; Integrated Security = true";
                conn = new SqlConnection(str);
                conn.Open();
                string sql = "SELECT * FROM {0}";
                sql = string.Format(sql,strName);
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:出现异常," + ex.Message);
            }
        }
       
    }
}
