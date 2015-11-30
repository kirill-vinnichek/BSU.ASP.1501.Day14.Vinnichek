﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Training.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
 
        public string LastName { get; set; }

        public string Email { get; set; }
 
        public string University { get; set; }
 
        public int Course { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }

    }
}