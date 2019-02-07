using System.IO;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Pads.ProjectPad;
using VS4Mac.JsonToClass.Extensions;
using VS4Mac.JsonToClass.Services;
using Xwt;

namespace VS4Mac.JsonToClass.Commands
{
    public class AddCodeClassFileCommand : CommandHandler
    {
        const string ClassOutputName = "Empty.cs";

        protected override void Run()
        {
            var projectFolder = (ProjectFolder)IdeApp.ProjectOperations.CurrentSelectedItem;
            string json = Clipboard.GetText();

            var filePath = new FilePath(Path.Combine(projectFolder.Path, ClassOutputName));
            string classCode = new JsonToClassService().GenerateClassCodeFromJson(json);

            IdeApp.ProjectOperations.CurrentSelectedProject.CreateAndAddFileToProject(filePath, classCode);
        }

        protected override void Update(CommandInfo info)
        {
            info.Visible = IdeApp.Workspace.IsOpen &&
                !string.IsNullOrEmpty(Clipboard.GetText()) &&
                IdeApp.ProjectOperations.CurrentSelectedItem is ProjectFolder;
        }

    }
}
