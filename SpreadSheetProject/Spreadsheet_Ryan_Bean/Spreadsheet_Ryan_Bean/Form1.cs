// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_Ryan_Bean
{
    using SpreadsheetEngine;

    /// <summary>
    /// Where I'll do my stuff.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// The spreadsheet where I do my calculations.
        /// </summary>
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

            this.spreadsheet.CellPropertyChanged += this.ACellPropertyHasChanged;
            this.dataGridView1.CellEndEdit += this.DataGridView1_CellEndEdit;
            this.dataGridView1.CellBeginEdit += this.DataGridView1_CellBeginEdit;
            this.changeBackgroundColorToolStripMenuItem.Click += this.ChangeBackgroundColorToolStripMenuItem_Click;
            this.undoToolStripMenuItem.Click += this.UndoToolStripMenuItem_Click;
            this.redoToolStripMenuItem.Click += this.RedoToolStripMenuItem_Click;
            this.undoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Enabled = false;
            this.saveToFileToolStripMenuItem.Click += this.SaveToFileToolStripMenuItem_Click;
            this.loadFromFileToolStripMenuItem.Click += this.LoadFromFileToolStripMenuItem_Click;
        }

        /// <summary>
        /// Fires when the user selects load from file in the menu.
        /// </summary>
        /// <param name="sender"> Sender. </param>
        /// <param name="e"> Event Args. </param>
        private void LoadFromFileToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "XML Files (*.xml)|*.xml";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.spreadsheet.ClearSpreadsheet();
                Stream fStream;
                if ((fStream = fileDialog.OpenFile()) != null)
                {
                    this.spreadsheet.LoadFromFile(fStream);
                    fStream.Close();
                }
            }
        }

        /// <summary>
        /// Fires when the user selects save to file in the menu.
        /// </summary>
        /// <param name="sender"> Sender. </param>
        /// <param name="e"> Event args. </param>
        private void SaveToFileToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "XML Files (*.xml)|*.xml";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream fStream;
                if ((fStream = fileDialog.OpenFile()) != null)
                {
                    this.spreadsheet.SaveToFile(fStream);
                    fStream.Close();
                }
            }
        }

        /// <summary>
        /// Fires when the user selects the Redo option.
        /// </summary>
        /// <param name="sender"> Sender. </param>
        /// <param name="e"> Event Args. </param>
        private void RedoToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            this.spreadsheet.Redo();
            this.UpdateUndoRedoButtons();
        }

        /// <summary>
        /// Fires when the user selects the Undo menu option.
        /// </summary>
        /// <param name="sender"> Sender. </param>
        /// <param name="e"> Event Args. </param>
        private void UndoToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            this.spreadsheet.Undo();
            this.UpdateUndoRedoButtons();
        }

        /// <summary>
        /// Fires when the user selects Change Background color menu option.
        /// </summary>
        /// <param name="sender"> Sender. </param>
        /// <param name="e"> Event Args. </param>
        private void ChangeBackgroundColorToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = false;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // object[] undoParameters = new object[2];
                // undoParameters[0] = this.dataGridView1.SelectedCells;
                // undoParameters[0] = (uint)this.dataGridView1.SelectedCells[0].Style.BackColor.ToArgb();
                // this.spreadsheet.AddUndo("SetCellsBackGroundColors", this, undoParameters, "Cell Color Change");
                // this.SetCellsBackGroundColors(this.dataGridView1.SelectedCells, (uint)colorDialog.Color.ToArgb());
                for (int i = 0; i < this.dataGridView1.SelectedCells.Count; i++)
                {
                    int row = this.dataGridView1.SelectedCells[i].RowIndex;
                    int col = this.dataGridView1.SelectedCells[i].ColumnIndex;

                    object[] param =
                    {
                        col,
                        row,
                        this.spreadsheet.GetCell(col, row).BGColor,
                    };
                    this.spreadsheet.AddUndo("SetCellBackGroundColor", this.spreadsheet, param, "Cell Color Change");
                    this.spreadsheet.SetCellBackGroundColor(col, row, (uint)colorDialog.Color.ToArgb());
                }
            }
        }

        /// <summary>
        /// Sets the background color of a collection of cells.
        /// </summary>
        /// <param name="cells"> The cells to change the color of. </param>
        /// <param name="color"> The color to change the cells to. </param>
        private void SetCellsBackGroundColors(DataGridViewSelectedCellCollection cells, uint color)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                int row = cells[i].RowIndex;
                int col = cells[i].ColumnIndex;
                this.spreadsheet.SetCellBackGroundColor(col, row, color);
            }
        }

        /// <summary>
        /// Fires when a cells text starts to change.
        /// </summary>
        /// <param name="sender"> datagridview object. </param>
        /// <param name="e"> the cell that is changing. </param>
        private void DataGridView1_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            string cellText = (string)this.dataGridView1.CurrentCell.Value;
            if (cellText == null)
            {
                cellText = string.Empty;
            }

            object[] param =
                    {
                        col,
                        row,
                        cellText,
                    };

            this.spreadsheet.AddUndo("SetCellText", this.spreadsheet, param, "Text Change");

            this.spreadsheet.SetCellText(col, row, cellText);
        }

        /// <summary>
        /// Fires whenever you tab or enter out of a cell.
        /// </summary>
        /// <param name="sender"> the datagridview object. </param>
        /// <param name="e"> I don't know what this even equals. </param>
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

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            this.spreadsheet.GetCell(col, row).Text = cellText;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        /// <summary>
        /// Fires whenever a cell property changes.
        /// </summary>
        /// <param name="sender"> The cell which changed. </param>
        /// <param name="e"> Whether or not the text or value changed. </param>
        private void ACellPropertyHasChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (((SpreadsheetEngine.Cell)sender) != null && e.PropertyName == "Value")
            {
                this.dataGridView1.Rows[((SpreadsheetEngine.Cell)sender).RowIndex].Cells[((SpreadsheetEngine.Cell)sender).ColumnIndex].Value = ((SpreadsheetEngine.Cell)sender).Value;
            }
            else if ((Cell)sender != null && e.PropertyName == "BGColor")
            {
                this.dataGridView1.Rows[((Cell)sender).RowIndex].Cells[((Cell)sender).ColumnIndex].Style.BackColor = Color.FromArgb((int)((Cell)sender).BGColor);
            }

            this.UpdateUndoRedoButtons();
        }

        private void UpdateUndoRedoButtons()
        {
            if (this.spreadsheet.UndosAvailable())
            {
                this.undoToolStripMenuItem.Enabled = true;
                this.undoToolStripMenuItem.Text = "Undo " + this.spreadsheet.GetTypeUndo();
            }
            else
            {
                this.undoToolStripMenuItem.Enabled = false;
                this.undoToolStripMenuItem.Text = "Undo";
            }

            if (this.spreadsheet.RedosAvailable())
            {
                this.redoToolStripMenuItem.Enabled = true;
                this.redoToolStripMenuItem.Text = "Redo " + this.spreadsheet.GetTypeRedo();
            }
            else
            {
                this.redoToolStripMenuItem.Enabled = false;
                this.redoToolStripMenuItem.Text = "Redo";
            }
        }
    }
}