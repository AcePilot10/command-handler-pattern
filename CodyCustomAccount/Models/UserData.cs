﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using dev.Entities.Models;

namespace CodyCustomAccount.Models
{
    public class UserData : IModel
    {
        [Key]
        public int _ID { get; set; }

        public string Guid { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}