using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    /// Логика взаимодействия для CheckRequestWindow.xaml
    /// </summary>
    public partial class CheckRequestWindow : Window
    {
        public CheckRequestWindow()
        {
            InitializeComponent();
            LoadDataGrid();
            LoadDataCombobox();
        }

        SqlConnection con = new SqlConnection(databaseconfig.sqlstring);

        public void LoadDataCombobox()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT ID as 'Номер заявки' from  [db10].[dbo].[Requests]", con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                idrequest.Items.Clear();


                while (reader.Read())
                {
                    idrequest.Items.Add(reader["Номер заявки"].ToString());
                }

                con.Close();

                con.Open();

                editmaster.Items.Clear();
                SqlCommand command2 = new SqlCommand("SELECT concat(FirstName, ' ',LastName) as Мастеры from [db10].[dbo].[Users] WHERE IDRole = '1'", con);
                SqlDataReader reader2 = command2.ExecuteReader();

                while (reader2.Read())
                {
                    editmaster.Items.Add(reader2["Мастеры"].ToString());
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных!" + ex.Message);
            }
        }


        public void LoadDataGrid()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(databaseconfig.sqlstring))
                {
                    con.Open();
                    string query = "SELECT Requests.ID as 'Номер заявки', concat(datepart(year, dateofadd),'-',datepart(month, dateofadd), " +
                        "'-',datepart(day, dateofadd)) as 'Дата добавления', Hardware as 'Оборудование', " +
                        "TypeOfProblem as 'Тип проблемы', Description as 'Описание', concat(Clients.FirstName, ' ', Clients.LastName) as " +
                        "Клиент, concat(Users.FirstName, ' ', Users.LastName) as Мастер, Status as 'Статус', Contact as 'Контактные данные' " +
                        "FROM[db10].[dbo].[Requests] inner join users on users.id = Requests.IDMaster inner join Clients on Clients.ID = " +
                        "Requests.IDClient";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    MyDataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных: " + ex.Message);
            }
        }


        private void back_button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void save_edited(object sender, RoutedEventArgs e)
        {
            if (idrequest.SelectedItem == null || editmaster.SelectedItem == null || editstatus.SelectedItem == null)
            {
                MessageBox.Show("Выберите номер заявки/Выберите мастера/Выберите статус");
                return;
            }

            object id = Convert.ToInt32(idrequest.SelectedItem);
            string description = newdescription.Text;
            object masterpick = editmaster.SelectedItem.ToString();
            object statuspick = (editstatus.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Введите обновленное описание");
                return;
            }


            try
            {
                using (SqlConnection con = new SqlConnection(databaseconfig.sqlstring))
                {
                    con.Open();

                    SqlCommand getMasterIdCmd = new SqlCommand("SELECT Id FROM [db10].[dbo].[Users] WHERE concat(FirstName, ' ',LastName) = @masterpick", con);
                    getMasterIdCmd.Parameters.AddWithValue("@masterpick", masterpick);
                    int masterId = Convert.ToInt32(getMasterIdCmd.ExecuteScalar());


                    string query = "UPDATE [db10].[dbo].[Requests] SET Description =" +
                        " @description, IDMaster = @masterId, Status = @statuspick WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@masterId", masterId);
                    cmd.Parameters.AddWithValue("@statuspick", statuspick);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Заявка успешно обновлена!");
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

        public void update_table(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            LoadDataGrid();
        }
    }
}
