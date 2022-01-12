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
using Datalya.Pages.FirstRun;
using System.Windows;

namespace Datalya.Windows
{
	/// <summary>
	/// Interaction logic for FirstRunWindow.xaml
	/// </summary>
	public partial class FirstRunWindow : Window
	{
		readonly WelcomeFirstRunPage welcomeFirstRunPage;
		readonly IntroFirstRunPage introFirstRunPage;
		readonly ThemeFirstRunPage themeFirstRunPage;

		public FirstRunWindow()
		{
			InitializeComponent();

			welcomeFirstRunPage = new(this);
			introFirstRunPage = new(this);
			themeFirstRunPage = new(this);

			ChangePage(0); // Show welcome page
		}

		private void CloseBtn_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		public void ChangePage(int id)
		{
			PageFrame.Content = id switch
			{
				0 => welcomeFirstRunPage,
				1 => introFirstRunPage,
				2 => themeFirstRunPage,
				_ => welcomeFirstRunPage
			}; // Set content
		}
	}
}
