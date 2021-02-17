using System.Collections.Generic;
using System.Drawing.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace LargeClockApp
{
    /// <summary>
    /// Interaction logic for CustomFontWindow.xaml
    /// </summary>
    public partial class CustomFontWindow : Window
    {
        public CustomFontWindow()
        {
            InitializeComponent();

            LoadFontList();
        }

        private void LoadFontList()
        {
            List<string> fontList = new List<string>();

            using (InstalledFontCollection col = new InstalledFontCollection())
            {
                foreach (var f in col.Families)
                {
                    fontList.Add(f.Name);
                }
            }

            FontList.ItemsSource = fontList;
        }

        private void SelectFont_Click(object sender, MouseButtonEventArgs e)
        {
            // capture the selected font and run update font from main window. 
            string fontFamily = FontList.SelectedItem.ToString();

            ClockSettings.Default.FontType = fontFamily;
            ClockSettings.Default.Save();

            ((MainWindow)this.Owner).UpdateFontStye(fontFamily);

            Close();
        }
    }
}
