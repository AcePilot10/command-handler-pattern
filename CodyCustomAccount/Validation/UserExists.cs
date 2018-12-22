using CodyCustomAccount.Models;
using dev.Core.Commands;
using dev.Core.Sql;
using dev.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodyCustomAccount.Validation
{
    public class UserExists : IValidation
    {

        private IQuery _query;

        public UserExists(IQuery query)
        {
            _query = query;
        }

        public bool IsValid(List<IModel> data)
        {
            var models = data.Get<UserData>();
            if (models == null)
            {
                return false;
            }

            foreach(var user in models)
            {
                string username = user.Username;
                if (!_query.Exist<UserData>("select * from Users where Username = @Username", new { Username = username }))
                {
                    return false;
                }
            }
            return true;
        }

        public string Message() => "Could not find user specified!";

    }
}
