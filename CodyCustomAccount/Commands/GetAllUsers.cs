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
    public class GetAllUsers : ICommand
    {

        private IQuery _query;

        public GetAllUsers(IQuery query)
        {
            _query = query;
        }

        public void Execute(List<IModel> data)
        {
            data.AddRange(_query.Get<UserData>("select * from Users", null));
        }
    }
}
