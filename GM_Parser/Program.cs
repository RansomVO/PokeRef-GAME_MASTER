using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using VanOrman.PokemonGO.GAME_MASTER.Parser;
using VanOrman.Utils;

namespace VanOrman.PokemonGO.GAME_MASTER.Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() < 1 || args[0].Contains("?"))
            {
                if (args.Count() < 1)
                {
                    ConsoleOutput.OutputError("Missing Argument.");
                    Console.Out.WriteLine();
                }

                ConsoleOutput.OutputSuccess("GAME_MASTER Parser");
                Console.Out.WriteLine("Usage: GM_Parser {GAME_MASTER Folder}");
                Console.Out.WriteLine("\tGAME_MASTER Folder: Path to folder where GAME_MASTER files are located.");
                Console.Out.WriteLine();

                return;
            }

            GameMasterReader gameMasterReader = new GameMasterReader();
            gameMasterReader.PerformConversions(args[0]);
        }
    }
}
