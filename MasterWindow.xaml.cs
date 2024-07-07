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
    /// Логика взаимодействия для MasterWindow.xaml
    /// </summary>
    public partial class MasterWindow : Window
    {
        public MasterWindow()
        {
            InitializeComponent();
            LoadData();
        }

        SqlConnection con = new SqlConnection(databaseconfig.sqlstring);

        public string FirstName { get; set; }

        public void Show()
        {
            enteras.Content = "Добро пожаловать, " + FirstName;
            base.Show();
        }

        public void LoadData()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT ID as 'Номер заявки' from  [db10].[dbo].[Requests]", con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                selectedid.Items.Clear();


                while (reader.Read())
                {
                    selectedid.Items.Add(reader["Номер заявки"].ToString());
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных!" + ex.Message);
            }
        }


        private void exit_button(object sender, RoutedEventArgs e)
        {
            sureexit exitDialog = new sureexit(this);
            exitDialog.ShowDialog();
        }

        private void save_all(object sender, RoutedEventArgs e)
        {
            if (selectedid.SelectedItem == null)
            {
                MessageBox.Show("Выберите номер заявки");
                return;
            }

            object id = Convert.ToInt32(selectedid.SelectedItem);
            string description = resourcesneed.Text;
            string time = timeofcompleted.Text;
            string price = priceofwork.Text;


            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(time) || string.IsNullOrEmpty(price))
            {
                MessageBox.Show("Введите обновленное описание/время выполнения/цену");
                return;
            }


            try
            {
                using (SqlConnection con = new SqlConnection(databaseconfig.sqlstring))
                {
                    con.Open();

                    string query = "INSERT INTO [db10].[dbo].[Reports] (IDRequest, Resources, " +
                        "Time, Price) VALUES (@IDRequest, @Resources, @Time, @Price)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@IDRequest", id);
                    cmd.Parameters.AddWithValue("@Resources", description);
                    cmd.Parameters.AddWithValue("@Time", time);
                    cmd.Parameters.AddWithValue("@Price", price);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Отчет успешно добавлен!");
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить данные!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + MessageBox.Show("Ошибка!"));
            }
        }
    }
}
