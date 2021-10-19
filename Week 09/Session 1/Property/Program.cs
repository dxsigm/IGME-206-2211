using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property
{
    public interface IPerson
    {
        string Name
        {
            get; set;
        }
    }

    public class Person : IPerson
    {
        private string name;

        public string Name
        {
            get
            {
                return Name;
            }

            set
            {
                name = value;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();

            person.Name = "david";
            Console.WriteLine(person.Name);
        }
    }
}
