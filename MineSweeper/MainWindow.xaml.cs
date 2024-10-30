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
using System.Numerics;
using MineSweeper1.DAL;
using MineSweeper1.Models;
using System.Security.Cryptography.X509Certificates;

namespace MineSweeper1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Drawing.Size _cellSize = new System.Drawing.Size(32, 32);//声明单元格大小
        private Level _level = Level.SIMPLE;//声明游戏难度变量
        private GameState _gameState = GameState.NONE;//声明游戏状态
        int[,] _backData = null;//背景数据
        Image[,] _backImage = null;//背景图片
        Random rnd = new Random();//生成随机数对象
        GameLevel _gameLevel = null;//声明游戏等级对象
        int[,] _foreData = null;//前景数据
        Image[,] _foreImage = null;//前景图片
        public static string PlayerN =null;//声明玩家名字
        private int yourScore = 0;//声明玩家得分
        private PlayerService playerService =new PlayerService();//声明玩家服务对象
        private static Player currentPlayer;//声明当前玩家对象
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;//设置窗口启动位置为屏幕中央
        }
        //更改窗口title
        public void PlayerName_TitleChanged(Loading loading)
        {
            PlayerN = loading.upName.Text;
            Title = $"扫雷-欢迎您{PlayerN}";
        }
        
        private void InitGameData()
        {
            switch (_level)
            {
                case Level.SIMPLE:
                    _gameLevel = new GameLevel(10, 9, 9);
                    break;
                case Level.NORMAL:
                    _gameLevel = new GameLevel(16, 16, 40);
                    break;
                case Level.HARD:
                    _gameLevel = new GameLevel(16, 30, 99);
                    break;
            }

            mineNumberUI.Text = _gameLevel._mineNum.ToString();//显示当前难度下的地雷数量

            BackStateUI.Width = _gameLevel._col * _cellSize.Width;//设置背景UI的宽度
            BackStateUI.Height = _gameLevel._row * _cellSize.Height;//设置背景UI的高度
            //BackStateUI.Background = Brushes.Red;
            ForeStateUI.Width = _gameLevel._col * _cellSize.Width;//设置前景UI的宽度
            ForeStateUI.Height = _gameLevel._row * _cellSize.Height;//设置前景UI的高度
            //ForeStateUI.Background = Brushes.Blue;

            this.Width = BackStateUI.Width + 30;//设置窗口宽度
            this.Height = BackStateUI.Height + 106;//设置窗口高度

            double screenWidth = SystemParameters.PrimaryScreenWidth;//获取屏幕宽度
            double screenHeight = SystemParameters.PrimaryScreenHeight;//获取屏幕高度

            //this.Left = (screenWidth - this.Width) / 2;//设置窗口左上角横坐标(窗口水平居中)
            //this.Top = (screenHeight - this.Height) / 2;//设置窗口左上角纵坐标(窗口垂直居中)

            _backData = new int[_gameLevel._row, _gameLevel._col];//初始化背景数据
            _backImage = new Image[_gameLevel._row, _gameLevel._col];//初始化背景图片
            _foreData = new int[_gameLevel._row, _gameLevel._col];//初始化前景数据
            _foreImage = new Image[_gameLevel._row, _gameLevel._col];//初始化前景图片

            //初始化背景数据
            for (int x = 0; x < _gameLevel._row; x++)
            {
                for (int y = 0; y < _gameLevel._col; y++)
                {
                    _backData[x, y] = (int)BackState.BLANK;//初始化背景数据为0
                    _foreData[x, y] = (int)ForeState.NORMAL;//初始化前景数据为NOMAL对应的值
                }
            }
            BackStateUI.Children.Clear();//清空背景UI的子元素
            ForeStateUI.Children.Clear();//清空前景UI的子元素
        }
        private Timer timer;//声明计时器对象
        private int elapsedTime;//记录游戏运行时间
        private int yourTime = 0;//记录你的时间
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            _gameState = GameState.START;//设置游戏状态为开始
            gameStateUI.Text = "游戏开始";

            

            elapsedTime = 0;
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            this.InitGameData();//初始化游戏数据

            this.SetRandomMine();//设置随机地雷

            this.SetCellNumber();//设置每个格子周围的地雷数量

            this.DynamicAddImage();//设置背景图片

            this.SetForeCellImage();//设置前景图片
        }
        private void SetRandomMine()//设置随机地雷
        {
            for (int k = 0; k < _gameLevel._mineNum; k++)
            {

                //找到可供放置地雷的空格数量
                int blankNum = 0;//记录空格数量
                                 //遍历整个游戏地图，统计空格子的数量
                for (int x = 0; x < _gameLevel._row; x++)
                {
                    for (int y = 0; y < _gameLevel._col; y++)
                    {
                        if (_backData[x, y] == (int)BackState.BLANK) blankNum++;
                    }
                }
                //生成一个1到blankNum之间的随机数
                int index = rnd.Next(1, blankNum);
                //重置空格子的数量
                blankNum = 0;
                //遍历整个游戏地图，如果当前位置是空格子，则将其设置为地雷
                for (int x = 0; x < _gameLevel._row; x++)
                {
                    for (int y = 0; y < _gameLevel._col; y++)
                    {
                        if (_backData[x, y] != (int)BackState.BLANK) continue;
                        blankNum++;
                        if (blankNum == index) _backData[x, y] = (int)BackState.MINE;
                    }
                }
            }
        }
        private void SetCellNumber()
        {
            //遍历整个游戏地图，设置每个格子周围的地雷数量
            for (int x = 0; x < _gameLevel._row; x++)
            {
                for (int y = 0; y < _gameLevel._col; y++)
                {
                    if (_backData[x, y] != (int)BackState.MINE)
                    {

                        if (y - 1 >= 0 && _backData[x, y - 1] == (int)BackState.MINE)// 左
                        {
                            _backData[x, y]++;
                        }
                        if (y + 1 < _gameLevel._col && _backData[x, y + 1] == (int)BackState.MINE)// 右
                        {
                            _backData[x, y]++;
                        }
                        if (x - 1 >= 0 && _backData[x - 1, y] == (int)BackState.MINE)// 上
                        {
                            _backData[x, y]++;
                        }
                        if (x + 1 < _gameLevel._row && _backData[x + 1, y] == (int)BackState.MINE)// 下
                        {
                            _backData[x, y]++;
                        }
                        if (x - 1 >= 0 && y - 1 >= 0 && _backData[x - 1, y - 1] == (int)BackState.MINE)// 左上
                        {
                            _backData[x, y]++;
                        }
                        if (x - 1 >= 0 && y + 1 < _gameLevel._col && _backData[x - 1, y + 1] == (int)BackState.MINE)// 右上
                        {
                            _backData[x, y]++;
                        }
                        if (x + 1 < _gameLevel._row && y - 1 >= 0 && _backData[x + 1, y - 1] == (int)BackState.MINE)// 左下
                        {
                            _backData[x, y]++;
                        }
                        if (x + 1 < _gameLevel._row && y + 1 < _gameLevel._col && _backData[x + 1, y + 1] == (int)BackState.MINE)// 右下
                        {
                            _backData[x, y]++;
                        }
                    }
                }
            }
            for (int x = 0; x < _gameLevel._row; x++)
            {
                for (int y = 0; y < _gameLevel._col; y++)
                {
                    TextBlock tb = new TextBlock();
                    tb.Text = Convert.ToString(_backData[x, y]);
                    tb.SetValue(LeftProperty, (double)y * _cellSize.Width);
                    tb.SetValue(TopProperty, (double)x * _cellSize.Height);
                    BackStateUI.Children.Add(tb);
                }
            }
        }
        private void DynamicAddImage()
        {
            //动态添加图片
            for (int x = 0; x < _gameLevel._row; x++)
            {
                for (int y = 0; y < _gameLevel._col; y++)
                {
                    _backImage[x, y] = new Image();
                    if (_backData[x, y] == (int)BackState.MINE)
                    {
                        BitmapImage btImMine = new BitmapImage(new Uri("E:/VS/Work/C#/MineSweeper/MineSweeper1/Images/mine.png"));
                        _backImage[x, y].Source = btImMine;

                        _backImage[x, y].SetValue(LeftProperty, (double)y * _cellSize.Width);
                        _backImage[x, y].SetValue(TopProperty, (double)x * _cellSize.Height);
                        BackStateUI.Children.Add(_backImage[x, y]);
                    }
                    else if (_backData[x, y] == (int)BackState.BLANK)
                    {
                        BitmapImage btImEmpty = new BitmapImage(new Uri("E:/VS/Work/C#/MineSweeper/MineSweeper1/Images/space.png"));
                        _backImage[x, y].Source = btImEmpty;

                        _backImage[x, y].SetValue(LeftProperty, (double)y * _cellSize.Width);
                        _backImage[x, y].SetValue(TopProperty, (double)x * _cellSize.Height);
                        BackStateUI.Children.Add(_backImage[x, y]);
                    }
                    else
                    {
                        BitmapImage btImNumber = new BitmapImage(new Uri("E:/VS/Work/C#/MineSweeper/MineSweeper1/Images/" + _backData[x, y] + ".png"));
                        _backImage[x, y].Source = btImNumber;

                        _backImage[x, y].SetValue(LeftProperty, (double)y * _cellSize.Width);
                        _backImage[x, y].SetValue(TopProperty, (double)x * _cellSize.Height);
                        BackStateUI.Children.Add(_backImage[x, y]);
                    }
                }
            }
        }
        private void SetForeCellImage()
        {
            ForeStateUI.Children.Clear();
            for (int x = 0; x < _gameLevel._row; x++)
            {
                for (int y = 0; y < _gameLevel._col; y++)
                {
                    _foreImage[x, y] = new Image();
                    BitmapImage btImFore = new BitmapImage(new Uri("E:/VS/Work/C#/MineSweeper/MineSweeper1/Images/foreImg.png"));
                    _foreImage[x, y].Source = btImFore;

                    _foreImage[x, y].SetValue(LeftProperty, (double)y * _cellSize.Width);
                    _foreImage[x, y].SetValue(TopProperty, (double)x * _cellSize.Height);
                    ForeStateUI.Children.Add(_foreImage[x, y]);
                }
            }
        }
        private void ForeStateUI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_gameState == GameState.NONE || _gameState == GameState.STOP) return;
            //获取点击位置
            Point p = e.MouseDevice.GetPosition(ForeStateUI);
            int rx = (int)p.Y / _cellSize.Height;
            int ry = (int)p.X / _cellSize.Width;
            //判断点击的数字
            if (_backData[rx, ry] > (int)BackState.BLANK)
            {
                if (ForeStateUI.Children.Contains(_foreImage[rx, ry]))
                {
                    ForeStateUI.Children.Remove(_foreImage[rx, ry]);
                }
                _foreData[rx, ry] = (int)ForeState.NONE;
            }
            //如果点击的是雷
            if (_backData[rx, ry] == (int)BackState.MINE)
            {
                if (ForeStateUI.Children.Contains(_foreImage[rx, ry]))
                {
                    ForeStateUI.Children.Remove(_foreImage[rx, ry]);
                }
                _foreData[rx, ry] = (int)ForeState.NONE;
                //如果点击的是雷
                if (!IsWin())
                {
                    yourTime = elapsedTime;
                    timer.Stop();
                    _gameState = GameState.STOP;
                    if (yourTime < 1000 && yourTime > 0)
                    {
                        gameStateUI.Text = "游戏结束！";
                        yourScore = (1000 - yourTime);
                        MessageBox.Show($"Game Over，你最终的得分为{yourScore}");
                    }
                    else if (yourTime > 1000 && yourTime < 7000)
                    {
                        gameStateUI.Text = "游戏结束！";
                        yourScore = (7000 - yourTime) / 10;
                        MessageBox.Show($"Game Over，你最终的得分为{yourScore}");
                    }
                    else
                    {
                        gameStateUI.Text = "游戏结束！";
                        MessageBox.Show($"Game Over，得分异常，成绩无效");
                    }
                }
            }
            //如果点击的是空白格
            if (_backData[rx, ry] == (int)BackState.BLANK)
            {
                this.RemoveBlank(rx, ry);
            }
        }
        //处理点击空白格
        private void RemoveBlank(int x, int y)
        {
            if (_foreData[x, y] == (int)ForeState.NORMAL && _backData[x, y] == (int)BackState.BLANK)
            {
                //翻开点击的空白格
                if (ForeStateUI.Children.Contains(_foreImage[x, y]))
                {
                    _foreData[x, y] = (int)ForeState.NONE;
                    ForeStateUI.Children.Remove(_foreImage[x, y]);
                }
                if (y - 1 >= 0)
                {
                    RemoveBlank(x, y - 1);
                }
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    RemoveBlank(x - 1, y - 1);
                }
                if (x - 1 >= 0)
                {
                    RemoveBlank(x - 1, y);
                }
                if (x - 1 >= 0 && y + 1 < _gameLevel._col)
                {
                    RemoveBlank(x - 1, y + 1);
                }
                if (y + 1 < _gameLevel._col)
                {
                    RemoveBlank(x, y + 1);
                }
                if (x + 1 < _gameLevel._row && y + 1 < _gameLevel._col)
                {
                    RemoveBlank(x + 1, y + 1);
                }
                if (x + 1 < _gameLevel._row)
                {
                    RemoveBlank(x + 1, y);
                }
                if (x + 1 < _gameLevel._row && y - 1 >= 0)
                {
                    RemoveBlank(x + 1, y - 1);
                }
                this.Open8Box(x, y);
            }
        }
        private void Open8Box(int x, int y)
        {
            //判断周围是否有雷
            if (y - 1 >= 0 && _backData[x, y - 1] > (int)BackState.BLANK && _foreData[x, y - 1] == (int)ForeState.NORMAL)
            {
                if (ForeStateUI.Children.Contains(_foreImage[x, y - 1]))
                {
                    _foreData[x, y - 1] = (int)ForeState.NONE;
                    ForeStateUI.Children.Remove(_foreImage[x, y - 1]);
                }
            }
            if (y - 1 >= 0 && x - 1 >= 0)
            {
                if (_backData[x - 1, y - 1] != (int)BackState.MINE)
                {
                    if (ForeStateUI.Children.Contains(_foreImage[x - 1, y - 1]))
                    {
                        _foreData[x - 1, y - 1] = (int)ForeState.NONE;
                        ForeStateUI.Children.Remove(_foreImage[x - 1, y - 1]);
                    }
                }
            }
            if (x - 1 >= 0 && _foreData[x - 1, y] == (int)ForeState.NORMAL)
            {
                if (ForeStateUI.Children.Contains(_foreImage[x - 1, y]))
                {
                    _foreData[x - 1, y] = (int)ForeState.NONE;
                    ForeStateUI.Children.Remove(_foreImage[x - 1, y]);
                }
            }
            if (x - 1 >= 0 && y + 1 < _gameLevel._col)
            {
                if (_backData[x - 1, y + 1] != (int)BackState.MINE)
                {
                    if (ForeStateUI.Children.Contains(_foreImage[x - 1, y + 1]))
                    {
                        _foreData[x - 1, y + 1] = (int)ForeState.NONE;
                        ForeStateUI.Children.Remove(_foreImage[x - 1, y + 1]);
                    }
                }
            }
            if (y + 1 < _gameLevel._col && _foreData[x, y + 1] == (int)ForeState.NORMAL)
            {
                if (ForeStateUI.Children.Contains(_foreImage[x, y + 1]))
                {
                    _foreData[x, y + 1] = (int)ForeState.NONE;
                    ForeStateUI.Children.Remove(_foreImage[x, y + 1]);
                }
            }
            if (x + 1 < _gameLevel._row && y + 1 < _gameLevel._col)
            {
                if (_backData[x + 1, y + 1] != (int)BackState.MINE)
                {
                    if (ForeStateUI.Children.Contains(_foreImage[x + 1, y + 1]))
                    {
                        _foreData[x + 1, y + 1] = (int)ForeState.NONE;
                        ForeStateUI.Children.Remove(_foreImage[x + 1, y + 1]);
                    }
                }
            }
            if (x + 1 < _gameLevel._row && _foreData[x + 1, y] == (int)ForeState.NORMAL)
            {
                if (ForeStateUI.Children.Contains(_foreImage[x + 1, y]))
                {
                    _foreData[x + 1, y] = (int)ForeState.NONE;
                    ForeStateUI.Children.Remove(_foreImage[x + 1, y]);
                }
            }
            if (x + 1 < _gameLevel._row && y - 1 >= 0)
            {
                if (_backData[x + 1, y - 1] != (int)BackState.MINE)
                {
                    if (ForeStateUI.Children.Contains(_foreImage[x + 1, y - 1]))
                    {
                        _foreData[x + 1, y - 1] = (int)ForeState.NONE;
                        ForeStateUI.Children.Remove(_foreImage[x + 1, y - 1]);
                    }
                }
            }
            if (y - 1 >= 0 && _foreData[x, y - 1] == (int)ForeState.NORMAL)
            {
                if (ForeStateUI.Children.Contains(_foreImage[x, y - 1]))
                {
                    _foreData[x, y - 1] = (int)ForeState.NONE;
                    ForeStateUI.Children.Remove(_foreImage[x, y - 1]);
                }
            }

        }
        private Boolean IsWin()
        {
            bool flag = true;
            //判断是否点到雷
            for(int x = 0; x < _gameLevel._row; x++)
            {
                for( int y = 0; y < _gameLevel._col; y++)
                {
                    if (_backData[x, y] == (int)BackState.MINE && _foreData[x, y] == (int)ForeState.NONE)
                    {
                        flag = false;
                    }
                }
            }
            //判断雷是否被扫光
            if( _gameLevel._mineNum == 0)
            {
                for( int x = 0; x < _gameLevel._row; x++)
                {
                    for( int y = 0; y < _gameLevel._col; y++)
                    {
                        if (_foreData[x, y] == (int)ForeState.FLAG&&_backData[x, y] != (int)BackState.MINE)
                        {
                            flag = false;
                            return flag;
                        }
                    }
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private void ForeStateUI_mouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_gameState == GameState.NONE || _gameState == GameState.STOP) return;
            Point point = e.GetPosition(ForeStateUI);
            int ry = (int)(point.X / _cellSize.Width);
            int rx = (int)(point.Y / _cellSize.Height);
            if(_foreData[rx,ry] == (int)ForeState.FLAG)
            {
                if (ForeStateUI.Children.Contains(_foreImage[rx,ry]))
                {
                    BitmapImage bitmap = new BitmapImage(new Uri("E:/VS/Work/C#/MineSweeper/MineSweeper1/Images/foreImg.png"));
                    _foreImage[rx, ry].Source = bitmap;
                    _foreData[rx, ry] = (int)ForeState.NORMAL;
                    _gameLevel._mineNum++;
                    mineNumberUI.Text = _gameLevel._mineNum.ToString();
                }
            }
            else if( _foreData[rx, ry] == (int)ForeState.NORMAL&&_gameLevel._mineNum > 0)
            {
                if (ForeStateUI.Children.Contains(_foreImage[rx, ry]))
                {
                    //ForeStateUI.Children.Remove(_foreImage[rx, ry]);
                    BitmapImage bitmap = new BitmapImage(new Uri("E:/VS/Work/C#/MineSweeper/MineSweeper1/Images/flag.png"));
                    _foreImage[rx, ry].Source = bitmap;
                    _foreData[rx, ry] = (int)ForeState.FLAG;
                    _gameLevel._mineNum--;
                    mineNumberUI.Text = _gameLevel._mineNum.ToString();
                }
            }
            //游戏胜利检测
            if (IsWin())
            {              
                yourTime = elapsedTime;
                timer.Stop();
                _gameState = GameState.STOP;
                if(yourTime < 1000 && yourTime > 0)
                {                 
                    gameStateUI.Text = "游戏胜利！";
                    yourScore = 1000 - yourTime;
                    currentPlayer = new Player()
                    {
                        PlayerName = PlayerN,
                        Score = yourScore,
                        GameDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    };
                    MessageBox.Show($"游戏胜利！你最终的得分为{yourScore}");
                    //UpdateDatabase();
                    playerService.AddPlayer(currentPlayer,"Simple");
                }
                else if(yourTime > 1000 && yourTime < 5000)
                {
                   
                    gameStateUI.Text = "游戏胜利！";
                    yourScore = (5000 - yourTime)/10;
                    currentPlayer = new Player()
                    {
                        PlayerName = PlayerN,
                        Score = yourScore,
                        GameDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    };
                    MessageBox.Show($"游戏胜利！你最终的得分为{yourScore}");
                    //UpdateDatabase();
                    playerService.AddPlayer(currentPlayer,"Normal");
                }
                else if(yourTime > 5000 && yourTime < 7000)
                {
                   
                    gameStateUI.Text = "游戏胜利！";
                    yourScore = (7000 - yourTime)/10;
                    currentPlayer = new Player()
                    {
                        PlayerName = PlayerN,
                        Score = yourScore,
                        GameDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    };
                    MessageBox.Show($"游戏胜利！你最终的得分为{yourScore}");
                    //UpdateDatabase();
                    playerService.AddPlayer(currentPlayer,"Hard");
                }
                else
                {
                    gameStateUI.Text = "游戏胜利！";
                    MessageBox.Show($"游戏胜利！得分异常，成绩无效");
                }
            }
        }
        // 定时器触发时执行的代码
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            elapsedTime++;//计时器每秒加1
            yourTime = elapsedTime;//将计时器的值赋给yourTime
            Dispatcher.Invoke(() =>
            {
                TimerUI.Text = yourTime.ToString();// 在主线程中更新时间UI
            });
        }
        private void Easy_Level_Click(object sender, RoutedEventArgs e)
        {
            Easy_Level.IsChecked = true;
            Normal_Level.IsChecked = false;
            Hard_Level.IsChecked = false;
            _level = Level.SIMPLE;
            timer.Stop();
            StartGame_Click(sender, e);
        }
        private void Normal_Level_Click(object sender, RoutedEventArgs e)
        {
            Easy_Level.IsChecked = false;
            Normal_Level.IsChecked = true;
            Hard_Level.IsChecked = false;
            _level = Level.NORMAL;
            timer.Stop();
            StartGame_Click(sender, e);
        }
        private void Hard_Level_Click(object sender, RoutedEventArgs e)
        {
            Easy_Level.IsChecked = false;
            Normal_Level.IsChecked = false;
            Hard_Level.IsChecked = true;
            _level = Level.HARD;
            timer.Stop();
            StartGame_Click(sender, e);
        }

        private void Ranking_Click(object sender, RoutedEventArgs e)
        {
            //创建一个新的窗口
            Ranking ranking = new Ranking();
            // ranking.RankingBox.ItemsSource = playerService.QueryPlayer("Simple");
            //显示窗口
            ranking.Show();
        }
        
        //记录玩家游戏数据并更新至数据库
        private void UpdateDatabase()
        {
            SqlConnection conn = null;
            try
            {
                string str = "Data Source=.;Initial Catalog=MineSweeper; Integrated Security = true";
                conn = new SqlConnection(str);
                conn.Open();
                string sql = $"INSERT INTO  score,PlayerName,GameDaT VALUES({yourScore},'{PlayerN}','{DateTime.Now}')";
            }catch (Exception ex)
            {
                MessageBox.Show("Error:出现异常," + ex.Message);
            }
        }
        
    }
}

            