using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consoler;

namespace Consoler.Commands
{
    class SayCommand : ConsolerCommand
    {
        public sealed override void executeCommand()
        {
            if (this.isFlagExist("pl"))
            {
                Console.WriteLine(this.getArg("pl"));
            }
            if (this.isFlagExist("p"))
            {
                Console.Write(this.getArg("p"));
            }
            base.executeCommand();
        }
    }
}
