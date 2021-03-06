﻿using dev.Core.Commands;
using dev.Core.Entities;
using dev.Entities.Models;
using System.Collections.Generic;

namespace dev.Business.Validators
{
    public class PasswordNotNullOrEmpty : IValidation
    {
        public bool IsValid(List<IModel> data)
        {
            var models = data.Get<User>();
            if (models == null)
                return false;

            foreach (var user in models)
                if (string.IsNullOrEmpty(user.Password))
                    return false;

            return true;
        }

        public string Message() => "Password is required.";
    }
}
