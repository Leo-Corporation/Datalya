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
using Datalya.Interfaces;
using Datalya.Pages;
using Datalya.UserControls;
using Datalya.Windows;
using LeoCorpLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Datalya.Classes
{
	public static class Global
	{
		/// <summary>
		/// Datalya's version.
		/// </summary>
		public static string Version => "1.0.0.21XX";

		/// <summary>
		/// The <see cref="Pages.DatabasePage"/> page.
		/// </summary>
		public static DatabasePage DatabasePage { get; set; }

		/// <summary>
		/// The <see cref="Pages.CreatorPage"/> page.
		/// </summary>
		public static CreatorPage CreatorPage { get; set; }

		/// <summary>
		/// The <see cref="Datalya.MainWindow"/> of Datalya.
		/// </summary>
		public static MainWindow MainWindow { get; set; }

		/// <summary>
		/// The <see cref="Windows.HomeWindow"/> of Datalya.
		/// </summary>
		public static HomeWindow HomeWindow { get; set; }

		/// <summary>
		/// Placeholder when there is no Block selected.
		/// </summary>
		internal static EmptyPropertyUI EmptyPropertyUI { get; set; }

		/// <summary>
		/// The current DataBase.
		/// </summary>
		public static DataBase CurrentDataBase { get; set; }

		/// <summary>
		/// The file path of the current <see cref="DataBase"/>.
		/// </summary>
		public static string DataBaseFilePath { get; set; }

		public static Settings Settings { get; set; }

		/// <summary>
		/// List of the available languages.
		/// </summary>
		public static List<string> LanguageList => new() { "English (United States)", "Français (France)" };

		/// <summary>
		/// List of the available languages codes.
		/// </summary>
		public static List<string> LanguageCodeList => new() { "en-US", "fr-FR" };

		/// <summary>
		/// Converts byte to correct size.
		/// </summary>
		/// <param name="size">The initial size.</param>
		/// <param name="converted">The converted size.</param>
		/// <param name="unitType">The final unit.</param>
		public static void ConvertByteToCorrectSize(int size, out double converted, out UnitType unitType)
		{
			if (size > 1000000)
			{
				converted = size / 1000000d; // Return
				unitType = UnitType.Megabyte; // Return
			}
			else if (size > 1000)
			{
				converted = size / 1000d; // Return
				unitType = UnitType.Kilobyte; // Return
			}
			else
			{
				converted = size; // Return
				unitType = UnitType.Byte; // Return
			}
		}

		public static bool DataBaseItemAlreadyExists(DataBaseInfo dataBaseInfo)
		{
			List<string> files = new(); // Files

			for (int i = 0; i < Settings.RecentFiles.Count; i++) // For each item
			{
				files.Add(Settings.RecentFiles[i].FilePath); // Add file path
			}

			return files.Contains(dataBaseInfo.FilePath); // Return
		}

		/// <summary>
		/// Changes the application's theme.
		/// </summary>
		public static void ChangeTheme()
		{
			App.Current.Resources.MergedDictionaries.Clear(); // Clear all resources
			ResourceDictionary resourceDictionary = new(); // Create a resource dictionary

			switch (Settings.Theme)
			{
				case Theme.Light:
					resourceDictionary.Source = new Uri("..\\Themes\\Light.xaml", UriKind.Relative); // Add source
					break;
				case Theme.Dark:
					resourceDictionary.Source = new Uri("..\\Themes\\Dark.xaml", UriKind.Relative); // Add source
					break;
				case Theme.System:
					if (IsSystemThemeDark())
					{
						resourceDictionary.Source = new Uri("..\\Themes\\Dark.xaml", UriKind.Relative); // Add source
					}
					else
					{
						resourceDictionary.Source = new Uri("..\\Themes\\Light.xaml", UriKind.Relative); // Add source
					}
					break;
			}

			App.Current.Resources.MergedDictionaries.Add(resourceDictionary); // Add the dictionary
		}

		public static bool IsSystemThemeDark()
		{
			if (Env.WindowsVersion != WindowsVersion.Windows10)
			{
				return false; // Avoid errors on older OSs
			}

			var t = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", "1");
			return t switch
			{
				0 => true,
				1 => false,
				_ => false
			}; // Return
		}
	}
}
