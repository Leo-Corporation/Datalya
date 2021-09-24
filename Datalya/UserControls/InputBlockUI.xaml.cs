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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Datalya.UserControls
{
	/// <summary>
	/// Interaction logic for InputBlockUI.xaml
	/// </summary>
	public partial class InputBlockUI : UserControl
	{
		public InputBlock InputBlock { get; set; }
		string ContentTxt { get; set; }
		bool isPlaceholderShown;
		public InputBlockUI(InputBlock inputBlock, string content = "")
		{
			InitializeComponent();
			InputBlock = inputBlock;
			ContentTxt = content; // Set

			InitUI();
		}

		private void InitUI()
		{
			NameTxt.Text = InputBlock.Name; // Set name
			if (ContentTxt != null && ContentTxt != string.Empty) // Is from edit
			{
				ValueTxt.Text = ContentTxt; // Set value 
				isPlaceholderShown = false; // Set to false
				ValueTxt.Foreground = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set foreground
			}
			else
			{
				ValueTxt.Text = InputBlock.PlaceHolder; // Set text
				isPlaceholderShown = true; // Set to true
				ValueTxt.Foreground = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["DarkGray"].ToString()) }; // Set foreground
			}
		}

		private void ValueTxt_GotFocus(object sender, RoutedEventArgs e)
		{
			if (isPlaceholderShown)
			{
				ValueTxt.Text = ""; // Clear
				isPlaceholderShown = false; // Set to false
				ValueTxt.Foreground = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set foreground
			}
		}

		private void ValueTxt_LostFocus(object sender, RoutedEventArgs e)
		{
			if (ValueTxt.Text == string.Empty)
			{
				ValueTxt.Text = InputBlock.PlaceHolder; // Set text
				isPlaceholderShown = true; // Set to true
				ValueTxt.Foreground = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["DarkGray"].ToString()) }; // Set foreground
			}
		}
	}
}
