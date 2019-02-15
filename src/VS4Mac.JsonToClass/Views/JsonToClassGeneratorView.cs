using System;
using System.IO;
using MonoDevelop.Components;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using VS4Mac.JsonToClass.Model;
using VS4Mac.JsonToClass.Services;
using Xwt;

namespace VS4Mac.JsonToClass.Views
{

    public partial class JsonToClassGeneratorView : PadContent
    {
        QuicktypeProperties quicktypeProperties;
        JsonToClassService jsonToClassService = new JsonToClassService();

        public JsonToClassGeneratorView()
        {
            InitializeComponent();
            InitializeQuicktype();
        }      

        private void InitializeQuicktype()
        {
            quicktypeProperties = new QuicktypeProperties(Path.GetTempFileName(), Path.Combine(Path.GetTempPath(), "EmptyClass.cs"));
        }

        private void GenerateClass()
        {
            quicktypeProperties.Namespace = namespaceEntry.Text;
            quicktypeProperties.ArrayType = (Model.ArrayType)arrayTypeComboBox.SelectedItem;
            quicktypeProperties.FeatureOutput = (Model.Feature)outputFeatureComboBox.SelectedItem;
            quicktypeProperties.CheckRequired = checkRequiredCheckBox.Active;
            quicktypeProperties.AllPropertiesOptional = allPropertiesOptionalCheckBox.Active;
            quicktypeProperties.CSharpVersion = (Model.CSharpVersion)languageVersionComboBox.SelectedItem;

            var generatedClass = jsonToClassService.GenerateClassCodeFromJson(Clipboard.GetText(), quicktypeProperties);
            IdeApp.Workbench.ActiveDocument.Editor.InsertAtCaret(generatedClass);
        }
    }
}
