using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoler.Commands
{
    class ExitCommand : ConsolerCommand
    {
        public sealed override void executeCommand()
        {
            Console.WriteLine("Bye!");
            Environment.Exit(1);  
        }
    }
}
