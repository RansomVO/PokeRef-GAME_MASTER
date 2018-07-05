using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Google.Protobuf.Reflection;
using ProtoBuf.Reflection;

namespace GM_protogen
{
    /// <summary>
    /// A code generator that writes C#
    /// </summary>
    public class GM_CSharpCodeGenerator : CSharpCodeGenerator
    {
        #region Data

        string[] EnumPrefixes =
        {
            "POKEMON_RARITY_",
        };

        #endregion Data

        #region ctor

        /// <summary>
        /// Reusable code-generator instance
        /// </summary>
        public static new GM_CSharpCodeGenerator Default { get; } = new GM_CSharpCodeGenerator();

        /// <summary>
        /// Create a new CSharpCodeGenerator instance
        /// </summary>
        /// 
        protected GM_CSharpCodeGenerator() { }
        
        #endregion ctor

        /// <summary>
        /// Start a file
        /// </summary>
        protected override void WriteFileHeader(GeneratorContext ctx, FileDescriptorProto file, ref object state)
        {
            ctx.WriteLine("// ***** This is for GAME_MASTER files. *****");

            string version = ctx.GetCustomOption("PROTO_VERSION");
            if (version != null)
            {
                version = "v" + version.Replace('.', '_');
                int pos = file.Package.IndexOf('.');
                file.Package = pos == -1 ?
                    file.Package + '.' + version :
                    file.Package.Substring(0, pos) + '.' + version + file.Package.Substring(pos);
            }

            base.WriteFileHeader(ctx, file, ref state);
        }

		public override IEnumerable<CodeFile> Generate(FileDescriptorSet set, NameNormalizer normalizer = null, Dictionary<string, string> options = null)
		{
			if (normalizer == null)
				normalizer = NameNormalizer.Null;

			return base.Generate(set, normalizer, options);
		}
		/// <summary>
		/// Write an enum value
		/// </summary>
		protected override void WriteEnumValue(GeneratorContext ctx, EnumValueDescriptorProto obj, ref object state)
        {
            ctx.WriteLine($"{Escape(obj.Name)} = {obj.Number},");
            foreach (var prefix in EnumPrefixes)
            {
                if (obj.Name.StartsWith(prefix))
                {
                    ctx.WriteLine($"{Escape(obj.Name.Substring(prefix.Length))} = {obj.Number},");
                }
            }
        }
	}
}
