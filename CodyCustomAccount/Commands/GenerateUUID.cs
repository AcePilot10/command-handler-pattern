using CodyCustomAccount.Models;
using dev.Core.Commands;
using dev.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodyCustomAccount.Commands
{
    public class GenerateUUID : ICommand
    {
        public void Execute(List<IModel> data)
        {
            var users = data.DataGet<UserData>();

            foreach (var user in users)
                user.Uuid = System.Guid.NewGuid();
        }
    }
}