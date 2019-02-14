using System;
using VS4Mac.JsonToClass.Extensions;

namespace VS4Mac.JsonToClass.Model
{
    public class QuicktypeProperties
    {
        public string JsonFilename { get; set; }
        public string OutputFile { get; set; }
        
        public string Namespace { get; set; }
        public ArrayType ArrayType { get; set; }
        public Feature FeatureOutput { get; set; }
        public bool CheckRequired { get; set; }
        public bool AllPropertiesOptional { get; set; }


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

            arguments += $" --array-type {this.ArrayType.GetDescription()}";

            arguments += $" --features {this.FeatureOutput.GetDescription()}";

            if (this.CheckRequired)
                arguments += " --check-required";

            if (this.AllPropertiesOptional)
                arguments += " --all-properties-optional";

            return arguments;
        }
    }
}
