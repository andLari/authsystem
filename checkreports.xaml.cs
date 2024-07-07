using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для checkreports.xaml
    /// </summary>
    public partial class checkreports : Window
    {
        public checkreports()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        SqlConnection con = new SqlConnection(databaseconfig.sqlstring);

        public void LoadDataGrid()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(databaseconfig.sqlstring))
                {
                    con.Open();
                    string query = "SELECT Reports.ID as 'Номер отчета', IDRequest 'Номер заявки', concat(Users.FirstName, ' ', Users.LastName) as 'Мастер', " +
                        "Resources as 'Необходимые ресурсы', Time as 'Необходимое время', Price as 'Цена' FROM[db10].[dbo].[Reports] " +
                        "inner join Requests on Requests.ID = Reports.IDRequest inner join Users on Users.Id = Requests.IDMaster; ";
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
    }
}
