using System;

namespace BankProject.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Description { get; set; }
    }
}
