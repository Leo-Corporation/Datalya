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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Datalya.UserControls;
/// <summary>
/// Interaction logic for NumberBlockCreatorUI.xaml
/// </summary>
public partial class NumberBlockCreatorUI : UserControl
{
	public NumberBlock NumberBlock { get; set; }
	public NumberBlockPropertiesUI NumberBlockPropertiesUI { get; init; }
	public NumberBlockCreatorUI(NumberBlock numberBlock = null)
	{
		InitializeComponent();
		NumberBlock = numberBlock;
		NumberBlockPropertiesUI = new(this, numberBlock);
		if (numberBlock is not null)
		{
			NameTxt.Text = numberBlock.Name;
		}
	}

	private void MoveUpBtn_Click(object sender, RoutedEventArgs e)
	{
		Global.MoveBlockUp(NumberBlock); // Move up
	}

	private void MoveDownBtn_Click(object sender, RoutedEventArgs e)
	{
		Global.MoveBlockDown(NumberBlock); // Move down
	}

	private void ConfigureBtn_Click(object sender, RoutedEventArgs e)
	{
		Global.CreatorPage.PropertyDisplayer.Content = NumberBlockPropertiesUI; // Set frame's content
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

			if (Global.CreatorPage.PropertyDisplayer.Content == NumberBlockPropertiesUI)
			{
				Global.CreatorPage.PropertyDisplayer.Content = Global.EmptyPropertyUI; // Set content
			}

			if (Global.CurrentDataBase.ItemsContent.Count > 0)
			{
				for (int i = 0; i < Global.CurrentDataBase.ItemsContent.Count; i++) // For each item
				{
					if (Global.CurrentDataBase.ItemsContent[i].Count - 1 < index)
					{
						continue;
					}
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
