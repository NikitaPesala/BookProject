﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class Favourites
    {
        public string Id { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }

    }
}
