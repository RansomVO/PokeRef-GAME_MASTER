using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.IO;

using VanOrman.Utils;

namespace GM_protogen
{
    class Program
    {
        private enum ErrorCode : int
        {
            Success = 0,
            Parameters,
            InputFolderDoesNotExist,
            OutputFolderCouldNotBeCreated,
            InvalidProto,
            FileDescriptorSetProcessFailure,
            Exception,
        }

        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                ConsoleOutput.OutputError("Usage: GM_protegen {inputFolder} {outputFolder}");
                return (int)ErrorCode.Parameters;
            }

            return (int)GenerateCode(args[0], args[1]);
        }

        static ErrorCode GenerateCode(string inputFolder, string outputFolder)
        {
            try
            {
                #region Validate Parameters

                if (!Directory.Exists(inputFolder))
                {
                    ConsoleOutput.OutputError("inputFolder not found: \"{inputFolder}\"");
                    return ErrorCode.InputFolderDoesNotExist;
                }

                if (!Directory.Exists(outputFolder))
                {
                    ConsoleOutput.OutputWarning("outputFolder not found, creating... \"{outputFolder}\"");
                    Directory.CreateDirectory(outputFolder);
                    if (!Directory.Exists(outputFolder))
                    {
                        ConsoleOutput.OutputError("Could not create outputFolder: \"{outputFolder}\"");
                        return ErrorCode.OutputFolderCouldNotBeCreated;
                    }
                }

                #endregion Validate Parameters

                #region Get the list of .proto files.

                var fileDescriptorSet = new FileDescriptorSet { AllowNameOnlyImport = true };
                fileDescriptorSet.AddImportPath(inputFolder);

                var inputFiles = new List<string>();
                foreach (var path in Directory.EnumerateFiles(inputFolder, "*.proto", SearchOption.AllDirectories))
                {
                    inputFiles.Add(MakeRelativePath(inputFolder, path));
                }

                bool error = false;
                foreach (var proto in inputFiles)
                {
                    if (!fileDescriptorSet.Add(proto, true))
                    {
                        error = true;
                        ConsoleOutput.OutputError($"Error Loading: {proto}");
                    }
                }
                if (error)
                {
                    return ErrorCode.InvalidProto;
                }

                fileDescriptorSet.Process();
                var errors = fileDescriptorSet.GetErrors();
                if (errors.Length > 0)
                {
                    foreach (var err in errors)
                    {
                        ConsoleOutput.OutputError(err.ToString());
                    }

                    return ErrorCode.FileDescriptorSetProcessFailure;
                }

                #endregion Get the list of .proto files.

                #region Generate the .cs files.

                foreach (var file in GM_CSharpCodeGenerator.Default.Generate(fileDescriptorSet))
                {
                    var filePath = Path.Combine(outputFolder, file.Name);

                    var fileFolder = Path.GetDirectoryName(filePath);
                    if(!Directory.Exists(fileFolder))
                    {
                        ConsoleOutput.OutputWarning($"Output directory does not exist, creating... {fileFolder}");
                        Directory.CreateDirectory(fileFolder);
                    }

                    File.WriteAllText(filePath, file.Text);
                    ConsoleOutput.OutputSuccess($"generated: {filePath}");
                }

                #endregion Generate the .cs files.

                return ErrorCode.Success;
            }
            catch (Exception ex)
            {
                ConsoleOutput.OutputException(ex);

                return ErrorCode.Exception;
            }
        }

        public static String MakeRelativePath(String fromPath, String toPath)
        {
            if (String.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
            if (String.IsNullOrEmpty(toPath)) throw new ArgumentNullException("toPath");

            Uri fromUri = new Uri(fromPath, UriKind.RelativeOrAbsolute);
            if (!fromUri.IsAbsoluteUri)
            {
                fromUri = new Uri(Path.Combine(Directory.GetCurrentDirectory(), fromPath));
            }
            Uri toUri = new Uri(toPath, UriKind.RelativeOrAbsolute);
            if (!toUri.IsAbsoluteUri)
            {
                toUri = new Uri(Path.Combine(Directory.GetCurrentDirectory(), toPath));
            }

            if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            String relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase))
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return relativePath;
        }
    }
}
