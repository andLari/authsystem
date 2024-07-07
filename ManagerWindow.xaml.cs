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
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public string FirstName { get; set; }

        public ManagerWindow()
        {
            InitializeComponent();
        }

        public new void Show()
        {
            enteras.Content = "Добро пожаловать, " + FirstName;
            base.Show();
        }

        private void exit_button(object sender, RoutedEventArgs e)
        {
            sureexit exitDialog = new sureexit(this);
            exitDialog.ShowDialog();
        }

        private void add_request(object sender, RoutedEventArgs e)
        {
            AddingRequestWindow addrequest = new AddingRequestWindow();
            addrequest.ShowDialog();
        }

        private void checkedit_request(object sender, RoutedEventArgs e)
        {
            CheckRequestWindow checkRequestWindow = new CheckRequestWindow();
            checkRequestWindow.ShowDialog();
        }

        private void check_report(object sender, RoutedEventArgs e)
        {
            checkreports rep = new checkreports();
            rep.ShowDialog();
        }
    }
}
