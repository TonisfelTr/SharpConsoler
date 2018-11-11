using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consoler;

namespace Consoler.Commands
{
    class VersionCommand : ConsolerCommand
    {
        public sealed override void executeCommand()
        {
            Console.WriteLine("Version of Consoler: " + ConsolerMain.CR_VERSION);
            base.executeCommand();
        }
    }
}
