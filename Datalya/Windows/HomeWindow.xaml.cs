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
using Datalya.UserControls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Datalya.Windows;

/// <summary>
/// Interaction logic for HomeWindow.xaml
/// </summary>
public partial class HomeWindow : Window
{
	int PageId { get; set; } // 0 = Recent, 1 = Pined
	public HomeWindow()
	{
		InitializeComponent();
		PageId = 0; // Set default value

		InitUI(); // Load the UI
	}

	internal void InitUI()
	{
		// Reset & Clear
		UnCheckAllTabs();
		HideAllTabs();

		// Register events
		StateChanged += (o, e) => RefreshState();
		Loaded += (o, e) => RefreshState();
		LocationChanged += (o, e) => RefreshState();
		SizeChanged += (o, e) =>
		{
			Global.Settings.MainWindowSize = (ActualWidth, ActualHeight);
		};

		// Check & Display default tab
		if (PageId == 0)
		{
			CheckButton(RecentTabBtn); // Check
			RecentItemDisplayer.Visibility = Visibility.Visible; // Show 
			RecentScroll.Visibility = Visibility.Visible; // Show 
		}
		else
		{
			CheckButton(PinedTabBtn); // Check
			PinedItemDisplayer.Visibility = Visibility.Visible; // Show 
			PinnedScroll.Visibility = Visibility.Visible; // Show 
		}

		// UI
		WelcomeMessageTxt.Text = $"{Properties.Resources.WelcomeBack} {Environment.UserName}"; // Set text

		// Recent tab
		RecentItemDisplayer.Children.Clear(); // Clear
		PinedItemDisplayer.Children.Clear(); // Clear

		if (Global.Settings.RecentFiles is not null)
		{
			Dictionary<DataBaseInfo, int> keyValuePairs = new(); // Create a dictionnary
			for (int i = 0; i < Global.Settings.RecentFiles.Count; i++)
			{
				keyValuePairs.Add(Global.Settings.RecentFiles[i], Global.Settings.RecentFiles[i].LastEditTime); // Add to dictionnary
			}

			var items = from pair in keyValuePairs orderby pair.Value descending select pair; // Sort

			foreach (KeyValuePair<DataBaseInfo, int> pair1 in items)
			{
				RecentItemDisplayer.Children.Add(new HomeDataBaseItem(pair1.Key)); // Add item
			}
		}

		// Pined tab
		if (Global.Settings.RecentFiles is not null)
		{
			for (int i = 0; i < Global.Settings.RecentFiles.Count; i++)
			{
				if (Global.Settings.RecentFiles[i].IsPinned) // If is pinned
				{
					PinedItemDisplayer.Children.Add(new HomeDataBaseItem(Global.Settings.RecentFiles[i])); // Add item
				}
			}
		}

		// Add placeholders if there is no item(s)
		if (RecentItemDisplayer.Children.Count == 0)
		{
			RecentItemDisplayer.Children.Add(new PlaceholderItem(Properties.Resources.NothingToShow, Properties.Resources.NoOpenedItems, "\uF47F")); // Add
		}

		if (PinedItemDisplayer.Children.Count == 0)
		{
			PinedItemDisplayer.Children.Add(new PlaceholderItem(Properties.Resources.NothingToShow, Properties.Resources.NoItemsPinned, "\uF602")); // Add
		}

		// Maximize/Restore
		WindowState = Global.Settings.IsMaximized ? WindowState.Maximized : WindowState.Normal; // Set state

		// Settings
		SettingsFrame.Navigate(Global.HomeSettingsPage);

		// Search box
		isPlaceholderShown = true; // Set to true
		SearchTxt.Text = Properties.Resources.Search; // Set text
		SearchTxt.Foreground = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["DarkGray"].ToString()) }; // Set foreground

		// Restore the previous size
		if (!Global.Settings.IsMaximized)
		{
			Width = Global.Settings.MainWindowSize?.Item1 ?? 1100;
			Height = Global.Settings.MainWindowSize?.Item2 ?? 600;
		}
	}

	private void HideAllTabs()
	{
		RecentItemDisplayer.Visibility = Visibility.Collapsed; // Hide
		PinedItemDisplayer.Visibility = Visibility.Collapsed; // Hide

		RecentScroll.Visibility = Visibility.Collapsed; // Hide
		PinnedScroll.Visibility = Visibility.Collapsed; // Hide
	}

	Button CheckedButton { get; set; }
	private void CheckButton(Button button)
	{
		button.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the foreground
		button.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["AccentColor"].ToString()) }; // Set the background

		CheckedButton = button; // Set the "checked" button
	}

	private void UnCheckAllTabs()
	{
		RecentTabBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
		RecentTabBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background

		PinedTabBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
		PinedTabBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background
	}

	private void TabEnter(object sender, MouseEventArgs e)
	{
		Button button = (Button)sender; // Create button

		button.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the foreground
		button.Background.BeginAnimation(SolidColorBrush.ColorProperty, Global.OnHoverTabColorAnimation); // Play animation

	}

	private void TabLeave(object sender, MouseEventArgs e)
	{
		Button button = (Button)sender; // Create button

		if (button != CheckedButton)
		{
			button.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground 
			button.Background.BeginAnimation(SolidColorBrush.ColorProperty, Global.OnLeaveTabColorAnimation); // Play animation

		}
	}

	private void DefineMaximumSize()
	{
		System.Windows.Forms.Screen currentScreen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle); // The current screen

		float dpiX, dpiY;
		double scaling = 100; // Default scaling = 100%

		using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHwnd(IntPtr.Zero))
		{
			dpiX = graphics.DpiX; // Get the DPI
			dpiY = graphics.DpiY; // Get the DPI

			scaling = dpiX switch
			{
				96 => 100, // Get the %
				120 => 125, // Get the %
				144 => 150, // Get the %
				168 => 175, // Get the %
				192 => 200, // Get the % 
				_ => 100
			};
		}

		double factor = scaling / 100d; // Calculate factor

		MaxHeight = currentScreen.WorkingArea.Height / factor + 7; // Set max size
		MaxWidth = currentScreen.WorkingArea.Width / factor + 7; // Set max size
	}

	private void RefreshState()
	{
		MaximizeBtn.Content = WindowState == WindowState.Maximized ? "\uFBA6" : "\uFA40"; // Set
		MaximizeToolTip.Content = WindowState == WindowState.Maximized ? Properties.Resources.Restore : Properties.Resources.Maximize; // Set
		DefineMaximumSize(); // Avoid taskbar overflow

		WindowBorder.CornerRadius = WindowState == WindowState.Maximized ? new(0) : new(5);
		WindowBorder.Margin = WindowState == WindowState.Maximized ? new(5, 5, 0, 0) : new(10); // Set
	}

	private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
	{
		WindowState = WindowState.Minimized; // Minimize window
	}

	private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
	{
		WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized; // Set
		RefreshState();
	}

	private void CloseBtn_Click(object sender, RoutedEventArgs e)
	{
		Global.Settings.IsMaximized = WindowState == WindowState.Maximized; // Set
		SettingsManager.Save(); // Save

		Environment.Exit(0); // Exit Datalya
	}

	internal void NewDbBtn_Click(object sender, RoutedEventArgs e)
	{
		new NewDataBaseWindow(true, sender is App).Show(); // Show
	}

	private void OpenDbBtn_Click(object sender, RoutedEventArgs e)
	{
		OpenFileDialog openFileDialog = new()
		{
			Filter = $"{Properties.Resources.DatalyaFile}|*.datalyadb|{Properties.Resources.AllFiles}|*.*", // Set filter
			Title = Properties.Resources.Open // Set title
		};

		if (openFileDialog.ShowDialog() ?? true)
		{
			DataBaseManager.Open(openFileDialog.FileName); // Open DataBase

			Global.MainWindow.WindowState = WindowState; // Set
			Global.MainWindow.Height = Height; // Set size
			Global.MainWindow.Width = Width; // Set size
			Global.MainWindow.Left = Left; // Set position
			Global.MainWindow.Top = Top; // Set position
			Global.MainWindow.Show();
			Hide(); // Close the window
		}
	}

	private void RecentTabBtn_Click(object sender, RoutedEventArgs e)
	{
		PageId = 0;
		UnCheckAllTabs(); // Clear
		CheckButton(RecentTabBtn); // Check

		HideAllTabs();
		RecentItemDisplayer.Visibility = Visibility.Visible; // Show
		RecentScroll.Visibility = Visibility.Visible; // Show
	}

	private void PinedTabBtn_Click(object sender, RoutedEventArgs e)
	{
		PageId = 1;
		UnCheckAllTabs(); // Clear
		CheckButton(PinedTabBtn); // Check

		HideAllTabs();
		PinedItemDisplayer.Visibility = Visibility.Visible; // Show
		PinnedScroll.Visibility = Visibility.Visible; // Show
	}

	bool toggled = false;
	private void SettingsBtn_Click(object sender, RoutedEventArgs e)
	{
		if (!toggled)
		{
			CheckButton(SettingsBtn); // Check 
			Global.HomeSettingsPage.InitUI();
			SettingsFrame.Visibility = Visibility.Visible;
			HomeGrid.Visibility = Visibility.Collapsed;
		}
		else
		{
			SettingsBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
			SettingsBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background

			SettingsFrame.Visibility = Visibility.Collapsed;
			HomeGrid.Visibility = Visibility.Visible;
		}
		toggled = !toggled;
	}

	bool isPlaceholderShown;
	private void SearchTxt_LostFocus(object sender, RoutedEventArgs e)
	{
		if (SearchTxt.Text == string.Empty)
		{
			isPlaceholderShown = true; // Set to true
			SearchTxt.Text = Properties.Resources.Search; // Set text
			SearchTxt.Foreground = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["DarkGray"].ToString()) }; // Set foreground
		}
	}

	private void SearchTxt_GotFocus(object sender, RoutedEventArgs e)
	{
		if (isPlaceholderShown)
		{
			isPlaceholderShown = false; // Set to false
			SearchTxt.Text = ""; // Clear
			SearchTxt.Foreground = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set foreground
		}
	}

	private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (isPlaceholderShown) return;

		for (int i = 0; i < RecentItemDisplayer.Children.Count; i++)
		{
			if (RecentItemDisplayer.Children[i] is HomeDataBaseItem dataBaseItem)
			{
				if (string.IsNullOrEmpty(SearchTxt.Text))
				{
					dataBaseItem.Visibility = Visibility.Visible;
					continue;
				}
				dataBaseItem.Visibility = !dataBaseItem.DataBaseInfo.Name.ToLower().Contains(SearchTxt.Text.ToLower()) ?
					Visibility.Collapsed : Visibility.Visible;
			}
		}
	}
}
