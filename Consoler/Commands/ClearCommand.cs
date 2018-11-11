using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consoler;

namespace Consoler.Commands
{
    class ClearCommand : ConsolerCommand
    {
        public sealed override void executeCommand()
        {
            Console.Clear();
            base.executeCommand();
        }
    }
}
