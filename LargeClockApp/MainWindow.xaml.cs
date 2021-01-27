using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace LargeClockApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string clockFormat = "12-hr";
        private string format = "hh:mm:ss tt";

        private string textColor;
        private string backgroundColor;
        private string textSize;

        private int distractionInterval = 1;


        public MainWindow()
        {
            InitializeComponent();

            // update clock format
            UpdateClockFormat();
            UpdateTextColor();
            UpdateBgColor();
            UpdateTextSize();

            DisplayClock();

            //DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            //dispatcherTimer.Tick += new EventHandler(PlayDistraction_Tick);
            //dispatcherTimer.Interval = new TimeSpan(distractionInterval, 0, 0);
            //dispatcherTimer.Start();


        }

        //private void PlayDistraction_Tick(object sender, EventArgs e)
        //{
        //    Random gen = new Random();
        //    distractionInterval = gen.Next(1, 4); // gens a number from 1 to 3

        //    SoundPlayer player = new SoundPlayer("./AudioResources/Quack.wav");
        //    player.Play();
        //}

        /// <summary>
        /// Set timer for clock
        /// </summary>
        private void DisplayClock()
        {
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1),
                DispatcherPriority.Normal, delegate
                {
                    clockLabel.Content = DateTime.Now.ToString(format);
                }, this.Dispatcher);

        }



        /// <summary>
        /// Update the Clock format
        /// </summary>
        private void UpdateClockFormat()
        {
            clockFormat = ClockSettings.Default.ClockFormat;

            switch (clockFormat)
            {
                case "12-hr":
                    format = "hh:mm:ss tt";
                    Item12hr.IsChecked = true;
                    break;
                case "24-hr":
                    format = "HH:mm:ss";
                    Item24hr.IsChecked = true;
                    break;
            }
        }

        private void UpdateTextColor()
        {
            textColor = ClockSettings.Default.TextColor;
            switch (textColor)
            {
                case "Black":
                    clockLabel.Foreground = Brushes.Black;
                    BlackText.IsChecked = true;
                    break;
                case "Red":
                    clockLabel.Foreground = Brushes.Red;
                    RedText.IsChecked = true;
                    break;
                case "Green":
                    clockLabel.Foreground = Brushes.GreenYellow;
                    GreenText.IsChecked = true;
                    break;
                case "Blue":
                    clockLabel.Foreground = Brushes.DeepSkyBlue;
                    BlueText.IsChecked = true;
                    break;
                case "Pink":
                    clockLabel.Foreground = Brushes.Pink;
                    PinkText.IsChecked = true;
                    break;
            }
        }

        private void UpdateBgColor()
        {
            backgroundColor = ClockSettings.Default.BackgroundColor;

            switch (backgroundColor)
            {
                case "Transparent":
                    clockLabel.Background = new SolidColorBrush(Colors.Transparent);
                    TranseparentBg.IsChecked = true;
                    break;
                case "Black":
                    clockLabel.Background = Brushes.Black;
                    BlackBg.IsChecked = true;
                    break;
                case "Yellow":
                    clockLabel.Background = Brushes.LightGoldenrodYellow;
                    YellowBg.IsChecked = true;
                    break;
                case "White":
                    clockLabel.Background = Brushes.White;
                    WhiteBg.IsChecked = true;
                    break;
                case "Pink":
                    clockLabel.Background = Brushes.Pink;
                    PinkBg.IsChecked = true;
                    break;
            }

        }

        private void UpdateTextSize()
        {
            textSize = ClockSettings.Default.TextSize;

            switch (textSize)
            {

                case "TextSize80":
                    clockLabel.FontSize = 80;
                    TextSize80.IsChecked = true;
                    break;
                case "TextSize100":
                    clockLabel.FontSize = 100;
                    TextSize100.IsChecked = true;
                    break;
                case "TextSize120":
                    clockLabel.FontSize = 120;
                    TextSize120.IsChecked = true;
                    break;
                case "TextSize150":
                    clockLabel.FontSize = 150;
                    TextSize150.IsChecked = true;
                    break;
            }
        }

        /// <summary>
        /// Move the clock on Mouse Down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// set clock format 12 hr/24 hr
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetClockFormat_Click(object sender, RoutedEventArgs e)
        {
            MenuItem selectedFormat = sender as MenuItem;

            // clear all options
            Item12hr.IsChecked = false;
            Item24hr.IsChecked = false;

            // save settings
            ClockSettings.Default.ClockFormat = selectedFormat.Header.ToString();
            ClockSettings.Default.Save();

            selectedFormat.IsChecked = true;

            UpdateClockFormat();
        }

        private void ChangeTextColor_Click(object sender, RoutedEventArgs e)
        {
            MenuItem selectedColor = sender as MenuItem;

            // clear all options
            BlackText.IsChecked = false;
            RedText.IsChecked = false;
            GreenText.IsChecked = false;
            BlueText.IsChecked = false;
            PinkText.IsChecked = false;
            CustomText.IsChecked = false;

            if (selectedColor.Header.ToString() == "Custom")
            {
                ColorDialog
            }

            ClockSettings.Default.TextColor = selectedColor.Header.ToString();
            ClockSettings.Default.Save();

            UpdateTextColor();
        }

        private void ChangeBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            MenuItem selectedBg = sender as MenuItem;

            // clear all option
            TranseparentBg.IsChecked = false;
            BlackBg.IsChecked = false;
            YellowBg.IsChecked = false;
            WhiteBg.IsChecked = false;
            PinkBg.IsChecked = false;

            ClockSettings.Default.BackgroundColor = selectedBg.Header.ToString();
            ClockSettings.Default.Save();

            UpdateBgColor();

        }

        private void TextSizeChange_Click(object sender, RoutedEventArgs e)
        {
            MenuItem textSize = sender as MenuItem;

            TextSize80.IsChecked = false;
            TextSize100.IsChecked = false;
            TextSize120.IsChecked = false;
            TextSize150.IsChecked = false;

            ClockSettings.Default.TextSize = textSize.Name;
            ClockSettings.Default.Save();

            UpdateTextSize();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
