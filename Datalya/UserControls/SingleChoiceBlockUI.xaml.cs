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

namespace Datalya.UserControls;

/// <summary>
/// Interaction logic for SingleChoiceBlockUI.xaml
/// </summary>
public partial class SingleChoiceBlockUI : UserControl
{
	public SingleChoiceBlock SingleChoiceBlock { get; set; }
	string ContentTxt { get; set; }
	public SingleChoiceBlockUI(SingleChoiceBlock singleChoiceBlock, string content = "")
	{
		InitializeComponent();
		SingleChoiceBlock = singleChoiceBlock; // Set value
		ContentTxt = content; // Set value

		InitUI();
	}

	private void InitUI()
	{
		NameTxt.Text = SingleChoiceBlock.Name; // Set text

		for (int i = 0; i < SingleChoiceBlock.Choices.Count; i++)
		{
			RadioButtonsDisplayer.Children.Add(new RadioButton()
			{
				Style = FindResource("RadioButtonStyle1") as Style, // Set style
				Content = SingleChoiceBlock.Choices[i], // Set content
				FontWeight = FontWeights.Bold, // Set font to bold
				VerticalContentAlignment = VerticalAlignment.Center, // Set vertical alignment
				IsChecked = ContentTxt == SingleChoiceBlock.Choices[i], // Set IsChecked
				Foreground = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }, // Set foreground
				Background = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background1"].ToString()) } // Set background
			});
		}
	}
}
