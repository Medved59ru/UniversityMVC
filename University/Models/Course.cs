﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Display(Name = "Название курса")]
        public string Name { get; set; }
    }
}