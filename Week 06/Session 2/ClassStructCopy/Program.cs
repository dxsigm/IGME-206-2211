using System;
using System.Collections.Generic;

namespace ClassCopy
{
    class MyContent : ICloneable
    {
        public string myString;

        public Object Clone()
        {
            // this will create a new MyContent object and copy myString
            // from this into the new object
            // and return the new object as System.Object
            // MemberwiseClone() does a shallow copy of a class object
            return this.MemberwiseClone();
        }

    }

    // inherit from the ICloneable interface to support deep copy
    class MyClass : ICloneable
    {
        // myContainingClass is a reference data type
        public MyContent myContainingClass = new MyContent();

        // val is a value data type
        public int val;

        // intArray is a reference data type
        public List<int> intArray = new List<int>();

        // the Clone() method is required by the interface
        // and should implement the deep copy of this class
        public Object Clone()
        {
            // first do a MemberwiseClone() of the top level of the class
            // this is a shallow copy which creates a new object and only copies the val field
            // myContainingClass and intArray will be copied by reference
            MyClass copy = (MyClass)this.MemberwiseClone();

            // call the MemberwiseClone() of the child class
            // by calling its Clone() method
            // note that MemberwiseClone is "protected" therefore it can only
            // be called within its own class
            // ie. we cannot do:
            // copy.myContainingClass = (MyContent)this.myContainingClass.MemberwiseClone();
            // MemberwiseClone() and Clone() return System.Object, therefore we need
            // to explicitly cast the object to the correct type
            copy.myContainingClass = (MyContent)this.myContainingClass.Clone();

            // GetRange() creates a new shoebox copy of a List<>
            copy.intArray = this.intArray.GetRange(0, intArray.Count);

            // return copy which is a MyClass object as System.Object
            // note that object = copy is valid (any class type can be referenced by a parent class such as object)
            // but copy = object is invalid (any object cannot be any class type)
            // for example, a Ming vase can be easily turned into an object (if you drop it for example)
            //       object = Ming vase  (valid)
            // but any object cannot be a Ming vase!
            //       Ming vase = object (invalid)
            return copy;
        }
    }

    // structures equivalent to the classes
    struct MyContentStruct
    {
        public string myString;
    }

    struct MyStruct
    {
        public MyContentStruct myContentStruct;
        public int val;
        public List<int> intArray;
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass objectA = new MyClass();
            objectA.myContainingClass.myString = "david";

            // if we want to copy objectA into objectB
            MyClass objectB;

            // this is NOT the way to do it!
            // classes are reference data types
            // both variables are pointing to the same object
            // there is only 1 shoebox which was created by objectA
            objectB = objectA;

            // one way is to create the new object
            objectB = new MyClass();

            // and explicitly copy every class member
            objectB.val = objectA.val;

            // and copy each member of the containing class
            objectB.myContainingClass.myString = objectA.myContainingClass.myString;

            // deep copy uses the ICloneable interface
            // and depends on our creating a Clone() method in the class
            // that does the field-by-field member copy
            // create an interface variable
            ICloneable cloneable;

            // set the interface variable equal to the object to copy
            cloneable = objectA;

            // call the Clone() method and return the correct class type
            objectB = (MyClass)cloneable.Clone();

            // we could also just call Clone() via objectA
            objectB = (MyClass)objectA.Clone();


            // compare the same code with structs
            // structs are value data types
            MyStruct structA;
            structA.myContentStruct.myString = "david";
            structA.val = 40;
            structA.intArray = new List<int>();
            structA.intArray.Add(1);
            structA.intArray.Add(2);
            structA.intArray.Add(3);
            structA.intArray.Add(4);
            structA.intArray.Add(5);

            // we now have 2 separate structures
            MyStruct structB = structA;

            // this does not change structA
            structB.myContentStruct.myString = "sue;"

            // however this does, because List<> is a reference data type
            structB.intArray.Add(6);

            // we can create a separate intArray in structB by using the GetRange() method
            structB.intArray = structA.intArray.GetRange(0, structA.intArray.Count);

            // and now 7 will only be added to structB's intArray
            structB.intArray.Add(7);

            // if "new" is required to create the variable, then you should assume it is 
            // a reference variable.  Note that "new" is not required to create struct variables
        }
    }
}
