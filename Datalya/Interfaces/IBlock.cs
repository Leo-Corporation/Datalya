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
using Datalya.Enums;
using System;

namespace Datalya.Interfaces;

[Obsolete("Use the Block class instead")]
public interface IBlock
{

	/// <summary>
	/// The name given by the user to the block.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// The <see cref="Enums.BlockType"/> of the block.
	/// </summary>
	public BlockType BlockType { get; }

	/// <summary>
	/// The value of the block
	/// </summary>
	public string BlockValue { get; set; }

	/// <summary>
	/// Changes the name.
	/// </summary>
	/// <param name="name">The new name.</param>
	public void ChangeName(string name);
}
