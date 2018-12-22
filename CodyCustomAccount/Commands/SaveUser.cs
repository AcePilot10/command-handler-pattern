using CodyCustomAccount.Models;
using dev.Core.Commands;
using dev.Core.Sql;
using dev.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodyCustomAccount.Commands
{
    public class SaveUser : ICommand
    {
        private IQuery _query;

        public SaveUser(IQuery query)
        {
            _query = query;
        }

        public void Execute(List<IModel> data)
        {
            var users = data.Get<UserData>();
            foreach (var user in users)
            {
                if (!_query.Exist<UserData>("SELECT * FROM Users WHERE Username = @Username", new { Username = user.Username }))
                {
                    //If the user doesn't exist, then we create a new user
                    string query = "INSERT INTO Users (Username, FirstName, LastName, Password, GUID) VALUES (" +
                    "'" + user.Username + "'," +
                    "'" + user.FirstName + "'," +
                    "'" + user.LastName + "'," +
                    "'" + user.Password + "'," +
                    "'" + user.Guid +
                    "')";
                    _query.Execute(query, null);
                }
                else
                {
                    //If the user does exist, then we update the existing one.
                    string query = "UPDATE Users SET " +
                        "FirstName = '" + user.FirstName + "'," +
                        "LastName = '" + user.LastName + "'," +
                        "Password = '" + user.Password + "' " +
                        "WHERE Username = '" + user.Username + "'";
                    _query.Execute(query, null);
                }
            }         
        }
    }
}