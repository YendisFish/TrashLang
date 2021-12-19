using System;

namespace Interpreter
{
    public class EntryPointHandler
    {
        public static void Main(string[] args)
        {
            if(args != null)
            {
                if (args[0].Contains(".trash"))
                {
                    if (File.Exists(args[0]))
                    {
                        Console.WriteLine("Interpreting TrashFile...");

                        Processors.Processor.Entrypoint(args[0]);
                    }
                }
            }
        }
    }
}