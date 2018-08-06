using System;
using System.IO;
using System.Linq;

using VanOrman.Utils;


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
                    WriteHelp();
                    return;
                }

                int decoded = 0;
                for (int arg = 0; arg < args.Length; arg++)
                {
                    bool createOnly = string.Equals(args[arg], "-c") || string.Equals(args[arg], "-create");
                    if (createOnly)
                    {
                        arg++;
                        if (arg >= args.Length)
                            break;
                    }

                    string folder;
                    string filePattern;
                    int pos = args[arg].LastIndexOf('\\');
                    if (pos == -1)
                    {
                        folder = ".";
                        filePattern = args[arg];
                    }
                    else
                    {
                        folder = args[arg].Substring(0, pos);
                        filePattern = args[arg].Substring(pos + 1);
                    }

                    if (!Directory.Exists(folder))
                    {
                        ConsoleOutput.OutputError("ERROR: Folder does not exist: \"{0}\"", folder);
                        continue;
                    }

                    foreach (var filePath in Directory.EnumerateFiles(folder, filePattern))
#if true
                        if (Path.GetExtension(filePath).Length == 0 && (!createOnly || !File.Exists(filePath + ".json")))
                        {
                            Console.Out.WriteLine("Decoding \"" + filePath + "\"");
                            GameMasterDecoder.WriteGameMasterJson(filePath);
                            decoded++;
                        }
#else
                        // This code is to reset the timestamps of the files
                        try
                        {
                            GameMasterTimestampUtils.FixGameMasterFileTime(filePath);
                        }
                        catch (Exception) { }
#endif
                }

                ConsoleOutput.OutputSuccess($"Finished. {decoded} files decoded.");
            }
            catch (Exception ex)
            {
                ConsoleOutput.OutputException(ex, "********** ERROR!!! **********");
            }
        }

        private static void WriteHelp()
        {
            Console.Out.WriteLine("GAME_MASTER Decoder");
            Console.Out.WriteLine("A tool to decode Pokémon GO GAME_MASTER files into JSON.");
            Console.Out.WriteLine();
            Console.Out.WriteLine("Usage: GM_Decoder [-create] {FilePattern} [[-create] {FilePattern}...]");
            Console.Out.WriteLine("\t-create (-c): Only decode the GAME_MASTER for the FilePattern if ");
            Console.Out.WriteLine("\t\tthe .json file doesn't already exist.");
            Console.Out.WriteLine("\tFilePattern: Pattern of the GAME_MASTER files to be decoded.");
            Console.Out.WriteLine();
            Console.Out.WriteLine("\tE.G.: GM_Decoder GAME_MASTER\\*");
            Console.Out.WriteLine();
        }
    }
}
