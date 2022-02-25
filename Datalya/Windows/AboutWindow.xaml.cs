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
using System.Windows;

namespace Datalya.Windows;

/// <summary>
/// Interaction logic for AboutWindow.xaml
/// </summary>
public partial class AboutWindow : Window
{
	public AboutWindow()
	{
		InitializeComponent();
		InitUI(); // Load the UI
	}

	private void InitUI()
	{
		VersionTxt.Text = $"{Properties.Resources.Version} {Global.Version}"; // Set text
	}

	private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
	{
		WindowState = WindowState.Minimized; // Minimize the window
	}

	private void CloseBtn_Click(object sender, RoutedEventArgs e)
	{
		Close(); // Close the window
	}

	private async void UpdateBtn_Click(object sender, RoutedEventArgs e)
	{
		if (await NetworkConnection.IsAvailableAsync())
		{
			string lastVer = await Update.GetLastVersionAsync(Global.LastVersionLink); // Get last version
			if (Update.IsAvailable(Global.Version, lastVer))
			{
				if (MessageBox.Show(Properties.Resources.UpdatesAvailable, $"{Properties.Resources.InstallVersion} {lastVer}", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
				{
					Env.ExecuteAsAdmin(AppDomain.CurrentDomain.BaseDirectory + @"\Xalyus Updater.exe"); // Execute as admin
					Environment.Exit(0); // Close
				}
			}
			else
			{
				MessageBox.Show(Properties.Resources.UpToDate, Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Information); // Show messgae
			}
		}
	}

	private void LicensesBtn_Click(object sender, RoutedEventArgs e)
	{
		MessageBox.Show($"{Properties.Resources.Licenses}\n\n" +
			"Fluent System Icons - MIT License - © 2020 Microsoft Corporation\n" +
			"ClosedXML - MIT License - © 2016 ClosedXML\n" +
			"LeoCorpLibrary - MIT License - © 2020-2022 Léo Corporation\n" +
			"Datalya - MIT License - © 2021-2022 Léo Corporation", Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Information);
	}
}
