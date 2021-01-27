using System.Windows;
using System.Windows.Media;

namespace LargeClockApp
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : Window
    {
        private string customColor;

        private string _selection;

        public ColorPicker(string selection)
        {
            InitializeComponent();

            _selection = selection;
        }


        private void ColorCanvas_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            customColor = colorPicker.SelectedColor.ToString();

        }

        private void SaveColor_Click(object sender, RoutedEventArgs e)
        {
            if (_selection == "text")
            {
                ClockSettings.Default.CustomTextColor = customColor;
                ClockSettings.Default.Save();

                ((MainWindow)this.Owner).UpdateTextColor();
            }
            else if (_selection == "bg")
            {
                ClockSettings.Default.CustomeBgColor = customColor;
                ClockSettings.Default.Save();

                ((MainWindow)this.Owner).UpdateBgColor();

            }




            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
