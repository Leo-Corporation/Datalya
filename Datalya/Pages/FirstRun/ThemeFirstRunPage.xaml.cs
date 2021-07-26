using Datalya.Classes;
using Datalya.Enums;
using Datalya.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
