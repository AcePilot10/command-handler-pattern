using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CodyCustomAccount.Commands;
using CodyCustomAccount.Models;
using CodyCustomAccount.Validation;
using dev.Core.Commands;
using dev.Core.Entities;
using dev.Entities.Models;
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

        [HttpPost("SaveUser")]
        public IResult SaveUser([FromBody]UserData user)
        {
            return _handler
                .Add(user)
                .Validate<FirstNameHasEnoughCharacters>()
                .Validate<LastNameHasEnoughCharacters>()
                .Validate<UsernameHasEnoughCharacters>()
                .Validate<PasswordHasEnoughCharacters>()
                .Validate<PasswordMatchesConfirmPassword>()
                .Command<GenerateUUID>()
                .Command<SaveUser>()
                .Invoke();
        }

        [HttpGet("GetUserByUsername/{username}")]
        public IResult GetUser([FromRoute]string username)
        {
            UserData user = new UserData()
            {
                Username = username
            };
            return _handler
                .Add(user)
                .Validate<UserExists>()
                .Command<GetUserByUsername>()
                .Invoke();
        }

        [HttpGet("GetAllUsers")]
        public IResult GetAllUsers()
        {
            return _handler
                .Command<GetAllUsers>()
                .Invoke();
        }
    }
}