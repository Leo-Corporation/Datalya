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

namespace Datalya.UserControls;

/// <summary>
/// Interaction logic for SelectorBlockCreatorUI.xaml
/// </summary>
public partial class SelectorBlockCreatorUI : UserControl
{
	SelectorBlockPropertiesUI SelectorBlockPropertiesUI { get; init; }
	internal Classes.SelectorBlock SelectorBlock { get; set; }
	public SelectorBlockCreatorUI(Classes.SelectorBlock selectorBlock = null)
	{
		InitializeComponent();
		SelectorBlock = selectorBlock;
		SelectorBlockPropertiesUI = new(this, SelectorBlock);
		if (SelectorBlock is not null)
		{
			NameTxt.Text = SelectorBlock.Name; // Set text
		}
	}

	private void ConfigureBtn_Click(object sender, RoutedEventArgs e)
	{
		Global.CreatorPage.PropertyDisplayer.Content = SelectorBlockPropertiesUI; // Set frame's content
	}

	private void DeleteBtn_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			if (Global.Settings.DisplayDeleteBlockMessage.Value)
			{
				if (MessageBox.Show(Properties.Resources.ConfirmDeleteBlockMsg, Properties.Resources.DatalyaCreator, MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
				{
					return; // Cancel
				}
			}

			int index = Global.CreatorPage.BlockDisplayer.Children.IndexOf(this); // Get index

			if (Global.CreatorPage.PropertyDisplayer.Content == SelectorBlockPropertiesUI)
			{
				Global.CreatorPage.PropertyDisplayer.Content = Global.EmptyPropertyUI; // Set content
			}

			if (Global.CurrentDataBase.ItemsContent.Count > 0)
			{
				for (int i = 0; i < Global.CurrentDataBase.ItemsContent.Count; i++) // For each item
				{
					Global.CurrentDataBase.ItemsContent[i].RemoveAt(index); // Remove item
				}
			}

			if (Global.CurrentDataBase.Blocks.Count > index)
			{
				Global.CurrentDataBase.Blocks.RemoveAt(index); // Remove item 
			}

			Global.CreatorPage.BlockDisplayer.Children.Remove(this); // Remove current block
		}
		catch { }
	}
}
