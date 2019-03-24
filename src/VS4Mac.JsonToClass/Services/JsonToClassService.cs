using System;
using System.Diagnostics;
using System.IO;
using VS4Mac.JsonToClass.Exceptions;
using VS4Mac.JsonToClass.Model;

namespace VS4Mac.JsonToClass.Services
{
    public static class JsonToClassService
    {
        public static string GenerateClassCodeFromJson(string json, QuicktypeProperties quicktypeProperties)
        {
            File.WriteAllText(quicktypeProperties.JsonFilename, json);

            var startInfo = CreateQuicktypeStartInfo(quicktypeProperties);
            var classCodeFromJson = GetClassCode(quicktypeProperties, startInfo);

            return classCodeFromJson;
        }


        public static string GenerateClassCodeFromJson(string json)
        {
            var jsonFilename = Path.GetTempFileName();
            File.WriteAllText(jsonFilename, json);

            var outputFile = Path.Combine(Path.GetTempPath(), "EmptyClass.cs");
            var quicktypeProperties = new QuicktypeProperties(jsonFilename, outputFile);

            var startInfo = CreateQuicktypeStartInfo(quicktypeProperties);
            var classCodeFromJson = GetClassCode(quicktypeProperties, startInfo);

            return classCodeFromJson;
        }

       
        private static string GetClassCode(QuicktypeProperties quicktypeProperties, ProcessStartInfo startInfo)
        {
            string result = string.Empty;
            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
                if (process.ExitCode == 127)
                    throw new UninstalledQuicktypeException();
                if (process.ExitCode != 0)
                    throw new Exception(process.ExitCode.ToString());

                result = File.ReadAllText(quicktypeProperties.OutputFile);
            }

            return result;
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
