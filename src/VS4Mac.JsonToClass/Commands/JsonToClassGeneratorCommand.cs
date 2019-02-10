using System;
using MonoDevelop.Components.Commands;
using VS4Mac.JsonToClass.Views;

namespace VS4Mac.JsonToClass.Commands
{
    public class JsonToClassGeneratorCommand : CommandHandler
    {
        protected override void Run()
        {
            using (var generateTextDialog = new JsonToClassGeneratorView())
            {
                generateTextDialog.Run(Xwt.MessageDialog.RootWindow);
            }

        }
    }
}
