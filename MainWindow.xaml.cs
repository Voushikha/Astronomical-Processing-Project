using AstroMaths;
using Haley.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace kanban
{
    public partial class MainWindow : Window
    {
        private AstroMathFunctions _astroFunctions;
        private bool isNightMode = false; // To track the current theme FOR NIGHT MODE

        public MainWindow()
        {
            InitializeComponent();
            _astroFunctions = new AstroMathFunctions();
        

            //// Add the ColorPickerButton to the MainWindow's resources
            //Resources.Add("ColorPickerButton", new ColorPickerButton());


        }

        #region language
        private void comboBoxLanguages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedLanguage = (comboBoxLanguages.SelectedItem as ComboBoxItem)?.Tag.ToString();

            if (selectedLanguage != null)
            {
                ApplyLanguage(selectedLanguage);
            }
        }

        private void ApplyLanguage(string language)
        {
            switch (language)

            {
                case "en": // English
                    InputSVlb.Content = "Star Velocity";
                    OutputSVlb.Content = "Star Velocity";
                    InputSDlb.Content = "Star Distance";
                    OutputSdlb.Content = "Star Distance";
                    InputTClb.Content = "Temperature Conversion";
                    OutputTClb.Content = "Temperature Conversion";
                    InputBEHlb.Content = "Blackhole Event Horizon";
                    OutputBHlb.Content = "Blackhole Event Horizon";

                    Language.Content = "Language";
                    Result.Content = "Result";
                    colorPkr.Content = "Colour Selection";
                    FontBtn.Content = "Font Style";
                    CalculateButton.Content = "Calculate";
                    ClearButton.Content = "Clear";
                    break;

                case "fr": // French
                    InputSVlb.Content = "Vitesse de l'étoile";
                    OutputSVlb.Content = "Vitesse de l'étoile";
                    InputSDlb.Content = "Distance de l'étoile";
                    OutputSdlb.Content = "Distance de l'étoile";
                    InputTClb.Content = "Conversion de température";
                    OutputTClb.Content = "Conversion de température";
                    InputBEHlb.Content = "Horizon des événements";
                    OutputBHlb.Content = "Horizon des événements";

                    Language.Content = "langue";
                    Result.Content = "Resultat";
                    colorPkr.Content = "Selection de Couleur";
                    FontBtn.Content = "Style de police";
                    CalculateButton.Content = "Calculer";
                    ClearButton.Content = "Effacer";
                    break;

                case "de": // German
                    InputSVlb.Content = "Sternengeschwindigkeit";
                    OutputSVlb.Content = "Sternengeschwindigkeit";
                    InputSDlb.Content = "Sternendistanz";
                    OutputSdlb.Content = "Sternendistanz";
                    InputTClb.Content = "Temperaturumrechnung";
                    OutputTClb.Content = "Temperaturumrechnung";
                    InputBEHlb.Content = "Ereignishorizont";
                    OutputBHlb.Content = "Ereignishorizont";

                    Language.Content = "Ergebnis";
                    Result.Content = "Ergebnis";
                    colorPkr.Content = "Farbauswahl";
                    FontBtn.Content = "Schriftstil";
                    CalculateButton.Content = "Berechnen";
                    ClearButton.Content = "Löschen";
                    break;
            }
        }
        //private void comboBoxLanguages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    // Get the selected language from ComboBox
        //    ComboBoxItem selectedItem = (ComboBoxItem)comboBoxLanguages.SelectedItem;

        //    // Check if the selectedItem is null or has a null Tag before proceeding
        //    if (selectedItem != null && selectedItem.Tag != null)
        //    {
        //        string selectedLanguage = selectedItem.Tag.ToString();
        //        ApplyLanguageToAllControls(selectedLanguage);
        //    }
        //    else
        //    {
        //        // Handle the case where no valid language is selected 
        //        System.Windows.MessageBox.Show("Please select a valid language.");
        //    }
        //}
        //// Method to change the language of all controls
        //private void ApplyLanguageToAllControls(string languageCode)
        //{
        //    foreach (var child in mainWindow.Children)
        //    {
        //        if (child is System.Windows.Controls.Label label)
        //        {
        //            if (languageCode == "en")
        //                label.Content = "English Label";
        //            else if (languageCode == "fr")
        //                label.Content = "Étiquette française";
        //            else if (languageCode == "de")
        //                label.Content = "Deutsches Etikett";
        //        }
        //        else if (child is System.Windows.Controls.TextBox textBox)
        //        {
        //            if (languageCode == "en")
        //                textBox.Text = "English Text";
        //            else if (languageCode == "fr")
        //                textBox.Text = "Texte français";
        //            else if (languageCode == "de")
        //                textBox.Text = "Deutscher Text";
        //        }
        //    }
        //}
        #endregion


        #region Calculate & Clear button
        // Calculate Button Event Handler
        private void TextBoxes()
        {
            txtBoxInputVelocity.Clear();
            txtBoxInputDistance.Clear();
            txtBoxInputTemp.Clear();
            txtBoxInputBlackhole.Clear();
            txtBoxOutputVelocity.Clear();
            txtBoxOutputDistance.Clear();
            txtBoxOutputTemp.Clear();
            txtBoxOutputBlackhole.Clear();
            txtBoxOutputResult.Clear();

        }
       
      
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
       

            try
            {
                // Parse inputs from textboxes
                double observedWavelength = double.Parse(txtBoxInputVelocity.Text);
                double restWavelength = double.Parse(txtBoxInputDistance.Text);
                double celsius = double.Parse(txtBoxInputTemp.Text);
                double blackholeMass = double.Parse(txtBoxInputBlackhole.Text);

                // Perform calculations
                double velocity = _astroFunctions.StarVelocity(observedWavelength, restWavelength);
                double distance = _astroFunctions.StarDistance(observedWavelength); // Parallax angle as input
                double kelvin = _astroFunctions.Kelvin(celsius);
                double eventHorizon = _astroFunctions.EventHorizon(blackholeMass);

                // Display results in the output textboxes
                txtBoxOutputVelocity.Text = $"{velocity:F2} m/s";
                txtBoxOutputDistance.Text = $"{distance:F2} parsecs";
                txtBoxOutputTemp.Text = $"{kelvin:F2} K";
                txtBoxOutputBlackhole.Text = $"{eventHorizon:E2} meters";
                txtBoxOutputResult.Text = "Calculation Successful!";
            }
            catch (FormatException)
            {
               
                System.Windows.MessageBox.Show("Please enter valid numeric values.",
                    "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"An error occurred: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Clear Button Event Handler
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // Clear all input and output textboxes
            TextBoxes();
        }
        #endregion

        // Font Selection Button Event Handler
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var fontDialog = new FontDialog();
        //    if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        // Adjust the font properties of the TextBox (tb1) based on user selection
        //        tb1.FontSize = fontDialog.Font.Size * 96.0 / 72.0; // Convert points to WPF units
        //        tb1.FontWeight = fontDialog.Font.Bold ? FontWeights.Bold : FontWeights.Normal;
        //        tb1.FontStyle = fontDialog.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
        //    }
        //}

        //Font Style Button 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Open the font dialog from System.Windows.Forms
            var fontDialog = new System.Windows.Forms.FontDialog();

            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Convert font size from points (WinForms) to WPF units (96 DPI)
                double fontSizeInWpfUnits = fontDialog.Font.Size * 96.0 / 72.0;

                // Apply the font settings to all controls in the main window
                foreach (var child in mainWindow.Children)
                {
                    if (child is System.Windows.Controls.TextBox textBox)
                    {
                        textBox.FontSize = fontSizeInWpfUnits;
                        textBox.FontWeight = fontDialog.Font.Bold ? FontWeights.Bold : FontWeights.Normal;
                        textBox.FontStyle = fontDialog.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
                        textBox.FontFamily = new System.Windows.Media.FontFamily(fontDialog.Font.Name);
                    }
                    else if (child is System.Windows.Controls.Label label)
                    {
                        label.FontSize = fontSizeInWpfUnits;
                        label.FontWeight = fontDialog.Font.Bold ? FontWeights.Bold : FontWeights.Normal;
                        label.FontStyle = fontDialog.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
                        label.FontFamily = new System.Windows.Media.FontFamily(fontDialog.Font.Name);
                    }
                    else if (child is System.Windows.Controls.Button button)
                    {
                        button.FontSize = fontSizeInWpfUnits;
                        button.FontWeight = fontDialog.Font.Bold ? FontWeights.Bold : FontWeights.Normal;
                        button.FontStyle = fontDialog.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
                        button.FontFamily = new System.Windows.Media.FontFamily(fontDialog.Font.Name);
                    }
                }
            }
        }



        #region color picker / NIGHT MODE

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                // Set the background color of the grid to the selected color
                mainWindow.Background = new SolidColorBrush(e.NewValue.Value);
            }
        }

        // This event handler is for the night mode toggle button
        private void NightModeToggle_Click(object sender, RoutedEventArgs e)
        {
            if (isNightMode)
            {
                // Switch to day mode
                mainWindow.Background = new SolidColorBrush(Colors.LightGray); // Background color for day mode
                NightModeToggle.Content = "Night Mode";
                ChangeTextColor(Colors.Black); // Change text color to black for day mode
            }
            else
            {
                // Switch to night mode
                mainWindow.Background = new SolidColorBrush(Colors.Black); // Background color for night mode
                NightModeToggle.Content = "Day Mode";
                ChangeTextColor(Colors.White); // Change text color to white for night mode
                ChangeTextBox(Colors.Black);
            }

            isNightMode = !isNightMode; // Toggle the mode
        }

        // Helper function to change text color for all labels
        private void ChangeTextColor(System.Windows.Media.Color color)
        {
            foreach (var child in mainWindow.Children)
            {
                if (child is System.Windows.Controls.Label label)
                {
                    label.Foreground = new SolidColorBrush(color);
                }
              
            }
        }

        // Helper function to change text color for all inside textboxes
        private void ChangeTextBox(System.Windows.Media.Color color)

        {
            foreach (var child in mainWindow.Children) { 
                if (child is System.Windows.Controls.TextBox textBox)
            {
                textBox.Foreground = new SolidColorBrush(color);
            }
            }
        }


    #endregion
}
    
}
