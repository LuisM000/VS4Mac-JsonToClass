using System;
using MonoDevelop.Components;
using Xwt;

namespace VS4Mac.JsonToClass.Views
{
    public partial class JsonToClassGeneratorView
    {
        private Control control;
        public override Control Control
        {
            get
            {
                if (control == null)
                {
                    control = new XwtControl(mainVBox);
                }
                return control;
            }
        }


        VBox mainVBox;
        Button generateButton;

        VBox namespaceBox;
        Label namespaceLabel;
        TextEntry namespaceEntry;

        VBox arrayTypeBox;
        Label arrayTypeLabel;
        ComboBox arrayTypeComboBox;

        VBox outputFeatureBox;
        Label outputFeatureLabel;
        ComboBox outputFeatureComboBox;

        CheckBox checkRequiredCheckBox;

        CheckBox allPropertiesOptionalCheckBox;

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

            arrayTypeBox = new VBox();
            arrayTypeLabel = new Label("Use T[] or List<T>");
            arrayTypeComboBox = new ComboBox();
            arrayTypeComboBox.Items.Add(Model.ArrayType.Array, "Array");
            arrayTypeComboBox.Items.Add(Model.ArrayType.List, "List");
            arrayTypeComboBox.SelectedIndex = 0;

            outputFeatureBox = new VBox();
            outputFeatureLabel = new Label("Output features");
            outputFeatureComboBox = new ComboBox();
            outputFeatureComboBox.Items.Add(Model.Feature.Complete, "Complete");
            outputFeatureComboBox.Items.Add(Model.Feature.AttributesOnly, "Attributes only");
            outputFeatureComboBox.Items.Add(Model.Feature.JustTypes, "Just Types");
            outputFeatureComboBox.SelectedIndex = 0;

            checkRequiredCheckBox = new CheckBox("Fail if required properties are missing");

            allPropertiesOptionalCheckBox = new CheckBox("Make all properties optional");
        }

        private void Build()
        {
            namespaceBox.PackStart(namespaceLabel);
            namespaceBox.PackEnd(namespaceEntry);

            arrayTypeBox.PackStart(arrayTypeLabel);
            arrayTypeBox.PackEnd(arrayTypeComboBox);

            outputFeatureBox.PackStart(outputFeatureLabel);
            outputFeatureBox.PackEnd(outputFeatureComboBox);

            mainVBox.PackStart(namespaceBox, marginLeft: 10, marginTop: 10, marginRight: 10);
            mainVBox.PackStart(arrayTypeBox, marginLeft: 10, marginTop: 5, marginRight: 10);
            mainVBox.PackStart(outputFeatureBox, marginLeft: 10, marginTop: 5, marginRight: 10);
            mainVBox.PackStart(checkRequiredCheckBox, marginLeft: 10, marginRight: 10);
            mainVBox.PackStart(allPropertiesOptionalCheckBox, marginLeft: 10, marginRight: 10);
            mainVBox.PackEnd(generateButton, false, WidgetPlacement.End, WidgetPlacement.End, margin: 10);
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
