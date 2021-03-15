using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

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

        public static void ShowProgress(int delta, long countAllTests = 0, int a = 0, int b = 0)
        {
            if (!If_part.writeAll)
            {
                if (countTests % delta == 0) Console.WriteLine($"Complete tests: {countTests}/{countAllTests}({Math.Round(((float)countTests / countAllTests) * 100, 1)}%), Errors: {countErrors}. [a = {a},b = {b}].");
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
                        typeDict.Add(typeMsg, newVal + 1);
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
                if (dict.Key == typeMsg) break;
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
            lastTypeError = typeTests;
            PlusOne(Tests.typeTests, typeTests);

            return MsgIndent("correct", typeTests, Tests.typeTests);
        }

        public static void IgnoreLastError()
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
                                int leftMsg = detail.Value.Count;
                                foreach (var Msg in detail.Value)
                                {
                                    if (show-- > 0)
                                    { 
                                        Console.WriteLine($"    {Msg}"); leftMsg--;
                                    }
                                    else if (skip-- > 0) 
                                    {
                                        if (skip >= leftMsg) 
                                        {
                                            Console.WriteLine($"        [.....] hide {leftMsg}.");
                                            break;
                                        }
                                        else
                                        {
                                            leftMsg--;
                                            continue;
                                        }
                                    } 
                                    else
                                    {
                                        show = SHOW;
                                        skip = SKIP;
                                        if (skip != 0) Console.WriteLine($"        [.....] hide {SKIP}.");                                        
                                        SKIP = (int)(SKIP * 1.3);
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
}
