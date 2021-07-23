﻿/*
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
using LeoCorpLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Shapes;

namespace Datalya.Windows
{
	/// <summary>
	/// Interaction logic for SettingsWindow.xaml
	/// </summary>
	public partial class SettingsWindow : Window
	{
		public SettingsWindow()
		{
			InitializeComponent();
			InitUI(); // Load the UI
		}

		private void InitUI()
		{
			// Checkboxes
			CheckUpdateOnStartChk.IsChecked = Global.Settings.CheckUpdatesOnStart; // Set
			NotifyUpdateChk.IsChecked = Global.Settings.NotifyUpdates; // Set

			// Load RadioButtons
			DarkRadioBtn.IsChecked = Global.Settings.Theme == Theme.Dark; // Change IsChecked property
			LightRadioBtn.IsChecked = Global.Settings.Theme == Theme.Light; // Change IsChecked property
			SystemRadioBtn.IsChecked = Global.Settings.Theme == Theme.System; // Change IsChecked property

			// Borders
			if (DarkRadioBtn.IsChecked.Value)
			{
				CheckedBorder = DarkBorder; // Set
			}
			else if (LightRadioBtn.IsChecked.Value)
			{
				CheckedBorder = LightBorder; // Set
			}
			else if (SystemRadioBtn.IsChecked.Value)
			{
				CheckedBorder = SystemBorder; // Set
			}
			RefreshBorders();

			// LangComboBox
			LangComboBox.Items.Add(Properties.Resources.Default); // Add
			for (int i = 0; i < Global.LanguageList.Count; i++) // For each language
			{
				LangComboBox.Items.Add(Global.LanguageList[i]); // Add language
			}

			LangComboBox.SelectedIndex = (Global.Settings.Language == "_default") ? 0 : Global.LanguageCodeList.IndexOf(Global.Settings.Language) + 1;

			// Apply buttons
			LangApplyBtn.Visibility = Visibility.Hidden; // Hide
			ThemeApplyBtn.Visibility = Visibility.Hidden; // Hide
		}

		private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized; // Minimize
		}

		private void CloseBtn_Click(object sender, RoutedEventArgs e)
		{
			Close(); // Close the window
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

		private void LightRadioBtn_Checked(object sender, RoutedEventArgs e)
		{
			ThemeApplyBtn.Visibility = Visibility.Visible; // Show the ThemeApplyBtn button
		}

		private void DarkRadioBtn_Checked(object sender, RoutedEventArgs e)
		{
			ThemeApplyBtn.Visibility = Visibility.Visible; // Show the ThemeApplyBtn button
		}

		private void SystemRadioBtn_Checked(object sender, RoutedEventArgs e)
		{
			ThemeApplyBtn.Visibility = Visibility.Visible; // Show the ThemeApplyBtn button
		}

		private void ThemeApplyBtn_Click(object sender, RoutedEventArgs e)
		{
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

			SettingsManager.Save(); // Save changes
			DisplayRestartMessage(); // Inform user
		}

		/// <summary>
		/// Restarts Datalya.
		/// </summary>
		private void DisplayRestartMessage()
		{
			if (MessageBox.Show(Properties.Resources.NeedRestartToApplyChanges, Properties.Resources.Datalya, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				Process.Start(Directory.GetCurrentDirectory() + @"\Datalya.exe"); // Start
				Environment.Exit(0); // Close
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

		private void ResetSettingsLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (MessageBox.Show(Properties.Resources.ResetSettingsConfirmMsg, Properties.Resources.Settings, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				Global.Settings = new()
				{
					Theme = Theme.System,
					CheckUpdatesOnStart = true,
					Language = "_default",
					NotifyUpdates = true,
					RecentFiles = new()
				}; // Create default settings

				SettingsManager.Save(); // Save the changes
				InitUI(); // Reload the page

				MessageBox.Show(Properties.Resources.SettingsReset, Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Information);
				Process.Start(Directory.GetCurrentDirectory() + @"\Datalya.exe");
				Environment.Exit(0); // Quit
			}
		}

		private void LangComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			LangApplyBtn.Visibility = Visibility.Visible; // Visible
		}

		private void LangApplyBtn_Click(object sender, RoutedEventArgs e)
		{
			Global.Settings.Language = LangComboBox.Text switch
			{
				"English (United States)" => Global.LanguageCodeList[0], // Set the settings value
				"Français (France)" => Global.LanguageCodeList[1], // Set the settings value
				_ => "_default" // Set the settings value
			};
			SettingsManager.Save(); // Save the changes
			LangApplyBtn.Visibility = Visibility.Hidden; // Hide
			DisplayRestartMessage();
		}

		private void CheckUpdateOnStartChk_Checked(object sender, RoutedEventArgs e)
		{
			Global.Settings.CheckUpdatesOnStart = CheckUpdateOnStartChk.IsChecked.Value; // Set
			SettingsManager.Save(); // Save changes
		}

		private void NotifyUpdateChk_Checked(object sender, RoutedEventArgs e)
		{
			Global.Settings.NotifyUpdates = NotifyUpdateChk.IsChecked.Value; // Set
			SettingsManager.Save(); // Save changes
		}
	}
}