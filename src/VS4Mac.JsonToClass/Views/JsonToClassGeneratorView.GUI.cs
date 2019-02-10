using System;
using Xwt;

namespace VS4Mac.JsonToClass.Views
{
    public partial class JsonToClassGeneratorView
    {
        VBox mainVBox;

        Button generateButton;

        VBox namespaceBox;
        Label namespaceLabel;
        TextEntry namespaceEntry;

        private void InitializeComponent()
        {
            Init();
            Build();
            AttachToEvents();
        }

        private void Init()
        {
            mainVBox = new VBox();

            generateButton = new Button("Generate");

            namespaceBox = new VBox();
            namespaceLabel = new Label("Generated namespace");
            namespaceEntry = new TextEntry();
        }

        private void Build()
        {
            namespaceBox.PackStart(namespaceLabel);
            namespaceBox.PackEnd(namespaceEntry);


            mainVBox.PackStart(namespaceBox);
            mainVBox.PackEnd(generateButton, false, WidgetPlacement.End, WidgetPlacement.End);

            this.Content = mainVBox;
        }

        private void AttachToEvents()
        {
            generateButton.Clicked += GenerateButton_Clicked;
        }

        void GenerateButton_Clicked(object sender, EventArgs e)
        {
            this.GenerateClass();
        }
    }
}
