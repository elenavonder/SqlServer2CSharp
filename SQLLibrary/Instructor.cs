using System;
using System.Collections.Generic;
using System.Text;

namespace SQLLibrary
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int YearsExperience { get; set; } = 0;
        public int IsTenured { get; set; } = 0;
    }
}
