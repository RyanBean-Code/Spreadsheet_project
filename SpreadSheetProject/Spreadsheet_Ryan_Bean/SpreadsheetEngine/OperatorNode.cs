// <copyright file="OperatorNode.cs" company="PlaceholderCompany">
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
    /// This node represents and operator, eg '+' or '*'.
    /// </summary>
    public class OperatorNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="newOperator"> The operator which the node is. </param>
        /// /// <param name="newPresenence"> Presenence. </param>
        /// <param name="newAssociativity"> Associativity. </param>
        public OperatorNode(char? newOperator = null, int? newPresenence = null, Associativity newAssociativity = Associativity.Left)
        {
            this.Operator = newOperator;
            this.Presenence = newPresenence;
            this.Associativity = newAssociativity;
            this.Left = this.Right = null;
        }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        public char? Operator { get; set; }

        /// <summary>
        /// Gets or sets the presenence of the operator.
        /// </summary>
        public int? Presenence { get; set; }

        /// <summary>
        /// Gets or sets the associativity of the operator.
        /// </summary>
        public Associativity Associativity { get; set; }

        /// <summary>
        /// Gets or sets the left node.
        /// </summary>
        public Node? Left { get; set; }

        /// <summary>
        /// Gets or sets the right node.
        /// </summary>
        public Node? Right { get; set; }
    }
}
    