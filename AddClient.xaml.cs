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
    public partial class AddClient : Window
    {
        private AddingRequestWindow _addingRequestWindow;

        public AddClient(AddingRequestWindow addingRequestWindow)
        {
            InitializeComponent();
            _addingRequestWindow = addingRequestWindow;
        }

        private void save_button(object sender, RoutedEventArgs e)
        {
            string Nclient = NameClient.Text;
            string LNclient = LnameClient.Text;

            try
            {
                if (string.IsNullOrEmpty(Nclient) || string.IsNullOrEmpty(LNclient))
                {
                    MessageBox.Show("Введены не все данные!");
                }
                else
                {
                    string query = "INSERT INTO [db10].[dbo].[Clients] (FirstName, LastName) " +
                                   "VALUES (@Nclient, @LNclient)";

                    using (SqlConnection con = new SqlConnection(databaseconfig.sqlstring))
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nclient", Nclient);
                        cmd.Parameters.AddWithValue("@LNclient", LNclient);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Клиент успешно добавлен!");
                            _addingRequestWindow.LoadRoles(); 
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось добавить данные!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Произошла ошибка!");
            }
        }
    }
}
