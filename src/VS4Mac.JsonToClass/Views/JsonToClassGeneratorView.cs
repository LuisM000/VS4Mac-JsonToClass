using System;
using System.IO;
using MonoDevelop.Ide;
using VS4Mac.JsonToClass.Model;
using VS4Mac.JsonToClass.Services;
using Xwt;

namespace VS4Mac.JsonToClass.Views
{
    public partial class JsonToClassGeneratorView : Dialog
    {
        QuicktypeProperties quicktypeProperties;
        MonoDevelop.Ide.Gui.Document document;
        JsonToClassService jsonToClassService = new JsonToClassService();

        public JsonToClassGeneratorView()
        {
            InitializeComponent();
            InitializeDocument();
            InitializeQuicktype();
        }

        private void InitializeDocument()
        {
            document = IdeApp.Workbench.NewDocument("GeneratedClass.cs", "text/plain", "");
        }

        private void InitializeQuicktype()
        {
            var jsonFilename = Path.GetTempFileName();
            var outputFile = Path.Combine(Path.GetTempPath(), "EmptyClass.cs");
            quicktypeProperties = new QuicktypeProperties(jsonFilename,outputFile);
        }

        private void GenerateClass()
        {
            quicktypeProperties.Namespace = namespaceEntry.Text;

            var generatedClass = jsonToClassService.GenerateClassCodeFromJson(Clipboard.GetText(), quicktypeProperties);
            document.Editor.InsertAtCaret(generatedClass);
        }

        
        protected override async void OnClosed()
        {
            base.OnClosed();
            document.Editor.Text = string.Empty;
            await document.Window.CloseWindow(true);
        }

    }
}
