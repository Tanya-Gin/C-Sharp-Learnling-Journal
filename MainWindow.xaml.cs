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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DockPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() 
        { 
            // 初始化组件
            InitializeComponent(); 
            // 添加按钮
            this.AddButton(); 
        }
        private void AddButton() 
        { 
            // 创建一个按钮
            Button btn = new Button(); 
            // 设置按钮的内容
            btn.Content = "button"; 
            // 将按钮添加到文档面板的子项中
        }
    }
}
