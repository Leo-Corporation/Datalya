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
using Datalya.UserControls;
using Microsoft.Win32;
using System.Windows;

namespace Datalya.Windows;

/// <summary>
/// Interaction logic for TemplateWindow.xaml
/// </summary>
public partial class TemplateWindow : Window
{
	internal NewDataBaseWindow NewDataBaseWindow { get; init; }
	public TemplateWindow(NewDataBaseWindow newDataBaseWindow)
	{
		InitializeComponent();
		NewDataBaseWindow = newDataBaseWindow; // Set

		InitUI(); // Load the UI
	}

	private void InitUI()
	{
		Topmost = true; // Always on top

		TemplateDisplayer.Children.Clear(); // Clear

		for (int i = 0; i < Global.BlockTemplates.Count; i++)
		{
			TemplateDisplayer.Children.Add(new TemplateItem(Global.BlockTemplates[i], this)); // Add new item
		}
	}

	private void ImportTemplate_Click(object sender, RoutedEventArgs e)
	{
		OpenFileDialog openFileDialog = new()
		{
			Filter = $"{Properties.Resources.Template}|*.datalyabt",
			Title = Properties.Resources.ImportTemplate
		}; // Create OpenFileDialog

		if (openFileDialog.ShowDialog() ?? true)
		{
			BlockTemplateManager.Import(openFileDialog.FileName, true); // Import
			InitUI(); // Refresh UI
		}
	}

	private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
	{
		WindowState = WindowState.Minimized; // Minimize
	}

	private void CloseBtn_Click(object sender, RoutedEventArgs e)
	{
		Close(); // Close the window
	}
}
