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
using Datalya.UserControls;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace Datalya.Windows
{
	/// <summary>
	/// Interaction logic for HomeWindow.xaml
	/// </summary>
	public partial class HomeWindow : Window
	{
		public HomeWindow()
		{
			InitializeComponent();
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

			// Check & Display default tab
			CheckButton(RecentTabBtn); // Check
			RecentItemDisplayer.Visibility = Visibility.Visible; // Show

			// UI
			WelcomeMessageTxt.Text = $"{Properties.Resources.WelcomeBack} {Environment.UserName}"; // Set text

			// Recent tab
			RecentItemDisplayer.Children.Clear(); // Clear
			if (Global.Settings.RecentFiles is not null)
			{
				for (int i = 0; i < Global.Settings.RecentFiles.Count; i++)
				{
					RecentItemDisplayer.Children.Add(new HomeDataBaseItem(Global.Settings.RecentFiles[i])); // Add item
				} 
			}
		}

		private void HideAllTabs()
		{
			RecentItemDisplayer.Visibility = Visibility.Collapsed; // Hide
			PinedItemDisplayer.Visibility = Visibility.Collapsed; // Hide
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
		}

		private void TabLeave(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender; // Create button

			if (button != CheckedButton)
			{
				button.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground 
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

			MaxHeight = currentScreen.WorkingArea.Height / factor + 5; // Set max size
			MaxWidth = currentScreen.WorkingArea.Width / factor + 5; // Set max size
		}

		private void RefreshState()
		{
			MaximizeBtn.Content = WindowState == WindowState.Maximized ? "\uFBA6" : "\uFA40"; // Set
			DefineMaximumSize(); // Avoid taskbar overflow

			WindowBorder.Margin = WindowState == WindowState.Maximized ? new(10, 10, 0, 0) : new(10); // Set
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
			Environment.Exit(0); // Exit Datalya
		}

		private void NewDbBtn_Click(object sender, RoutedEventArgs e)
		{
			new NewDataBaseWindow(true).Show(); // Show
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
				Global.MainWindow.Show();
				Hide(); // Close the window
			}
		}

		private void RecentTabBtn_Click(object sender, RoutedEventArgs e)
		{
			UnCheckAllTabs(); // Clear
			CheckButton(RecentTabBtn); // Check
		}

		private void PinedTabBtn_Click(object sender, RoutedEventArgs e)
		{
			UnCheckAllTabs(); // Clear
			CheckButton(PinedTabBtn); // Check
		}
	}
}
