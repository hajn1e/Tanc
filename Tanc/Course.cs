using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Tanc
{
    class Course
    {
        private long Id;
        private string Name;
        private string Type;

        private int Lenght;
        private string Instructor;


        public Course (long id, string name, string type, int lenght,  string instructor)
        {
            Id= id;
            Name = name;
            Type = type;
            Lenght = lenght;
            Instructor= instructor;

        }

        public long id { get => Id; set => Id = value; }
        public string name { get => Name; set => Name = value; }
        public string type { get => Type; set => Type = value; }

        public int lenght { get => Lenght; set => Lenght = value; }
        public string instructor { get => Instructor; set => Instructor = value; }

        public override string ToString()
        {
            return $"{Name} | {Type} | {Lenght} perc | Oktató. {Instructor}";
        }
    }
}
