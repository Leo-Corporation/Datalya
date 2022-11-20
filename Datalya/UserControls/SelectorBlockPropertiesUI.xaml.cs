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
using Datalya.Windows;
using PeyrSharp.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Datalya.UserControls;

/// <summary>
/// Interaction logic for SelectorBlockPropertiesUI.xaml
/// </summary>
public partial class SelectorBlockPropertiesUI : UserControl
{
	private SelectorBlockCreatorUI ParentElement { get; init; }
	internal Classes.SelectorBlock CSelectorBlock { get; set; }
	public SelectorBlockPropertiesUI(SelectorBlockCreatorUI selectorBlockCreatorUI, Classes.SelectorBlock s)
	{
		InitializeComponent();
		ParentElement = selectorBlockCreatorUI; // Set

		if (s is not null)
		{
			NameTxt.Text = s.Name; // Set text
			string choices = "";
			for (int i = 0; i < s.Choices.Count; i++)
			{
				choices += s.Choices[i] + "\n"; // Add
			}

			ChoicesTxt.Text = choices; // Set text
		}
	}

	private void SaveBtn_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrEmpty(ChoicesTxt.Text) || string.IsNullOrWhiteSpace(ChoicesTxt.Text))
		{
			MessageBox.Show(Properties.Resources.PleaseAddItemSelectorBlock, $"{Properties.Resources.DatalyaCreator} - {Properties.Resources.Selector}", MessageBoxButton.OK, MessageBoxImage.Error);
			return; // Stop
		}

		if (!string.IsNullOrEmpty(NameTxt.Text) && !string.IsNullOrWhiteSpace(NameTxt.Text))
		{
			// Name
			CSelectorBlock = new() { Name = NameTxt.Text };
			ParentElement.NameTxt.Text = CSelectorBlock.Name; // Set name 

			// Choices
			string[] lines = ChoicesTxt.Text.Split(new string[] { "\n", "\r", "\r\n" }, StringSplitOptions.RemoveEmptyEntries); // Split
			var filledLines = lines.RemoveElement(string.Empty); // Remove
			for (int i = 0; i < filledLines.Length; i++)
			{
				CSelectorBlock.Choices.Add(filledLines[i]); // Add
			}

			// Var
			ParentElement.SelectorBlock = CSelectorBlock; // Set

			Global.CreatorPage.SaveChanges(); // Save
		}
	}

	ContentCollectionsWindow ContentCollectionsWindow => new(ChoicesTxt);
	private void ContentCollectionsBtn_Click(object sender, RoutedEventArgs e)
	{
		ContentCollectionsWindow.Show();
	}
}
