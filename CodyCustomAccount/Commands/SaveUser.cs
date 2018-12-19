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
            var users = data.Get<User>();

            foreach (var user in users)
                _query.Save(user, new { user.ID });
        }
    }
}