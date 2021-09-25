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
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Datalya.UserControls
{
	/// <summary>
	/// Interaction logic for HomeDataBaseItem.xaml
	/// </summary>
	public partial class HomeDataBaseItem : UserControl
	{
		DataBaseInfo DataBaseInfo { get; init; }
		public HomeDataBaseItem(DataBaseInfo dataBase)
		{
			InitializeComponent();
			DataBaseInfo = dataBase; // Set value

			InitUI(); // Load the UI
		}

		private void InitUI()
		{
			NameTxt.Text = DataBaseInfo.Name; // Set text
			LastEditTxt.Text = Env.UnixTimeToDateTime(DataBaseInfo.LastEditTime).ToString("g"); // Set text

			// Size
			double size;
			UnitType unitType;

			Global.ConvertByteToCorrectSize(DataBaseInfo.Size, out size, out unitType);
			string unit = unitType switch
			{
				UnitType.Byte => Properties.Resources.Bytes, // Set text
				UnitType.Kilobyte => Properties.Resources.Ko, // Set text
				UnitType.Megabyte => Properties.Resources.Mo, // Set text
				_ => Properties.Resources.Ko // Set text
			}; // Set unit text

			SizeTxt.Text = $"{Math.Round(size)} {unit}"; // Set text
			PinBtn.Content = DataBaseInfo.IsPinned ? "\uF604" : "\uF602"; // Set text
			PinToolTip.Content = DataBaseInfo.IsPinned ? Properties.Resources.Unpin : Properties.Resources.Pin; // Set content
		}

		private void PinBtn_Click(object sender, RoutedEventArgs e)
		{
			DataBaseInfo.IsPinned = !DataBaseInfo.IsPinned; // Set value
			PinBtn.Content = DataBaseInfo.IsPinned ? "\uF604" : "\uF602"; // Set text
			PinToolTip.Content = DataBaseInfo.IsPinned ? Properties.Resources.Unpin : Properties.Resources.Pin; // Set content

			Global.Settings.RecentFiles[Global.Settings.RecentFiles.IndexOf(DataBaseInfo)] = DataBaseInfo;
			SettingsManager.Save();
			Global.HomeWindow.InitUI(); // Refresh
		}

		private void RemoveBtn_Click(object sender, RoutedEventArgs e)
		{
			Global.Settings.RecentFiles.Remove(DataBaseInfo); // Remove
			SettingsManager.Save();
			Global.HomeWindow.InitUI(); // Refresh
		}

		private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (File.Exists(DataBaseInfo.FilePath)) // If the file exists
			{
				DataBaseManager.Open(DataBaseInfo.FilePath); // Open DataBase

				Global.MainWindow.WindowState = Global.HomeWindow.WindowState; // Set
				Global.MainWindow.Height = Global.HomeWindow.Height; // Set size
				Global.MainWindow.Width = Global.HomeWindow.Width; // Set size
				Global.MainWindow.Left = Global.HomeWindow.Left; // Set position
				Global.MainWindow.Top = Global.HomeWindow.Top; // Set position
				Global.MainWindow.Show(); // Show
				Global.HomeWindow.Hide(); // Hide
			}
		}
	}
}
