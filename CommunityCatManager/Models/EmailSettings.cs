﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityCatManager.Models
{
    public class EmailSettings
    {
        public string Domain { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
