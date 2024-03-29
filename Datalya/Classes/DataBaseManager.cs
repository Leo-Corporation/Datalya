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
using PeyrSharp.Env;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Datalya.Classes;

/// <summary>
/// Methods to manage a <see cref="DataBase"/>.
/// </summary>
public static class DataBaseManager
{
	/// <summary>
	/// Saves a spceified <see cref="DataBase"/> in a file.
	/// </summary>
	/// <param name="dataBase"></param>
	public static void Save(DataBase dataBase, string filePath)
	{
		try
		{
			dataBase.DataBaseInfo.FilePath = filePath; // Set

			// Get size
			if (File.Exists(filePath))
			{
				FileInfo fileInfo = new(filePath); // Create fileinfo 
				dataBase.DataBaseInfo.Size = (int)fileInfo.Length; // Set
			}

			DataBase dataBase1 = dataBase; // Set value
			dataBase1.DataBaseInfo.LastEditTime = Sys.UnixTime; // Set

			if (!Global.DataBaseItemAlreadyExists(dataBase.DataBaseInfo)) // Check
			{
				Global.Settings.RecentFiles.Add(dataBase.DataBaseInfo); // Add
				SettingsManager.Save();
			}
			else
			{
				List<string> files = new();
				for (int i = 0; i < Global.Settings.RecentFiles.Count; i++)
				{
					files.Add(Global.Settings.RecentFiles[i].FilePath);
				}

				int index = files.IndexOf(filePath);
				Global.Settings.RecentFiles[index] = dataBase1.DataBaseInfo; // Set
			}
			XmlSerializer xmlSerializer = new(dataBase1.GetType()); // XML Serializer

			StreamWriter streamWriter = new(filePath); // The place where the file is gonna be written
			xmlSerializer.Serialize(streamWriter, dataBase); // Save

			streamWriter.Dispose(); // Dispose
			Global.IsModified = false; // Saved modifications
		}
		catch (Exception ex)
		{
			MessageBox.Show($"{Properties.Resources.Error}\n{ex.Message}", Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	/// <summary>
	/// Opens a specified <see cref="DataBase"/> from a file path.
	/// </summary>
	/// <param name="filePath"></param>
	public static void Open(string filePath)
	{
		try
		{
			if (File.Exists(filePath))
			{
				XmlSerializer xmlSerializer = new(typeof(DataBase)); // XML Serializer
				StreamReader streamReader = new(filePath); // The location of the file

				var db = (DataBase)xmlSerializer.Deserialize(streamReader); // Deserialize

				if (Global.IsVersionGreaterThanActual(db.DataBaseInfo.Version))
				{
					if (MessageBox.Show(Properties.Resources.NewerVersionDatabaseMsg, Properties.Resources.Datalya, MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
					{
						return;
					}
				}
				else
				{
					db.DataBaseInfo.Version = Global.Version;
				}

				Global.CurrentDataBase = db; // Read the database
				streamReader.Dispose(); // Dispose

				Global.CurrentDataBase.DataBaseInfo.LastEditTime = Sys.UnixTime; // Set
				if (string.IsNullOrEmpty(Global.CurrentDataBase.DataBaseInfo.Version))
				{
					Global.CurrentDataBase.DataBaseInfo.Version = Global.Version;
				}

				Global.DatabasePage.InitDataBaseUI(); // Refresh database view
				Global.CreatorPage.InitUI(); // Refresh creator view
				Global.MainWindow.RefreshName(); // Refresh database name

				Global.DataBaseFilePath = filePath; // Set
				Global.CurrentDataBase.DataBaseInfo.FilePath = filePath; // Set
				Global.IsModified = false;

				if (!Global.DataBaseItemAlreadyExists(Global.CurrentDataBase.DataBaseInfo)) // Check
				{
					Global.Settings.RecentFiles.Add(Global.CurrentDataBase.DataBaseInfo); // Add
					SettingsManager.Save();
				}
			}
			else
			{
				MessageBox.Show(Properties.Resources.FileNotFound, Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Error); // Show message
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show($"{Properties.Resources.Error}\n{ex.Message}", Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}
