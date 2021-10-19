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
                // this will create an infinitely recursive property
                // since referencing Name calls Name.get
                //return Name;

                // return the private name field
                return name;
            }

            set
            {
                // this will create an infinitely recursive property 
                // since referencing Name with the equal operator calls Name.set
                //Name = value;

                // set the private name field to the operand
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
