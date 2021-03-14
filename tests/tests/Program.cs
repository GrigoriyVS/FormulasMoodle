using System;
using System.Numerics;

namespace tests
{
    class Program
    {
        static void Main(string[] args)
        {
            If_part.writeAll = !!false;


            If_part.Test_If_siple(new Vector2(9000,10000), new Vector2(-10000, 10000), 70, If_part.ConditionIf.greater);

            Console.Read();


        }
    }
}
