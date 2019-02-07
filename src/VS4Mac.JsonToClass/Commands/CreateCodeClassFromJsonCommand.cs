using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using VS4Mac.JsonToClass.Services;
using Xwt;

namespace VS4Mac.JsonToClass.Commands
{
    public class CreateCodeClassFromJsonCommand : CommandHandler
    {
        protected override void Run()
        {
            string json = Clipboard.GetText();
            string classCode = new JsonToClassService().GenerateClassCodeFromJson(json);

            IdeApp.Workbench.ActiveDocument.Editor.InsertAtCaret(classCode);
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = IdeApp.Workbench.ActiveDocument?.Editor != null &&
                            !string.IsNullOrEmpty(Clipboard.GetText());
        }
    }
}
