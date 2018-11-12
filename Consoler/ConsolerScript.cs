using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoler
{
    sealed class ConsolerScript
    {
        public static void LoadScript(string path)
        {
            try
            {
                FileStream script = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(script);

                Console.WriteLine("Executing script...");
                while (reader.EndOfStream) 
                {
                    ConsolerMain.parseCommand(reader.ReadLine());
                }

                reader.Close();
                script.Close();
            } catch (FileNotFoundException)
            {
                Console.WriteLine("This script file is not exist.");
            } catch (ArgumentException)
            {
                Console.WriteLine("You didn't set path to a script.");
            }

        }
    }
}
