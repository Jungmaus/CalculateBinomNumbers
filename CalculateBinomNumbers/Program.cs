using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CalculateBinomNumbers
{
    class Program
    {
        static List<BinomItem> binomArchitecht = new List<BinomItem>();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("We got ready in => " + SetBinomArchitecht());
            Console.WriteLine();
            Console.WriteLine();

            do
            {
                Console.Write("Which binom result you want to see ? ((a+b)^2 = 2 <= Just enter this number) => ");
                string readLine = Console.ReadLine();

                int binomNumber;
                if(int.TryParse(readLine, out binomNumber) && binomNumber <= 50)
                {

                    string binomResult = GetBinomResult(binomNumber);
                    if (!string.IsNullOrEmpty(binomResult))
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.Write("Binom Structure => " + binomResult.Split('/')[0]);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.Write(binomResult.Split('/')[1]);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.Write(binomResult.Split('/')[2]);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.Write("Press any key..");

                        Console.ReadKey();
                    }
                }

               
                Console.Clear();
            }
            while (true);

        }

        private static string GetBinomResult(int number)
        {
            for (int i = 0; i < binomArchitecht.Count - 1; i++)
            {
                if (binomArchitecht[i] != null && binomArchitecht[i].Number == number)
                    return binomArchitecht[i].Result + "/" + binomArchitecht[i].PositiveFullResult + "/" + binomArchitecht[i].NegativeFullResult;
            }
            return "";
        }

        private static TimeSpan SetBinomArchitecht()
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                string result = "";
                string[] splitted;
                BinomItem item;
                for (int i = 0; i <= 51; i++)
                {
                    result = "";
                    item = new BinomItem();
                    item.Number = i;
                    for (int z = 1; z <= (i + 1); z++)
                    {
                        if (z == 1 && (i + 1) != 1)
                            result += "1-";
                        else if (z == 1 || (z == (i + 1)))
                            result += "1";
                        else
                        {
                            splitted = binomArchitecht[i - 1].Result.Split('-');

                            if ((z - 2) >= 0 && (z - 1) >= 0)
                                result += (Convert.ToInt64(splitted[z - 2]) + Convert.ToInt64(splitted[z - 1])) + "-";
                            else
                                result += (Convert.ToInt64(splitted[0]) + Convert.ToInt64(splitted[0])) + "-";
                        }
                    }
                    item.Result = result;

                    splitted = item.Result.Split('-');
                    item.PositiveFullResult = $"(a + b)^{i} = ";
                    item.NegativeFullResult = $"(a - b)^{i} = ";

                    for (int x = 0; x < splitted.Length; x++)
                    {
                        if (x == 0)
                        {
                            item.PositiveFullResult += $"a^{i}";
                            item.NegativeFullResult += $"a^{i}";
                        }
                        else if (x == splitted.Length - 1)
                        {
                            item.PositiveFullResult += $" + b^{i}";
                            item.NegativeFullResult += $" - b^{i}";
                        }
                        else
                        {
                            item.PositiveFullResult += $" + {splitted[x]}a^{i - x}b^{x}";
                            item.NegativeFullResult += $" - {splitted[x]}a^{i - x}b^{x}";
                        }

                    }

                    if (binomArchitecht.Count != 0)
                        binomArchitecht.Insert(binomArchitecht.Count, item);
                    else
                        binomArchitecht.Insert(0, item);
                }
                sw.Stop();
                return sw.Elapsed;
            }
            catch(Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Oh sh*t here we go again.");
                Console.WriteLine();
                Console.Write(ex);
                return new TimeSpan();
            }
        }

        private class BinomItem
        {
            public int Number { get; set; }
            public string Result { get; set; }
            public string PositiveFullResult { get; set; }
            public string NegativeFullResult { get; set; }
        }
    }
}
