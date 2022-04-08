﻿// <copyright file="UndoRedoCollection.cs" company="PlaceholderCompany">
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
        private object[] parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedoCollection"/> class.
        /// </summary>
        /// <param name="newMethodName"> The name of the method. </param>
        /// <param name="newOwner"> The owner of the method. </param>
        /// <param name="newParameters"> The parameters for the method. </param>
        /// <param name="newUndoRedoName"> The name of the action. </param>
        public UndoRedoCollection(string newMethodName, object newOwner, object[] newParameters, string? newUndoRedoName = null)
        {
            this.methodName = newMethodName;
            this.owner = newOwner;
            this.parameters = newParameters;
            this.UndoRedoName = newUndoRedoName;
        }

        /// <summary>
        /// Gets or sets the undoRedoName
        /// </summary>
        public string? UndoRedoName { get; set; }

        /// <summary>
        /// Gets the array of parameters for the action.
        /// </summary>
        public object[] Parameters
        {
            get
            {
                return this.parameters;
            }
        }

        /// <summary>
        /// Gets the owner of the method.
        /// </summary>
        public object Owner
        {
            get
            {
                return this.owner;
            }
        }

        /// <summary>
        /// Gets the name of the method that is called.
        /// </summary>
        public string MethodName
        {
            get
            {
                return this.methodName;
            }
        }

        /// <summary>
        /// Calls the method which will either undo or redo the action.
        /// </summary>
        public void PerformUndoRedo()
        {
            MethodInfo method = this.owner.GetType().GetMethod(this.methodName);
            method.Invoke(this.owner, this.parameters);
        }
    }
}
