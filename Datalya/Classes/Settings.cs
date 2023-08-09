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
using Datalya.Enums;
using PeyrSharp.Env;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Datalya.Classes;

/// <summary>
/// Settings of Datalya
/// </summary>
public class Settings
{
	/// <summary>
	/// The <see cref="Enum.Theme"/> of Datalya.
	/// </summary>
	public Theme Theme { get; set; }

	/// <summary>
	/// The language of Datalya
	/// </summary>
	public string Language { get; set; }

	/// <summary>
	/// True if Datalya should check updates on start.
	/// </summary>
	public bool CheckUpdatesOnStart { get; set; }

	/// <summary>
	/// True if Datalya should notify updates on start.
	/// </summary>
	public bool NotifyUpdates { get; set; }

	/// <summary>
	/// Recent files opened by the user.
	/// </summary>
	public List<DataBaseInfo> RecentFiles { get; set; }

	/// <summary>
	/// True if Datalya should start maximized.
	/// </summary>
	public bool IsMaximized { get; set; }

	/// <summary>
	/// True if Datalya should display the "First Run" experience.
	/// </summary>
	public bool IsFirstRun { get; set; }

	/// <summary>
	/// True if Datalya should display a warning message when deleting a Block.
	/// </summary>
	public bool? DisplayDeleteBlockMessage { get; set; }

	/// <summary>
	/// The default tab that sould be selected when opening Datalya.
	/// </summary>
	public DatabaseMenuTabs? DefaultMenuTab { get; set; }

	/// <summary>
	/// The default size that should be used when launching the app.
	/// </summary>
	public (double, double)? MainWindowSize { get; set; }
}

public static class SettingsManager
{
	/// <summary>
	/// Loads Datalya settings.
	/// </summary>
	public static void Load()
	{
		string path = FileSys.AppDataPath + @"\Léo Corporation\Datalya\Settings.xml"; // The path of the settings file

		if (File.Exists(path)) // If the file exist
		{
			XmlSerializer xmlSerializer = new(typeof(Settings)); // XML Serializer
			StreamReader streamReader = new(path); // Where the file is going to be read

			Global.Settings = (Settings)xmlSerializer.Deserialize(streamReader); // Read
			Global.Settings.MainWindowSize ??= (650, 1100);
			streamReader.Dispose();
		}
		else
		{
			Global.Settings = new Settings
			{
				Theme = Theme.System,
				Language = "_default",
				RecentFiles = new(),
				CheckUpdatesOnStart = true,
				NotifyUpdates = true,
				IsMaximized = false,
				IsFirstRun = true,
				DisplayDeleteBlockMessage = true,
				DefaultMenuTab = DatabaseMenuTabs.File
			}; // Create a new settings file

			Save(); // Save the changes
		}
	}

	/// <summary>
	/// Saves Datalya settings.
	/// </summary>
	public static void Save()
	{
		string path = FileSys.AppDataPath + @"\Léo Corporation\Datalya\Settings.xml"; // The path of the settings file

		XmlSerializer xmlSerializer = new(typeof(Settings)); // Create XML Serializer

		if (!Directory.Exists(FileSys.AppDataPath + @"\Léo Corporation\Datalya")) // If the directory doesn't exist
		{
			Directory.CreateDirectory(FileSys.AppDataPath + @"\Léo Corporation\Datalya"); // Create the directory
		}

		StreamWriter streamWriter = new(path); // The place where the file is going to be written
		xmlSerializer.Serialize(streamWriter, Global.Settings);

		streamWriter.Dispose();
	}
}
