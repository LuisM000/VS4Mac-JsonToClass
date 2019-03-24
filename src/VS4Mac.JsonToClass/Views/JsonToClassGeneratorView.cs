using System;
using System.IO;
using MonoDevelop.Components;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using VS4Mac.JsonToClass.Exceptions;
using VS4Mac.JsonToClass.Model;
using VS4Mac.JsonToClass.Services;
using Xwt;

namespace VS4Mac.JsonToClass.Views
{

    public partial class JsonToClassGeneratorView : PadContent
    {
        QuicktypeProperties quicktypeProperties;

        public JsonToClassGeneratorView()
        {
            InitializeQuicktype();
            InitializeComponent();
        }      

        private void InitializeQuicktype()
        {
            quicktypeProperties = GetSavedQuicktypeProperties() ?? new QuicktypeProperties(Path.GetTempFileName(), Path.Combine(Path.GetTempPath(), "EmptyClass.cs"));
        }

        private void GenerateClass()
        {
            try
            {
                LoadQuicktypePropertiesFromGUI();
                var generatedClass = JsonToClassService.GenerateClassCodeFromJson(Clipboard.GetText(), quicktypeProperties);
                IdeApp.Workbench.ActiveDocument.Editor.InsertAtCaret(generatedClass);
            }
            catch (UninstalledQuicktypeException)
            {
                MessageDialog.ShowWarning("Ouch!", "Something has happened. You may not have Quicktime installed");
            }
        }

        private void SaveQuicktypeSettings()
        {
            var progressMonitor = IdeApp.Workbench.ProgressMonitors.GetStatusProgressMonitor("Saving settings...", Stock.StatusSolutionOperation, false, true, false);
            try
            {
                LoadQuicktypePropertiesFromGUI();
                SettingsService.SaveQuicktypeProperties(quicktypeProperties);
                quicktypeProperties = SettingsService.LoadQuicktypeProperties();
                progressMonitor.ReportSuccess("Saved settings");
            }
            catch (Exception ex)
            {
                progressMonitor.ReportError("Ouch! Something has happened...", ex);
            }
            finally
            {
                progressMonitor.Dispose();
            }
        }

        private QuicktypeProperties LoadQuicktypePropertiesFromGUI()
        {
            quicktypeProperties.Namespace = namespaceEntry.Text;
            quicktypeProperties.ArrayType = (ArrayType)arrayTypeComboBox.SelectedItem;
            quicktypeProperties.FeatureOutput = (Feature)outputFeatureComboBox.SelectedItem;
            quicktypeProperties.CheckRequired = checkRequiredCheckBox.Active;
            quicktypeProperties.AllPropertiesOptional = allPropertiesOptionalCheckBox.Active;
            quicktypeProperties.CSharpVersion = (CSharpVersion)languageVersionComboBox.SelectedItem;
            quicktypeProperties.PropertyDensity = (Density)propertyDensityComboBox.SelectedItem;
            quicktypeProperties.NumberType = (NumberType)numberTypeComboBox.SelectedItem;
            quicktypeProperties.AnyType = (AnyType)anyTypeComboBox.SelectedItem;
            quicktypeProperties.BaseClass = (BaseClassType)baseClassComboBox.SelectedItem;
            quicktypeProperties.DetectUUIDs = detectUUIDsCheckBox.Active;
            quicktypeProperties.DetectBooleansInStrings = detectBooleansInStringsCheckBox.Active;
            quicktypeProperties.DetectDatesAndTimes = detectDatesAndTimesCheckBox.Active;
            quicktypeProperties.DetectEnums = detectEnumsCheckBox.Active;
            quicktypeProperties.DetectIntegersInStrings = detectIntegersInStringsCheckBox.Active;
            quicktypeProperties.DetectMaps = detectMapsCheckBox.Active;
            quicktypeProperties.NoIgnoreJsonRefs = noIgnoreJsonRefsCheckBox.Active;
            quicktypeProperties.MergeSimilarClasses = mergeSimiliarClassesCheckBox.Active;

            return quicktypeProperties;
        }

       
        private QuicktypeProperties GetSavedQuicktypeProperties()
        {
            QuicktypeProperties savedQuicktypeProperties = null;
            try
            {
                savedQuicktypeProperties = SettingsService.LoadQuicktypeProperties();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return savedQuicktypeProperties;
        }
    }
}
