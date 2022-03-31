// <copyright file="AdditionNode.cs" company="PlaceholderCompany">
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
    /// Summary.
    /// </summary>
    public class AdditionNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionNode"/> class.
        /// </summary>
        /// <param name="op"> + operator. </param>
        /// <param name="p"> Presenence. </param>
        /// <param name="a"> Associativity. </param>
        public AdditionNode(char op = '+', int p = 1, Associativity a = Associativity.Left)
            : base(op, p, a)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionNode"/> class.
        /// </summary>
        public AdditionNode()
        {
            this.Operator = '+';
            this.Presenence = 1;
            this.Associativity = Associativity.Left;
        }
    }
}
