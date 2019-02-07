using System;
using System.Diagnostics;
using System.IO;

namespace VS4Mac.JsonToClass.Services
{
    public class JsonToClassService
    {

        public string GenerateClassCodeFromJson(string json)
        {
            var jsonFilename = Path.GetTempFileName();
            File.WriteAllText(jsonFilename, json);

            var classCodeFromJson = string.Empty;
            var outputFile = Path.Combine(Path.GetTempPath(), "EmptyClass.cs");
            var startInfo = CreateQuicktypeStartInfo(jsonFilename, outputFile);

            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    classCodeFromJson = File.ReadAllText(outputFile);
                }
            }

            return classCodeFromJson;
        }

        private static ProcessStartInfo CreateQuicktypeStartInfo(string jsonFilename, string outputFile)
        {
            return new ProcessStartInfo()
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                FileName = "/bin/bash",
                Arguments = $"-c \"quicktype {jsonFilename} -o {outputFile}\""
            };
        }

    }
}
