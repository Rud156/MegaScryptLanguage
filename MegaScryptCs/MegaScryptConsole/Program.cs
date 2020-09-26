using System;
using System.Collections.Generic;
using MegaScrypt;

namespace MegaScryptConsole
{
    class Program
    {
        static object Print(List<object> parameters)
        {
            foreach (var parameter in parameters)
            {
                Console.WriteLine(parameter.ToString());
            }

            return null;
        }

        static object Abs(List<object> parameters)
        {
            int i = (int)parameters[0];
            return Math.Abs(i);
        }

        static void Main(string[] args)
        {
            Machine machine = new Machine();

            machine.Declare(Print);
            machine.Declare(Abs, new string[] { "i" });

            string script = "";
            string line;

            while (true)
            {
                line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    try
                    {
                        machine.Execute(script);
                        //PrintVariables(machine);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                }
                else
                {
                    script += line + "\n";
                }
            }

            Console.ReadKey();
        }

        static void PrintVariables(Machine machine)
        {
            List<string> varNames = machine.Target.VariableNames;
            foreach (string varName in varNames)
            {
                Console.WriteLine($"{varName} = {machine.Target.Get(varName)}");
            }
        }
    }
}
