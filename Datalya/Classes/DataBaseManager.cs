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
using LeoCorpLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Datalya.Classes
{
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
				XmlSerializer xmlSerializer = new(dataBase1.GetType()); // XML Serializer
				dataBase1.DataBaseInfo.LastEditTime = Env.UnixTime; // Set

				StreamWriter streamWriter = new(filePath); // The place where the file is gonna be written
				xmlSerializer.Serialize(streamWriter, dataBase); // Save

				streamWriter.Dispose(); // Dispose
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

					Global.CurrentDataBase = (DataBase)xmlSerializer.Deserialize(streamReader); // Read the database
					streamReader.Dispose(); // Dispose

					Global.CurrentDataBase.DataBaseInfo.LastEditTime = Env.UnixTime; // Set

					Global.DatabasePage.InitDataBaseUI(); // Refresh database view
					Global.CreatorPage.InitUI(); // Refresh creator view
					Global.MainWindow.RefreshName(); // Refresh database name

					Global.DataBaseFilePath = filePath; // Set
					Global.CurrentDataBase.DataBaseInfo.FilePath = filePath; // Set
					
					if (!Global.Settings.RecentFiles.Contains(Global.CurrentDataBase.DataBaseInfo)) // Check
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
}
