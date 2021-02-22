using LargeClockApp.Windows;
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

        private int repeatinterval = 10; // minutes
        private bool IsAlarmEnabled = false;

        public string customColor;

        private DispatcherTimer alertTimer;

        public MainWindow()
        {
            InitializeComponent();

            // update clock format
            UpdateClockFormat();
            UpdateAlarmSound();
            UpdateTextColor();
            UpdateBgColor();
            UpdateTextSize();
            UpdateFontStye();


            DisplayClock();


            alertTimer = new DispatcherTimer();
            alertTimer.Tick += new EventHandler(PlayAlarm_Tick);
            alertTimer.Interval = new TimeSpan(0, repeatinterval, 0);

        }

        private void StartRepeatingAlarm()
        {
            alertTimer.Start();
        }

        private void StopRepeatingAlarm()
        {
            alertTimer.Stop();
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


        public void UpdateTextColor()
        {
            textColor = ClockSettings.Default.TextColor;
            switch (textColor)
            {
                case "Black":
                    clockLabel.Foreground = Brushes.Black;
                    StoryBoardAlert.Foreground = Brushes.Black;
                    BlackText.IsChecked = true;
                    break;
                case "Red":
                    clockLabel.Foreground = Brushes.Red;
                    StoryBoardAlert.Foreground = Brushes.Red;
                    RedText.IsChecked = true;
                    break;
                case "Green":
                    clockLabel.Foreground = Brushes.GreenYellow;
                    StoryBoardAlert.Foreground = Brushes.GreenYellow;
                    GreenText.IsChecked = true;
                    break;
                case "Blue":
                    clockLabel.Foreground = Brushes.DeepSkyBlue;
                    StoryBoardAlert.Foreground = Brushes.DeepSkyBlue;
                    BlueText.IsChecked = true;
                    break;
                case "Pink":
                    clockLabel.Foreground = Brushes.Pink;
                    StoryBoardAlert.Foreground = Brushes.Pink;
                    PinkText.IsChecked = true;
                    break;
                case "Custom":
                    clockLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ClockSettings.Default.CustomTextColor));
                    StoryBoardAlert.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ClockSettings.Default.CustomTextColor)); ;
                    CustomText.IsChecked = true;
                    break;

            }
        }

        public void UpdateBgColor()
        {
            backgroundColor = ClockSettings.Default.BackgroundColor;

            switch (backgroundColor)
            {
                case "Transparent":
                    ClockGrid.Background = new SolidColorBrush(Colors.Transparent);
                    TranseparentBg.IsChecked = true;
                    break;
                case "Black":
                    ClockGrid.Background = Brushes.Black;
                    BlackBg.IsChecked = true;
                    break;
                case "Yellow":
                    ClockGrid.Background = Brushes.LightGoldenrodYellow;
                    YellowBg.IsChecked = true;
                    break;
                case "White":
                    ClockGrid.Background = Brushes.White;
                    WhiteBg.IsChecked = true;
                    break;
                case "Pink":
                    ClockGrid.Background = Brushes.Pink;
                    PinkBg.IsChecked = true;
                    break;
                case "Custom":
                    ClockGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ClockSettings.Default.CustomeBgColor));
                    CustomBg.IsChecked = true;
                    break;
            }

        }

        public void UpdateTextSize(double clockFontSize = 0)
        {
            clockLabel.FontSize = clockFontSize != 0 ? clockFontSize : ClockSettings.Default.ClockSize;
            StoryBoardAlert.FontSize = clockFontSize != 0 ? clockFontSize * 0.25 : ClockSettings.Default.ClockSize * 0.25;
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

        private void ChangeAlarmTime_Click(object sender, RoutedEventArgs e)
        {
            MenuItem selectedAlarmTime = sender as MenuItem;

            // clear all options
            T10Mins.IsChecked = false;
            T15Mins.IsChecked = false;
            T20Mins.IsChecked = false;
            DisabledAlarm.IsChecked = false;

            // enable alarm
            IsAlarmEnabled = true;

            switch (selectedAlarmTime.Name)
            {
                case "T10Mins":
                    repeatinterval = 10;
                    T10Mins.IsChecked = true;
                    break;
                case "T15Mins":
                    repeatinterval = 15;
                    T15Mins.IsChecked = true;
                    break;
                case "T20Mins":
                    repeatinterval = 20;
                    T20Mins.IsChecked = true;
                    break;
                case "DisabledAlarm":
                    DisabledAlarm.IsChecked = true;
                    IsAlarmEnabled = false;
                    break;
            }

            // enable/disable alarm
            if (IsAlarmEnabled)
            {
                StartRepeatingAlarm();
            }
            else
            {
                StopRepeatingAlarm();
            }

        }

        private void UpdateAlarmSound()
        {
            string savedSound = ClockSettings.Default.SetAlarmSound;

            switch (savedSound)
            {
                case "quack.wav":
                    QuackSound.IsChecked = true;
                    break;
                case "ahahah.wav":
                    AhSound.IsChecked = true;
                    break;
            }
        }

        public void UpdateFontStye(string fontStyle = "")
        {
            if (fontStyle == "")
            {
                fontStyle = ClockSettings.Default.FontType;
            }

            clockLabel.FontFamily = new FontFamily(fontStyle);

        }


        private void SetAlarmSound_Click(object sender, RoutedEventArgs e)
        {
            MenuItem selectedSound = sender as MenuItem;

            QuackSound.IsChecked = false;
            AhSound.IsChecked = false;

            switch (selectedSound.Name)
            {
                case "QuackSound":
                    QuackSound.IsChecked = true;
                    ClockSettings.Default.SetAlarmSound = "quack.wav";
                    break;
                case "AhSound":
                    AhSound.IsChecked = true;
                    ClockSettings.Default.SetAlarmSound = "ahahah.wav";
                    break;
            }


            ClockSettings.Default.Save();


            UpdateAlarmSound();
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
                ColorPicker colorPicker = new ColorPicker("text");
                colorPicker.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                colorPicker.Owner = this;
                colorPicker.Show();
            }
            else
            {
                ClockSettings.Default.CustomTextColor = "#FF000000";
                ClockSettings.Default.Save();
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
            CustomBg.IsChecked = false;

            if (selectedBg.Header.ToString() == "Custom")
            {
                ColorPicker colorPicker = new ColorPicker("bg");
                colorPicker.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                colorPicker.Owner = this;
                colorPicker.Show();
            }
            else
            {
                ClockSettings.Default.CustomeBgColor = "#FF000000";
                ClockSettings.Default.Save();
            }

            ClockSettings.Default.BackgroundColor = selectedBg.Header.ToString();
            ClockSettings.Default.Save();

            UpdateBgColor();

        }

        private void CustomFont_Click(object sender, RoutedEventArgs e)
        {
            CustomFontWindow customFont = new CustomFontWindow();

            customFont.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            customFont.Owner = this;
            customFont.Show();
        }

        private void CustomMessage_Click(object sender, RoutedEventArgs e)
        {
            CustomMessageWindow customMessage = new CustomMessageWindow();
            customMessage.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            customMessage.Owner = this;
            customMessage.Show();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }




        #region Sound Effects
        private void PlayAlarm_Tick(object sender, EventArgs e)
        {
            string audioSource = ClockSettings.Default.SetAlarmSound;
            SoundPlayer player = new SoundPlayer($"./AudioResources/{audioSource}");
            player.Play();
        }



        #endregion


    }
}
