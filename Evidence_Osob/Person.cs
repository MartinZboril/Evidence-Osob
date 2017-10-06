using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Evidence_Osob
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RC { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }

        public override string ToString()
        {
            return Name + " " + Surname + " " + RC + " " + DateOfBirth + " " + Gender;
        }
    }
}