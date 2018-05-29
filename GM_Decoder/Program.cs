using System;
using System.IO;
using System.Linq;

using GameMaster = POGOProtos.Networking.Responses.DownloadItemTemplatesResponse;

namespace VanOrman.PokemonGO.GAME_MASTER.Decoder
{
    /*  ==================================================================================================================
     *  NOTES:
     *  
     *   .proto files from https://github.com/Furtif/POGOProtos
     *   
     *   protobuf-net project: https://github.com/mgravell/protobuf-net
     *   
     *   protogen utility to generate class from .proto files: https://protogen.marcgravell.com/
     *  ==================================================================================================================
    */

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Count() < 1 || args[0].Contains("?"))
                {
                    Console.Out.WriteLine("GAME_MASTER Decoder");
                    Console.Out.WriteLine("A tool to decode Pokémon GO GAME_MASTER files into JSON.");
                    Console.Out.WriteLine();
                    Console.Out.WriteLine("Usage: GM_Decoder {FilePath}");
                    Console.Out.WriteLine("\tFilePath: Path to the GAME_MASTER file to be decoded.");
                    Console.Out.WriteLine();

                    return;
                }

                if (!File.Exists(args[0]))
                {
                    Console.Error.WriteLine("The specified file does not exist: " + args[0]);

                    return;
                }

                // Deserialize specified GAME_MASTER file.
                GameMaster gameMaster = GameMasterDecoder.ReadGameMaster(args[0]);
                GameMasterDecoder.WriteGameMasterJson(gameMaster, args[0] + ".json");
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
