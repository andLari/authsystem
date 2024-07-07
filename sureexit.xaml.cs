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

namespace variant3compservice
{
    /// <summary>
    /// Логика взаимодействия для sureexit.xaml
    /// </summary>
    public partial class sureexit : Window
    {
        private Window parentWindow;

        public sureexit(Window parent)
        {
            InitializeComponent();
            parentWindow = parent;
        }

        private void exityes(object sender, RoutedEventArgs e)
        {
            parentWindow.Close();
            this.Close();
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void exitno(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
