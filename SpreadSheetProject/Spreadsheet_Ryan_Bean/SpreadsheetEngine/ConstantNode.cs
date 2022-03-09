using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// This node represents a Constant Value, eg 1.5.
    /// </summary>
    public class ConstantNode : Node
    {
        /// <summary>
        /// Gets or sets the value of the node.
        /// </summary>
        public double Value { get; set; }
    }
}
