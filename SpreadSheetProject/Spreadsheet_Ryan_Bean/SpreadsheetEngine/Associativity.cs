// <copyright file="Associativity.cs" company="PlaceholderCompany">
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
    /// Used in OperatorNode class to get the associativity of the operator
    /// </summary>
    public enum Associativity
    {
        /// <summary>
        /// Left Associative.
        /// </summary>
        Left,

        /// <summary>
        /// Right Associative.
        /// </summary>
        Right,
    };

}
