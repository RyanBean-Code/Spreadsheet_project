// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace SpreadsheetEngine
{
    using System;
    using System.ComponentModel;
    using CptS321;

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
            if (cIndex < this.cells.GetLength(0) && rIndex <= this.cells.GetLength(1) && cIndex >= 0 && rIndex >= 0)
            {
                return this.cells[cIndex, rIndex];
            }

            return null;
        }

        /// <summary>
        /// Gets the cell at the given string coordinate.
        /// </summary>
        /// <param name="coordinate"> The cells location, eg. "A2". </param>
        /// <returns> The cell at the given location. </returns>
        public Cell? GetCellAtStringCoordinate(string? coordinate)
        {
            if (string.IsNullOrEmpty(coordinate))
            {
                return null;
            }

            int col = char.ToUpper(Convert.ToChar(coordinate[0])) - 65;
            int row = int.Parse(System.Text.RegularExpressions.Regex.Match(coordinate, @"\d+").Value) - 1;
            return this.GetCell(col, row);
        }

        /// <summary>
        /// Sets the Value for a cell.
        /// Is used in the form class in the CellBeginEdit event.
        /// </summary>
        /// <param name="cIndex"> Column index of the cell. </param>
        /// <param name="rIndex"> Row index of the cell. </param>
        /// <param name="value"> New value of the cell. </param>
        public void SetCellValue(int cIndex, int rIndex, string value)
        {
            this.GetCell(cIndex, rIndex).Value = value;
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
                    if (senderCell.Text.StartsWith('='))
                    {
                        //senderCell.Value = this.GetCellAtStringCoordinate(senderCell.Text.Substring(1)).Value;
                        this.SetCellValue(senderCell);
                    }
                    else
                    {
                        senderCell.Value = senderCell.Text;
                    }
                }

                this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
            }
        }

        /// <summary>
        /// Used to set the value of a cell when the text starts with '=' character.
        /// </summary>
        /// <param name="cell"> The Cell value to set. </param>
        private void SetCellValue(Cell cell)
        {
            bool isValid = true;
            ExpressionTree exp = new ExpressionTree(cell.Text.Substring(1));
            List<string> varibles = exp.GetVariableNames();
            foreach (string varName in varibles)
            {
                string varValueStr = this.GetCellAtStringCoordinate(varName).Value;
                double varValue;
                if (!string.IsNullOrEmpty(varValueStr) && double.TryParse(varValueStr, out varValue))
                {
                    exp.SetVariable(varName, varValue);
                }
                else
                {
                    isValid = false;
                }
            }

            if (isValid)
            {
                cell.Value = Convert.ToString(exp.Evaluate());
            }
            else
            {
                cell.Value = cell.Text;
            }
        }
    }
}
