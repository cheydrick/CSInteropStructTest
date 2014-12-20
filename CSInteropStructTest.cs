using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CSharpInteropStructTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get integer from dll
            int int_from_dll = 0;

            int_from_dll = InteropStructTestDLLWrapper.GetInteger();
            Console.WriteLine("int_from_dll: {0}", int_from_dll);

            //Pass pointer to integer to dll, value is changed by reference
            InteropStructTestDLLWrapper.PassIntegerPointer(ref int_from_dll);
            Console.WriteLine("\nint_from_dll: {0}", int_from_dll);

            //Pass pointer to simple struct to dll, values are changed by reference
            InteropStructTestDLLWrapper.SimpleStruct simple_struct = new InteropStructTestDLLWrapper.SimpleStruct();

            InteropStructTestDLLWrapper.PassSimpleStructPointer(ref simple_struct);
            Console.Write("\nsimple_struct firstInt:{0} ", simple_struct.firstInt);
            Console.WriteLine("secondInt:{0}", simple_struct.secondInt);

            //Pass pointer to array of simple structs, value is changed by reference
            InteropStructTestDLLWrapper.SimpleStruct[] simple_struct_array = new InteropStructTestDLLWrapper.SimpleStruct[5];

            InteropStructTestDLLWrapper.PassSimpleStructArray(5, simple_struct_array);
            for (int i = 0; i < 5; i++)
            {
                Console.Write("\nsimple_struct_array firstInt:{0} ", simple_struct_array[i].firstInt);
                Console.WriteLine("secondInt:{0}", simple_struct_array[i].secondInt);
            }
            
            //Pass pointer to complex struct to dll, values are changed by reference
            InteropStructTestDLLWrapper.ComplexStruct complex_struct = new InteropStructTestDLLWrapper.ComplexStruct();

            InteropStructTestDLLWrapper.PassComplexStructPointer(ref complex_struct);
            Console.Write("\ncomplex_struct firstChar:{0} ", (int)complex_struct.firstChar);
            Console.Write("secondChar:{0} ", (int)complex_struct.secondChar);
            Console.Write("firstLong:{0} ", complex_struct.firstLong);
            Console.Write("firstShort:{0} ", complex_struct.firstShort);
            Console.Write("thirdChar:{0} ", (int)complex_struct.thirdChar);
            Console.WriteLine("fourthChar:{0}", (int)complex_struct.fourthChar);

            //Pass pointer to array of complex structs, value is changed by reference
            InteropStructTestDLLWrapper.ComplexStruct[] complex_struct_array = new InteropStructTestDLLWrapper.ComplexStruct[5];

            InteropStructTestDLLWrapper.PassComplexStructArray(5, complex_struct_array);
            for (int i = 0; i < 5; i++)
            {
                Console.Write("\ncomplex_struct_array firstChar:{0} ", (int)complex_struct_array[i].firstChar);
                Console.Write("secondChar:{0} ", (int)complex_struct_array[i].secondChar);
                Console.Write("firstLong:{0} ", complex_struct_array[i].firstLong);
                Console.Write("firstShort:{0} ", complex_struct_array[i].firstShort);
                Console.Write("thirdChar:{0} ", (int)complex_struct_array[i].thirdChar);
                Console.WriteLine("fourthChar:{0}", (int)complex_struct_array[i].fourthChar);
            }
            
            //Sanity check of type and struct sizes as the DLL sees them.
            InteropStructTestDLLWrapper.DebugCTypeSizes();
            InteropStructTestDLLWrapper.DebugCSTypeSizes();
            
            Console.ReadLine();
        }
    }

    public class InteropStructTestDLLWrapper
    {
        public struct SimpleStruct
        {
            public int firstInt;
            public int secondInt;
        }

        public struct ComplexStruct
        {
            public char firstChar;
            public char secondChar;
            //C# longs are 8 bytes. 32-it C longs are 4 bytes. Use C# int for C long.
            //public ulong firstLong;
            public int firstLong;
            public short firstShort;
            public char thirdChar;
            public char fourthChar;
        }
        
        [DllImport("InteropStructTest.dll")]
        public extern static int GetInteger();

        [DllImport("InteropStructTest.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void PassIntegerPointer([In, Out]ref int i);

        [DllImport("InteropStructTest.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void PassSimpleStructPointer([In, Out]ref SimpleStruct s);

        [DllImport("InteropStructTest.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void PassSimpleStructArray(int size, [In, Out] SimpleStruct[] s);

        [DllImport("InteropStructTest.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void PassComplexStructPointer([In, Out]ref ComplexStruct s);

        [DllImport("InteropStructTest.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void PassComplexStructArray(int size, [In, Out] ComplexStruct[] s);

        [DllImport("InteropStructTest.dll")]
        public extern static int DebugCTypeSizes();

        public static void DebugCSTypeSizes()
        {
            Console.WriteLine("\nSize of C# char is: {0}", sizeof(char));
            Console.WriteLine("Size of C# short is: {0}", sizeof(short));
            Console.WriteLine("Size of C# int is: {0}", sizeof(int));
            Console.WriteLine("Size of C# long is: {0}", sizeof(long));
            Console.WriteLine("Size of C# SimpleStruct: {0}", Marshal.SizeOf(typeof(InteropStructTestDLLWrapper.SimpleStruct)));
            Console.WriteLine("Size of C# ComplexStruct: {0}", Marshal.SizeOf(typeof(InteropStructTestDLLWrapper.ComplexStruct)));
        }
    }
}
