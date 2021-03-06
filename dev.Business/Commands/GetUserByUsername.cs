﻿using dev.Core.Commands;
using dev.Core.Entities;
using dev.Core.Sql;
using dev.Entities.Models;
using System.Collections.Generic;

namespace dev.Business.Commands
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
            var username = data.KvpGetSingle<string>("username");

<<<<<<< HEAD
            if (_query.Exist<User>("select * from Users where Username = @UserName", new { UserName = username }))
                data.AddRange(_query.Get<User>("select * from Users where Username = @UserName", new { UserName = username }));
=======
            if (_query.Exist<User>("select * from [User] where UserName = @UserName", new { username }))
                data.AddRange(_query.Get<User>("select * from [User] where UserName = @UserName", new { UserName = username }));
>>>>>>> upstream/master
            else
                data.AddRange(_query.Get<User>("select * from [User] where Email = @Email", new { Email = username }));

        }
    }
}
