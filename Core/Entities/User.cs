﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class User : IEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
