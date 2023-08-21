﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project.Entity
{
    public class Test : IEntity
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public List<string> Options { get; set; }

        public string RightOption { get; set; }
    }
}
