using System;
using MonoDevelop.Components;
using MonoDevelop.Ide.Gui;
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
        Notebook mainNotebook;
        VBox languageTabVBox;
        VBox otherTabVBox;
        Button generateButton;

        #region Language tab

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

        #endregion

        #region Other tab

        VBox languageVersionBox;
        Label languageVersionLabel;
        ComboBox languageVersionComboBox;

        #endregion

        private void InitializeComponent()
        {
            Init();
            Build();
            AttachToEvents();
        }

        private void Init()
        {
            mainVBox = new VBox();
            mainNotebook = new Notebook();
            languageTabVBox = new VBox();// { BackgroundColor = MonoDevelop.Ide.Gui.Styles.PadBackground };
            otherTabVBox = new VBox();// { BackgroundColor = MonoDevelop.Ide.Gui.Styles.PadBackground };
            generateButton = new Button("Generate")
            {
                BackgroundColor = Styles.BaseSelectionBackgroundColor,
                LabelColor = Styles.BaseSelectionTextColor
            };

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

            languageVersionBox = new VBox();
            languageVersionLabel = new Label("C# version");
            languageVersionComboBox = new ComboBox();
            languageVersionComboBox.Items.Add(Model.CSharpVersion.Five, "5");
            languageVersionComboBox.Items.Add(Model.CSharpVersion.Six, "6");
            languageVersionComboBox.SelectedIndex = 1;
        }

        private void Build()
        {
            namespaceBox.PackStart(namespaceLabel);
            namespaceBox.PackEnd(namespaceEntry);

            arrayTypeBox.PackStart(arrayTypeLabel);
            arrayTypeBox.PackEnd(arrayTypeComboBox);

            outputFeatureBox.PackStart(outputFeatureLabel);
            outputFeatureBox.PackEnd(outputFeatureComboBox);

            languageTabVBox.PackStart(namespaceBox, marginLeft: 10, marginTop: 10, marginRight: 10);
            languageTabVBox.PackStart(arrayTypeBox, marginLeft: 10, marginTop: 5, marginRight: 10);
            languageTabVBox.PackStart(outputFeatureBox, marginLeft: 10, marginTop: 5, marginRight: 10);
            languageTabVBox.PackStart(checkRequiredCheckBox, marginLeft: 10, marginRight: 10);
            languageTabVBox.PackStart(allPropertiesOptionalCheckBox, marginLeft: 10, marginRight: 10, marginBottom: 10);

            languageVersionBox.PackStart(languageVersionLabel);
            languageVersionBox.PackEnd(languageVersionComboBox);

            otherTabVBox.PackStart(languageVersionBox, marginLeft: 10, marginTop: 10, marginRight: 10);

            mainNotebook.Add(languageTabVBox, "Language");
            mainNotebook.Add(otherTabVBox, "Other");

            mainVBox.PackStart(mainNotebook, marginTop: 5);
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
