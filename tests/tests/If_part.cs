﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace tests
{
    static class Tests 
    {
        public static int countTests = 0;
        public static int countErrors = 0;
        public static Dictionary<string, int> typeError = new Dictionary<string, int>();

        public static string ThrowError(string typeError) 
        {
            countErrors++;
            int countErrorsOfType;
            if (Tests.typeError.TryGetValue(typeError, out countErrorsOfType))
            {
                foreach (var dict in Tests.typeError)
                {
                    if (dict.Key == typeError)
                    {
                        int newVal = dict.Value;
                        Tests.typeError.Remove(typeError);
                        Tests.typeError.Add(typeError, newVal+1);
                        break;
                    }

                }
            }
            else
            Tests.typeError.Add(typeError, 1);

            return "  \t\tINCORRECT";
        }

        public static void ShowErrors()
        {            
            Console.WriteLine("\n\nCount Errors:" + countErrors);
            for (int i = 0; i < 25; i++) Console.Write("="); Console.WriteLine();

            foreach (var error in typeError)
            {
                Console.WriteLine($"Erorr {error.Key}: {error.Value}");
            }
        }
    }
    static class If_part
    {
        public static bool writeAll = true;
        public static void Write<T>(T val) 
        {
            if(writeAll) Console.Write(val);
        }
        public static void WriteLine<T>(T val)
        {
            if (writeAll) Console.WriteLine(val);
        }
        public static void WriteLine()
        {
            if (writeAll) Console.WriteLine();
        }

        public enum ConditionIf { equals, less, greater, less_eq, greater_eq }
        public static void If_siple(float a, float b, ConditionIf cIf = ConditionIf.equals) 
        {
            WriteLine($"[a={a},b={b}]");
            float valueIf = 1;
            float valueElse = 0;
            switch (cIf)
            {
                case ConditionIf.equals:
                    bool isEquals = a == b;
                    if (isEquals) 
                    {
                        WriteLine(valueIf);
                    }
                    else 
                    {
                        WriteLine(valueElse);
                    }



                    break;



                /*
                 tanh(0...9999)=> 0..1                                       0.2384062 корректировка на точке tanh(1)=1
                f1)floor(tanh(({a}/abs({a}-{b}+0.0001))/({b}/abs({a}-{b}+0.0001)))+0.2384062)  a>b=1     a<b=0
                f2)floor(tanh(({b}/abs({a}-{b}+0.0001))/({a}/abs({a}-{b}+0.0001)))+0.2384062)  a>b=0     a<b=1
                   a      b       |  f1  |   f2
                [0..1)   [0..1)   |  -   |   -  
                [0..1)   [1..inf) |  0   |   1   
                [1..inf) [0..1)   |  1   |   0   
                [1..inf) [1..inf) |  1   |   1     
                 */
                case ConditionIf.less:
                    bool isLess = a > b;
                    if (isLess) 
                    {
                        WriteLine(valueIf);
                    }
                    else
                    {
                        WriteLine(valueElse);
                    }

                    WriteLine(Math.Floor(Math.Tanh((a / Math.Abs(a - b + 0.0001)) / (b / Math.Abs(a - b + 0.0001))) + 0.2384062));
                    WriteLine(Math.Floor(Math.Tanh((b / Math.Abs(a - b + 0.0001)) / (a / Math.Abs(a - b + 0.0001))) + 0.2384062/*<=*/));

                    break;
                
                
                case ConditionIf.greater:
                    bool isGreater = a > b;
                    //if (a < 0 || b < 0) break;
                    if (isGreater) 
                    {
                        WriteLine(valueIf);
                    }
                    else
                    {
                        WriteLine(valueElse);
                    }

                    //проблемы с числами (-inf.:-1]
                    Write(isGreater);
                    Write(":a>b:");
                    float uformula1 = (float)Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001)+ 0.0001) ) + 0.2384058);/*<*/
                    WriteLine(uformula1);

                    Write(!isGreater);
                    Write(":a<b:");
                    float uformula2 = (float)Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b +- 0.0001)+ 0.0001) ) + 0.2384058);
                    Write(uformula2);
                    WriteLine((((isGreater ? 1 : 0) == uformula1) && ((!isGreater ? 1 : 0) == uformula2)) ? "  correct" : Tests.ThrowError("Отрицательные числа"));

                    /*
                    float formula1 = (float)
                        Math.Floor(Math.Tanh((a / Math.Abs(a - b + 0.0001)) / (b / Math.Abs(a - b + 0.0001))) + 0.2384062);
                    */

                    
                    float formula1 = (float)
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001))+1) + 0.2384062));//(a>=0)
                    float formula1_obr = (float)
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + 0.2384062)-1);//(a<0)

                    float formula2 = (float)
                        (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062));//(b>=0)
                    float formula2_obr = (float)
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062) - 1);//(b<0)

                    double formula3 =
                        (Math.Floor(Math.Tanh(a + 0.0001)) + 1) * (Math.Floor(Math.Tanh(b + 0.0001)) + 1); // (a>=0 && b>=0)

                    WriteLine($"a>=0:  [{formula1}:{formula1_obr}]");
                    WriteLine($"b>=0:  [{formula2}:{formula2_obr}]");
                    Write("(a>=0 && b>=0):" + formula3);

                    WriteLine(((a >= 0 && b >= 0 ? 1 : 0) == formula3) ? "  \tcorrect" : Tests.ThrowError("2"));


                    double formula1_Less0_aGreaterB =
                        Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + 0.2384058) *
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + 0.2384062)) * (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062)) + /*(a>=0 && b>=0)*/

                        1 *
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + 0.2384062)) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062) - 1) + /*(a>=0 && b<0)*/

                        /*0 * (Math.Floor(Math.Tanh(b + 0.0001)) + 1) * Math.Abs(Math.Floor(Math.Tanh(a + 0.0001))) +*/ /*(a<0 && b>=0) всегда 0, не учитываем*/

                        Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + -0.0001) + 0.0001)) + 0.2384058) *
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + 0.2384062) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062) - 1);   /*(a<0 && b<0)*/
                    /**/
                    Write(Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001))) + 0.2384062) *
                        (Math.Floor(Math.Tanh(a + 0.0001)) + 1) * (Math.Floor(Math.Tanh(b + 0.0001)) + 1));
                    Write("+" + 1 *
                        (Math.Floor(Math.Tanh(a + 0.0001)) + 1) * Math.Abs(Math.Floor(Math.Tanh(b + 0.0001))));
                    Write("+"+Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + 0.0001))) + 0.2384062) *
                        Math.Abs(Math.Floor(Math.Tanh(b + 0.0001))) * Math.Abs(Math.Floor(Math.Tanh(a + 0.0001))));
                    WriteLine();


                    Write("\t\t\t" + formula1_Less0_aGreaterB + " " + Math.Floor(formula1_Less0_aGreaterB));
                    WriteLine(((a > b) ? 1 : 0) == formula1_Less0_aGreaterB ? "  \tcorrect" : Tests.ThrowError("4"));
                    break;
                
                
                case ConditionIf.less_eq:
                    if (a <= b) 
                    {
                        WriteLine(valueIf);
                    }
                    else
                    {
                        WriteLine(valueElse);
                    }
                    break;
                
                
                case ConditionIf.greater_eq:
                    if (a >= b) 
                    {
                        WriteLine(valueIf);
                    }
                    else
                    {
                        WriteLine(valueElse);
                    }
                    break;
                default:
                    break;
            }
        }
        public static void Test_If_siple(Vector2 interval_a, Vector2 interval_b, int delta = 1, ConditionIf cIf = ConditionIf.equals) 
        {
            for (int a  = (int)interval_a.X; a < interval_a.Y; a += delta)
            {
                for (int b = (int)interval_b.X; b < interval_b.Y; b += delta)
                {
                   
                    If_siple(a, b, cIf);
                    WriteLine();
                }
            }
            Tests.ShowErrors();
        }

        public static void If_nested()
        {

        }
    }
}
