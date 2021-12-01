using BankProject.Entities;
using System;
using System.Data.SqlClient;

namespace BankProject
{
    public class SessionInfo
    {
        public Guid Id;
        public User User;
        public SqlConnection Connection;

        public SessionInfo(Guid sessionId, User user, SqlConnection connection)
        {
            Id = sessionId; 
            User = user;
            Connection = connection;
        }
    }
}
