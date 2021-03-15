using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace tests
{
    static class Tests 
    {
        private static string lastTypeError = "";
        public static bool skipLargeStat = true;

        private static int countTests = 0;
        private static int countErrors = 0;
        private static Dictionary<string, int> typeError = new Dictionary<string, int>();
        private static Dictionary<string, List<string>> typeDetailsError = new Dictionary<string, List<string>>();
        private static Dictionary<string, int> typeTests = new Dictionary<string, int>();
        private static List<string> ignorList = new List<string>();

        public static long CountTests(List<Vector2> values, int delta) 
        {
            long countTests = 1;
            foreach (var val in values)
            {
                countTests *= (long)(val.Y - val.X);
            }
            return (long)((countTests * typeTests.Count) / (Math.Pow(delta, values.Count)));
        }

        public static void ShowProgress(int delta,long countAllTests = 0, int a=0, int b=0)
        {
            if (!If_part.writeAll) 
            {
                if(countTests % delta == 0) Console.WriteLine($"Complete tests: {countTests}/{countAllTests}({Math.Round(((float)countTests/countAllTests)*100,1)}%), Errors: {countErrors}. [a = {a},b = {b}].");
            }
        }

        public static void PlusOne(Dictionary<string, int> typeDict, string typeMsg) 
        {
            if (typeDict.TryGetValue(typeMsg, out _))
            {
                foreach (var dict in typeDict)
                {
                    if (dict.Key == typeMsg)
                    {                       
                        int newVal = dict.Value;
                        typeDict.Remove(typeMsg);
                        typeDict.Add(typeMsg, newVal+1);                        
                        break;
                    }

                }
            }
            else
                typeDict.Add(typeMsg, 1);
        }
        public static void PlusMsg(Dictionary<string, List<string>> typeDict, string typeMsg, string Msg)
        {
            if (typeDict.TryGetValue(typeMsg, out _))
            {
                foreach (var dict in typeDict)
                {
                    if (dict.Key == typeMsg)
                    {
                        List<string> newVal = dict.Value;
                        typeDict.Remove(typeMsg);
                        newVal.Add(Msg);
                        typeDict.Add(typeMsg, newVal);
                        break;
                    }
                }
            }
            else
                typeDict.Add(typeMsg, new List<string>() { Msg });
        }

        public static string MsgIndent(string Msg, string typeMsg, Dictionary<string, int> typeDict) 
        {
            int defaultIndent = 3;
            foreach (var dict in typeDict)
            {
                if(dict.Key == typeMsg) break;
                defaultIndent++;
            }

            string indent = "";
            for (int i = 0; i < defaultIndent; i++)
            {
                indent += "\t";
            }
            return indent + Msg;
        }

        public static string ThrowError(string typeError, string typeDetailsError = "No details.") 
        {
            countTests++;
            countErrors++;
            lastTypeError = typeError;
            PlusOne(Tests.typeError, typeError);
            if (typeDetailsError != "No details.") 
            {
                PlusMsg(Tests.typeDetailsError, typeError, typeDetailsError);
            }

            return MsgIndent("INCORRECT", typeError, Tests.typeError);
        }
        public static string ThrowCorrect(string typeTests)
        {
            countTests++;
            PlusOne(Tests.typeTests, typeTests);

            return MsgIndent("correct", typeTests, Tests.typeTests);
        }

        public static void IgnoreError()
        {
            foreach (var item in ignorList)
            {
                if (item == lastTypeError) return;
            }
            ignorList.Add(lastTypeError);            
        }

        private static bool isIgnored(string typeError) 
        {
            foreach (var item in ignorList)
            {
                if (item == typeError) return true;
            }
            return false;
        }

        public static void ShowErrors(bool onDetails = false)
        {            
            Console.WriteLine("\n\nCount Errors:" + countErrors);
            for (int i = 0; i < 25; i++) Console.Write("="); Console.WriteLine();

            foreach (var error in typeError)
            {
                Console.WriteLine($"Erorr <{error.Key}>: {error.Value}");
                if (isIgnored(error.Key))
                {
                    Console.WriteLine("\tIgnorred.");
                }
                else 
                { 
                    if (onDetails)
                    {
                        int SHOW, show; SHOW = 3; show = 10;
                        int SKIP, skip; SKIP = skip = skipLargeStat ? 350 : 0;
                        Console.WriteLine("  Details error:");
                        foreach (var detail in typeDetailsError)
                        {
                            if (detail.Key == error.Key)
                            {
                                foreach (var Msg in detail.Value)
                                {
                                    if (show-- > 0)
                                        Console.WriteLine($"    {Msg}");
                                    else if (skip-- > 0) continue;
                                    else
                                    {
                                        show = SHOW;
                                        skip = SKIP;
                                        SKIP = (int)(SKIP * 1.3);
                                        if (skip != 0) Console.WriteLine($"        [.....] hide {SKIP}.");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        public static void ShowStat(bool onDetails = false)
        {
            ShowErrors(onDetails);
            Console.WriteLine("\nAll Tests:" + countTests);
            for (int i = 0; i < 25; i++) Console.Write("="); Console.WriteLine();

            foreach (var test in typeTests)
            {
                Console.WriteLine($"Complete <{test.Key}>: {test.Value}");
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
                    double formula1_aEqB =
                        Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + 0.2384062) *
                        Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + 0.0001) + 0.0001)) + 0.2384062);

                    double formula1_Part1_aEqB =
                        /*Math.Floor*/(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + 0.2384062);
                    
                    double formula1_Part2_aEqB =
                       /*Math.Floor*/(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + 0.0001) + 0.0001)) + 0.2384062);

                    WriteLine(((a == b) ? 1 : 0) == formula1_aEqB ? Tests.ThrowCorrect("aEqB") : Tests.ThrowError("aEqB "+((a<0&&b>0 || b<0&&a>0)?"разные знаки":"одного знака"), $"[{a},{b}]=[a=b:{formula1_aEqB}({formula1_Part1_aEqB}*{formula1_Part2_aEqB})]"));
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
                    bool isLess = a < b;
                    if (isLess) 
                    {
                        WriteLine(valueIf);
                    }
                    else
                    {
                        WriteLine(valueElse);
                    }

                    WriteLine(Math.Floor(Math.Tanh((a / Math.Abs(a - b + 0.0001)) / (b / Math.Abs(a - b + 0.0001))) + 0.2384062));//устарело
                    Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + 0.2384058);

                    WriteLine(Math.Floor(Math.Tanh((b / Math.Abs(a - b + 0.0001)) / (a / Math.Abs(a - b + 0.0001))) + 0.2384062/*<=*/));//устарело
                    Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + 0.0001) + 0.0001)) + 0.2384058);


                    double formula1_aLessB =
                        Math.Floor(Math.Tanh((Math.Abs(b/*a*/) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a/*b*/) / Math.Abs(a - b + 0.0001) + 0.0001)) + 0.2384058) *
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + 0.2384062)) * (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062)) + 
                        1 * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + 0.2384062) - 1)* (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062)) + /*b>=0 && a<0*/
                        Math.Floor(Math.Tanh((Math.Abs(a/*b*/) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b/*a*/) / Math.Abs(a - b + -0.0001) + 0.0001)) + 0.2384058) *
                        
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + 0.2384062) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062) - 1);  

                    Write("\t\t\t" + formula1_aLessB + " " + Math.Floor(formula1_aLessB));
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
                    float uformula1 = (float)Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001)+ 0.0001) ) + 0.2384058);/*<*/
                    WriteLine(uformula1);

                    Write(!isGreater);
                    Write(":a<b:");
                    float uformula2 = (float)Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + 0.0001)+ 0.0001) ) + 0.2384058);
                    Write(uformula2);
                    
                    WriteLine((((isGreater ? 1 : 0) == uformula1) && ((!isGreater ? 1 : 0) == uformula2)) || a == b  ? 
                        Tests.ThrowCorrect("Отрицательные числа, равные") : 
                        Tests.ThrowError("Отрицательные числа, равные", $"[{a},{b}]=[a>b:{uformula1},a<b:{uformula2}]"));
                    Tests.IgnoreError();

                    /*
                    float formula1 = (float)
                        Math.Floor(Math.Tanh((a / Math.Abs(a - b + 0.0001)) / (b / Math.Abs(a - b + 0.0001))) + 0.2384062);
                    */


                    //value>=0
                    float formula1 = (float)
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001))+1) + 0.2384062));//(a>=0)
                    float formula1_obr = (float)
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + 0.2384062)-1);//(a<0)
                    
                    float formula1_Eq = (float)
                        (/*Math.Floor*/(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + 0.2383638));//(a<=0)
                    WriteLine(((a > 0 ? 1 : 0) == Math.Floor(formula1_Eq)) ? Tests.ThrowCorrect("a>0") : Tests.ThrowError("a>0", $"[{a}]=[a>0:({formula1_Eq};{Math.Floor(formula1_Eq)})]"));

                    float formula2 = (float)
                        (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062));//(b>=0)
                    float formula2_obr = (float)
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062) - 1);//(b<0)


                    double formula3 =
                        Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + 0.2384062) * Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062); // (a>=0 && b>=0)




                    WriteLine($"a>=0:  [{formula1}:{formula1_obr}]");
                    WriteLine($"b>=0:  [{formula2}:{formula2_obr}]");
                    Write("(a>=0 && b>=0):" + formula3);

                    WriteLine(((a >= 0 && b >= 0 ? 1 : 0) == formula3) ? Tests.ThrowCorrect("2") : Tests.ThrowError("2",$"[{a},{b}]={formula3}")) ;


                    //поддерживает отрицательные, положительные и ноль
                    double formula1_aGreaterB =
                        Math.Floor(Math.Tanh((Math.Abs(a) / Math.Abs(a - b + 0.0001)) / (Math.Abs(b) / Math.Abs(a - b + 0.0001) + 0.0001)) + 0.2384058) *
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + 0.2384062)) * (Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062)) + /*(a>=0 && b>=0)*/

                        1 *
                        (Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + 0.2384062)) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062) - 1) + /*(a>=0 && b<0)*/

                        /*0 * (Math.Floor(Math.Tanh(b + 0.0001)) + 1) * Math.Abs(Math.Floor(Math.Tanh(a + 0.0001))) +*/ /*(a<0 && b>=0) всегда 0, не учитываем*/

                        Math.Floor(Math.Tanh((Math.Abs(b) / Math.Abs(a - b + 0.0001)) / (Math.Abs(a) / Math.Abs(a - b + -0.0001) + 0.0001)) + 0.2384058) *
                        Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(a + 0.0001)) + 1) + 0.2384062) - 1) * Math.Abs(Math.Floor(Math.Tanh((Math.Tanh(b + 0.0001)) + 1) + 0.2384062) - 1);   /*(a<0 && b<0)*/
                    


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
            Tests.skipLargeStat = true;
            for (int a  = (int)interval_a.X; a < interval_a.Y; a += delta)
            {
                for (int b = (int)interval_b.X; b < interval_b.Y; b += delta)
                {                   
                    If_siple(a, b, cIf);
                    Tests.ShowProgress(130000,Tests.CountTests(new List<Vector2>() { interval_a, interval_b }, delta),a,b);
                    WriteLine();
                }
            }
            Tests.ShowStat(onDetails:true);
        }

        public static void If_nested()
        {

        }
    }
}
