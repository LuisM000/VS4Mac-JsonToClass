using System;
namespace VS4Mac.JsonToClass.Model
{
    public class QuicktypeProperties
    {
        public string Namespace { get; set; }
        public string JsonFilename { get; set; }
        public string OutputFile { get; set; }

        public QuicktypeProperties(string jsonFilename, string outputFile)
        {
            this.JsonFilename = jsonFilename;
            this.OutputFile = outputFile;
        }

        public string GenerateArguments()
        {
            string arguments = $"quicktype {this.JsonFilename} -o {this.OutputFile}";

            if (!string.IsNullOrWhiteSpace(this.Namespace))
                arguments += $" --namespace {this.Namespace}";

            return arguments;
        }
    }
}
