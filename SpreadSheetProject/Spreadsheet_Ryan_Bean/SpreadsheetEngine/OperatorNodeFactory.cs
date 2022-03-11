// <copyright file="OperatorNodeFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Handles the creation of operator nodes.
    /// </summary>
    public class OperatorNodeFactory
    {
        /// <summary>
        /// Creates an operator node based on the operator passed into the function.
        /// </summary>
        /// <param name="op"> The operator. </param>
        /// <returns> A new instance of any supported operator node. </returns>
        /// <exception cref="NotSupportedException"> Should only trigger if an operator not supported is passed into the function. </exception>
        public static OperatorNode CreateOperatorNode(char op)
        {
            switch (op)
            {
                case '*':
                    return new MultiplicationNode();
                case '/':
                    return new DivisionNode();
                case '+':
                    return new AdditionNode();
                case '-':
                    return new SubtractionNode();
            }

            throw new NotSupportedException();
        }
    }
}
