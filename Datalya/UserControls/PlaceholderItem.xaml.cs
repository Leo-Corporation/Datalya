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
using System.Windows.Controls;

namespace Datalya.UserControls;

/// <summary>
/// Interaction logic for PlaceholderItem.xaml
/// </summary>
public partial class PlaceholderItem : UserControl
{
	public string Title { get; set; }
	public string Description { get; set; }
	public string Icon { get; set; }
	public PlaceholderItem(string title, string description, string icon)
	{
		InitializeComponent();
		Title = title; // Set value
		Description = description; // Set value
		Icon = icon; // Set value

		InitUI(); // Load the UI
	}

	private void InitUI()
	{
		TitleTxt.Text = Title; // Set text
		DescriptionTxt.Text = Description; // Set text
		IconTxt.Text = Icon; // Set text
	}
}
