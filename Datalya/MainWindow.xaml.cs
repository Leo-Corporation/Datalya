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
using Datalya.Windows;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Datalya;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private Button CheckedButton { get; set; }
	public MainWindow()
	{
		InitializeComponent();
		InitUI();
	}

	private void InitUI()
	{
		// Register events
		StateChanged += (o, e) => RefreshState();
		Loaded += (o, e) => RefreshState();
		LocationChanged += (o, e) => RefreshState();

		// UI
		WindowContent.Content = Global.DatabasePage; // Set content
		CheckButton(DatabaseBtn); // Check
		DataBaseNameTxt.Text = Global.CurrentDataBase.DataBaseInfo.Name; // Set text
		EditNameTextBox.Text = Global.CurrentDataBase.DataBaseInfo.Name; // Set text
	}

	private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
	{
		WindowState = WindowState.Minimized; // Minimize
	}

	private void CloseBtn_Click(object sender, RoutedEventArgs e)
	{
		if (Global.IsModified && Global.CurrentDataBase.ItemsContent.Count > 0)
		{
			var dialog = MessageBox.Show(Properties.Resources.CloseDBConfirmMsg, Properties.Resources.Datalya, MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
			if (dialog == MessageBoxResult.Yes)
			{
				if (!string.IsNullOrEmpty(Global.DataBaseFilePath))
				{
					DataBaseManager.Save(Global.CurrentDataBase, Global.DataBaseFilePath); // Save
				}
				else
				{
					SaveFileDialog saveFileDialog = new()
					{
						FileName = $"{Global.CurrentDataBase.DataBaseInfo.Name}.datalyadb", // Set file name
						Filter = $"{Properties.Resources.DatalyaFile}|*.datalyadb", // Set filter
						Title = Properties.Resources.SaveAs // Set title
					};

					if (saveFileDialog.ShowDialog() ?? true)
					{
						DataBaseManager.Save(Global.CurrentDataBase, saveFileDialog.FileName); // Save DataBase
					}
				}
			}
			else if (dialog == MessageBoxResult.No)
			{
				// Don't save
			}
			else if (dialog == MessageBoxResult.Cancel)
			{
				return;
			}
		}

		Global.Settings.IsMaximized = WindowState == WindowState.Maximized; // Set
		SettingsManager.Save(); // Save

		Environment.Exit(0); // Quit 
	}

	private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
	{
		WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized; // Set
		RefreshState();
	}

	internal void RefreshName()
	{
		EditNameTextBox.Text = Global.CurrentDataBase.DataBaseInfo.Name; // Set text
		DataBaseNameTxt.Text = Global.CurrentDataBase.DataBaseInfo.Name; // Set text
	}

	private void RefreshState()
	{
		MaximizeBtn.Content = WindowState == WindowState.Maximized ? "\uFBA6" : "\uFA40"; // Set
		MaximizeToolTip.Content = WindowState == WindowState.Maximized ? Properties.Resources.Restore : Properties.Resources.Maximize; // Set
		DefineMaximumSize(); // Avoid taskbar overflow

		WindowBorder.Margin = WindowState == WindowState.Maximized ? new(10, 10, 0, 0) : new(10); // Set
	}

	private void TabEnter(object sender, MouseEventArgs e)
	{
		Button button = (Button)sender; // Create button

		button.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the foreground
	}

	private void TabLeave(object sender, MouseEventArgs e)
	{
		Button button = (Button)sender; // Create button

		if (button != CheckedButton)
		{
			button.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
		}
	}

	internal void CheckButton(Button button)
	{
		button.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the foreground
		button.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["AccentColor"].ToString()) }; // Set the background

		CheckedButton = button; // Set the "checked" button
	}

	internal void ResetAllCheckStatus()
	{
		DatabaseBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
		DatabaseBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background

		CreatorBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
		CreatorBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background

		SettingsBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
		SettingsBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background
	}

	private void DatabaseBtn_Click(object sender, RoutedEventArgs e)
	{
		ResetAllCheckStatus(); // Reset
		CheckButton(DatabaseBtn); // Check

		Global.DatabasePage.InitDataBaseUI(); // Refresh datagrid
		WindowContent.Navigate(Global.DatabasePage); // Navigate
	}

	private void CreatorBtn_Click(object sender, RoutedEventArgs e)
	{
		ResetAllCheckStatus(); // Reset
		CheckButton(CreatorBtn); // Check
		WindowContent.Navigate(Global.CreatorPage); // Navigate
	}

	private void EditFileNameBtn_Click(object sender, RoutedEventArgs e)
	{
		if (DataBaseNameTxt.Visibility == Visibility.Collapsed) // If edit mode enabled
		{
			Global.CurrentDataBase.DataBaseInfo.Name = EditNameTextBox.Text; // Set text
			DataBaseNameTxt.Text = EditNameTextBox.Text; // Set text
		}

		DataBaseNameTxt.Visibility = (DataBaseNameTxt.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible; // Hide or show
		EditNameTextBox.Visibility = (EditNameTextBox.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible; // Hide or show
		EditFileNameBtn.Content = (DataBaseNameTxt.Visibility == Visibility.Visible) ? "\uF3DE" : "\uF295"; // Set text
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

		MaxHeight = currentScreen.WorkingArea.Height / factor + 5; // Set max size
		MaxWidth = currentScreen.WorkingArea.Width / factor + 5; // Set max size
	}

	private void SettingsBtn_Click(object sender, RoutedEventArgs e)
	{
		ResetAllCheckStatus(); // Reset
		CheckButton(SettingsBtn); // Check
		WindowContent.Navigate(Global.SettingsPage); // Navigate
	}
}
