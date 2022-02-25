// <copyright file="Cell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;

    /// <summary>
    /// Derived class of the cell class so I can instantiate it.
    /// </summary>
    public class Cell : CellBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="newColumnIndex"> The Index of the column. </param>
        /// <param name="newRowIndex"> The Index of the row. </param>
        public Cell(int newColumnIndex, int newRowIndex)
            : base(newColumnIndex, newRowIndex)
        {
        }
    }
}
