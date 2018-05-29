using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
                    Console.Error.WriteLine("The specified folder does not exist: " + args[0]);

                    return;
                }

                Utils.Init(args[0]);

                GameMasterReader gameMasterReader = new GameMasterReader();
                gameMasterReader.Read(Path.Combine(Utils.RootFolder, @"tech\GAME_MASTER\archive\"));

                GameMasterDataWriter gameMasterDataWriter = new GameMasterDataWriter(gameMasterReader.GameMasterTemplate, gameMasterReader.LegacyGameMasterTemplates);
                gameMasterDataWriter.Write();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("********** ERROR!!! **********");

                while (ex != null)
                {
                    Console.Error.WriteLine("-----" + ex.Message + "-----");
                    Console.Error.WriteLine(ex.StackTrace);
                    Console.Error.WriteLine();

                    ex = ex.InnerException;
                }
            }
        }
    }
}
