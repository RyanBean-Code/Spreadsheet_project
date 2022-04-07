// <copyright file="UndoRedoCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class which represents an undo or redo action.
    /// </summary>
    internal class UndoRedoCollection
    {
        /// <summary>
        /// Represents the method name needed to make a undo or redo action.
        /// </summary>
        private string methodName;

        /// <summary>
        /// Represents the owner of the method name.
        /// </summary>
        private object owner;

        /// <summary>
        /// Represents the list of parameters in the method call.
        /// </summary>
        private List<object> parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedoCollection"/> class.
        /// </summary>
        /// <param name="newMethodName"> The name of the method. </param>
        /// <param name="newOwner"> The owner of the method. </param>
        /// <param name="newParameters"> The parameters for the method. </param>
        public UndoRedoCollection(string newMethodName, object newOwner, List<object> newParameters, string? newUndoRedoName = null)
        {
            this.methodName = newMethodName;
            this.owner = newOwner;
            this.parameters = newParameters;
            this.UndoRedoName = newUndoRedoName;
        }

        /// <summary>
        /// Gets or sets the undoRedoName
        /// </summary>
        public string? UndoRedoName { get; set; };

        /// <summary>
        /// Calls the method which will either undo or redo the action.
        /// </summary>
        public void PerformUndoRedo()
        {
            MethodInfo method = this.owner.GetType().GetMethod(this.methodName, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            method.Invoke(this.owner, new object[] { this.parameters });
        }
    }
}
