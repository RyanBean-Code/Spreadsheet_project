// <copyright file="UndoRedoCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class which represents an undo or redo action.
    /// </summary>
    internal class UndoRedoCollection
    {
        private string methodName;

        private object owner;

        private List<object> parameters;

        public UndoRedoCollection(string newMethodName, object newOwner, List<object> newParameters)
        {
            this.methodName = newMethodName;
            this.owner = newOwner;
            this.parameters = newParameters;
        }
    }
}
