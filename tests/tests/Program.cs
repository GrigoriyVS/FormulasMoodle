﻿using System;
using System.Numerics;

namespace tests
{
    class Program
    {
        static void Main(string[] args)
        {
            If_part.writeAll = false;


            If_part.Test_If_siple(new Vector2(-100,10), new Vector2(-5, 3), 1, If_part.ConditionIf.greater);

            Console.Read();


        }
    }
}
