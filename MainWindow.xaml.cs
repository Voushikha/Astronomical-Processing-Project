using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Forms; // Ensure you reference the System.Windows.Forms assembly
using AstroMaths;

namespace kanban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AstroMathFunctions _astroFunctions;

        public MainWindow()
        {
            InitializeComponent();
            _astroFunctions = new AstroMathFunctions(); // Initialize AstroMathFunctions
        }
        // Calculate Button Event Handler
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
    }
}
