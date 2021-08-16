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
using Datalya.Interfaces;
using Datalya.UserControls;
using Microsoft.Win32;
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

namespace Datalya.Pages
{
	/// <summary>
	/// Interaction logic for CreatorPage.xaml
	/// </summary>
	public partial class CreatorPage : Page
	{
		public CreatorPage()
		{
			InitializeComponent();
			Global.EmptyPropertyUI = new();

			InitUI();
		}

		internal void InitUI()
		{
			// Clear
			BlockDisplayer.Children.Clear(); // Clear

			PropertyDisplayer.Content = Global.EmptyPropertyUI; // Set content

			// Load Blocks
			for (int i = 0; i < Global.CurrentDataBase.Blocks.Count; i++)
			{
				if (Global.CurrentDataBase.Blocks[i] is InputBlock inputBlock)
				{
					BlockDisplayer.Children.Add(new InputBlockCreatorUI(inputBlock)); // Add item
				}
				else if (Global.CurrentDataBase.Blocks[i] is MultichoicesBlock multichoicesBlock)
				{
					BlockDisplayer.Children.Add(new MultichoicesBlockCreatorUI(multichoicesBlock)); // Add item
				}
				else if (Global.CurrentDataBase.Blocks[i] is SingleChoiceBlock singleChoiceBlock)
				{
					BlockDisplayer.Children.Add(new SingleChoiceBlockCreatorUI(singleChoiceBlock)); // Add item
				}
				else if (Global.CurrentDataBase.Blocks[i] is Classes.SelectorBlock s)
				{
					BlockDisplayer.Children.Add(new SelectorBlockCreatorUI(s));
				}
			}
		}

		private void ImputBlockBtn_Click(object sender, RoutedEventArgs e)
		{
			BlockDisplayer.Children.Add(new InputBlockCreatorUI()); // Add block
			for (int i = 0; i < Global.CurrentDataBase.ItemsContent.Count; i++)
			{
				Global.CurrentDataBase.ItemsContent[i].Add("");
			}
			SaveChanges(); // Save
		}

		private void MultichoicesBlockBtn_Click(object sender, RoutedEventArgs e)
		{
			BlockDisplayer.Children.Add(new MultichoicesBlockCreatorUI()); // Add block
			for (int i = 0; i < Global.CurrentDataBase.ItemsContent.Count; i++)
			{
				Global.CurrentDataBase.ItemsContent[i].Add("");
			}
			SaveChanges(); // Save
		}

		private void SingleChoiceBlockBtn_Click(object sender, RoutedEventArgs e)
		{
			BlockDisplayer.Children.Add(new SingleChoiceBlockCreatorUI()); // Add block
			for (int i = 0; i < Global.CurrentDataBase.ItemsContent.Count; i++)
			{
				Global.CurrentDataBase.ItemsContent[i].Add("");
			}
			SaveChanges(); // Save
		}

		private void SelectorBlockBtn_Click(object sender, RoutedEventArgs e)
		{
			BlockDisplayer.Children.Add(new SelectorBlockCreatorUI()); // Add block
			for (int i = 0; i < Global.CurrentDataBase.ItemsContent.Count; i++)
			{
				Global.CurrentDataBase.ItemsContent[i].Add("");
			}
			SaveChanges(); // Save
		}

		private void SaveChangesBtn_Click(object sender, RoutedEventArgs e)
		{
			SaveChanges(); // Save
		}

		internal void SaveChanges()
		{
			try
			{
				List<Classes.Block> blocks = new();
				foreach (UIElement uIElement in BlockDisplayer.Children)
				{
					if (uIElement is InputBlockCreatorUI inputCreatorUI)
					{
						if (inputCreatorUI.InputBlock is not null)
						{
							blocks.Add(inputCreatorUI.InputBlock); // Add item 
						}
					}
					else if (uIElement is MultichoicesBlockCreatorUI multichoicesBlockCreatorUI)
					{
						if (multichoicesBlockCreatorUI.MultichoicesBlock is not null)
						{
							blocks.Add(multichoicesBlockCreatorUI.MultichoicesBlock); // Add item 
						}
					}
					else if (uIElement is SelectorBlockCreatorUI selectorBlockCreatorUI)
					{
						if (selectorBlockCreatorUI.SelectorBlock is not null)
						{
							blocks.Add(selectorBlockCreatorUI.SelectorBlock); // Add item 
						}
					}
					else if (uIElement is SingleChoiceBlockCreatorUI choiceBlockCreatorUI)
					{
						if (choiceBlockCreatorUI.SingleChoiceBlock is not null)
						{
							blocks.Add(choiceBlockCreatorUI.SingleChoiceBlock); // Add item 
						}
					}
				}
				Global.CurrentDataBase.Blocks = blocks;
				Global.IsModified = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{Properties.Resources.ErrorOccured}\n{ex.Message}", Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void ImportBlockBtn_Click(object sender, RoutedEventArgs e)
		{
			if (Global.CurrentDataBase.Blocks.Count == 0)
			{
				OpenFileDialog openFileDialog = new()
				{
					Filter = $"{Properties.Resources.Template}|*.datalyabt",
					Title = Properties.Resources.ImportTemplate
				}; // Create OpenFileDialog

				if (openFileDialog.ShowDialog() ?? true)
				{
					BlockTemplateManager.Import(openFileDialog.FileName); // Import
				}
			}
			else
			{
				MessageBox.Show(Properties.Resources.CannotImportTemplateBlockExists, Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Error); // Show message
			}
		}

		private void ExportBlockBtn_Click(object sender, RoutedEventArgs e)
		{
			if (Global.CurrentDataBase.Blocks.Count > 0)
			{
				SaveFileDialog saveFileDialog = new()
				{
					Filter = $"{Properties.Resources.Template}|*.datalyabt",
					Title = Properties.Resources.ExportAsTemplate
				};

				if (saveFileDialog.ShowDialog() ?? true)
				{
					BlockTemplateManager.Export(new() { Name = saveFileDialog.FileName, Blocks = Global.CurrentDataBase.Blocks }, saveFileDialog.FileName); // Export
				} 
			}
		}
	}
}
