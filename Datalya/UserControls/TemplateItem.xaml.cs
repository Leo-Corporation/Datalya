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
using Datalya.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Datalya.UserControls;

/// <summary>
/// Interaction logic for TemplateItem.xaml
/// </summary>
public partial class TemplateItem : UserControl
{
	BlockTemplate BlockTemplate { get; set; }
	TemplateWindow TemplateWindow { get; init; }
	public TemplateItem(BlockTemplate blockTemplate, TemplateWindow templateWindow)
	{
		InitializeComponent();
		BlockTemplate = blockTemplate; // Set
		TemplateWindow = templateWindow; // Set

		InitUI(); // Load the UI
	}

	private void InitUI()
	{
		NameTxt.Text = BlockTemplate.Name; // Set text
		MouseEnter += (o, e) => { BorderContent.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background2"].ToString()) }; };
		MouseLeave += (o, e) => { BorderContent.Background = new SolidColorBrush { Color = (Color)ColorConverter.ConvertFromString(App.Current.Resources["Background3"].ToString()) }; };
	}

	private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
	{
		if (!TemplateWindow.IsFromCreator)
		{
			TemplateWindow.NewDataBaseWindow.BlockTemplate = BlockTemplate; // Set value
			TemplateWindow.NewDataBaseWindow.TemplateNameTxt.Text = BlockTemplate.Name; // Set text
			TemplateWindow.NewDataBaseWindow.InitUI();
			TemplateWindow.Close(); // Close 
		}
		else
		{
			for (int i = 0; i < BlockTemplate.Blocks.Count; i++)
			{
				Global.CurrentDataBase.Blocks.Add(BlockTemplate.Blocks[i]); // Add block
			}
			Global.CreatorPage.InitUI(); // Refresh
			Global.DatabasePage.InitUI(); // Refresh
			TemplateWindow.Close(); // Close 
		}
	}
}
