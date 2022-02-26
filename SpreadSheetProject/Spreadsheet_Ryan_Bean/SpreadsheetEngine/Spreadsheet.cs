// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Class for implementations with the Spreadsheet.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// 2D array of cells.
        /// </summary>
        public Cell[,] cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="numRows"> Number of Rows in the Spreadsheet. </param>
        /// <param name="numColumns"> Number of Columns in the Spreadsheet. </param>
        public Spreadsheet(int numRows, int numColumns)
        {
            this.cells = new Cell[numColumns, numRows];
            this.InitializeCells(numColumns, numRows);
        }

        /// <summary>
        /// Event Handler for when a cell changes.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged = delegate { };

        /// <summary>
        /// Gets the number of columns in the Spreadsheet.
        /// </summary>
        public int ColumnCount
        {
            get { return this.cells.GetLength(0); }
        }

        /// <summary>
        /// Gets the number of rows in the Spreadsheet.
        /// </summary>
        public int RowCount
        {
            get { return this.cells.GetLength(1); }
        }

        /// <summary>
        /// Returns the cell at the given indices.
        /// </summary>
        /// <param name="cIndex"> Column Index. </param>
        /// <param name="rIndex"> Row Index. </param>
        /// <returns> The Cell at the Given indices or null if the cell does not exist. </returns>
        public Cell? GetCell(int cIndex, int rIndex)
        {
            if (cIndex <= this.cells.GetLength(0) + 1 && rIndex <= this.cells.GetLength(1) + 1)
            {
                return this.cells[cIndex, rIndex];
            }

            return null;
        }

        /// <summary>
        /// Initializes all the cells in the spreadsheet.
        /// </summary>
        private void InitializeCells(int numColumns, int numRows)
        {
            for (int cIndex = 0; cIndex < numColumns; cIndex++)
            {
                for (int rIndex = 0; rIndex < numRows; rIndex++)
                {
                    this.cells[cIndex, rIndex] = new Cell(cIndex, rIndex);
                    this.cells[cIndex, rIndex].PropertyChanged += this.Spreadsheet_PropertyChanged;
                }
            }
        }

        /// <summary>
        /// Fires whenever a cell changes.
        /// </summary>
        /// <param name="sender"> The cell which changed. </param>
        /// <param name="e"> Whether the value or text changed. </param>
        private void Spreadsheet_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Cell? senderCell = (Cell)sender;
            if (senderCell != null)
            {
                if (e.PropertyName == "Text")
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (senderCell.Text.StartsWith('='))
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    {
                        string formula = senderCell.Text.Substring(1);
                        int col = char.ToUpper(Convert.ToChar(formula[0])) - 65;
                        int row = int.Parse(System.Text.RegularExpressions.Regex.Match(formula, @"\d+").Value) - 1;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        senderCell.Value = this.GetCell(col, row).Value;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }
                    else
                    {
                        senderCell.Value = senderCell.Text;
                    }
                }

                this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
            }
        }
    }
}
