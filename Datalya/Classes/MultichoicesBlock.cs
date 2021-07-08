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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalya.Classes
{
	public class MultichoicesBlock : IBlock
	{
		public string Name { get; set; }

		public BlockType BlockType => BlockType.Multichoices;

		/// <summary>
		/// Possible choices.
		/// </summary>
		public List<string> Choices { get; set; }

		public List<string> SelectedChoices { get; set; }

		public string BlockValue { get; set; }

		public MultichoicesBlock(string name)
		{
			Name = name;
			Choices = new();
			SelectedChoices = new();
		}

		public void ChangeName(string name) => Name = name;

		public override string ToString()
		{
			string result = ""; // Final result string

			for (int i = 0; i < SelectedChoices.Count; i++)
			{
				string s = i == SelectedChoices.Count - 1 ? "" : ", "; // Set
				result += $"{SelectedChoices[i]}{s}"; // Add text to string
			}
			BlockValue = result;
			return result;
		}
	}
}
