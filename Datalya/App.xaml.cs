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
using Datalya.Windows;
using LeoCorpLibrary;
using System;
using System.IO;
using System.Windows;

namespace Datalya;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		SettingsManager.Load(); // Load settings
		Global.ChangeTheme(); // Change theme
		Global.ChangeLanguage(); // Change language
		Global.CreateJumpLists(); // Create jumplist

		// Check settings
		CheckSettings();

		Global.CurrentDataBase = new() { DataBaseInfo = new() { Name = Datalya.Properties.Resources.DatalyaDataBase } }; // Create default database
		Global.BlockTemplates = new(); // Create new List
		Global.BlockTemplates.Add(Global.BooksTemplate); // Add template
		Global.BlockTemplates.Add(Global.PeopleTemplate); // Add template

		Global.DatabasePage = new(); // Create new Database page
		Global.CreatorPage = new(); // Create new Creator page
		Global.HomeWindow = new(); // Create new HomeWindow
		Global.MainWindow = new(); // Create new MainWindow

		if (!Global.Settings.IsFirstRun)
		{
			if (Global.Settings.CheckUpdatesOnStart && Global.Settings.NotifyUpdates)
			{
				NotfyUpdates(); // Notify if updates are available
			}

			if (e.Args.Length == 0)
			{
				Global.HomeWindow.Show(); // Show 
			}
			else
			{
				if (e.Args[0] == "/action" && e.Args[1] != null)
				{
					switch (e.Args[1])
					{
						case "0":
							Global.HomeWindow.Show(); // Show 
							break;
						case "1":
							Global.HomeWindow.NewDbBtn_Click(null, null);
							break;
						case "2":
							Global.HomeWindow.NewDbBtn_Click(this, null);
							break;
						default:
							Global.HomeWindow.Show(); // Show 
							break;
					}
				}
				else
				{
					if (File.Exists(e.Args[0]) && new FileInfo(e.Args[0]).Extension == ".datalyadb")
					{
						DataBaseManager.Open(e.Args[0]); // Open database 

						Global.DatabasePage.InitUI(); // Load the UI
						Global.CreatorPage.InitUI(); // Load the UI

						Global.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen; // Set
						Global.MainWindow.Show(); // Show
						Global.MainWindow.WindowStartupLocation = WindowStartupLocation.Manual; // Set
					}
				}
			}
		}
		else
		{
			new FirstRunWindow().Show(); // Show
		}
	}

	private async void NotfyUpdates()
	{
		string lastVer = await Update.GetLastVersionAsync(Global.LastVersionLink);
		if (Update.IsAvailable(Global.Version, lastVer))
		{
			System.Windows.Forms.NotifyIcon notifyIcon = new(); // Create NotifyIcon
			notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.BaseDirectory + @"\Datalya.exe");
			notifyIcon.BalloonTipClicked += (o, e) =>
			{
				if (MessageBox.Show(Datalya.Properties.Resources.UpdatesAvailable, $"{Datalya.Properties.Resources.InstallVersion} {lastVer}", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
				{
					Env.ExecuteAsAdmin(Directory.GetCurrentDirectory() + @"\Xalyus Updater.exe"); // Start the updater
					Environment.Exit(0); // Close
				}
			};

			notifyIcon.Visible = true; // Show
			notifyIcon.ShowBalloonTip(5000, Datalya.Properties.Resources.Datalya, Datalya.Properties.Resources.UpdatesAvailableShort, System.Windows.Forms.ToolTipIcon.Info);
			notifyIcon.Visible = false; // Hide
		}
	}

	private void CheckSettings()
	{
		if (!Global.Settings.DisplayDeleteBlockMessage.HasValue)
		{
			Global.Settings.DisplayDeleteBlockMessage = true;
		}

		if (!Global.Settings.DefaultMenuTab.HasValue)
		{
			Global.Settings.DefaultMenuTab = 0;
		}

		SettingsManager.Save(); // Save changes
	}
}
