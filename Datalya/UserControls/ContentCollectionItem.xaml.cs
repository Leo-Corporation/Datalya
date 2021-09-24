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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Datalya.UserControls
{
	/// <summary>
	/// Interaction logic for ContentCollectionItem.xaml
	/// </summary>
	public partial class ContentCollectionItem : UserControl
	{
		public ContentCollection ContentCollection { get; init; }
		public Action<ContentCollection> CloseEvent { get; init; }

		public ContentCollectionItem(ContentCollection contentCollection, Action<ContentCollection> closeEvent)
		{
			InitializeComponent();
			ContentCollection = contentCollection; // Set
			CloseEvent = closeEvent;

			InitUI(); // Load the UI
		}

		public void InitUI()
		{
			NameTxt.Text = ContentCollection.Name; // Set text
		}

		private void ItemBtn_MouseEnter(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender; // Create button

			button.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the foreground 
		}

		private void ItemBtn_MouseLeave(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender; // Create button

			button.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground 
		}

		private void NameTxt_MouseDown(object sender, MouseButtonEventArgs e)
		{
			CloseEvent(ContentCollection);
		}

		private void ItemBtn_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
