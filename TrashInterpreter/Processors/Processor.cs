using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Processors
{
    public class Processor
    {
        public static void Entrypoint(string filepath)
        {
            string raw = File.ReadAllText(filepath);
            string[] formatted = File.ReadAllLines(filepath);

            string className;

            try
            {
                className = formatted[0].Replace('{', ' ').Trim();
            } 
            catch(Exception ex)
            {
                throw;
            }
            
            if(formatted[0].Contains("{"))
            {
                HandleBody(formatted);
            }
        }

        public static void HandleBody(string[] body)
        {
            Dictionary<string, string> stringvars = new();
            Dictionary<string, int> intvars = new();

            foreach (string val in body)
            {
                if (val != "}")
                {
                    if (val.Contains("loop"))
                    {
                        char[] getchars = val.ToCharArray();

                        string args = "";

                        bool gettingchars = false;

                        for(int x = 0; x < getchars.Count(); x++)
                        {
                            if(getchars[x] == '(')
                            {
                                gettingchars = true;
                            }

                            if(getchars[x] == ')')
                            {
                                gettingchars = false;
                            }

                            if (gettingchars = true)
                            {
                                args = args + getchars[x].ToString();
                            }
                        }

                        string[] loopargs = args.Split(':');

                        if(loopargs.Count() > 0)
                        {
                            ProcessLoop(loopargs, stringvars, intvars);
                        }
                    }

                    if(val.Contains("hair"))
                    {
                        string getchars = val.Replace("hair", "").Trim();

                        string args = "";

                        bool gettingchars = true;


                        string[] toFormat = getchars.Split('=');


                        List<string> ToConvert = new();
                        
                        foreach(string argstr in toFormat)
                        {
                            ToConvert.Add(argstr.Trim());
                        }
                        
                        string[] converted = ToConvert.ToArray();

                        Tuple<string, string> formatted = FormatString(converted);

                        stringvars.Add(formatted.Item1, formatted.Item2);

                        //stringvars.Add(args);
                        //FORMAT STRING VARS HERE!!!!!
                    }

                    if (val.Contains("number"))
                    {
                        char[] getchars = val.Replace("number", "").Trim().ToCharArray();

                        string args = "";

                        bool gettingchars = true;

                        for (int x = 0; x < getchars.Count(); x++)
                        {
                            if (getchars[x] == '=')
                            {
                                gettingchars = false;
                            }

                            if (gettingchars = true)
                            {
                                args = args + getchars[x].ToString();
                            }
                        }

                        //intvars.Add(args);
                        //FORMAT INT VARS HERE!!!!
                    }
                }
            }
        }

        public static Tuple<string, string> FormatString(string[] args)
        {
            return (args[0], args[1]).ToTuple<string, string>();
        }

        public static void ExecuteLoopArgs(string args)
        {
            if(args.Contains("output"))
            {
                string[] formatOutput = args.Split('/');
                Console.WriteLine(formatOutput[1]);
            }
        }

        public static void ProcessLoop(string[] args, Dictionary<string, string> strings, Dictionary<string, int> integers) 
        {
            foreach(string key in integers.Keys)
            {
                if(args[0].Contains(key))
                {
                    if(args[0].Contains(">"))
                    {
                        int currentKeyValue = Convert.ToInt32(args[0].Split('>')[1].Trim());

                        while(integers[key] == currentKeyValue)
                        {
                            ExecuteLoopArgs(args[1]);
                        }
                    }
                }
            }
        }
    }
}
