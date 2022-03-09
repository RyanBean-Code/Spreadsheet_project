using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// This node represents and operator, eg '+' or '*'.
    /// </summary>
    public class OperatorNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="newOperator"> The operator which the node is. </param>
        public OperatorNode(char newOperator)
        {
            this.Operator = newOperator;
            this.Left = this.Right = null;
        }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        public char Operator { get; set; }

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
