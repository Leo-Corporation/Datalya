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
using LeoCorpLibrary;
using System.Windows;
using System.Windows.Controls;

namespace Datalya.UserControls;
/// <summary>
/// Interaction logic for DateBlockPropertiesUI.xaml
/// </summary>
public partial class DateBlockPropertiesUI : UserControl
{
	private DateBlockCreatorUI ParentElement { get; init; }
	internal DateBlock DateBlock { get; set; }
	
	public DateBlockPropertiesUI(DateBlockCreatorUI dateBlockCreatorUI, DateBlock dateBlock)
	{
		InitializeComponent();
		DateBlock = dateBlock; // Set
		ParentElement = dateBlockCreatorUI; // Set

		if (dateBlock is not null)
		{
			NameTxt.Text = dateBlock.Name; // Set text
			UseDefaultDateChk.IsChecked = dateBlock.UseDefaultDate; // Set the check state
			DefaultDatePicker.DisplayDate = Env.UnixTimeToDateTime(dateBlock.DefaultDate ?? Env.UnixTime); // Set the date
		}
	}

	private void SaveBtn_Click(object sender, RoutedEventArgs e)
	{
		if (!string.IsNullOrEmpty(NameTxt.Text) && !string.IsNullOrWhiteSpace(NameTxt.Text))
		{
			DateBlock = new() { Name = NameTxt.Text, UseDefaultDate = UseDefaultDateChk.IsChecked.Value, DefaultDate = DefaultDatePicker.DisplayDate.Second };
			ParentElement.NameTxt.Text = DateBlock.Name; // Set name 
			ParentElement.Date = InputBlock; // Set
			Global.CreatorPage.SaveChanges(); // Save
		}
	}
}
