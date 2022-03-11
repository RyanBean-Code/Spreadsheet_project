using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Represents a variable, eg "A1".
    /// The value of this node is stored in the var dictionary.
    /// </summary>
    public class VariableNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="name"> name of the Varible. </param>
        public VariableNode(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        public string? Name { get; set; }
    }
}
