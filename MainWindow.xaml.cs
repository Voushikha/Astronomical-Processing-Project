using System.Windows;
using System.Windows.Media;
using System.Windows.Forms;


namespace kanban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        
            

       
        #region

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // tb1.FontFamily = new FontFamily(dlg.Font.FontFamily.Name);
                tb1.FontSize = fontDialog.Font.Size * 96.0 / 72.0;
                tb1.FontWeight = fontDialog.Font.Bold ? FontWeights.Bold : FontWeights.Normal;
                tb1.FontStyle = fontDialog.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
            }
        }


    }
}