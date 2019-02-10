using System;
using System.Diagnostics;
using System.IO;
using VS4Mac.JsonToClass.Model;

namespace VS4Mac.JsonToClass.Services
{
    public class JsonToClassService
    {
        public string GenerateClassCodeFromJson(string json, QuicktypeProperties quicktypeProperties)
        {
            File.WriteAllText(quicktypeProperties.JsonFilename, json);

            var classCodeFromJson = string.Empty;
            var startInfo = CreateQuicktypeStartInfo(quicktypeProperties);

            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    classCodeFromJson = File.ReadAllText(quicktypeProperties.OutputFile);
                }
            }

            return classCodeFromJson;
        }

        public string GenerateClassCodeFromJson(string json)
        {
            var jsonFilename = Path.GetTempFileName();
            File.WriteAllText(jsonFilename, json);

            var classCodeFromJson = string.Empty;
            var outputFile = Path.Combine(Path.GetTempPath(), "EmptyClass.cs");
            var quicktypeProperties = new QuicktypeProperties(jsonFilename, outputFile);
            var startInfo = CreateQuicktypeStartInfo(quicktypeProperties);

            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    classCodeFromJson = File.ReadAllText(quicktypeProperties.OutputFile);
                }
            }

            return classCodeFromJson;
        }

        private static ProcessStartInfo CreateQuicktypeStartInfo(QuicktypeProperties quicktypeProperties)
        {
            return new ProcessStartInfo()
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                FileName = "/bin/bash",
                Arguments = $"-c \"{quicktypeProperties.GenerateArguments()}\""
            };
        }

    }
}
