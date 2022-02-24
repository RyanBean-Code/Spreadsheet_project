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
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// Constructor for the Form.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            char columnName = 'A';
            for (int i = 0; i < 26; i++)
            {
                this.dataGridView1.Columns.Add(columnName.ToString(), columnName.ToString());
                columnName++;
            }

            for (int i = 1; i < 51; i++)
            {
                this.dataGridView1.Rows.Add();
                this.dataGridView1.RowTemplate.HeaderCell.Value = i.ToString();
            }
        }
    }
}