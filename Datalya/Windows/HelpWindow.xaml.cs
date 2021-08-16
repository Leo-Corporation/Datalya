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
using Datalya.Pages.Help;
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
	/// Interaction logic for HelpWindow.xaml
	/// </summary>
	public partial class HelpWindow : Window
	{
		AddItemHelpPage AddItemHelpPage => new(); // Create page
		EditItemHelpPage EditItemHelpPage => new(); // Create page
		DeleteItemHelpPage DeleteItemHelpPage => new(); // Create page
		CreateDataBaseHelpPage CreateDataBaseHelpPage => new(); // Create page
		SaveDataBaseHelpPage SaveDataBaseHelpPage => new(); // Create page
		GetStartedBlocksHelpPage GetStartedBlocksHelpPage => new(); // Create page

		public HelpWindow()
		{
			InitializeComponent();
			InitUI(); // Load the UI
		}

		private void InitUI()
		{
			// Register events
			StateChanged += (o, e) => RefreshState();
			Loaded += (o, e) => RefreshState();
			LocationChanged += (o, e) => RefreshState();

			// Set home page
			NavigateToPage(0); // Navigate
			AddItemBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["AccentColor"].ToString()) }; // Set the background
			AddItemBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the background

			CheckedButton = AddItemBtn; // Check
		}
		private Button CheckedButton { get; set; }

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
				button.Background.BeginAnimation(SolidColorBrush.ColorProperty, Global.OnLeaveTabColorAnimation); ; // Play animation
			}
		}

		private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized; // Minimize
		}

		private void CloseBtn_Click(object sender, RoutedEventArgs e)
		{
			Close(); // Close window
		}

		private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized; // Set
			RefreshState();
		}

		private void RefreshState()
		{
			MaximizeBtn.Content = WindowState == WindowState.Maximized ? "\uFBA6" : "\uFA40"; // Set
			MaximizeToolTip.Content = WindowState == WindowState.Maximized ? Properties.Resources.Restore : Properties.Resources.Maximize; // Set
			DefineMaximumSize(); // Avoid taskbar overflow

			WindowBorder.Margin = WindowState == WindowState.Maximized ? new(10, 10, 0, 0) : new(10); // Set
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

		private void NavigateToPage(int id)
		{
			ContentDisplayer.Navigate(id switch
			{ 
				0 => AddItemHelpPage,
				1 => EditItemHelpPage,
				2 => DeleteItemHelpPage,
				3 => CreateDataBaseHelpPage,
				4 => SaveDataBaseHelpPage,
				5 => GetStartedBlocksHelpPage,
				_ => AddItemHelpPage
			}); // Navigate

			UncheckAll();
		}

		private void UncheckAll()
		{
			AddItemBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background
			AddItemBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground

			EditItemBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background
			EditItemBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground

			DeleteItemBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background
			DeleteItemBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground

			CreateItemBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background
			CreateItemBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground

			SaveItemBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background
			SaveItemBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground

			StartBlocksBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background
			StartBlocksBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
		}

		private void AddItemBtn_Click(object sender, RoutedEventArgs e)
		{
			NavigateToPage(0); // Navigate
			AddItemBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["AccentColor"].ToString()) }; // Set the background
			AddItemBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the background

			CheckedButton = AddItemBtn; // Check
		}

		private void EditItemBtn_Click(object sender, RoutedEventArgs e)
		{
			NavigateToPage(1); // Navigate
			EditItemBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["AccentColor"].ToString()) }; // Set the background
			EditItemBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the background

			CheckedButton = EditItemBtn; // Check
		}

		private void DeleteItemBtn_Click(object sender, RoutedEventArgs e)
		{
			NavigateToPage(2); // Navigate
			DeleteItemBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["AccentColor"].ToString()) }; // Set the background
			DeleteItemBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the background

			CheckedButton = DeleteItemBtn; // Check
		}

		private void CreateItemBtn_Click(object sender, RoutedEventArgs e)
		{
			NavigateToPage(3); // Navigate
			CreateItemBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["AccentColor"].ToString()) }; // Set the background
			CreateItemBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the background

			CheckedButton = CreateItemBtn; // Check
		}

		private void SaveItemBtn_Click(object sender, RoutedEventArgs e)
		{
			NavigateToPage(4); // Navigate
			SaveItemBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["AccentColor"].ToString()) }; // Set the background
			SaveItemBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the background

			CheckedButton = SaveItemBtn; // Check
		}

		private void StartBlocksBtn_Click(object sender, RoutedEventArgs e)
		{
			NavigateToPage(5); // Navigate
			StartBlocksBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["AccentColor"].ToString()) }; // Set the background
			StartBlocksBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the background

			CheckedButton = StartBlocksBtn; // Check
		}
	}
}
