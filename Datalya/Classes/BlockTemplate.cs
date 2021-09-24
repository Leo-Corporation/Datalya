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
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Datalya.Classes
{
	/// <summary>
	/// Template for <see cref="Block"/>.
	/// </summary>
	public class BlockTemplate
	{
		/// <summary>
		/// Name of the template.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Blocks of the template.
		/// </summary>
		public List<Block> Blocks { get; set; }
	}

	/// <summary>
	/// Manages <see cref="BlockTemplate"/>.
	/// </summary>
	public static class BlockTemplateManager
	{
		/// <summary>
		/// Imports a <see cref="BlockTemplate"/>.
		/// </summary>
		/// <param name="filePath"></param>
		public static void Import(string filePath, bool fromNew = false)
		{
			if (!File.Exists(filePath))
			{
				MessageBox.Show(Properties.Resources.FileNotFound, Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			XmlSerializer xmlSerializer = new(typeof(BlockTemplate)); // Create XML Serializer
			StreamReader streamReader = new(filePath); // Where the file is going to be read

			var template = (BlockTemplate)xmlSerializer.Deserialize(streamReader); // Deserialize

			if (!fromNew)
			{
				Global.CurrentDataBase.Blocks = template.Blocks; // Set 
			}
			else
			{
				if (!Global.BlockTemplates.Contains(template))
				{
					Global.BlockTemplates.Add(template); // Add 
				}
				else
				{
					MessageBox.Show(Properties.Resources.TemplateAlreadyImported, Properties.Resources.Datalya, MessageBoxButton.OK, MessageBoxImage.Information); // Show message
				}
			}
			Global.CreatorPage.InitUI();

			streamReader.Dispose(); // Dispose
		}

		/// <summary>
		/// Exports a <see cref="BlockTemplate"/> in a file.
		/// </summary>
		/// <param name="blockTemplate"></param>
		/// <param name="filePath"></param>
		public static void Export(BlockTemplate blockTemplate, string filePath)
		{
			XmlSerializer xmlSerializer = new(typeof(BlockTemplate)); // Create XML Serializer
			StreamWriter streamWriter = new(filePath); // Where the file is going to be written

			xmlSerializer.Serialize(streamWriter, blockTemplate); // Write file
			streamWriter.Dispose(); // Dispose
		}
	}
}
