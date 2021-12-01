using BankProject.Pages;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace BankProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SqlConnection connection = InitializeConnection();
            mainFrame.NavigationService.Navigate(
                new LoginPage(
                    new SessionInfo(
                        new Guid(),
                        new Entities.User(),
                        connection)));
        }

        private SqlConnection InitializeConnection()
        {
            try
            {
                System.Security.SecureString password = new System.Security.SecureString();
                string pas = "Room_303";

                foreach (char item in pas)
                {
                    password.AppendChar(item);
                }
                password.MakeReadOnly();
                SqlCredential credential = new SqlCredential("student", password);
                SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder
                {
                    InitialCatalog = "NikishinAO",
                    DataSource = "194.190.114.107\\STUDENT,1495",
                    IntegratedSecurity = false
                };
                var connection = new SqlConnection(connectionString.ConnectionString, credential);

                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                return connection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
                return null;
            }
        }
    }
}
