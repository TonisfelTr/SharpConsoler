using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoler
{
    class ConsolerCommand
    {
        private string CommandName { get; set; }
        private List<String> CommandFlags { get; set; }
        private List<String> CommandArgs { get; set; }
        private string ShortHelpText { get; set; }
        private string FullHelpText { get; set; }
        public virtual void executeCommand()
        {
            this.CommandFlags.Clear();
            this.CommandArgs.Clear();
        }
        public ConsolerCommand(string name, List<String> flags, List<String> args)
        {
            this.CommandName = name;
            this.CommandFlags = flags;
            this.CommandArgs = args;
        }

        public ConsolerCommand()
        {

        }
        public ConsolerCommand setShortHelpText(string text)
        {
            this.ShortHelpText = text;
            return this;
        }
        public string getShortHelpText()
        {
            return this.ShortHelpText;
        }
        public ConsolerCommand setFullHelpText(string text)
        {
            this.FullHelpText = text;
            return this;
        }
        public string getFullHelpText()
        {
            if (this.FullHelpText != null)
                return this.FullHelpText;
            else
                return this.ShortHelpText;
        }
        public string getCommandName()
        {
            return this.CommandName;
        }

        public ConsolerCommand setCommandName(string name)
        {
            this.CommandName = name;
            return this;
        }
        public string getArg(string flagName)
        {
            if (this.CommandFlags.IndexOf(flagName) < 0)
            {
                Console.WriteLine("Command cannot be executed without flags.");
                return "";
            }

            if (flagName[0] != '-' &&
                        this.CommandArgs[this.CommandFlags.IndexOf(flagName)] != "")
                return this.CommandArgs[this.CommandFlags.IndexOf(flagName)];
            else
            {
                Console.WriteLine("One of flags has no argument. If it's ever exist (f.e. symbols), use quotes.");
                return "";
            }
        }
        public string getArg(string[] flagNames)
        {
            System.Collections.IEnumerator e = flagNames.GetEnumerator();
            e.Reset();
            while (e.MoveNext())
            {
                if (this.CommandFlags.IndexOf(e.Current.ToString()) != -1)
                {
                    if (e.Current.ToString()[0] != '-' && 
                        this.CommandArgs[this.CommandFlags.IndexOf(e.Current.ToString())] != "")
                        return this.CommandArgs[this.CommandFlags.IndexOf(e.Current.ToString())];
                    else
                    {
                        Console.WriteLine("One of flags has no argument. If it's ever exist (f.e. symbols), use quotes.");
                        return "";
                    }
                }
            }

            Console.WriteLine("Invalid syntax: argument is set but flag is not exist.");
            return "";
        }
        public bool isFlagExist(string flagName)
        {
            bool isFlagExist = this.CommandFlags.IndexOf(flagName) != -1;
            if (isFlagExist)
            {
                if (flagName[0] == '-')
                {
                    return true;
                }
                else
                {
                    if (this.CommandArgs[this.CommandFlags.IndexOf(flagName)] == "")
                        Console.WriteLine("One of flags has no argument. If it's ever exist (f.e. symbols), use quotes.");
                    return true;
                }
            }

            return false;
        }
        public bool isFlagsNotEmpty()
        {
            if (this.CommandFlags.Count > 0)
                return true;
            else
                return false;
        }
        public void Merge(ConsolerCommand command)
        {
            this.CommandArgs = command.CommandArgs;
            this.CommandFlags = command.CommandFlags;
        }       
    }
}
