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
using System.Windows.Input;

namespace Datalya.UserControls;
/// <summary>
/// Interaction logic for NumberBlockUI.xaml
/// </summary>
public partial class NumberBlockUI : UserControl
{
	internal NumberBlock NumberBlock { get; set; }
	string Content { get; set; }
	public NumberBlockUI(NumberBlock numberBlock, string content = "")
	{
		InitializeComponent();
		NumberBlock = numberBlock;
		Content = content;
		InitUI();
	}

	private void InitUI()
	{
		NameTxt.Text = NumberBlock.Name;
		if (NumberBlock.UseRange && NumberBlock.UseComboBox)
		{
			ItemComboBox.Visibility = Visibility.Visible;
			ValueTxt.Visibility = Visibility.Collapsed;

			for (int i = NumberBlock.Range.Value.Item1; i <= NumberBlock.Range.Value.Item2; i++)
			{
				ItemComboBox.Items.Add(i);
			}
			ItemComboBox.Text = Content;
			return;
		}
		ValueTxt.Text = Content;
	}

	private void ValueTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
	{
		if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != "-")
		{
			e.Handled = true;
		}
	}
}
