using System.Windows;

namespace LargeClockApp.Windows
{
    /// <summary>
    /// Interaction logic for CustomMessageWindow.xaml
    /// </summary>
    public partial class CustomMessageWindow : Window
    {
        public CustomMessageWindow()
        {
            InitializeComponent();

            //Load message from settings. 
            NewMessageText.Text = ClockSettings.Default.SavedStoryBoard;

            if (ClockSettings.Default.IsStoryBoardVisible == "Visible")
            {
                IsStoryBoardEnabled_Check.IsChecked = true;
            }
            else
            {
                IsStoryBoardEnabled_Check.IsChecked = false;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            // save new message
            ClockSettings.Default.SavedStoryBoard = NewMessageText.Text;
            ClockSettings.Default.IsStoryBoardVisible = (bool)IsStoryBoardEnabled_Check.IsChecked ? "Visible" : "Collapsed";
            ClockSettings.Default.Save();

            Close();
        }
    }
}
