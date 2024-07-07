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
using System.Windows.Shapes;

namespace variant3compservice
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Window
    {

        SqlConnection con = new SqlConnection(databaseconfig.sqlstring);

        public registration()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void LoadRoles()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT Role from [db10].[dbo].[Role]", con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                rolebox.Items.Clear();

                while (reader.Read())
                {
                    rolebox.Items.Add(reader["Role"].ToString());
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных!" + ex.Message);
            }
        }

        private void back_button(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }

        private void sign_up(object sender, RoutedEventArgs e)
        {
            string name = textboxname.Text;
            string lname = textboxfname.Text;
            string login = logintextbox.Text;
            string password = passwordbox.Text;
            string confirm = confirmpassword.Text;

            
            if (rolebox.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль!", "Ошибка");
                return;
            }

            string role = rolebox.SelectedItem.ToString();

            try
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Вы не ввели все данные!");
                    return; 
                }
                else if (password != confirm)
                {
                    MessageBox.Show("Пароли не совпадают!");
                    return; 
                }


                string query = "INSERT INTO [db10].[dbo].[users] (FirstName, LastName, IDRole, Login, Password) " +
                               "VALUES (@name, @lname, @roleID, @login, @password)";

                using (SqlConnection con = new SqlConnection(databaseconfig.sqlstring))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@lname", lname);
                    cmd.Parameters.AddWithValue("@roleID", role == "Мастер" ? 1 : 2);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Вы успешно зарегистрированы!");
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить данные!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось добавить данные! Подробности: {ex.Message}");
            }
        }
    }
}

