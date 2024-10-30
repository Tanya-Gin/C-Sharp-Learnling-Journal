using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace Minesweeper
{
    public partial class MainWindow : Window
    {
        private const int Rows = 10;
        private const int Columns = 10;
        private const int Mines = 10;
        private Button[,] buttons;
        private bool[,] mines;
        private bool[,] revealed;
        private bool gameOver;
        private Timer timer;
        private int elapsedTime;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            buttons = new Button[Rows, Columns];
            mines = new bool[Rows, Columns];
            revealed = new bool[Rows, Columns];
            gameOver = false;
            elapsedTime = 0;
            TimerTextBlock.Text = "0";
            MineCountTextBlock.Text = Mines.ToString();

            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            MineField.Children.Clear();
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Button button = new Button
                    {
                        Tag = new Tuple<int, int>(r, c)
                    };
                    button.Click += Button_Click;
                    button.MouseRightButtonUp += Button_RightClick;
                    MineField.Children.Add(button);
                    buttons[r, c] = button;
                }
            }

            PlaceMines();
        }

        private void PlaceMines()
        {
            Random random = new Random();
            int minesPlaced = 0;
            while (minesPlaced < Mines)
            {
                int r = random.Next(Rows);
                int c = random.Next(Columns);
                if (!mines[r, c])
                {
                    mines[r, c] = true;
                    minesPlaced++;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameOver)
                return;

            Button button = sender as Button;
            Tuple<int, int> position = button.Tag as Tuple<int, int>;
            int row = position.Item1;
            int col = position.Item2;

            if (mines[row, col])
            {
                GameOver();
                button.Background = Brushes.Red;
                return;
            }

            RevealCell(row, col);
        }

        private void Button_RightClick(object sender, MouseButtonEventArgs e)
        {
            if (gameOver)
                return;

            Button button = sender as Button;
            if (button.Content == null)
            {
                button.Content = "F";
                button.Background = Brushes.LightCoral;
            }
            else if (button.Content.ToString() == "F")
            {
                button.Content = null;
                button.Background = Brushes.LightGray;
            }
        }

        private void RevealCell(int row, int col)
        {
            if (revealed[row, col])
                return;

            revealed[row, col] = true;
            int mineCount = CountAdjacentMines(row, col);

            Button button = buttons[row, col];
            button.IsEnabled = false;
            button.Content = mineCount > 0 ? mineCount.ToString() : "";
            button.Background = Brushes.LightGreen;

            if (mineCount == 0)
            {
                for (int r = Math.Max(0, row - 1); r <= Math.Min(Rows - 1, row + 1); r++)
                {
                    for (int c = Math.Max(0, col - 1); c <= Math.Min(Columns - 1, col + 1); c++)
                    {
                        RevealCell(r, c);
                    }
                }
            }

            if (CheckWin())
            {
                timer.Stop();
                MessageBox.Show("You Win!");
                gameOver = true;
            }
        }

        private int CountAdjacentMines(int row, int col)
        {
            int count = 0;
            for (int r = Math.Max(0, row - 1); r <= Math.Min(Rows - 1, row + 1); r++)
            {
                for (int c = Math.Max(0, col - 1); c <= Math.Min(Columns - 1, col + 1); c++)
                {
                    if (mines[r, c])
                        count++;
                }
            }
            return count;
        }

        private bool CheckWin()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (!mines[r, c] && !revealed[r, c])
                        return false;
                }
            }
            return true;
        }

        private void GameOver()
        {
            timer.Stop();
            MessageBox.Show("Game Over!");
            gameOver = true;

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (mines[r, c])
                    {
                        buttons[r, c].Background = Brushes.Red;
                    }
                }
            }
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                elapsedTime++;
                TimerTextBlock.Text = elapsedTime.ToString();
            });
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            InitializeGame();
        }


        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            CustomWindow customWindow = new CustomWindow();

            InitializeGame();
        }
    }
    public class CustomWindow : Window
    {
        public CustomWindow()
        {

        }
    }
}
