// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace SpreadsheetEngine
{
    using System;
    using System.ComponentModel;
    using System.Xml;
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

        private Stack<UndoRedoCollection> undos;

        private Stack<UndoRedoCollection> redos;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="numRows"> Number of Rows in the Spreadsheet. </param>
        /// <param name="numColumns"> Number of Columns in the Spreadsheet. </param>
        public Spreadsheet(int numRows, int numColumns)
        {
            this.cells = new Cell[numColumns, numRows];
            this.InitializeCells(numColumns, numRows);
            this.undos = new Stack<UndoRedoCollection>();
            this.redos = new Stack<UndoRedoCollection>();
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
        /// This method adds an undo the the undos member.
        /// </summary>
        /// <param name="methodName"> Name of the method to call. </param>
        /// <param name="methodOwner"> Owner of the method. </param>
        /// <param name="paramaters"> Parameters for the method. </param>
        /// <param name="undoName"> name of the undo example "Text Change". </param>
        public void AddUndo(string methodName, object methodOwner, object[] paramaters, string undoName)
        {
            this.undos.Push(new UndoRedoCollection(methodName, methodOwner, paramaters, undoName));
        }

        /// <summary>
        /// Does the undo.
        /// </summary>
        public void Undo()
        {
            if (this.undos.Count > 0)
            {
                UndoRedoCollection undo = this.undos.Pop();

                // object[] redoParameters = new object[3];
                // redoParameters[0] = undo.Parameters[0];
                // redoParameters[1] = undo.Parameters[1];
                // if (undo.UndoRedoName == "Text Change")
                // {
                //     redoParameters[2] = this.GetCell((int)redoParameters[0], (int)redoParameters[1]).Text;
                // }
                // else if (undo.UndoRedoName == "Cell Color Change")
                // {
                //     redoParameters[2] = this.GetCell((int)redoParameters[0], (int)redoParameters[1]).BGColor;
                // }
                this.redos.Push(new UndoRedoCollection(undo.MethodName, undo.Owner, this.CreateNewUndoRedoParameters(undo), undo.UndoRedoName));
                undo.PerformUndoRedo();
            }
        }

        /// <summary>
        /// Does a redo.
        /// </summary>
        public void Redo()
        {
            if (this.redos.Count > 0)
            {
                UndoRedoCollection redo = this.redos.Pop();

                // this.undos.Push(redo);
                this.undos.Push(new UndoRedoCollection(redo.MethodName, redo.Owner, this.CreateNewUndoRedoParameters(redo), redo.UndoRedoName));
                redo.PerformUndoRedo();
            }
        }

        /// <summary>
        /// Check if you can perform an undo action.
        /// </summary>
        /// <returns> True if the undo stack isn't empty. </returns>
        public bool UndosAvailable()
        {
            return this.undos.Count > 0;
        }

        /// <summary>
        /// Check if you can perform a redo action.
        /// </summary>
        /// <returns> True is the redo stack isn't empty. </returns>
        public bool RedosAvailable()
        {
            return this.redos.Count > 0;
        }

        /// <summary>
        /// Resets the spreasheet.
        /// Used before we load in a file.
        /// </summary>
        public void ClearSpreadsheet()
        {
            foreach (Cell cell in this.cells)
            {
                cell.Text = string.Empty;
                cell.BGColor = 0xffffffff;
            }

            this.undos.Clear();
            this.redos.Clear();
        }

        /// <summary>
        /// Gets the type of the undo the user can perform.
        /// </summary>
        /// <returns> The UndoRedoName Property of the undo on top of the stack. </returns>
        public string? GetTypeUndo()
        {
            return this.undos.Peek().UndoRedoName;
        }

        /// <summary>
        /// Gets the type of the redo the user can perform.
        /// </summary>
        /// <returns> The UndoRedoName property of the redo on top of the stack. </returns>
        public string? GetTypeRedo()
        {
            return this.redos.Peek().UndoRedoName;
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
        public void SetCellText(int cIndex, int rIndex, string value)
        {
            this.GetCell(cIndex, rIndex).Text = value;
        }

        /// <summary>
        /// Sets the value of the background color inn the cell.
        /// </summary>
        /// <param name="cIndex"> Index of the column. </param>
        /// <param name="rIndex"> Index of the Row. </param>
        /// <param name="color"> A unint value of the color. </param>
        public void SetCellBackGroundColor(int cIndex, int rIndex, uint color)
        {
            this.cells[cIndex, rIndex].BGColor = color;
        }

        /// <summary>
        /// Called to create an XML file to save the data in the spreadsheet.
        /// </summary>
        /// <param name="fileStream"> The stream to write to. </param>
        public void SaveToFile(Stream fileStream)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlWriter.Create(fileStream, settings))
            {
                writer.WriteStartElement("spreadsheet");
                foreach (Cell cell in this.cells)
                {
                    if (!string.IsNullOrEmpty(cell.Text) || cell.BGColor != 0xffffffff)
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteAttributeString("name", this.GetCellName(cell));
                        writer.WriteElementString("bgcolor", cell.BGColor.ToString());
                        writer.WriteElementString("text", cell.Text);
                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Called to load data from an XML file.
        /// </summary>
        /// <param name="fileStream"> The stream to read from. </param>
        public void LoadFromFile(Stream fileStream)
        {
            using (XmlReader reader = XmlReader.Create(fileStream))
            {
                while (reader.Read())
                {
                    if (!reader.IsEmptyElement)
                    {
                        if (reader.IsStartElement() && (reader.Name == "spreadsheet"))
                        {
                            string cellName = string.Empty;
                            uint cellColor = 0xffffffff;
                            string cellText = string.Empty;
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Element)
                                {
                                    if (reader.Name == "cell")
                                    {
                                        cellName = reader.GetAttribute("name");
                                    }
                                    else if (reader.Name == "bgcolor")
                                    {
                                        uint.TryParse(reader.ReadElementContentAsString(), out cellColor);
                                    }
                                    else if (reader.Name == "text")
                                    {
                                        cellText = reader.ReadElementContentAsString();
                                    }
                                }

                                if (!string.IsNullOrEmpty(cellName))
                                {
                                    this.GetCellAtStringCoordinate(cellName).Text = cellText;
                                    this.GetCellAtStringCoordinate(cellName).BGColor = cellColor;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates parameters for undoing and redoing.
        /// </summary>
        /// <param name="prevUndoRedo"> The previous undo or redo action. </param>
        /// <returns> An object[] which stores the parameters for the function. </returns>
        private object[] CreateNewUndoRedoParameters(UndoRedoCollection prevUndoRedo)
        {
            object[] redoParameters = new object[3];
            redoParameters[0] = prevUndoRedo.Parameters[0];
            redoParameters[1] = prevUndoRedo.Parameters[1];
            if (prevUndoRedo.UndoRedoName == "Text Change")
            {
                redoParameters[2] = this.GetCell((int)redoParameters[0], (int)redoParameters[1]).Text;
            }
            else if (prevUndoRedo.UndoRedoName == "Cell Color Change")
            {
                redoParameters[2] = this.GetCell((int)redoParameters[0], (int)redoParameters[1]).BGColor;
            }

            return redoParameters;
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
                    if (senderCell.Text.StartsWith('=') && senderCell.Text.Length > 1)
                    {
                        this.SetCellValue(senderCell);
                    }
                    else
                    {
                        senderCell.Value = senderCell.Text;
                    }
                }

                // This is needed to keep updating the value of the cells if another one changes.
                else if (e.PropertyName == "Value")
                {
                    this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
                }
                else if (e.PropertyName == "BGColor")
                {
                    this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("BGColor"));
                }
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
                Cell cellVaribale = this.GetCellAtStringCoordinate(varName);
                double varValue;
                if (!string.IsNullOrEmpty(cellVaribale.Value) && double.TryParse(cellVaribale.Value, out varValue))
                {
                    exp.SetVariable(varName, varValue);
                }
                else
                {
                    isValid = false;
                }

                cellVaribale.dependantCells.Add(cell);
                cellVaribale.PropertyChanged -= this.CellVaribale_PropertyChanged;
                cellVaribale.PropertyChanged += this.CellVaribale_PropertyChanged;
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

        /// <summary>
        /// Used to update a cells value when one on the variables in the cells expression changes.
        /// </summary>
        /// <param name="cell"> The cell to update. </param>
        private void UpdateCellValue(Cell cell)
        {
            bool isValid = true;
            ExpressionTree exp = new ExpressionTree(cell.Text.Substring(1));
            List<string> variables = exp.GetVariableNames();
            foreach (string varName in variables)
            {
                Cell cellVariable = this.GetCellAtStringCoordinate(varName);
                double varValue;
                if (double.TryParse(cellVariable.Value, out varValue))
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

        /// <summary>
        /// Used to check whether of not a cell is still dependant on the value of another.
        /// </summary>
        /// <param name="cellToUpdate"> The cell thats being updated. </param>
        /// <param name="cellThatChanged"> The cell that that got changed. </param>
        /// <returns> True or False. </returns>
        private bool CellIsStillDependant(Cell cellToUpdate, Cell cellThatChanged)
        {
            if (cellToUpdate.Text.StartsWith('='))
            {
                ExpressionTree exp = new ExpressionTree(cellToUpdate.Text.Substring(1));
                List<string> variables = exp.GetVariableNames();
                string cellThatChangedName = this.GetCellName(cellThatChanged);
                if (variables.Contains(cellThatChangedName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Event Handler to update cell values when one cell that is dependant on another changes.
        /// </summary>
        /// <param name="sender"> The cell that just changed. </param>
        /// <param name="e"> Should always be equal to "Value". </param>
        private void CellVaribale_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Cell senderCell = (Cell)sender;
            if (senderCell != null)
            {
                if (e.PropertyName == "Value")
                {
                    List<Cell> cellsToRemove = new List<Cell>();
                    foreach (Cell cell in senderCell.dependantCells)
                    {
                        if (this.CellIsStillDependant(cell, senderCell))
                        {
                            this.UpdateCellValue(cell);
                        }
                        else
                        {
                            cellsToRemove.Add(cell);
                        }
                    }

                    foreach (Cell cell in cellsToRemove)
                    {
                        senderCell.dependantCells.Remove(cell);
                    }

                    if (senderCell.dependantCells.Count == 0)
                    {
                        senderCell.PropertyChanged -= this.CellVaribale_PropertyChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the name of the cell ex "A1" or "F20".
        /// </summary>
        /// <param name="cell"> The cell to get the Name of. </param>
        /// <returns> The name of the cell in string form. </returns>
        private string GetCellName(Cell cell)
        {
            return Convert.ToString(Convert.ToChar(cell.ColumnIndex + 65)) + Convert.ToString(cell.RowIndex + 1);
        }
    }
}
