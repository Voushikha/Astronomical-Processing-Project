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


                    colorPkr.Content = "Farbauswahl";
                    FontBtn.Content = "Schriftstil";
                    CalculateButton.Content = "Berechnen";
                    ClearButton.Content = "Löschen";
                    break;
            }
        }
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Adjust the font properties of the TextBox (tb1) based on user selection
                tb1.FontSize = fontDialog.Font.Size * 96.0 / 72.0; // Convert points to WPF units
                tb1.FontWeight = fontDialog.Font.Bold ? FontWeights.Bold : FontWeights.Normal;
                tb1.FontStyle = fontDialog.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
            }
        }

        #region color picker
        //private void ColorPkr_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorDialog colorDialog = new ColorDialog();
        //    if (colorDialog.ShowDialog() == true)
        //    {
        //        mainWindow.Background = new SolidColorBrush(colorDialog.Color);
        //    }
        //}

        #endregion
    }
    
}
