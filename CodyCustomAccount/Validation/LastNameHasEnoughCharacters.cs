using CodyCustomAccount.Models;
using dev.Core.Commands;
using dev.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodyCustomAccount.Validation
{
    public class LastNameHasEnoughCharacters : IValidation
    {
        public bool IsValid(List<IModel> data)
        {
            var models = data.Get<UserData>();
            if (models == null)
            {
                return false;
            }

            foreach (var user in models)
            {
                if (user.LastName.Length < 1)
                {
                    return false;
                }
            }

            return true;
        }

        public string Message() => "Last name must contain at least 1 character!";
    }
}