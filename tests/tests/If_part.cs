using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace tests
{    
    static class If_part
    {
        static double withEq_old = +0.2384062;
        static double withEq = +0.2384059;
        static double withoutEq = +0.2384058;
        static double withoutEq_2 = 0.2384058;
        static double withoutEq_2old = 0.2383638;

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

                    //0==0 error
                    double formula1_aEqB =
                        Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + withEq) *
                        Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + 0.0001) + 0.0001)) + withEq);

                    //0==0 complete
                    double formula1_aEqWithZerosB_old =
                        Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + withEq) *
                        Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + 0.0001) + 0.0001)) + withEq) *
                        ((Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(a) + 0.0001)) + 1) + withoutEq_2)) * (Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(b) + 0.0001)) + 1) + withoutEq_2))) +/*|a|>0 && |b|>0 (не ноль)*/
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(a) + 0.0001)) + 1) + withoutEq_2) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(b) + 0.0001)) + 1) + withoutEq_2) - 1);
;
                    //double formula1_Part1_aEqB =
                    //    /*Math.Floor*/(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + withEq);

                    //double formula1_Part2_aEqB =
                    //   /*Math.Floor*/(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + 0.0001) + 0.0001)) + withEq);

                    //WriteLine(((a == b) ? 1 : 0) == formula1_aEqB ? Tests.ThrowCorrect("aEqB") : Tests.ThrowError("aEqB "+((a<0&&b>0 || b<0&&a>0)?"разные знаки":"одного знака"), $"[{a},{b}]=[a=b:{formula1_aEqB}]"));

                    double formula1_aEqWithZerosB =
                        Math.Floor(Math.Abs(Math.Tanh(b - a) + 0.999999));

                    WriteLine(((a == b) ? 1 : 0) == formula1_aEqWithZerosB ? Tests.ThrowCorrect("new aEqB") : Tests.ThrowError("new aEqB " + ((a < 0 && b > 0 || b < 0 && a > 0) ? "разные знаки" : "одного знака"), $"[{a},{b}]=[a=b:{formula1_aEqWithZerosB}]"));
                    break;



                /*
                 tanh(0...9999)=> 0..1                                       withEq корректировка на точке tanh(1)=1
                f1)floor(tanh(({a}/abs({a}-{b}+0.0001))/({b}/abs({a}-{b}+0.0001)))+withEq)  a>b=1     a<b=0
                f2)floor(tanh(({b}/abs({a}-{b}+0.0001))/({a}/abs({a}-{b}+0.0001)))+withEq)  a>b=0     a<b=1
                   a      b       |  f1  |   f2
                [0..1)   [0..1)   |  -   |   -  
                [0..1)   [1..inf) |  0   |   1   
                [1..inf) [0..1)   |  1   |   0   
                [1..inf) [1..inf) |  1   |   1     
                 */
                case ConditionIf.less:
                    bool isLess = a < b;
                    if (isLess) 
                    {
                        WriteLine(valueIf);
                    }
                    else
                    {
                        WriteLine(valueElse);
                    }

                    //отрицательные числа не поддерживаются
                    WriteLine(Math.Floor(Math.Tanh((a / Math.Abs(a - b + 0.0001)) / (b / Math.Abs(a - b + 0.0001))) + withEq));//устарело
                    double formula_aGrB = Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + withoutEq); // a>b
                    double formula_aGrEqB = Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + withEq); // a>=b

                    WriteLine(Math.Floor(Math.Tanh((b / Math.Abs(a - b + 0.0001)) / (a / Math.Abs(a - b + 0.0001))) + withEq/*<=*/));//устарело
                    double formula_bGra = Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + 0.0001) + 0.0001)) + withoutEq); // b>a
                    double formula_bGrEqa = Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + 0.0001) + 0.0001)) + withEq); // b>=a

                    WriteLine($"[{a},{b}]");
                    Write("a>b " + formula_aGrB + " "); WriteLine(((a > b) ?1:0) == formula_aGrB);
                    Write("a>=b " + formula_aGrEqB + " "); WriteLine(((a >= b) ? 1 : 0) == formula_aGrEqB);
                    Write("b>a " + formula_bGra + " "); WriteLine(((b > a) ? 1 : 0) == formula_bGra);
                    Write("b>=a " + formula_bGrEqa + " "); WriteLine(((b >= a) ? 1 : 0) == formula_bGrEqa);
                    WriteLine();

                    double formula1_aLessB_old =
                        Math.Floor(Math.Tanh((Math.Abs(b/*a*/) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a/*b*/) / Math.Abs(a - b + 0.0001) + 0.0001)) + withoutEq) *
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq)) * (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq)) + 
                        1 * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq) - 1)* (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq)) + /*b>=0 && a<0*/
                        Math.Floor(Math.Tanh((Math.Abs(a/*b*/) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b/*a*/) / Math.Abs(a - b + -0.0001) + 0.0001)) + withoutEq) *                        
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq) - 1);

                    double formula1_aLessB =
                        Math.Floor(Math.Abs(Math.Tanh(b-a)+0.999999));

                    //Write("\t\t\t" + formula1_aLessB + " " + Math.Floor(formula1_aLessB));
                    WriteLine(((a < b) ? 1 : 0) == formula1_aLessB ? Tests.ThrowCorrect("aLessB") : Tests.ThrowError("aLessB", $"[{a},{b}]=[a<b:{formula1_aLessB}]"));


                    break;
                
                
                case ConditionIf.greater:
                    //if (a < 0 || b < 0) break;

                /*==============================================*/
                    bool isGreater = a > b;
                    
                    if (isGreater) 
                    {
                        WriteLine(valueIf);
                    }
                    else
                    {
                        WriteLine(valueElse);
                    }



                /*==============================================*/
                    //проблемы с числами (-inf.:0)
                    Write(isGreater);
                    Write(":a>b:");
                    float uformula1 = (float)Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + withoutEq);/*<*/
                    WriteLine(uformula1);

                    Write(!isGreater);
                    Write(":a<b:");
                    float uformula2 = (float)Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + 0.0001) + 0.0001)) + withoutEq);
                    Write(uformula2);
                    
                    WriteLine((((isGreater ? 1 : 0) == uformula1) && ((!isGreater ? 1 : 0) == uformula2)) || a == b  ? 
                        Tests.ThrowCorrect("Отрицательные числа, равные") : 
                        Tests.ThrowError("Отрицательные числа, равные", $"[{a},{b}]=[a>b:{uformula1},a<b:{uformula2}]"));
                    Tests.IgnoreLastError();

                    /*
                    float formula1 = (float)
                        Math.Floor(Math.Tanh((a / Math.Abs(a - b + 0.0001)) / (b / Math.Abs(a - b + 0.0001))) + withEq);
                    */


                    //value>=0
                    float formula1 = (float)
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq));//(a>=0)
                    float formula1_obr = (float)
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq)-1);//(a<0)
                    
                    float formula1_Eq = (float)
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withoutEq_2));//(a>0)
                    float formula1_Eq_obr = (float)
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withoutEq_2)-1);//(a<=0)


                    float formula1_EqMod = (float)
                        (Math.Floor(Math.Abs(Math.Tanh(a + 1 + 0.0001) + withEq)));//(|a|>0) (|a|!=0) (a!=0)
                    float formula1_EqMod_old = (float)
                        (Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(a) + 0.0001)) + 1) + withoutEq_2));//(|a|>0) (|a|!=0) (a!=0)
                    WriteLine(((Math.Abs(a) > 0 ? 1 : 0) == formula1_EqMod) ? Tests.ThrowCorrect("|a|>0") : Tests.ThrowError("|a|>0", $"[{a}]=[|a|>0:({formula1_EqMod})]"));


                    float formula1_isNull = (float)
                        (Math.Floor(Math.Abs(Math.Tanh(a + 1 + 0.0001) + withoutEq)));//(|a|==0)
                    float formula1_isNull_old = (float)
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(a) + 0.0001)) + 1) + withoutEq_2)-1);//(|a|==0)
                    WriteLine(((Math.Abs(a) == 0 ? 1 : 0) == formula1_isNull) ? Tests.ThrowCorrect("|a|==0") : Tests.ThrowError("|a|==0", $"[{a}]=[|a|==0:({formula1_isNull})]"));

                    

                    //float formula1_Eq0 = (float)
                    //    (Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withoutEq_2) - 1) *
                    //    (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq)));//(a<=0)*(a>=0)
                    //WriteLine(((a == 0 ? 1 : 0) == formula1_Eq0) ? Tests.ThrowCorrect("a==0") : Tests.ThrowError("a==0", $"[{a}]=[a==0:({formula1_Eq0})]"));

                    //float formula1_notEq0 = (float)
                    //    Math.Abs(Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withoutEq_2) - 1) *
                    //    (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq))-1);//(a<=0)*(a>=0)
                    //WriteLine(((a != 0 ? 1 : 0) == formula1_notEq0) ? Tests.ThrowCorrect("a!=0") : Tests.ThrowError("a!=0", $"[{a}]=[a!=0:({formula1_notEq0})]"));

                    float formula2 = (float)
                        (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq));//(b>=0)
                    float formula2_obr = (float)
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq) - 1);//(b<0)


                    double formula3 =
                        Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq) * Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq); // (a>=0 && b>=0)




                    WriteLine($"a>=0:  [{formula1}:{formula1_obr}]");
                    WriteLine($"b>=0:  [{formula2}:{formula2_obr}]");
                    Write("(a>=0 && b>=0):" + formula3);

                    WriteLine(((a >= 0 && b >= 0 ? 1 : 0) == formula3) ? Tests.ThrowCorrect("2") : Tests.ThrowError("2",$"[{a},{b}]={formula3}")) ;


                    //поддерживает отрицательные, положительные и ноль
                    double formula1_aGreaterB =
                        Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + withoutEq) *
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq)) * (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq)) + /*(a>=0 && b>=0)*/

                        1 *
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq)) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq) - 1) + /*(a>=0 && b<0)*/

                        /*0 * (Math.Floor(Math.Tanh(b + 0.0001)) + 1) * Math.Abs(Math.Floor(Math.Tanh(a + 0.0001))) +*/ /*(a<0 && b>=0) всегда 0, не учитываем*/

                        Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + -0.0001) + 0.0001)) + withoutEq) *
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq) - 1);   /*(a<0 && b<0)*/
                    


                    Write("\t\t\t" + formula1_aGreaterB + " " + Math.Floor(formula1_aGreaterB));
                    WriteLine(((a > b) ? 1 : 0) == formula1_aGreaterB ? Tests.ThrowCorrect("4") : Tests.ThrowError("4"));




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

                    double formula_aLessEqB =
                        /*Math.Floor*/(Math.Tanh((Math.Abs(b/*a*/) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a/*b*/) / Math.Abs(a - b + 0.0001) + 0.0001)) + withEq) *
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq)) * (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq)) +
                        1 * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq) - 1) * (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq)) + /*b>=0 && a<0*/
                        /*Math.Floor*/(Math.Tanh((Math.Abs(a/*b*/) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b/*a*/) / Math.Abs(a - b + -0.0001) + 0.0001)) + withEq) *
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq) - 1) *
                        
                        ((Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(a) + 0.0001)) + 1) + withoutEq_2)) * (Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(b) + 0.0001)) + 1) + withoutEq_2))) +/*|a|>0 && |b|>0 (не ноль)*/
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(a) + 0.0001)) + 1) + withoutEq_2) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(b) + 0.0001)) + 1) + withoutEq_2) - 1);


                    WriteLine(((a <= b ? 1 : 0) == Math.Floor(formula_aLessEqB)) ? Tests.ThrowCorrect("a <= b") : Tests.ThrowError("a <= b", $"[{a},{b}]={formula_aLessEqB}"));
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

                    double formula1_aGreaterEqB =
                         Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + withEq) *
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq)) * (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq)) + /*(a>=0 && b>=0)*/
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq)) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq) - 1) + /*(a>=0 && b<0)*/                                                
                        Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + -0.0001) + 0.0001)) + withEq) *
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq) - 1) *   /*(a<0 && b<0)*/

                        ((Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(a) + 0.0001)) + 1) + withoutEq_2)) * (Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(b) + 0.0001)) + 1) + withoutEq_2))) +/*|a|>0 && |b|>0 (не ноль)*/
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(a) + 0.0001)) + 1) + withoutEq_2) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(b) + 0.0001)) + 1) + withoutEq_2) - 1);

                        WriteLine(((a >= b) ? 1 : 0) == Math.Floor(formula1_aGreaterEqB) ? Tests.ThrowCorrect("a >= b") : Tests.ThrowError("a >= b", $"[{a},{b}]={formula1_aGreaterEqB}"));
                    break;
                default:
                    break;
            }
        }




        public static void Test_If_siple(Vector2 interval_a, Vector2 interval_b, int delta = 1, ConditionIf cIf = ConditionIf.equals) 
        {
            Tests.skipLargeStat = true;
            for (int a  = (int)interval_a.X; a < interval_a.Y; a += delta)
            {
                for (int b = (int)interval_b.X; b < interval_b.Y; b += delta)
                {                   
                    If_siple(a, b, cIf);
                    Tests.ShowProgress(180000,Tests.CountTests(new List<Vector2>() { interval_a, interval_b }, delta),a,b);
                    WriteLine();
                }
            }
            Tests.ShowStat(onDetails:true);
        }

        public static void If_nested(float a, float b, float c, float d, ConditionIf cIf = ConditionIf.equals)
        {
            WriteLine($"[a={a},b={b}]");
            float valueIfIf = 0;
            float valueIfElse = 1;
            float valueElseIf = 10;
            float valueElseElse = 11;
            switch (cIf)
            {
                case ConditionIf.equals:
                    break;
                case ConditionIf.less:
                    break;
                case ConditionIf.greater:
                    break;
                case ConditionIf.less_eq:
                    break;
                case ConditionIf.greater_eq:

                    if (a >= b)
                    {
                        if (c >= d)
                        {
                            WriteLine(valueIfIf);
                        }
                        else
                        {
                            WriteLine(valueIfElse);
                        }
                    }
                    else
                    {
                        if (c >= d)
                        {
                            WriteLine(valueElseIf);
                        }
                        else
                        {
                            WriteLine(valueElseElse);
                        }
                    }

                    double formula_ifif =
                        (Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + withEq) *
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq)) * (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq)) + /*(a>=0 && b>=0)*/
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq)) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq) - 1) + /*(a>=0 && b<0)*/
                        Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + -0.0001) + 0.0001)) + withEq) *
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + withEq) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + withEq) - 1) *   /*(a<0 && b<0)*/
                        ((Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(a) + 0.0001)) + 1) + withoutEq_2)) * (Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(b) + 0.0001)) + 1) + withoutEq_2))) +/*|a|>0 && |b|>0 (не ноль)*/
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(a) + 0.0001)) + 1) + withoutEq_2) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(b) + 0.0001)) + 1) + withoutEq_2) - 1)) *

                        (Math.Floor(Math.Tanh((Math.Abs(c) / Math.Abs(c - d + 0.0001)) / (Math.Abs(d) / Math.Abs(c - d + 0.0001) + 0.0001)) + withEq) *
                        (Math.Floor(Math.Tanh((Math.Tanh(c + 0.0001)) + 1) + withEq)) * (Math.Floor(Math.Tanh((Math.Tanh(d + 0.0001)) + 1) + withEq)) + /*(c>=0 && d>=0)*/
                        (Math.Floor(Math.Tanh((Math.Tanh(c + 0.0001)) + 1) + withEq)) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(d + 0.0001)) + 1) + withEq) - 1) + /*(c>=0 && d<0)*/
                        Math.Floor(Math.Tanh((Math.Abs(d) / Math.Abs(c - d + 0.0001)) / (Math.Abs(c) / Math.Abs(c - d + -0.0001) + 0.0001)) + withEq) *
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(c + 0.0001)) + 1) + withEq) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(d + 0.0001)) + 1) + withEq) - 1) *   /*(c<0 && d<0)*/
                        ((Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(c) + 0.0001)) + 1) + withoutEq_2)) * (Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(d) + 0.0001)) + 1) + withoutEq_2))) +/*|c|>0 && |d|>0 (не ноль)*/
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(c) + 0.0001)) + 1) + withoutEq_2) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(Math.Abs(d) + 0.0001)) + 1) + withoutEq_2) - 1));

                    WriteLine(((a >= b && c >= d+1 ? 1 : 0) == formula_ifif) ? Tests.ThrowCorrect("a >= b && c >= d") : Tests.ThrowError("a >= b && c >= d", $"[{a},{b},{c},{d}]={formula_ifif}"));
                    
                    
                    
                    break;
            }
        }
        public static void Test_If_nested(Vector2 interval_a, Vector2 interval_b, Vector2 interval_c, Vector2 interval_d, int delta = 1, ConditionIf cIf = ConditionIf.equals)
        {
            Tests.skipLargeStat = true;
            for (int a = (int)interval_a.X; a < interval_a.Y; a += delta)
            {
                for (int b = (int)interval_b.X; b < interval_b.Y; b += delta)
                {
                    for (int c = (int)interval_c.X; c < interval_c.Y; c += delta)
                    {
                        for (int d = (int)interval_d.X; d < interval_d.Y; d += delta)
                        {
                            If_nested(a, b, c, d, cIf);
                            Tests.ShowProgress(180000, Tests.CountTests(new List<Vector2>() { interval_a, interval_b , interval_c , interval_d }, delta), a, b);
                            WriteLine();
                        }
                    }
                }
            }
            Tests.ShowStat(onDetails: true);
        }
    }
}