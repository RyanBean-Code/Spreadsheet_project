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
        }

        /// <summary>
        /// This is called when the value of a cell changes.
        /// </summary>
        /// <param name="sender"> the datagrid view object. </param>
        /// <param name="e"> the event when the cell changes. </param>
        public void CellChanged(object sender, EventArgs e)
        {
            int row = this.dataGridView1.CurrentCell.RowIndex;
            int col = this.dataGridView1.CurrentCell.ColumnIndex;
            if (this.dataGridView1.CurrentCell.Value.ToString().StartsWith('='))
            {
                this.dataGridView1.CurrentCell.Value = this.spreadsheet.UpdateCell(row, col, (string)this.dataGridView1.CurrentCell.Value);
            }
            else
            {
                this.spreadsheet.GetCell(col, row).Text = (string)this.dataGridView1.CurrentCell.Value;
            }
        }
    }
}