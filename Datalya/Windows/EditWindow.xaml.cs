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
	/// Interaction logic for EditWindow.xaml
	/// </summary>
	public partial class EditWindow : Window
	{
		List<string> Item { get; set; }
		public EditWindow(List<string> item)
		{
			InitializeComponent();
			Item = item; // Set value

			InitUI(); // Load the UI
		}

		private void InitUI()
		{
			try
			{
				for (int i = 0; i < Global.CurrentDataBase.Blocks.Count; i++)
				{
					BlockDisplayer.Children.Add(Global.CurrentDataBase.Blocks[i].BlockType switch
					{
						BlockType.Input => new InputBlockUI((InputBlock)Global.CurrentDataBase.Blocks[i], Item[i]), // Add block
						BlockType.Multichoices => new MultichoicesBlockUI((MultichoicesBlock)Global.CurrentDataBase.Blocks[i], Item[i]), // Add block
						BlockType.Selector => new UserControls.SelectorBlock((Classes.SelectorBlock)Global.CurrentDataBase.Blocks[i], Item[i]), // Add block
						BlockType.SingleChoice => new SingleChoiceBlockUI((SingleChoiceBlock)Global.CurrentDataBase.Blocks[i], Item[i]), // Add block
						_ => new InputBlockUI((InputBlock)Global.CurrentDataBase.Blocks[i]) // Add block
					}); // Add
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{Properties.Resources.ErrorOccured}\n{ex.Message}", Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void EditBtn_Click(object sender, RoutedEventArgs e)
		{
			if (Global.CurrentDataBase.ItemsContent.Contains(Item))
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
						MultichoicesBlock multichoicesBlock = new(multichoicesBlockUI.MultichoicesBlock.Name);
						for (int i = 0; i < multichoicesBlockUI.CheckBoxesDisplayer.Children.Count; i++)
						{
							if (multichoicesBlockUI.CheckBoxesDisplayer.Children[i] is CheckBox)
							{
								CheckBox checkBox = (CheckBox)multichoicesBlockUI.CheckBoxesDisplayer.Children[i];
								if (checkBox.IsChecked.Value)
								{
									multichoicesBlock.SelectedChoices.Add(checkBox.Content.ToString());
								}
							}
						}

						blocks.Add(multichoicesBlock);
					}
					else if (uIElement is UserControls.SelectorBlock selectorBlock)
					{
						Classes.SelectorBlock cSelector = new(selectorBlock.CSelectorBlock.Name);
						if (selectorBlock.ItemComboBox.SelectedItem is not null)
						{
							cSelector.BlockValue = selectorBlock.ItemComboBox.SelectedItem.ToString();
							blocks.Add(cSelector);
						}
					}
					else if (uIElement is SingleChoiceBlockUI singleChoiceBlockUI)
					{
						SingleChoiceBlock singleChoiceBlock = new(singleChoiceBlockUI.SingleChoiceBlock.Name);
						for (int i = 0; i < singleChoiceBlockUI.RadioButtonsDisplayer.Children.Count; i++)
						{
							if (singleChoiceBlockUI.RadioButtonsDisplayer.Children[i] is RadioButton)
							{
								RadioButton radioButton = (RadioButton)singleChoiceBlockUI.RadioButtonsDisplayer.Children[i];
								if (radioButton.IsChecked.Value)
								{
									singleChoiceBlock.BlockValue = radioButton.Content.ToString(); // Set
								}
							}
						}

						blocks.Add(singleChoiceBlock);
					}
				}

				List<string> newItem = new();
				for (int i = 0; i < blocks.Count; i++)
				{
					newItem.Add(blocks[i].ToString()); // Add item
				}

				Global.CurrentDataBase.ItemsContent[Global.CurrentDataBase.ItemsContent.IndexOf(Item)] = newItem; // Edit
				Global.DatabasePage.InitDataBaseUI(); // Refresh database UI
				Close(); // Close window 
			}
			else
			{
				MessageBox.Show(Properties.Resources.CantEditItemDeletedMsg, Properties.Resources.EditElement, MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized; // Minimize
		}

		private void CloseBtn_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
