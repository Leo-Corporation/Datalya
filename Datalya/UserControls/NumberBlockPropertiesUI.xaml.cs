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

namespace Datalya.UserControls;
/// <summary>
/// Interaction logic for NumberBlockPropertiesUI.xaml
/// </summary>
public partial class NumberBlockPropertiesUI : UserControl
{
	NumberBlockCreatorUI ParentElement { get; set; }
	NumberBlock NumberBlock { get; set; }
	public NumberBlockPropertiesUI(NumberBlockCreatorUI numberBlockCreatorUI, NumberBlock numberBlock)
	{
		InitializeComponent();
		ParentElement = numberBlockCreatorUI;
		NumberBlock = numberBlock;
		if (numberBlock is not null)
		{
			NameTxt.Text = numberBlock.Name;
			UseDropDownChk.IsChecked = numberBlock.UseComboBox;
			UseDropDownChk.IsEnabled = numberBlock.UseRange;
			UseRangeChk.IsChecked = numberBlock.UseRange;
			MinTxt.Text = numberBlock.Range.HasValue ? numberBlock.Range.Value.Item1.ToString() : "";
			MaxTxt.Text = numberBlock.Range.HasValue ? numberBlock.Range.Value.Item2.ToString() : "";
		}
	}

	private void SaveBtn_Click(object sender, RoutedEventArgs e)
	{
		bool minOk = int.TryParse(MinTxt.Text, out int min);
		bool maxOk = int.TryParse(MaxTxt.Text, out int max);

		if ((UseRangeChk.IsChecked ?? false) && (!minOk || !maxOk)) return;
		if (!string.IsNullOrEmpty(NameTxt.Text) && !string.IsNullOrWhiteSpace(NameTxt.Text))
		{
			NumberBlock = new() { Name = NameTxt.Text, UseComboBox = UseDropDownChk.IsChecked ?? false, UseRange = UseRangeChk.IsChecked ?? false, Range = (UseRangeChk.IsChecked ?? false) ? (Math.Min(min, max), Math.Max(min, max)) : null };
			ParentElement.NameTxt.Text = NumberBlock.Name; // Set name 
			ParentElement.NumberBlock = NumberBlock; // Set
			Global.CreatorPage.SaveChanges(); // Save
		}
	}

	private void UseRangeChk_Checked(object sender, RoutedEventArgs e)
	{
		UseDropDownChk.IsEnabled = true;
	}

	private void UseRangeChk_Unchecked(object sender, RoutedEventArgs e)
	{
		UseDropDownChk.IsEnabled = false;
	}
}
