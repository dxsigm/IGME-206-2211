using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedList
{
    class Program
    {
        class FamilyMember
        {
            public static string familyCar;

            public string name;
            public int age;

            public string DriveCar(string destination)
            {
                return "Driving " + familyCar + " to " + destination;
            }
        }


        static void Main(string[] args)
        {
            FamilyMember david = new FamilyMember();
            david.name = "David";
            david.age = 54;

            FamilyMember.familyCar = "pontiac";

            Console.WriteLine(david.DriveCar("bank"));

            FamilyMember.familyCar = "porsche";

            Console.WriteLine(david.DriveCar("bank"));


            SortedList<string, int> personAge = new SortedList<string, int>();

            personAge["david"] = 54;
            personAge["sue"] = 82;
            personAge["adam"] = 16;

            List<int> intList = new List<int>();
            foreach( int thisInt in intList )
            {

            }

            foreach( KeyValuePair<string, int> keyValuePair in personAge )
            {
                Console.WriteLine("personAge[{0}] = {1}", keyValuePair.Key, keyValuePair.Value);
            }

        }
    }
}
