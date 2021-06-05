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

namespace Datalya
{
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

			// UI
			WindowContent.Content = Global.DatabasePage; // Set content
			CheckButton(DatabaseBtn); // Check
		}

		private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized; // Minimize
		}

		private void CloseBtn_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0); // Quit
		}

		private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized; // Set
			RefreshState();
		}

		private void RefreshState()
		{
			MaximizeBtn.Content = WindowState == WindowState.Maximized ? "\uFBA6" : "\uFA40"; // Set
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

		private void CheckButton(Button button)
		{
			button.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the foreground
			button.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["AccentColor"].ToString()) }; // Set the background

			CheckedButton = button; // Set the "checked" button
		}

		private void ResetAllCheckStatus()
		{
			DatabaseBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
			DatabaseBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background

			CreatorBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
			CreatorBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background
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

		}
	}
}
