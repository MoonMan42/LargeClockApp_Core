using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Windows;
using System.Windows.Input;

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
            LoadFontSize();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //save clock font and size 
            var fontStyle = FontList.SelectedItem;

            if (fontStyle != null)
            {
                ClockSettings.Default.FontType = FontList.SelectedItem.ToString();
            }

            ClockSettings.Default.FontSize = FontSizeSlider.Value;
            ClockSettings.Default.Save();


            base.OnClosing(e);
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

        private void LoadFontSize()
        {
            FontSizeSlider.Value = ClockSettings.Default.FontSize;
        }



        private void FontSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // save fontsize value and refresh the size of the clock
            double clockFontSize = FontSizeSlider.Value;

            ((MainWindow)this.Owner)?.UpdateTextSize(clockFontSize);
        }

        private void SelectFont_Click(object sender, MouseButtonEventArgs e)
        {
            // capture the selected font and run update font from main window. 
            string fontFamily = FontList.SelectedItem.ToString();
            ((MainWindow)this.Owner).UpdateFontStye(fontFamily);
        }
    }
}
