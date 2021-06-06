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
using Datalya.Enums;
using Datalya.Interfaces;
using Datalya.UserControls;
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
using System.Windows.Shapes;

namespace Datalya.Windows
{
	/// <summary>
	/// Interaction logic for AddWindow.xaml
	/// </summary>
	public partial class AddWindow : Window
	{
		public AddWindow()
		{
			InitializeComponent();
			InitUI(); // Load the UI
		}

		private void InitUI()
		{
			try
			{
				for (int i = 0; i < Global.DataBaseBlocks.Count; i++)
				{
					BlockDisplayer.Children.Add(Global.DataBaseBlocks[i].BlockType switch
					{
						BlockType.Input => new InputBlockUI((InputBlock)Global.DataBaseBlocks[i]), // Add block
						BlockType.Multichoices => new MultichoicesBlockUI((MultichoicesBlock)Global.DataBaseBlocks[i]), // Add block
						BlockType.Selector => new UserControls.SelectorBlock((Classes.SelectorBlock)Global.DataBaseBlocks[i]), // Add block
						BlockType.SingleChoice => new SingleChoiceBlockUI((SingleChoiceBlock)Global.DataBaseBlocks[i]), // Add block
						_ => new InputBlockUI((InputBlock)Global.DataBaseBlocks[i]) // Add block
					}); // Add
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{Properties.Resources.ErrorOccured}\n{ex.Message}", Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized; // Minimize
		}

		private void CloseBtn_Click(object sender, RoutedEventArgs e)
		{
			Close(); // Closes the window
		}

		private void AddBtn_Click(object sender, RoutedEventArgs e)
		{
			List<IBlock> blocks = new();

			foreach (UIElement uIElement in BlockDisplayer.Children)
			{
				if (uIElement is InputBlockUI inputBlockUI)
				{
					if (inputBlockUI.ValueTxt.Text is not null)
					{
						InputBlock i = new(inputBlockUI.InputBlock.Name);
						i.BlockValue = inputBlockUI.ValueTxt.Text;
						blocks.Add(i); // Add item 
					}
				}
				else if (uIElement is MultichoicesBlockUI multichoicesBlockUI)
				{
					//TODO
				}
				else if (uIElement is UserControls.SelectorBlock selectorBlock)
				{
					//TODO
				}
				else if (uIElement is SingleChoiceBlockUI singleChoiceBlockUI)
				{
					//TODO
				}
			}

			List<DataBaseItem> dbs = new();
			for (int i = 0; i < Global.DataBaseContent.Count; i++)
			{
				dbs.Add(Global.DataBaseContent[i]);
			}
			dbs.Add(new(blocks));

			Global.DataBaseContent = dbs;
			Global.DatabasePage.InitDataBaseUI(); // Refresh database UI
			Close(); // Close window
		}
	}
}
