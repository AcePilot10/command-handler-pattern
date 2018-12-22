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
    public class GetUserByUsername : ICommand
    {
        private IQuery _query;

        public GetUserByUsername(IQuery query)
        {
            _query = query;
        }

        public void Execute(List<IModel> data)
        {
            var models = data.Get<UserData>();
            foreach (var user in models)
            {
                string username = user.Username;
                if (_query.Exist<UserData>("select * from Users where Username = @Username", new { Username = username }))
                    data.AddRange(_query.Get<UserData>("select * from Users where Username = @Username", new { Username = username }));
                data.Remove(user);
            }
        }
    }
}