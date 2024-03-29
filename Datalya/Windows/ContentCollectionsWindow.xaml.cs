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
using Datalya.Classes;
using Datalya.UserControls;
using System.Windows;
using System.Windows.Controls;

namespace Datalya.Windows;

/// <summary>
/// Interaction logic for ContentCollectionsWindow.xaml
/// </summary>
public partial class ContentCollectionsWindow : Window
{
	TextBox TextBox { get; init; }
	public ContentCollectionsWindow(TextBox textBox)
	{
		InitializeComponent();
		TextBox = textBox;

		InitUI();
	}

	private void InitUI()
	{
		for (int i = 0; i < Global.ContentCollections.Count; i++)
		{
			ItemDisplayer.Children.Add(new ContentCollectionItem(Global.ContentCollections[i], (ContentCollection contentCollection) => { SelectCollection(contentCollection); }));
		}
	}

	private void SelectCollection(ContentCollection contentCollection)
	{
		for (int i = 0; i < ItemDisplayer.Children.Count; i++)
		{
			if (((ContentCollectionItem)ItemDisplayer.Children[i]).ContentCollection == contentCollection)
			{
				TextBox.Text = contentCollection.Content; // Set text
				break;
			}
		}

		Close();
	}

	private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
	{
		WindowState = WindowState.Minimized; // Minimize
	}

	private void CloseBtn_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}
}
