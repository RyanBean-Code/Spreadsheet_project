// <copyright file="MultiplicationNode.cs" company="PlaceholderCompany">
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
    /// summary.
    /// </summary>
    public class MultiplicationNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationNode"/> class.
        /// </summary>
        /// <param name="op"> * operator. </param>
        /// <param name="p"> Presenence. </param>
        /// <param name="a"> Associativity. </param>
        public MultiplicationNode(char op = '*', int p = 2, Associativity a = Associativity.Left)
            : base(op, p, a)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationNode"/> class.
        /// </summary>
        public MultiplicationNode()
        {
            this.Operator = '*';
            this.Presenence = 2;
            this.Associativity = Associativity.Left;
        }
    }
}
