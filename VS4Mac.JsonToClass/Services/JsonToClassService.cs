using System;
using System.Diagnostics;
using System.IO;

namespace VS4Mac.JsonToClass.Services
{
    public class JsonToClassService
    {
        static readonly string outputClassCodeFileName = "EmptyClass.cs";

        public string GenerateClassCodeFromJson(string json)
        {
            var jsonFilename = Path.GetTempFileName();
            File.WriteAllText(jsonFilename, json);
            var classCodeFromJson = string.Empty;
            var startInfo = CreateQuicktypeStartInfo(jsonFilename);

            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    classCodeFromJson = File.ReadAllText(outputClassCodeFileName);
                }
            }

            return classCodeFromJson;
        }

        private static ProcessStartInfo CreateQuicktypeStartInfo(string jsonFilename)
        {
            return new ProcessStartInfo()
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                FileName = "/bin/bash",
                Arguments = $"-c \"quicktype {jsonFilename} -o {outputClassCodeFileName}\""
            };
        }
    }
}
