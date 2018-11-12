using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consoler;

namespace Consoler.Commands
{
    class ScriptCommand : ConsolerCommand
    {
        public sealed override void executeCommand()
        {
            ConsolerScript.LoadScript(this.getArg("p"));
            base.executeCommand();
        }
    }
}
