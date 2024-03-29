﻿/*
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
using ClosedXML.Excel;
using Datalya.Classes;
using Datalya.Enums;
using Datalya.Windows;
using Microsoft.Win32;
using PeyrSharp.Env;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Datalya.Pages;

/// <summary>
/// Interaction logic for DatabasePage.xaml
/// </summary>
public partial class DatabasePage : Page
{
	private Button CheckedButton { get; set; }

	public DatabasePage()
	{
		InitializeComponent();
		InitUI(); // Init the UI
	}

	internal void InitUI()
	{
		CheckButton(Global.Settings.DefaultMenuTab switch
		{
			DatabaseMenuTabs.File => FileRibBtn,
			DatabaseMenuTabs.Edit => EditRibBtn,
			DatabaseMenuTabs.Export => ExportRibBtn,
			DatabaseMenuTabs.Help => HelpRibBtn,
			_ => FileRibBtn
		}); // Check

		FileTab.Visibility = Global.Settings.DefaultMenuTab == DatabaseMenuTabs.File ? Visibility.Visible : Visibility.Collapsed; // Show
		EditTab.Visibility = Global.Settings.DefaultMenuTab == DatabaseMenuTabs.Edit ? Visibility.Visible : Visibility.Collapsed; // Show
		ExportTab.Visibility = Global.Settings.DefaultMenuTab == DatabaseMenuTabs.Export ? Visibility.Visible : Visibility.Collapsed; // Show
		HelpTab.Visibility = Global.Settings.DefaultMenuTab == DatabaseMenuTabs.Help ? Visibility.Visible : Visibility.Collapsed; // Show

		InitDataBaseUI(); // Init database UI
	}

	internal void InitDataBaseUI()
	{
		try
		{
			// Clear
			DataBaseGridView.Columns.Clear();
			DataBaseListView.ItemsSource = null; // Bind
			searchModeEnabled = false; // Disable search mode

			SearchBtnTxt.Text = Properties.Resources.Search; // Set text
			SearchIconTxt.Text = "\uF690"; // Set text

			if (Global.CurrentDataBase.Blocks.Count > 0)
			{
				// Binding
				DataBaseListView.ItemsSource = Global.CurrentDataBase.ItemsContent; // Bind

				// Columns
				for (int i = 0; i < Global.CurrentDataBase.Blocks.Count; i++)
				{
					DataBaseGridView.Columns.Add(new() { Header = Global.CurrentDataBase.Blocks[i].Name, DisplayMemberBinding = new Binding($"Item[{i}]") });
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void FileRibBtn_Click(object sender, RoutedEventArgs e)
	{
		ResetAllCheckStatus(); // Reset
		CheckButton(FileRibBtn); // Check

		HideAllTabs(); // Hide all ribbon tabs
		FileTab.Visibility = Visibility.Visible; // Show
	}

	private void EditRibBtn_Click(object sender, RoutedEventArgs e)
	{
		ResetAllCheckStatus(); // Reset
		CheckButton(EditRibBtn); // Check

		HideAllTabs(); // Hide all ribbon tabs
		EditTab.Visibility = Visibility.Visible; // Show
	}

	private void ExportRibBtn_Click(object sender, RoutedEventArgs e)
	{
		ResetAllCheckStatus(); // Reset
		CheckButton(ExportRibBtn); // Check

		HideAllTabs(); // Hide all ribbon tabs
		ExportTab.Visibility = Visibility.Visible; // Show
	}

	private void HelpRibBtn_Click(object sender, RoutedEventArgs e)
	{
		ResetAllCheckStatus(); // Reset
		CheckButton(HelpRibBtn); // Check

		HideAllTabs(); // Hide all ribbon tabs
		HelpTab.Visibility = Visibility.Visible; // Show
	}

	private void TabEnter(object sender, MouseEventArgs e)
	{
		Button button = (Button)sender; // Create button

		button.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the foreground
		button.Background.BeginAnimation(SolidColorBrush.ColorProperty, Global.OnHoverColorAnimation); // Play animation
	}

	private void TabLeave(object sender, MouseEventArgs e)
	{
		Button button = (Button)sender; // Create button

		if (button != CheckedButton)
		{
			button.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground 
			button.Background.BeginAnimation(SolidColorBrush.ColorProperty, Global.OnLeaveColorAnimation); ; // Play animation
		}
	}

	private void CheckButton(Button button)
	{
		button.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["WindowButtonsHoverForeground1"].ToString()) }; // Set the foreground
		button.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["AccentColor"].ToString()) }; // Set the background

		CheckedButton = button; // Set the "checked" button
	}

	private void ResetAllCheckStatus()
	{
		FileRibBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
		FileRibBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["LightBackground"].ToString()) }; // Set the background

		EditRibBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the foreground
		EditRibBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["LightBackground"].ToString()) }; // Set the background

		ExportRibBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the background
		ExportRibBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["LightBackground"].ToString()) }; // Set the background

		HelpRibBtn.Foreground = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Foreground1"].ToString()) }; // Set the background
		HelpRibBtn.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["LightBackground"].ToString()) }; // Set the background
	}

	internal void HideAllTabs()
	{
		FileTab.Visibility = Visibility.Collapsed; // Hide
		EditTab.Visibility = Visibility.Collapsed; // Hide
		ExportTab.Visibility = Visibility.Collapsed; // Hide
		HelpTab.Visibility = Visibility.Collapsed; // Hide
	}

	private void SaveBtn_Click(object sender, RoutedEventArgs e)
	{
		if (!string.IsNullOrEmpty(Global.DataBaseFilePath) && Global.CurrentDataBase.Blocks.Count > 0 && Global.CurrentDataBase.ItemsContent.Count > 0) // If the DataBase isn't empty
		{
			DataBaseManager.Save(Global.CurrentDataBase, Global.DataBaseFilePath); // Save
		}
		else if (string.IsNullOrEmpty(Global.DataBaseFilePath) && Global.CurrentDataBase.Blocks.Count > 0 && Global.CurrentDataBase.ItemsContent.Count > 0)
		{
			SaveAsBtn_Click(SaveBtn, null);
		}
	}

	private void SaveAsBtn_Click(object sender, RoutedEventArgs e)
	{
		if (Global.CurrentDataBase.Blocks.Count > 0 && Global.CurrentDataBase.ItemsContent.Count > 0) // If the DataBase isn't empty
		{
			SaveFileDialog saveFileDialog = new()
			{
				FileName = $"{Global.CurrentDataBase.DataBaseInfo.Name}.datalyadb", // Set file name
				Filter = $"{Properties.Resources.DatalyaFile}|*.datalyadb", // Set filter
				Title = Properties.Resources.SaveAs // Set title
			};

			if (saveFileDialog.ShowDialog() ?? true)
			{
				Global.DataBaseFilePath = saveFileDialog.FileName; // Set
				DataBaseManager.Save(Global.CurrentDataBase, saveFileDialog.FileName); // Save DataBase
			}
		}
	}

	private void NewDatabaseBtn_Click(object sender, RoutedEventArgs e)
	{
		if (Global.CurrentDataBase.Blocks.Count > 0) // If database is open
		{
			if (MessageBox.Show(Properties.Resources.CloseDBConfirmMsg, Properties.Resources.Datalya, MessageBoxButton.YesNoCancel, MessageBoxImage.Information) == MessageBoxResult.Yes)
			{
				if (!string.IsNullOrEmpty(Global.DataBaseFilePath))
				{
					DataBaseManager.Save(Global.CurrentDataBase, Global.DataBaseFilePath); // Save
				}
				else
				{
					SaveAsBtn_Click(NewDatabaseBtn, null); // Call SaveAs code
				}

				new NewDataBaseWindow(true).Show(); // Show
			}
		}
		else
		{
			new NewDataBaseWindow(true).Show(); // Show
		}
	}

	private void OpenDbBtn_Click(object sender, RoutedEventArgs e)
	{
		OpenFileDialog openFileDialog = new()
		{
			Filter = $"{Properties.Resources.DatalyaFile}|*.datalyadb|{Properties.Resources.AllFiles}|*.*", // Set filter
			Title = Properties.Resources.Open // Set title
		};

		if (openFileDialog.ShowDialog() ?? true)
		{
			DataBaseManager.Open(openFileDialog.FileName); // Open DataBase
		}
	}

	private void InfoBtn_Click(object sender, RoutedEventArgs e)
	{
		if (Global.CurrentDataBase is not null && Global.CurrentDataBase.DataBaseInfo is not null)
		{
			new DataBaseInfoWindow().Show(); // Show infos 
		}
	}

	private void CloseDbBtn_Click(object sender, RoutedEventArgs e)
	{
		if (Global.CurrentDataBase.Blocks.Count > 0)
		{
			var dialogResult = MessageBox.Show(Properties.Resources.CloseDBConfirmMsg, Properties.Resources.Datalya, MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
			if (dialogResult == MessageBoxResult.Yes)
			{
				Global.CurrentDataBase.DataBaseInfo.LastEditTime = Sys.UnixTime;
				if (!string.IsNullOrEmpty(Global.DataBaseFilePath))
				{
					DataBaseManager.Save(Global.CurrentDataBase, Global.DataBaseFilePath); // Save
				}
				else
				{
					SaveFileDialog saveFileDialog = new()
					{
						FileName = $"{Global.CurrentDataBase.DataBaseInfo.Name}.datalyadb", // Set file name
						Filter = $"{Properties.Resources.DatalyaFile}|*.datalyadb", // Set filter
						Title = Properties.Resources.SaveAs // Set title
					};

					if (saveFileDialog.ShowDialog() ?? true)
					{
						DataBaseManager.Save(Global.CurrentDataBase, saveFileDialog.FileName); // Save DataBase
					}
				}
			}
			else if (dialogResult == MessageBoxResult.No)
			{
				// Don't save
			}
			else if (dialogResult == MessageBoxResult.Cancel)
			{
				return; // Cancel
			}

			Global.DataBaseFilePath = ""; // Reset

			DataBaseInfo dbi = new() { Authors = new(), CreationTime = Sys.UnixTime, LastEditTime = Sys.UnixTime, Size = 0 };
			dbi.Authors.Add(Environment.UserName); // Add
			dbi.Name = Properties.Resources.DatalyaDataBase; // Set

			Global.CurrentDataBase = new() { DataBaseInfo = dbi }; // New Database

			Global.DatabasePage.InitDataBaseUI(); // Refresh UI
			Global.CreatorPage.InitUI(); // Refresh UI
			Global.MainWindow.RefreshName();
			Global.MainWindow.Hide(); // Close

			Global.HomeWindow.InitUI(); // Refresh
			Global.HomeWindow.WindowState = Global.MainWindow.WindowState; // Set
			Global.HomeWindow.Height = Global.MainWindow.Height; // Set size
			Global.HomeWindow.Width = Global.MainWindow.Width; // Set size
			Global.HomeWindow.Left = Global.MainWindow.Left; // Set position
			Global.HomeWindow.Top = Global.MainWindow.Top; // Set position
			Global.HomeWindow.Show(); // Show home page
		}


	}

	private void AddItemBtn_Click(object sender, RoutedEventArgs e)
	{
		if (Global.CurrentDataBase.Blocks.Count > 0)
		{
			new AddWindow().Show(); // Show "Add" window 
		}
	}

	private void DeleteBtn_Click(object sender, RoutedEventArgs e)
	{

		try
		{
			if (DataBaseListView.SelectedItems.Count > 0)
			{
				var selectedItems = DataBaseListView.SelectedItems; // Set
				for (int i = 0; i < selectedItems.Count; i++)
				{
					Global.CurrentDataBase.ItemsContent.Remove((List<string>)selectedItems[i]);
				}

				Global.IsModified = true;
				InitDataBaseUI();
			}
			else
			{
				MessageBox.Show(Properties.Resources.DataBaseEmptyDeleteMsg, Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show($"{Properties.Resources.ErrorOccured}.\n{ex.Message}", Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	private void EditBtn_Click(object sender, RoutedEventArgs e)
	{
		if (DataBaseListView.SelectedItems.Count > 0)
		{
			new EditWindow((List<string>)DataBaseListView.SelectedItem).Show(); // Show EditWindow
		}
	}

	private void DeleteAllBtn_Click(object sender, RoutedEventArgs e)
	{
		if (DataBaseListView.Items.Count > 0)
		{
			if (MessageBox.Show(Properties.Resources.DeleteAllMsg, Properties.Resources.Datalya, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
			{
				Global.CurrentDataBase.ItemsContent = new(); // Clear 
				Global.IsModified = true;
				InitDataBaseUI(); // Refresh UI
			}
		}
		else
		{
			MessageBox.Show(Properties.Resources.DataBaseEmptyDeleteMsg, Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}

	private void AboutBtn_Click(object sender, RoutedEventArgs e)
	{
		Global.MainWindow.SettingsBtn_Click(sender, e);
	}

	private void GetHelpBtn_Click(object sender, RoutedEventArgs e)
	{
		new HelpWindow().Show(); // Show help
	}

	private void DuplicateItemBtn_Click(object sender, RoutedEventArgs e)
	{
		if (DataBaseListView.SelectedItems.Count > 0)
		{
			var selectedItem = DataBaseListView.SelectedItem; // Get selected item

			Global.CurrentDataBase.ItemsContent.Add((List<string>)selectedItem); // Duplicate
			Global.IsModified = true;
			InitDataBaseUI(); // Refresh UI
			DataBaseListView.SelectedItem = selectedItem; // Set selected item
		}
	}

	private void ExportDbExcelBtn_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			if (Global.CurrentDataBase.ItemsContent.Count > 0) // If there is items
			{
				SaveFileDialog saveFileDialog = new()
				{
					FileName = Global.CurrentDataBase.DataBaseInfo.Name, // File name
					Filter = $"{Properties.Resources.ExcelWorkbook}|*.xlsx", // Set filter
					Title = Properties.Resources.ExportToExcel // Set title
				}; // Create SaveFileDialog 

				if (saveFileDialog.ShowDialog() ?? true)
				{
					using var workbook = new XLWorkbook();
					var worksheet = workbook.Worksheets.Add(Global.CurrentDataBase.DataBaseInfo.Name);

					// Populate cells
					// Headers
					for (int i = 0; i < Global.CurrentDataBase.Blocks.Count; i++)
					{
						worksheet.Cell(1, i + 1).Value = Global.CurrentDataBase.Blocks[i].Name; // Set the header in the first row
						worksheet.Cell(1, i + 1).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin); // Thin border
						worksheet.Cell(1, i + 1).Style.Border.SetOutsideBorderColor(XLColor.Black); // Black border
						worksheet.Cell(1, i + 1).Style.Font.Bold = true; // Bold
					}

					// Add auto filter
					worksheet.RangeUsed().SetAutoFilter();

					// Content
					for (int i = 1; i <= Global.CurrentDataBase.ItemsContent.Count; i++)
					{
						for (int j = 1; j <= Global.CurrentDataBase.ItemsContent[i - 1].Count; j++) // For each item
						{
							worksheet.Cell(i + 1, j).Value = Global.CurrentDataBase.ItemsContent[i - 1][j - 1]; // Set item value
							worksheet.Cell(i + 1, j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin); // Thin border
							worksheet.Cell(i + 1, j).Style.Border.SetOutsideBorderColor(XLColor.Black); // Black border
							worksheet.Column(j).AdjustToContents(); // Set width
						}
					}

					workbook.SaveAs(saveFileDialog.FileName); // Save
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show($"{Properties.Resources.ErrorOccured}.\n{ex.Message}", Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	private void DataBaseListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		if (DataBaseListView.SelectedItems.Count > 0)
		{
			new EditWindow((List<string>)DataBaseListView.SelectedItem).Show(); // Show EditWindow
		}
	}

	private void DataBaseListView_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Delete)
		{
			DeleteBtn_Click(sender, null);
		}
	}

	bool searchModeEnabled = false;
	private void SearchBtn_Click(object sender, RoutedEventArgs e)
	{
		if (!searchModeEnabled) // If search mode is disabled
		{
			new SearchWindow().Show(); // Show search window 
		}
		else
		{
			DataBaseListView.ItemsSource = Global.CurrentDataBase.ItemsContent; // Bind to the original collection

			SearchBtnTxt.Text = Properties.Resources.Search; // Set text
			SearchIconTxt.Text = "\uF690"; // Set text
			searchModeEnabled = false; // Disable search mode
		}
	}

	internal void HighlightSearchResults(List<List<string>> results)
	{
		DataBaseListView.ItemsSource = results; // Bind
		searchModeEnabled = true; // Enable search mode

		SearchBtnTxt.Text = Properties.Resources.ExitSearch; // Set text
		SearchIconTxt.Text = "\uF36A"; // Set text
	}
}
