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
        public CSharpVersion CSharpVersion { get; set; }
        public Density PropertyDensity { get; set; }
        public NumberType NumberType { get; set; }
        public AnyType AnyType { get; set; }
        public BaseClassType BaseClass { get; set; }
        public bool DetectUUIDs { get; set; }
        public bool DetectBooleansInStrings { get; set; }
        public bool DetectDatesAndTimes { get; set; }
        public bool DetectEnums { get; set; }
        public bool DetectIntegersInStrings { get; set; }
        public bool DetectMaps { get; set; }
        public bool NoIgnoreJsonRefs { get; set; }
        public bool MergeSimilarClasses { get; set; }

        public QuicktypeProperties() { }//don't remove. Used for serialization
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

            arguments += $" --csharp-version {this.CSharpVersion.GetDescription()}";

            arguments += $" --density {this.PropertyDensity.GetDescription()}";

            arguments += $" --number-type {this.NumberType.GetDescription()}";

            arguments += $" --any-type {this.AnyType.GetDescription()}";

            arguments += $" --base-class {this.BaseClass.GetDescription()}";

            if (!this.DetectUUIDs)
                arguments += " --no-uuids";

            if (!this.DetectBooleansInStrings)
                arguments += " --no-boolean-strings";

            if(!this.DetectDatesAndTimes)
                arguments += " --no-date-times";

            if (!this.DetectEnums)
                arguments += " --no-enums";

            if (!this.DetectIntegersInStrings)
                arguments += " --no-integer-strings";

            if (!this.DetectMaps)
                arguments += " --no-maps";

            if (!this.NoIgnoreJsonRefs)
                arguments += " --no-ignore-json-refs";

            if (!this.MergeSimilarClasses)
                arguments += " --no-combine-classes";

            return arguments;
        }
    }
}
