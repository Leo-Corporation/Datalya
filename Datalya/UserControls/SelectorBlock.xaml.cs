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
using System.Windows;
using System.Windows.Controls;

namespace Datalya.UserControls;

/// <summary>
/// Interaction logic for SelectorBlock.xaml
/// </summary>
public partial class SelectorBlock : UserControl
{
	public Classes.SelectorBlock CSelectorBlock { get; set; }
	string ContentTxt { get; set; }
	public SelectorBlock(Classes.SelectorBlock selectorBlock, string content = "")
	{
		InitializeComponent();
		CSelectorBlock = selectorBlock; // Set value
		ContentTxt = content; // Set value

		InitUI();
	}

	private void InitUI()
	{
		NameTxt.Text = CSelectorBlock.Name; // Set text

		// Load combobox
		for (int i = 0; i < CSelectorBlock.Choices.Count; i++)
		{
			ItemComboBox.Items.Add(CSelectorBlock.Choices[i]); // Add
		}

		ItemComboBox.SelectedItem = ContentTxt; // Set
	}

	private void ItemComboBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		ItemComboBox.IsDropDownOpen = true;
	}

	private void ItemComboBox_GotFocus(object sender, RoutedEventArgs e)
	{
		ItemComboBox.IsDropDownOpen = true;
	}
}
