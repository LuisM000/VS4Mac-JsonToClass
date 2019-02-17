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

        VBox propertyDensityBox;
        Label propertyDensityLabel;
        ComboBox propertyDensityComboBox;

        VBox numberTypeBox;
        Label numberTypeLabel;
        ComboBox numberTypeComboBox;

        VBox anyTypeBox;
        Label anyTypeLabel;
        ComboBox anyTypeComboBox;

        VBox baseClassBox;
        Label baseClassLabel;
        ComboBox baseClassComboBox;

        CheckBox detectUUIDsCheckBox;

        CheckBox detectBooleansInStringsCheckBox;

        CheckBox detectDatesAndTimesCheckBox;

        CheckBox detectEnumsCheckBox;

        CheckBox detectIntegersInStringsCheckBox;

        CheckBox detectMapsCheckBox;

        CheckBox noIgnoreJsonRefsCheckBox;

        CheckBox mergeSimiliarClassesCheckBox;

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
            languageTabVBox = new VBox();
            otherTabVBox = new VBox();
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

            propertyDensityBox = new VBox();
            propertyDensityLabel = new Label("Property density");
            propertyDensityComboBox = new ComboBox();
            propertyDensityComboBox.Items.Add(Model.Density.Normal, "Normal");
            propertyDensityComboBox.Items.Add(Model.Density.Dense, "Dense");
            propertyDensityComboBox.SelectedIndex = 0;

            numberTypeBox = new VBox();
            numberTypeLabel = new Label("Type to use for numbers");
            numberTypeComboBox = new ComboBox();
            numberTypeComboBox.Items.Add(Model.NumberType.Double, "Double");
            numberTypeComboBox.Items.Add(Model.NumberType.Decimal, "Decimal");
            numberTypeComboBox.SelectedIndex = 0;

            anyTypeBox = new VBox();
            anyTypeLabel = new Label("Type to use for \"any\"");
            anyTypeComboBox = new ComboBox();
            anyTypeComboBox.Items.Add(Model.AnyType.Object, "Object");
            anyTypeComboBox.Items.Add(Model.AnyType.Dynamic, "Dynamic");
            anyTypeComboBox.SelectedIndex = 0;

            baseClassBox = new VBox();
            baseClassLabel = new Label("Base class");
            baseClassComboBox = new ComboBox();
            baseClassComboBox.Items.Add(Model.BaseClassType.EntityData, "Entity Data");
            baseClassComboBox.Items.Add(Model.BaseClassType.Object, "Object");
            baseClassComboBox.SelectedIndex = 1;

            detectUUIDsCheckBox = new CheckBox("Detect UUIDs") { Active = true };
            detectBooleansInStringsCheckBox = new CheckBox("Detect booleans in strings") { Active = true };
            detectDatesAndTimesCheckBox = new CheckBox("Detect dates & times") { Active = true };
            detectEnumsCheckBox = new CheckBox("Detect enums") { Active = true };
            detectIntegersInStringsCheckBox = new CheckBox("Detect integers in strings") { Active = true };
            detectMapsCheckBox = new CheckBox("Detect maps") { Active = true };
            noIgnoreJsonRefsCheckBox = new CheckBox("Don't treat $ref as a reference in JSON") { Active = true };
            mergeSimiliarClassesCheckBox = new CheckBox("Merge similar classes") { Active = true };
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

            propertyDensityBox.PackStart(propertyDensityLabel);
            propertyDensityBox.PackEnd(propertyDensityComboBox);

            numberTypeBox.PackStart(numberTypeLabel);
            numberTypeBox.PackEnd(numberTypeComboBox);

            anyTypeBox.PackStart(anyTypeLabel);
            anyTypeBox.PackEnd(anyTypeComboBox);

            baseClassBox.PackStart(baseClassLabel);
            baseClassBox.PackEnd(baseClassComboBox);

            otherTabVBox.PackStart(languageVersionBox, marginLeft: 10, marginTop: 10, marginRight: 10);
            otherTabVBox.PackStart(propertyDensityBox, marginLeft: 10, marginTop: 5, marginRight: 10);
            otherTabVBox.PackStart(numberTypeBox, marginLeft: 10, marginTop: 5, marginRight: 10);
            otherTabVBox.PackStart(anyTypeBox, marginLeft: 10, marginTop: 5, marginRight: 10);
            otherTabVBox.PackStart(baseClassBox, marginLeft: 10, marginTop: 5, marginRight: 10);
            otherTabVBox.PackStart(detectUUIDsCheckBox, marginLeft: 10, marginRight: 10);
            otherTabVBox.PackStart(detectBooleansInStringsCheckBox, marginLeft: 10, marginRight: 10);
            otherTabVBox.PackStart(detectDatesAndTimesCheckBox, marginLeft: 10, marginRight: 10);
            otherTabVBox.PackStart(detectEnumsCheckBox, marginLeft: 10, marginRight: 10);
            otherTabVBox.PackStart(detectIntegersInStringsCheckBox, marginLeft: 10, marginRight: 10);
            otherTabVBox.PackStart(detectMapsCheckBox, marginLeft: 10, marginRight: 10);
            otherTabVBox.PackStart(noIgnoreJsonRefsCheckBox, marginLeft: 10, marginRight: 10);
            otherTabVBox.PackStart(mergeSimiliarClassesCheckBox, marginLeft: 10, marginRight: 10, marginBottom: 10);


            mainNotebook.Add(languageTabVBox, "Language");
            mainNotebook.Add(otherTabVBox, "Other");

            mainVBox.PackStart(mainNotebook, marginTop: 8);
            mainVBox.PackEnd(generateButton, false, WidgetPlacement.End, WidgetPlacement.End, margin: 7);
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
