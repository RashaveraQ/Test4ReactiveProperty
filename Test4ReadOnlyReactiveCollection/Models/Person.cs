using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Person
    {
        public Person(Person person)
        {
            Name = person.Name;
            Age = person.Age;
            Height = person.Height;
            Weight = person.Weight;
        }

        public Person() {}

        public string Name { get; set; }
        public int Age { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
    }
}
