﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Bibim.Collections.Generic;
using Bibim.Graphics;

namespace Bibim.Asset.Pipeline.Recipes
{
    public sealed class CompileShaderEffect : CookingNode<ShaderEffect>
    {
        #region Fields
        private Dictionary<string, string> defines;
        #endregion

        #region Properties
        public string Input
        {
            get;
            set;
        }

        public string Defines
        {
            get
            {
                if (defines != null && defines.Count > 0)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (var item in defines)
                    {
                        if (builder.Length > 0)
                            builder.Append(';');

                        if (string.IsNullOrEmpty(item.Value))
                            builder.Append(item.Key);
                        else
                            builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    }

                    return builder.ToString();
                }
                else
                    return string.Empty;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    if (defines != null)
                        defines.Clear();

                    return;
                }

                string[] items = value.Split(';');

                defines = defines ?? new Dictionary<string, string>(items.Length);
                defines.Clear();

                foreach (var item in items)
                {
                    int index = item.IndexOf('=');
                    if (index != -1)
                        defines[item.Substring(0, index).Trim()] = item.Substring(index + 1).Trim();
                    else
                        defines[item.Trim()] = null;
                }
            }
        }
        #endregion

        #region Constructor
        public CompileShaderEffect()
            : this(string.Empty, null)
        {
        }

        public CompileShaderEffect(string input)
            : this(input, null)
        {
        }

        public CompileShaderEffect(string input, string defines)
        {
            this.Input = input ?? string.Empty;
            this.Defines = defines;
        }
        #endregion

        public override ShaderEffect Cook(CookingContext context)
        {
            string assetPath = Path.Combine(context.Directory, context.ExpandVariables(Input));
            string input = Path.Combine(context.BaseDirectory, assetPath);
            if (File.Exists(input) == false)
                throw new FileNotFoundException(string.Empty, input);

            context.AddDependency(input);

            StringBuilder outputString = new StringBuilder();
            foreach (var item in defines)
            {
                if (string.IsNullOrEmpty(item.Value))
                    outputString.AppendLine(string.Format("#define {0}", item.Key));
                else
                    outputString.AppendLine(string.Format("#define {0} {1}", item.Key, item.Value));
            }

            using (var inputStream = new FileStream(input, FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(inputStream);
                while (reader.EndOfStream == false)
                    outputString.AppendLine(reader.ReadLine());
            }

            return new ShaderEffect(null)
            {
                Tag = new ShaderEffectCookingTag(outputString.ToString())
            };

            /*
            string outputFXOFileName = Path.GetFileName(Path.GetTempFileName());
            StringBuilder definesArguments = new StringBuilder();
            foreach (var item in defines)
            {
                if (string.IsNullOrEmpty(item.Value))
                    definesArguments.AppendFormat("/D {0} ", item.Key);
                else
                    definesArguments.AppendFormat("/D {0}={1} ", item.Key, item.Value);
            }

            string args = string.Format("/T fx_2_0 /Fo \"{1}\" {2} /Zi /nologo \"{0}\"", Path.GetFileName(input), outputFXOFileName, definesArguments.ToString());

            ProcessStartInfo start = new ProcessStartInfo(@"Plugin\fxc.exe", args)
            {
                WorkingDirectory = Path.GetDirectoryName(input),
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
            Process process = Process.Start(start);
            process.WaitForExit();

            string compileMessage = process.StandardOutput.ReadToEnd();
            string compileErrorMessage = process.StandardError.ReadToEnd().Replace(input, assetPath);
            string outputFXOFilePath = Path.Combine(Path.GetDirectoryName(input), outputFXOFileName);

            if (string.IsNullOrEmpty(compileErrorMessage))
            {
                Trace.WriteLine(string.Format("Compile result\n{0}", compileMessage));

                byte[] buffer = null;
                using (var fs = new FileStream(outputFXOFilePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader fxoReader = new BinaryReader(fs);
                    buffer = fxoReader.ReadBytes((int)fs.Length);
                }

                File.Delete(outputFXOFilePath);

                return new ShaderEffect(null)
                {
                    Tag = new ShaderEffectCookingTag(buffer)
                };
            }
            else
            {
                File.Delete(outputFXOFilePath);
                throw new InvalidDataException(string.Format("Compile failure.\n{0}\n{1}\n{2}", compileMessage, compileErrorMessage, args));
            }
            */
        }
    }
}
