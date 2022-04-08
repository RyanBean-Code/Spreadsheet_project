// <copyright file="CellBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;

    /// <summary>
    /// This is the cell class which will represent the cells of my sheet.
    /// </summary>
    public abstract class CellBase : INotifyPropertyChanged
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
        /// Represents the color of the cell, I was hoping to do this with just the property but I kept getting
        /// Stackoverflow exceptions.
        /// </summary>
        protected uint color;

        /// <summary>
        /// The index of the Row the cell is.
        /// </summary>
        private int rowIndex;

        /// <summary>
        /// The index of the column the cell is.
        /// </summary>
        private int columnIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellBase"/> class.
        /// Constuctor for the Cell class.
        /// Sets the row and column index of the cell.
        /// </summary>
        /// <param name="newRowIndex"> Value for the row index. </param>
        /// <param name="newColumnIndex"> Value for the Column index. </param>
        public CellBase(int newColumnIndex, int newRowIndex)
        {
            this.rowIndex = newRowIndex;
            this.columnIndex = newColumnIndex;
            this.text = this.value = string.Empty;
            this.color = 0xffffffff;
        }

        /// <summary>
        /// The event that is triggered when new text is added in the textbox.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        /// <summary>
        /// Gets or sets the background color of the cell.
        /// </summary>
        public uint BGColor
        {
            get
            {
                return this.color;
            }

            set
            {
                this.color = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("BGColor"));
            }
        }

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
        /// Gets or sets the value member variable.
        /// </summary>
        public string? Value
        {
            get
            {
                return this.value;
            }

            protected internal set
            {
                if (value != this.value)
                {
                    this.value = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }
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

        /// <summary>
        /// I made this because in the Spreadsheet class I need to set the value of the column and row index for each cell.
        /// </summary>
        /// <param name="newColumnIndex"> Value for the columnIndex member. </param>
        /// <param name="newRowIndex"> Value for the rowIndex member. </param>
        public void SetCellColumnAndRowIndex(int newColumnIndex, int newRowIndex)
        {
            this.columnIndex = newColumnIndex;
            this.rowIndex = newRowIndex;
        }
    }
}