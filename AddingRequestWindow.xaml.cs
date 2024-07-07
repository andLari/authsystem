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
    /// Логика взаимодействия для AddingRequestWindow.xaml
    /// </summary>
    public partial class AddingRequestWindow : Window
    {
        public AddingRequestWindow()
        {
            InitializeComponent();
            LoadRoles();
        }

        SqlConnection con = new SqlConnection(databaseconfig.sqlstring);

        public void LoadRoles()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT concat(FirstName, ' ',LastName) as Клиенты from [db10].[dbo].[Clients]", con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                clientpickbox.Items.Clear();


                while (reader.Read())
                {
                    clientpickbox.Items.Add(reader["Клиенты"].ToString());
                }

                con.Close();

                con.Open();

                masterpickbox.Items.Clear();
                SqlCommand command2 = new SqlCommand("SELECT concat(FirstName, ' ',LastName) as Мастеры from [db10].[dbo].[Users] WHERE IDRole = '1'", con);
                SqlDataReader reader2 = command2.ExecuteReader();

                while (reader2.Read())
                {
                    masterpickbox.Items.Add(reader2["Мастеры"].ToString());
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
            this.Close();
        }

        private void save_button(object sender, RoutedEventArgs e)
        {
            if (clientpickbox.SelectedItem == null || masterpickbox.SelectedItem == null || statuspickbox.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента/Выберите мастера/Выберите статус");
                return;
            }

            string datepick = datepicker.Text;
            string hardware = hardwarebox.Text;
            string type_of_problem = type_of_problembox.Text;
            string description = descriptionbox.Text;
            string clientpick = clientpickbox.SelectedItem.ToString();
            string masterpick = masterpickbox.SelectedItem.ToString();
            object statuspick = (statuspickbox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string contacts = contactsbox.Text;


            if (string.IsNullOrEmpty(datepick) || string.IsNullOrEmpty(hardware) || string.IsNullOrEmpty(type_of_problem)
                || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(contacts))
            {
                MessageBox.Show("Ошибка, введите/выберите все значения!");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(databaseconfig.sqlstring))
                {
                    con.Open();


                    SqlCommand getClientIdCmd = new SqlCommand("SELECT Id FROM [db10].[dbo].[Clients] WHERE concat(FirstName, ' ',LastName) = @clientpick", con);
                    getClientIdCmd.Parameters.AddWithValue("@clientpick", clientpick);
                    int clientId = Convert.ToInt32(getClientIdCmd.ExecuteScalar());


                    SqlCommand getMasterIdCmd = new SqlCommand("SELECT Id FROM [db10].[dbo].[Users] WHERE concat(FirstName, ' ',LastName) = @masterpick", con);
                    getMasterIdCmd.Parameters.AddWithValue("@masterpick", masterpick);
                    int masterId = Convert.ToInt32(getMasterIdCmd.ExecuteScalar());


                    string query = "INSERT INTO [db10].[dbo].[Requests] (DateofAdd, Hardware, TypeOfProblem, Description, IDClient, IDMaster, Status, Contact) " +
                                   "VALUES (@datepick, @hardware, @type_of_problem, @description, @clientId, @masterId, @statuspick, @contacts)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@datepick", datepick);
                    cmd.Parameters.AddWithValue("@hardware", hardware);
                    cmd.Parameters.AddWithValue("@type_of_problem", type_of_problem);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    cmd.Parameters.AddWithValue("@masterId", masterId);
                    cmd.Parameters.AddWithValue("@statuspick", statuspick);
                    cmd.Parameters.AddWithValue("@contacts", contacts);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Заявка успешно добавлена!");
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить данные!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }


        private void create_client(object sender, RoutedEventArgs e)
        {
            AddClient addcl = new AddClient(this);
            addcl.Show();
        }
    }
}
