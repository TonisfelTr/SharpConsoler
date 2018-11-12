using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Consoler.Commands;

namespace Consoler
{
    class ConsolerMain
    {
        public const string CR_AUTHOR = "Ilya Bagdanov";
        public const double CR_VERSION = 1.31;

        private static List<ConsolerCommand> commands = new List<ConsolerCommand>();

        private static bool isCommandExists(string cmdName)
        {
            IEnumerator<ConsolerCommand> e = commands.GetEnumerator();
            e.Reset();
            while (e.MoveNext())
            {
                if (e.Current.getCommandName() == cmdName)
                    return true;
            }

            return false;
        }
        private static ConsolerCommand GetCommand(string cmdName)
        {
            IEnumerator<ConsolerCommand> e = commands.GetEnumerator();
            e.Reset();
            while (e.MoveNext())
            {
                if (e.Current.getCommandName() == cmdName)
                    return e.Current;
            }

            throw new Exception("This command is not exist.");
        }
        public static ConsolerCommand parseCommand(string cmd)
        {
            int spaceIndex;
            string name, tmp;
            List<String> flags = new List<String>();
            List<string> arguments = new List<String>();
            cmd = cmd.Trim(' ');
            spaceIndex = cmd.IndexOf(" ");

            if (spaceIndex == -1)
                spaceIndex = cmd.Length;

            name = cmd.Substring(0, spaceIndex);

            if (spaceIndex != cmd.Length) { 
                cmd = cmd.Substring(spaceIndex+1, cmd.Length-spaceIndex-1);

                while (cmd.Length > 0)
                {
                    if (cmd[0] != '-')
                    {
                        Console.WriteLine("Invalid syntax.");
                        break;
                    }
                    if (cmd.IndexOf(" ") > 0)
                        flags.Add(cmd.Substring(1, cmd.IndexOf(" ")-1));
                    else
                        flags.Add(cmd.Substring(1, cmd.Length-1));
                    arguments.Add("");

                    if (flags.Last()[0] == '-')
                    {
                        cmd = cmd.Substring(cmd.IndexOf(flags.Last()[flags.Last().Length - 1])+1);
                        continue;
                    }
                    cmd = cmd.Substring(cmd.IndexOf(" ")+1);
                    try
                    {
                        if (Regex.Match(cmd[0].ToString(), @"[0-9]").Success)
                        {
                            if (cmd.IndexOf(" ") > 0)
                                tmp = cmd.Substring(0, cmd.IndexOf(" ") + 1);
                            else
                                tmp = cmd.Substring(0, cmd.Length);
                            int.Parse(tmp);
                            arguments[arguments.Count-1] = tmp.Trim(' ');
                            if (cmd.IndexOf(" ") != -1)
                                cmd = cmd.Substring(cmd.IndexOf(" ")+1, cmd.Length- cmd.IndexOf(" ")-1);
                        }
                        else if (Regex.Match(cmd[0].ToString(), @"[a-zA-Z]").Success)
                        {
                            if (cmd.IndexOf(" ") > 0)
                                tmp = cmd.Substring(0, cmd.IndexOf(" ") + 1);
                            else
                                tmp = cmd.Substring(0, cmd.Length);
                            arguments[arguments.Count - 1] = tmp.Trim(' ');
                            if (cmd.IndexOf(" ") != -1)
                                cmd = cmd.Substring(cmd.IndexOf(" ")+1, cmd.Length- cmd.IndexOf(" ")-1);
                        }
                        else if (cmd[0] == '"')
                        {
                            int end = cmd.IndexOf('"', 1);

                            if (cmd[end-1] == '\\')
                            {
                                while (cmd[end - 1] == '\\')
                                    end = cmd.IndexOf('"', end + 1);
                            }

                            tmp = cmd.Substring(1, end-1);
                            arguments[arguments.Count - 1] = tmp.Trim(' ');
                            if (cmd.IndexOf(" ") > end)
                                cmd = cmd.Substring(end+1);
                            else
                                cmd = "";
                        }
                        if (cmd.IndexOf(" ") == -1)
                            break;
                        continue;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine(String.Format("Invalid argument for \"{0}\" flag.", flags[flags.Count - 1]));
                    }
                }
            }
            return new ConsolerCommand(name, flags, arguments);
        }
        public static void register(ConsolerCommand cmd)
        {
            ConsolerMain.commands.Add(cmd);
        }

        public static void listen()
        {
            Console.WriteLine("Erida Consoler v" + CR_VERSION + " for C#.");
            Console.WriteLine("Author - " + CR_AUTHOR);
            Console.WriteLine("Type \"help\" to get command list.");
            Console.Write("Consoler> ");
            ConsolerCommand helpCmd = new HelpCommand(commands)
                .setCommandName("help")
                .setShortHelpText("Write list of registred commands.");
            ConsolerCommand altHelpCmd = new HelpCommand(commands)
                .setCommandName("?")
                .setShortHelpText("Alternative command to write list of registred commands.");
            ConsolerCommand exitCmd = new ExitCommand()
                .setCommandName("exit")
                .setShortHelpText("Terminate all executed processes and close consoler.");
            ConsolerCommand versionCmd = new VersionCommand()
                .setCommandName("version")
                .setShortHelpText("Write version of consoler.");
            ConsolerCommand clearCmd = new ClearCommand()
                .setCommandName("clear")
                .setShortHelpText("Clear consoler space from text and other stuff.");
            ConsolerCommand sayCmd = new SayCommand()
                .setCommandName("say")
                .setShortHelpText("Print phrase with line break or without it.")
                .setFullHelpText("Print phrase with line break or without it.\n" +
                "Arguments:\n" +
                "-pl - Print line with line break.\n" +
                "-p - Print just text without line breaks.");
            ConsolerCommand scriptCmd = new ScriptCommand()
                .setCommandName("execute")
                .setShortHelpText("Execute script with consoler syntax.")
                .setFullHelpText("Execute script with consoler syntax. Lines must be ended by nothing, just line break to use next command.\n" +
                "Argument -p is the one, it's need to set path to executing file. File can be any extension and size.");
            register(helpCmd);
            register(altHelpCmd);
            register(exitCmd);
            register(versionCmd);
            register(clearCmd);
            register(sayCmd);
            register(scriptCmd);
            while (true)
            {
                string line = Console.ReadLine();
                if (line != "")
                {
                    ConsolerCommand dedicatedCommand = parseCommand(line);
                    if (isCommandExists(dedicatedCommand.getCommandName()))
                    {
                        GetCommand(dedicatedCommand.getCommandName()).Merge(dedicatedCommand);
                        GetCommand(dedicatedCommand.getCommandName()).executeCommand();
                        Console.Write("Consoler> ");
                    }
                    else {
                        Console.WriteLine("Command \"" + dedicatedCommand.getCommandName() + "\" is not exist or not registred. Type \"help\" to " +
                            "get registred commands list.");
                        Console.Write("Consoler> ");
                    }
                }
                else
                {
                    Console.Write("Consoler> ");
                }
            }
        }
    }
}
