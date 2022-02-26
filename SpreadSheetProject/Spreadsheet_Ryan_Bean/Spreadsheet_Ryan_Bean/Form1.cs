// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_Ryan_Bean
{
    /// <summary>
    /// Where I'll do my stuff.
    /// </summary>
    public partial class Form1 : Form
    {
        private SpreadsheetEngine.Spreadsheet spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// Constructor for the Form.
        /// </summary>
        public Form1()
        {
            this.spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            this.InitializeComponent();
            char columnName = 'A';
            for (int i = 0; i < 26; i++)
            {
                this.dataGridView1.Columns.Add(columnName.ToString(), columnName.ToString());
                columnName++;
            }

            for (int i = 1; i < 51; i++)
            {
                this.dataGridView1.RowTemplate.HeaderCell.Value = i.ToString();
                this.dataGridView1.Rows.Add();
            }

            this.spreadsheet.CellPropertyChanged += ACellPropertyHasChanged;
            this.dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
        }

        private void DataGridView1_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            string cellText = string.Empty;
            try
            {
                cellText = (string)this.dataGridView1.CurrentCell.Value;
            }
            catch (NullReferenceException)
            {
                cellText = string.Empty;
            }

            this.spreadsheet.GetCell(col, row).Text = cellText;
        }

        private void ACellPropertyHasChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (((SpreadsheetEngine.Cell)sender) != null && e.PropertyName == "Value")
            {
                this.dataGridView1.Rows[((SpreadsheetEngine.Cell)sender).RowIndex].Cells[((SpreadsheetEngine.Cell)sender).ColumnIndex].Value = ((SpreadsheetEngine.Cell)sender).Value;
            }
        }
    }
}