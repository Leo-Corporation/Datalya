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
using System.Windows;

namespace Datalya.Windows;
/// <summary>
/// Interaction logic for SearchWindow.xaml
/// </summary>
public partial class SearchWindow : Window
{
	public SearchWindow()
	{
		InitializeComponent();
		InitUI(); // Load the UI
	}

	private void InitUI()
	{
		// Load ComboBox items
		for (int i = 0; i < Global.CurrentDataBase.Blocks.Count; i++)
		{
			FieldComboBox.Items.Add(Global.CurrentDataBase.Blocks[i].Name); // Add each field to the ComboBox
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

	private void CloseWindowBtn_Click(object sender, RoutedEventArgs e)
	{
		Close(); // Close the window
	}

	private void SearchBtn_Click(object sender, RoutedEventArgs e)
	{

	}
}
