/*
MIT License

Copyright (c) Léo Corporation

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. 
*/
using Datalya.Classes;
using Datalya.Enums;
using Datalya.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Datalya.Pages.FirstRun
{
	/// <summary>
	/// Interaction logic for ThemeFirstRunPage.xaml
	/// </summary>
	public partial class ThemeFirstRunPage : Page
	{
		FirstRunWindow FirstRunWindow { get; init; }
		public ThemeFirstRunPage(FirstRunWindow firstRunWindow)
		{
			InitializeComponent();
			FirstRunWindow = firstRunWindow; // Set

			InitUI();
		}

		private void InitUI()
		{
			CheckedBorder = Global.Settings.Theme switch
			{
				Theme.Light => LightBorder, // Light theme
				Theme.Dark => DarkBorder, // Dark theme
				Theme.System => SystemBorder, // System theme
				_ => SystemBorder // System theme
			};
			RefreshBorders(); // Refresh

			LightRadioBtn.IsChecked = Global.Settings.Theme == Theme.Light; // Set IsChecked property
			DarkRadioBtn.IsChecked = Global.Settings.Theme == Theme.Dark; // Set IsChecked property
			SystemRadioBtn.IsChecked = Global.Settings.Theme == Theme.System; // Set IsChecked property
		}

		private void NextBtn_Click(object sender, RoutedEventArgs e)
		{
			var currentTheme = Global.Settings.Theme; // Set valeu
			if (DarkRadioBtn.IsChecked.Value)
			{
				Global.Settings.Theme = Theme.Dark; // Set theme to dark
			}
			else if (LightRadioBtn.IsChecked.Value)
			{
				Global.Settings.Theme = Theme.Light; // Set theme to light
			}
			else if (SystemRadioBtn.IsChecked.Value)
			{
				Global.Settings.Theme = Theme.System; // Set theme to system
			}

			Global.Settings.IsFirstRun = false;
			SettingsManager.Save(); // Save changes

			if (currentTheme != Global.Settings.Theme)
			{
				MessageBox.Show(Properties.Resources.ChangesMadeRestartNeeded, Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Information); // Show info
			}

			FirstRunWindow.Close(); // Close
			Global.HomeWindow.Show(); // Show 
		}

		Border CheckedBorder { get; set; }

		private void Border_MouseEnter(object sender, MouseEventArgs e)
		{
			Border border = (Border)sender;
			border.BorderBrush = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["AccentColor"].ToString()) }; // Set color
		}

		private void Border_MouseLeave(object sender, MouseEventArgs e)
		{
			Border border = (Border)sender;
			if (border != CheckedBorder)
			{
				border.BorderBrush = new SolidColorBrush() { Color = Colors.Transparent }; // Set color 
			}
		}

		private void LightBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			LightRadioBtn.IsChecked = true; // Set IsChecked
			CheckedBorder = LightBorder; // Set
			RefreshBorders();
		}

		private void DarkBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			DarkRadioBtn.IsChecked = true; // Set IsChecked
			CheckedBorder = DarkBorder; // Set
			RefreshBorders();
		}

		private void SystemBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			SystemRadioBtn.IsChecked = true; // Set IsChecked
			CheckedBorder = SystemBorder; // Set
			RefreshBorders();
		}

		private void RefreshBorders()
		{
			LightBorder.BorderBrush = new SolidColorBrush() { Color = Colors.Transparent }; // Set color 
			DarkBorder.BorderBrush = new SolidColorBrush() { Color = Colors.Transparent }; // Set color 
			SystemBorder.BorderBrush = new SolidColorBrush() { Color = Colors.Transparent }; // Set color 

			CheckedBorder.BorderBrush = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["AccentColor"].ToString()) }; // Set color
		}
	}
}
