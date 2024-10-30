using System;
using System.Collections.Generic;
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

namespace MineSweeper1
{
    /// <summary>
    /// Loading.xaml 的交互逻辑
    /// </summary>
    public partial class Loading : Window
    {
        //public static Loading loading;
        public Loading()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;//设置窗口启动位置为屏幕中央
        }
        // 定义一个单例类，用于保存需要传递的数据
        private void openMineSweeper(object sender, RoutedEventArgs e)
        {
            //string loading = upName.Text;
            MainWindow mainWindow = new MainWindow();
            //将PlayerN传递给MainWindow
            mainWindow.PlayerName_TitleChanged(this);
            mainWindow.Show();
            this.Close();
        }
    }
}
