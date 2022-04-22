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
        /// A list of all the cells whose values depend on this cell.
        /// </summary>
        public List<Cell> dependantCells;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="newColumnIndex"> The Index of the column. </param>
        /// <param name="newRowIndex"> The Index of the row. </param>
        public Cell(int newColumnIndex, int newRowIndex)
            : base(newColumnIndex, newRowIndex)
        {
            this.dependantCells = new List<Cell>();
        }

        /// <summary>
        /// Gets the numerical representation of the cell.
        /// </summary>
        public int CellNumber
        {
            get
            {
                return (this.RowIndex * 26) + this.ColumnIndex;
            }
        }
    }
}
