using System;
using System.IO;
using System.Linq;

using VanOrman.Utils;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Count() < 1 || args[0].Contains("?"))
                {
                    if (args.Count() < 1)
                    {
                        ConsoleOutput.OutputError("Missing Arguments.");
                        Console.Out.WriteLine();
                    }

                    ConsoleOutput.OutputSuccess("GAME_MASTER Data Generator");
                    Console.Out.WriteLine("Usage: GM_DataGenerator {RootFolder}");
                    Console.Out.WriteLine("\tRootFolder: Path to the root folder where the HTML project is located.");
                    Console.Out.WriteLine();

                    return;
                }

                if (!Directory.Exists(args[0]))
                {
                    ConsoleOutput.OutputError($"The specified folder does not exist: \"{args[0]}\"");

                    return;
                }

                Utils.Init(args[0]);

                GameMasterReader gameMasterReader = new GameMasterReader();
                gameMasterReader.Read(Path.Combine(Utils.RootFolder, @"_GAME_MASTER"));

                GameMasterDataWriter gameMasterDataWriter = new GameMasterDataWriter(gameMasterReader.GameMasterTemplate, gameMasterReader.LegacyGameMasterTemplates);
                gameMasterDataWriter.Write();
            }
            catch (Exception ex)
            {
                ConsoleOutput.OutputException(ex, "********************************** ERROR!!! ***********************************");
            }
        }
    }
}
