using System;
using System.IO;

namespace MethodsEnumStruct
{
    enum GenderPronoun
    {
        him,
        her,
        them
    }

    enum CollegeYear : byte
    {
        freshman = 24,
        sophomore = 23,
        junior = 22,
        senior = 21
    }

    struct StudentStruct
    {
        // member variables with defined accessibility
        public CollegeYear eCollegeYear;
        public GenderPronoun eGender;
        public string sName;
        public double grade;
        private string password;

        // member property for accessing the private password field
        public string Password
        {
            get
            {
                return Decrypt(this.password);
            }

            set
            {
                this.password = Encrypt(value);
            }
        }

        private string Decrypt(string pw)
        {
            string decryptedValue = "";
            char[] cPassword;

            this.password = "structure member field";
            pw = "local variable";

            cPassword = pw.ToCharArray();
            foreach (char c in cPassword)
            {
                decryptedValue += (c + 1);
            }

            return decryptedValue;
        }

        private string Encrypt(string password)
        {
            string encryptedValue = "";
            char[] cPassword;

            cPassword = password.ToCharArray();
            foreach (char c in cPassword)
            {
                encryptedValue += (c - 1);
            }

            return encryptedValue;
        }
    }

    struct CourseStruct
    {
        public string courseCode;
        public StudentStruct[] students;

        public CourseStruct(string szCourseCode, StudentStruct[] students)
        {
            this.courseCode = szCourseCode;
            this.students = students;
        }

        public void AddStudent(StudentStruct student)
        {
            StudentStruct[] s;
            int newLength;
            int i = 0;

            // if the students array has not been created yet, set the new length to 1,
            // otherwise newLength should be 1 + the current Length (to hold the new student we are adding)
            newLength = (this.students == null) ? 1 : (this.students.Length + 1);

            // create the array of the new length
            s = new StudentStruct[newLength];

            // if the students array has existing values in it
            if (this.students != null)
            {
                // copy them into our new array
                foreach (StudentStruct st in this.students)
                {
                    s[i] = st;
                    ++i;
                }
            }

            // append our new student to the new array
            s[i] = student;

            // set our students array to point to the new array
            this.students = s;
        }
    }

    // Class Examples
    // Author: David Schuh
    // Purpose: Contains examples for Week 3
    // Restrictions: Only contains code snippets.  
    static class Examples
    {
        // Method: Main
        // Purpose: The main entry point for the executable. Code snippet examples.
        // Restrictions: None
        static void Main(string[] args)
        {
            ///////////////////////////////////////////////////////
            // Methods
            ///////////////////////////////////////////////////////
            {
                int i = 0;
                for (i = 0; i < 10; ++i)
                {
                    Console.WriteLine($"{i + 34 + "!"} is " + (IsEven(i) ? "even" : "odd"));
                }

                // further example of the ternary operator
                // set the string to "even" or "odd" depending on whether i is even or odd
                // (boolean test) ? "value if true" : "value if false"
                string sResult = (i % 2 == 0) ? "even" : "odd";

                Console.WriteLine(CalcAverage(true, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
                Console.WriteLine(CalcAverage(true, 1, 2));

                int one = 0;
                int two;

                // method overloading and passing variables by value or by reference
                SetOne(one);
                SetOne(ref one);

                SetTwo(out two);

                // the TryParse() method!
                string sNumber = null;
                int nNumber;
                do
                {
                    Console.Write("Enter a number: ");
                    sNumber = Console.ReadLine();
                } while (!int.TryParse(sNumber, out nNumber));
            }


            ///////////////////////////////////////////////////////
            // Structs
            ///////////////////////////////////////////////////////
            {
                StudentStruct[] students = new StudentStruct[2];

                students[0].eGender = GenderPronoun.her;
                students[0].eCollegeYear = CollegeYear.freshman;
                students[0].sName = "June Smith";
                students[0].grade = 56.9;
                students[0].Password = "pass1234";

                students[1].eGender = GenderPronoun.them;
                students[1].eCollegeYear = CollegeYear.junior;
                students[1].sName = "Jan Smith";
                students[1].grade = 99.9;

                foreach (StudentStruct student in students)
                {
                    Console.WriteLine($"{student.sName} | {student.eCollegeYear} | {student.eGender} | {student.grade}");
                }
            }
        }

        // Method: Main
        // Purpose: return whether an int is even
        // Restrictions: None
        static bool IsEven(int nTest)
        {
            // returns whether nTest is odd or even
            bool returnVal;

            // we could execute multiple returns, 
            // but having 1 return value and 1 return statement at the end is much clearer

            if (nTest % 2 == 0)
            {
                returnVal = true;
                //return (true);
            }
            else
            {
                returnVal = false;
                //return (false);
            }

            return (returnVal);
        }

        // Method: Main
        // Purpose: Calculate the average of an array of ints.  If skew is true, add 1 to each array el
        // Restrictions: None
        static double CalcAverage(bool skew, params int[] intArray)
        {
            // calculates the average of a list of ints
            // if skew is true, add 1 to each number
            double returnVal = 0;

            foreach (int thisInt in intArray)
            {
                returnVal += (thisInt + (skew ? 1 : 0));
            }

            returnVal /= intArray.Length;

            return (returnVal);
        }

        // Method: Main
        // Purpose: Set an int to 1 by value
        // Restrictions: None
        static void SetOne(int val)
        {
            val = 1;
        }

        // Method: Main
        // Purpose: Set an int to 1 by reference
        // Restrictions: None
        static void SetOne(ref int val)
        {
            val = 1;
        }

        // Method: Main
        // Purpose: Set an int to 1 by output reference
        // Restrictions: None
        static void SetTwo(out int val)
        {
            val = 2;
        }
    }
}

