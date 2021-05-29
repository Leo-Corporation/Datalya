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

namespace Datalya.UserControls
{
	/// <summary>
	/// Interaction logic for MultichoicesBlockPropertiesUI.xaml
	/// </summary>
	public partial class MultichoicesBlockPropertiesUI : UserControl
	{
		public MultichoicesBlockPropertiesUI()
		{
			InitializeComponent();
			AddCheckBoxItem(); // Add item
		}

		private void SaveBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void AddBtn_Click(object sender, RoutedEventArgs e)
		{
			AddCheckBoxItem(); // Add item
		}

		internal void AddCheckBoxItem()
		{
			try
			{
				TextBox textBox = new()
				{
					Style = FindResource("RegularTextBoxStyle") as Style, // Define style
					Padding = new(7), // Set padding
					Margin = new(10, 5, 10, 5), // Set marginn
					BorderThickness = new(0), // Remove border
					Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }, // Set the foreground
					Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background2"].ToString()) }, // Set the background
					FontWeight = FontWeights.Bold, // Set font to bold
					TextAlignment = TextAlignment.Left, // Left text alignment
					VerticalAlignment = VerticalAlignment.Center // Set vertical alignment
				}; // Create textbox

				Button button = new()
				{
					Style = FindResource("RegularButtonStyle") as Style, // Define style
					Padding = new(10, 5, 10, 5), // Set padding
					Margin = new(0, 5, 10, 5), // Set margin
					BorderThickness = new(0), // Remove border
					Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background2"].ToString()) }, // Set the background
					HorizontalAlignment = HorizontalAlignment.Center, // Set horizontal alignment
					VerticalAlignment = VerticalAlignment.Center, // Set vertical alignment
					Cursor = Cursors.Hand, // Set cursor
					Content = "\uF34D", // Set text
					FontFamily = new(new Uri("pack://application:,,,/"), "./Fonts/#FluentSystemIcons-Regular"),
					FontSize = 16, // Set font size
				}; // Create Delete button

				Grid grid = new();
				grid.ColumnDefinitions.Add(new()); // Create col definitions
				grid.ColumnDefinitions.Add(new() { Width = GridLength.Auto }); // Create col definitions

				grid.Children.Add(textBox); // Add control
				grid.Children.Add(button); // Add control

				textBox.SetValue(Grid.ColumnProperty, 0); // Set col
				button.SetValue(Grid.ColumnProperty, 1); // Set col

				button.Click += (o, e) => TextBoxesDisplayer.Children.Remove(grid); // Remove

				TextBoxesDisplayer.Children.Add(grid); // Add
			}
			catch { }
		}
	}
}
