using BankProject.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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

namespace BankProject.Pages
{
    public partial class EmployeesPage : Page
    {
        SessionInfo SessionInfo;
        User CurrentUser;
        Employee CurrentEmployee;

        ObservableCollection<Employee> Employees;
        public EmployeesPage(SessionInfo sessionInfo, User currentUser, Employee currentEmployee)
        {
            InitializeComponent();
            SessionInfo = sessionInfo;
            CurrentUser = currentUser;
            CurrentEmployee = currentEmployee;

            Employees = GetEmployees(sessionInfo.Connection).Result;
        }

        private async Task<ObservableCollection<Employee>> GetEmployees(SqlConnection connection)
        {
            var employees = new ObservableCollection<Employee>();

            using (connection)
            {
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM Employee";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Id = reader.GetGuid(0),
                            RowId = reader.GetGuid(1),
                            UserId = reader.GetGuid(2),
                            LastName = reader.GetString(3),
                            FirstName = reader.GetString(4),
                            MiddleName = reader.GetString(5),
                            BirthDate = reader.GetDateTime(6),
                            PhoneNumber = reader.GetString(7),
                            PersonalPhoneNumber = reader.GetString(8),
                            DepartmentId = reader.GetGuid(9),
                            DepatmentName = reader.GetString(10)

                        });
                    }
                }

                return employees;
            }
        }
    }
}
