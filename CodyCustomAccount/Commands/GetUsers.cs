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
    public class GetUsers : ICommand
    {
        private IQuery _query;

        public GetUsers(IQuery query)
        {
            _query = query;
        }

        public void Execute(List<IModel> data)
        {
            var usernames = data.Get<UserData>();           
            foreach(var username in usernames) 
            {
                string query = "SELECT * FROM Users WHERE Username = " + username;
                var response = _query.Get<string>(query, null);
            }
        }
    }
}