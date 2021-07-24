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
using Datalya.Classes;
using LeoCorpLibrary;
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
	/// Interaction logic for NewDataBaseWindow.xaml
	/// </summary>
	public partial class NewDataBaseWindow : Window
	{
		internal BlockTemplate BlockTemplate { get; set; }
		bool showMainWindow = false;
		public NewDataBaseWindow(bool closeHome = false)
		{
			InitializeComponent();
			showMainWindow = closeHome; // Set
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
			Global.HomeWindow.Show(); // Show home page

			if (showMainWindow)
			{
				Global.HomeWindow.Hide(); // Close/Hide 
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
		}
	}
}
