using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoler.Commands
{
    class HelpCommand : ConsolerCommand
    {
        private List<ConsolerCommand> commandList;
        
        private bool isCommandExists(string cmdName)
        {
            IEnumerator<ConsolerCommand> e = this.commandList.GetEnumerator();
            e.Reset();
            while (e.MoveNext())
            {
                if (e.Current.getCommandName() == cmdName)
                    return true;
            }

            return false;
        }
        private int GetCommandIndex(string cmdName)
        {
            IEnumerator<ConsolerCommand> e = this.commandList.GetEnumerator();
            e.Reset();
            while (e.MoveNext())
            {
                if (e.Current.getCommandName() == cmdName)
                    return this.commandList.IndexOf(e.Current);
            }

            return -1;
        }
        private ConsolerCommand GetCommand(string cmdName)
        {
            IEnumerator<ConsolerCommand> e = this.commandList.GetEnumerator();
            e.Reset();
            while (e.MoveNext())
            {
                if (e.Current.getCommandName() == cmdName)
                    return e.Current;
            }

            throw new Exception("This command is not exist.");
        }

        public HelpCommand(List<ConsolerCommand> commandList)
        {
            this.commandList = commandList;
        }
        public sealed override void executeCommand()
        {
            if (this.isFlagsNotEmpty())
            {
                string[] flags = new string[2];
                flags[0] = "full";
                flags[1] = "f";

                string commandName = this.getArg(flags);
                if (this.isCommandExists(commandName))
                {
                    Console.WriteLine("Command \"" + commandName + "\"");
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine(this.GetCommand(commandName).getFullHelpText());
                    Console.WriteLine("---------------------------------------");
                }
                else
                {
                    if (commandName != "")
                        Console.WriteLine("Command \"" + commandName + "\" is not exist or not registred.");
                } 
            } else
            {
                Console.WriteLine("Commands of consoler: ");
                Console.WriteLine("---------------------------------------");
                foreach (ConsolerCommand command in commandList)
                {
                    Console.WriteLine(command.getCommandName() + " - " + command.getShortHelpText());
                }
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Type \"help -f <command_name>\" to get full help if it's exist.");
            }
            base.executeCommand();
            return;
        }
    }
}
