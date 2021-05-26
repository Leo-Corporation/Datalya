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
using Datalya.Interfaces;
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

namespace Datalya.Pages
{
	/// <summary>
	/// Interaction logic for DatabasePage.xaml
	/// </summary>
	public partial class DatabasePage : Page
	{
		private Button CheckedButton { get; set; }
		public DatabasePage()
		{
			InitializeComponent();
			InitUI(); // Init the UI
		}

		private void InitUI()
		{
			CheckButton(FileRibBtn); // Check
			FileTab.Visibility = Visibility.Visible; // Show
		}

		private void FileRibBtn_Click(object sender, RoutedEventArgs e)
		{
			ResetAllCheckStatus(); // Reset
			CheckButton(FileRibBtn); // Check

			HideAllTabs(); // Hide all ribbon tabs
			FileTab.Visibility = Visibility.Visible; // Show
		}

		private void EditRibBtn_Click(object sender, RoutedEventArgs e)
		{
			ResetAllCheckStatus(); // Reset
			CheckButton(EditRibBtn); // Check

			HideAllTabs(); // Hide all ribbon tabs
			EditTab.Visibility = Visibility.Visible; // Show
		}

		private void ExportRibBtn_Click(object sender, RoutedEventArgs e)
		{
			ResetAllCheckStatus(); // Reset
			CheckButton(ExportRibBtn); // Check

			HideAllTabs(); // Hide all ribbon tabs
		}

		private void HelpRibBtn_Click(object sender, RoutedEventArgs e)
		{
			ResetAllCheckStatus(); // Reset
			CheckButton(HelpRibBtn); // Check

			HideAllTabs(); // Hide all ribbon tabs
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
			FileRibBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
			FileRibBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background

			EditRibBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
			EditRibBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the foreground
						
			ExportRibBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the background
			ExportRibBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background
			
			HelpRibBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the background
			HelpRibBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) }; // Set the background
		}

		internal void HideAllTabs()
		{
			FileTab.Visibility = Visibility.Collapsed; // Hide
			EditTab.Visibility = Visibility.Collapsed; // Hide
		}

		private void SaveBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void SaveAsBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void NewDatabaseBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void OpenDbBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void InfoBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void CloseDbBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void AddItemBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void DeleteBtn_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
