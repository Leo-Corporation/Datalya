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
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace Datalya.Windows
{
	/// <summary>
	/// Interaction logic for NewDataBaseWindow.xaml
	/// </summary>
	public partial class NewDataBaseWindow : Window
	{
		internal BlockTemplate BlockTemplate { get; set; }

		readonly bool showMainWindow = false;
		readonly bool showTemplatesWindow = false;
		public NewDataBaseWindow(bool closeHome = false, bool showTemplates = false)
		{
			InitializeComponent();
			showMainWindow = closeHome; // Set
			showTemplatesWindow = showTemplates;

			InitUI();
			if (showTemplatesWindow)
			{
				new TemplateWindow(this).Show();
			}
		}

		internal void InitUI()
		{
			try
			{
				// Clear
				DataBaseGridView.Columns.Clear();
				DataBaseListView.ItemsSource = null; // Bind
				DataBaseListView.Visibility = Visibility.Collapsed; // Hide
				DataBasePreviewTxt.Visibility = Visibility.Collapsed; // Hide

				if (BlockTemplate is not null && BlockTemplate.Blocks.Count > 0)
				{
					DataBaseListView.Visibility = Visibility.Visible; // Show the list view if there are items to display
					DataBasePreviewTxt.Visibility = Visibility.Visible; // Show the text as well if there items

					// Columns
					for (int i = 0; i < BlockTemplate.Blocks.Count; i++) // Add columns
					{
						DataBaseGridView.Columns.Add(new() { Header = BlockTemplate.Blocks[i].Name, DisplayMemberBinding = new Binding($"Item[{i}]") });
					}

					// Rows
					List<List<string>> items = new(); // Collection of rows
					for (int i = 0; i < 2; i++) // Two rows
					{
						List<string> item = new();
						for (int j = 0; j < BlockTemplate.Blocks.Count; j++) // For each column
						{
							item.Add(Properties.Resources.Item); // Add a new item with the text "Element" to the current row
						}

						items.Add(item); // Add row to the list view items
					}

					DataBaseListView.ItemsSource = items; // Show items in the list view
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Properties.Resources.ErrorOccured, MessageBoxButton.OK, MessageBoxImage.Error); // Show error message
			}
		}

		private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized; // Minimize
		}

		private void CloseBtn_Click(object sender, RoutedEventArgs e)
		{
			Close(); // Close the window
		}

		private void SelectTemplateBtn_Click(object sender, RoutedEventArgs e)
		{
			new TemplateWindow(this).Show();
		}

		private void CreateBtn_Click(object sender, RoutedEventArgs e)
		{
			Global.DataBaseFilePath = ""; // Reset

			DataBaseInfo dbi = new() { Authors = new(), CreationTime = Env.UnixTime, LastEditTime = Env.UnixTime, Size = 0 };
			dbi.Authors.Add(Environment.UserName); // Add
			dbi.Name = NameTxt.Text; // Set text

			Global.CurrentDataBase = new() { DataBaseInfo = dbi }; // New Database

			if (BlockTemplate is not null)
			{
				Global.CurrentDataBase.Blocks = BlockTemplate.Blocks; // Set blocks
			}

			Global.DatabasePage.InitDataBaseUI(); // Refresh UI
			Global.CreatorPage.InitUI(); // Refresh UI
			Global.MainWindow.RefreshName();
			Global.MainWindow.Hide(); // Close

			Global.HomeWindow.InitUI(); // Refresh
			Global.HomeWindow.WindowState = Global.MainWindow.WindowState; // Set
			Global.HomeWindow.Show(); // Show home page

			if (showMainWindow)
			{
				Global.HomeWindow.Hide(); // Close/Hide 
				Global.MainWindow.WindowState = Global.HomeWindow.WindowState; // Set
				Global.MainWindow.Height = Global.HomeWindow.Height; // Set size
				Global.MainWindow.Width = Global.HomeWindow.Width; // Set size
				Global.MainWindow.Left = Global.HomeWindow.Left; // Set position
				Global.MainWindow.Top = Global.HomeWindow.Top; // Set position

				Global.MainWindow.ResetAllCheckStatus(); // Reset
				if (BlockTemplate is not null)
				{
					Global.MainWindow.WindowContent.Content = Global.DatabasePage; // Set
					Global.MainWindow.CheckButton(Global.MainWindow.DatabaseBtn); // Check
				}
				else
				{
					Global.MainWindow.WindowContent.Content = Global.CreatorPage; // Set
					Global.MainWindow.CheckButton(Global.MainWindow.CreatorBtn); // Check
				}

				Global.MainWindow.Show(); // Show
			}

			Close(); // Close the window
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			Close(); // Close the window
		}

		private void ClearTemplateBtn_Click(object sender, RoutedEventArgs e)
		{
			BlockTemplate = null; // Clear
			TemplateNameTxt.Text = Properties.Resources.None; // Set text
			InitUI();
		}
	}
}
