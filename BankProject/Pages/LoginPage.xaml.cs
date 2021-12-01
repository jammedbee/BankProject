using BankProject.Entities;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BankProject.Pages
{
    public partial class LoginPage : Page
    {
        SessionInfo SessionInfo;

        public LoginPage(SessionInfo sessionInfo)
        {
            InitializeComponent();
            SessionInfo = sessionInfo;
        }

        private async void authorizeButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            byte[] bytePassword = SHA512.Create()
                .ComputeHash(Encoding.Unicode.GetBytes(passwordPasswordBox.Password));
            string password = Encoding.Unicode.GetString(bytePassword);

            using (var connection = new SqlConnection(SessionInfo.Connection.ConnectionString, SessionInfo.Connection.Credential))
            {
                try
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();
                    command.CommandText = $"SELECT [dbo].[Authorization](@login, @password";
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    Guid? result = (Guid?)await command.ExecuteScalarAsync();

                    var user = new User();
                    var employee = new Employee();

                    if (result != null)
                    {
                        user.Login = login;
                        command.CommandText = $"SELECT TOP(1) * FROM [Employees] WHERE [UserId] = @userId";
                        command.Parameters.AddWithValue("@userId", result);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                employee.Id = reader.GetGuid(0);
                                employee.RowId = reader.GetGuid(1);
                                employee.UserId = reader.GetGuid(2);
                                employee.LastName = reader.GetString(3);
                                employee.FirstName = reader.GetString(4);
                                employee.MiddleName = reader.GetString(5);
                                employee.BirthDate = reader.GetDateTime(6);
                                employee.PhoneNumber = reader.GetString(7);
                                employee.PersonalPhoneNumber = reader.GetString(8);
                                employee.DepartmentId = reader.GetGuid(9);
                                employee.DepatmentName = reader.GetString(10);
                            }
                        }
                    }
                    
                    connection.Close();

                    (this.Parent as Frame).Navigate(new EmployeesPage(SessionInfo, user, employee));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.Source);
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (SessionInfo.Connection.State == System.Data.ConnectionState.Open)
                SessionInfo.Connection.Close();
            SessionInfo.Connection.Dispose();
            Application.Current.Shutdown();
        }

        
    }
}
