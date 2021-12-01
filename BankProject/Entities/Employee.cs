using System;

namespace BankProject.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public Guid RowId { get; set; }
        public Guid UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string PersonalPhoneNumber { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepatmentName { get; set; }
    }
}
