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
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Datalya.UserControls
{
	/// <summary>
	/// Interaction logic for MultichoicesBlockUI.xaml
	/// </summary>
	public partial class MultichoicesBlockUI : UserControl
	{
		public MultichoicesBlock MultichoicesBlock { get; set; }
		string ContentTxt { get; set; }
		public MultichoicesBlockUI(MultichoicesBlock multichoicesBlock, string content = "")
		{
			InitializeComponent();
			MultichoicesBlock = multichoicesBlock; // Set
			ContentTxt = content; // Set

			InitUI();
		}

		private void InitUI()
		{
			NameTxt.Text = MultichoicesBlock.Name; // Set text

			string[] selectedChoices = ContentTxt.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries); // Split

			// Add Checkboxes
			for (int i = 0; i < MultichoicesBlock.Choices.Count; i++) // For each choice
			{
				CheckBoxesDisplayer.Children.Add(new CheckBox()
				{
					Style = FindResource("CheckBoxStyle1") as Style, // Set style
					BorderThickness = new(2), // Set border thickness
					Content = MultichoicesBlock.Choices[i], // Set content
					FontWeight = FontWeights.Bold, // Set font to bold
					VerticalContentAlignment = VerticalAlignment.Center, // Set vertical alignment
					IsChecked = selectedChoices.Contains(MultichoicesBlock.Choices[i]), // Set IsChecked
					Foreground = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }, // Set foreground
					Background = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) } // Set background
				});
			}
		}
	}
}
