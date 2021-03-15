using System;
using System.Numerics;

namespace tests
{
    class Program
    {
        static void Main(string[] args)
        {
            If_part.writeAll = !!false;


            //If_part.Test_If_siple(
            //    new Vector2(-999,999), 
            //    new Vector2(-999, 999), 
            //    1, If_part.ConditionIf.greater_eq);

            If_part.Test_If_nested(
                new Vector2(-19, 19), 
                new Vector2(-19, 19),
                new Vector2(-19, 19),
                new Vector2(-19, 19),
                1, If_part.ConditionIf.greater_eq);

            Console.Read();


        }
    }
}
