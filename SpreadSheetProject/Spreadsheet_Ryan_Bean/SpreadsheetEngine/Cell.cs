﻿// <copyright file="Cell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;

    /// <summary>
    /// This is the cell class which will represent the cells of my sheet.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// The text that is inside the cell.
        /// </summary>
        protected string? text;

        /// <summary>
        /// This represents the value of teh text in the string.
        /// If the text doesn't start with the '=' character than this will just equal the text.
        /// </summary>
        protected string? value;

        /// <summary>
        /// The index of the Row the cell is.
        /// </summary>
        private int rowIndex;

        /// <summary>
        /// The index of the column the cell is.
        /// </summary>
        private int columnIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// Constuctor for the Cell class.
        /// Sets teh row and column index of the cell.
        /// </summary>
        /// <param name="newRowIndex"> Value for the row index. </param>
        /// <param name="newColumnIndex"> Value for the Column index. </param>
        public Cell(int newRowIndex, int newColumnIndex)
        {
            this.rowIndex = newRowIndex;
            this.columnIndex = newColumnIndex;
        }

        /// <summary>
        /// The event that is triggered when new text is added in the textbox.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Gets or sets the text is the textbox.
        /// Can return the text that is inside the cell or set it.
        /// When the text is set that event handler is called.
        /// </summary>
        public string? Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text != value)
                {
                    this.text = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        /// <summary>
        /// Gets the value member variable.
        /// </summary>
        public string? Value
        {
            get
            {
                return this.value;
            }
        }

        /// <summary>
        /// Gets the column index.
        /// Readonly.
        /// </summary>
        public int ColumnIndex
        {
            get { return this.columnIndex; }
        }

        /// <summary>
        /// Gets the row index.
        /// Readonly.
        /// </summary>
        public int RowIndex
        {
            get { return this.rowIndex; }
        }
    }
}