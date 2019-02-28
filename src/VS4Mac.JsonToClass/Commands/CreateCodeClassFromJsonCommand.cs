using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using VS4Mac.JsonToClass.Exceptions;
using VS4Mac.JsonToClass.Services;
using Xwt;

namespace VS4Mac.JsonToClass.Commands
{
    public class CreateCodeClassFromJsonCommand : CommandHandler
    {
        protected override void Run()
        {
            try
            {
                string json = Clipboard.GetText();
                string classCode = JsonToClassService.GenerateClassCodeFromJson(json);

                IdeApp.Workbench.ActiveDocument.Editor.InsertAtCaret(classCode);
            }
            catch (UninstalledQuicktypeException)
            {
                MessageDialog.ShowWarning("Ouch!", "Something has happened. You may not have Quicktime installed");
            }
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = IdeApp.Workbench.ActiveDocument?.Editor != null &&
                            !string.IsNullOrEmpty(Clipboard.GetText());
        }
    }
}
