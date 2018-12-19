using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodyCustomAccount.Commands;
using CodyCustomAccount.Models;
using CodyCustomAccount.Validation;
using dev.Core.Commands;
using dev.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodyCustomAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IHandler _handler;

        public UserController(IHandler handler)
        {
            _handler = handler;
        }

        [HttpPost("create")]
        public IResult CreateUser([FromBody]UserData user)
        {
            Console.WriteLine("Creating User");
            return _handler
                .Add(user)
                .Validate<FirstNameHasEnoughCharacters>()
                .Validate<LastNameHasEnoughCharacters>()
                .Validate<UsernameHasEnoughCharacters>()
                .Validate<PasswordHasEnoughCharacters>()
                .Validate<PasswordMatchesConfirmPassword>()
                .Command<SaveUser>()
                .Invoke();
        }
    }
}