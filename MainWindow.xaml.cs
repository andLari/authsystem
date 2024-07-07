using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace variant3compservice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }

        private void signup(object sender, RoutedEventArgs e)
        {
            registration reg = new registration();
            this.Close();
            reg.Show();
        }

        private void signin(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = logintextbox.Text;
                string password = passwordbox.Password;

                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Вы не ввели все данные!");
                    return;
                }

                using (SqlConnection con = new SqlConnection(databaseconfig.sqlstring))
                {
                    con.Open();
                    string select_data = "SELECT * FROM [db10].[dbo].[Users] WHERE Login = @Login AND Password = @Password";

                    using (SqlCommand cmd = new SqlCommand(select_data, con))
                    {
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string username = reader["Login"].ToString();
                                string userRole = reader["IDRole"].ToString();
                                string firstName = reader["FirstName"].ToString();

                                MasterWindow master = new MasterWindow();
                                ManagerWindow manager = new ManagerWindow();

                                if (userRole == "1")
                                {
                                    master.FirstName = firstName;
                                    master.Show();
                                }
                                else if (userRole == "2")
                                {
                                    manager.FirstName = firstName;
                                    manager.Show();
                                }

                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Пользователь не найден или неверный логин или пароль");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка!");
                MessageBox.Show(ex.Message);
            }
        }

    }
}
