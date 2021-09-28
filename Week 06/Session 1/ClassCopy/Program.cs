using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassCopy
{
    class MyContent : ICloneable
    {
        public string myString;

        public Object Clone()
        {
            return MemberwiseClone();
        }

    }

    class MyClass : ICloneable
    {
        // myContainingClass is a reference data type
        public MyContent myContainingClass = new MyContent();

        // val is a value data type
        public int val;

        // intArray is a ref data type
        public List<int> intArray = new List<int>();

        public Object Clone()
        {
            MyClass copy = (MyClass)this.MemberwiseClone();

            copy.myContainingClass = (MyContent) this.myContainingClass.Clone();

            return copy;

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass objectA = new MyClass();
            objectA.myContainingClass.myString = "david";

            MyClass objectB;
            objectB = objectA;

            objectB.myContainingClass.myString = "sue";

            objectB = new MyClass();
            objectB.val = objectA.val;
            objectB.myContainingClass.myString = objectA.myContainingClass.myString;

            object

        }
    }
}
