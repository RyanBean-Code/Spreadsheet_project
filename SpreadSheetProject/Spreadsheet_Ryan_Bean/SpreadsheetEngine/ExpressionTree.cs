// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Expression Tree clas for evalualting expressions the tree way.
    /// </summary>
    public class ExpressionTree
    {
        ///// <summary>
        ///// String which will hold the expression.
        ///// </summary>
        //private string? expression;

        private Node root;

        /// <summary>
        /// Dictionary which will contian the varible for my expression.
        /// </summary>
        private Dictionary<string, double> varDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="newExpression"> This is the expression to be evaulated. </param>
        public ExpressionTree(string newExpression = "A1+B1+C1")
        {
            //this.expression = newExpression;
            //this.varDictionary = new Dictionary<string, double>(3);
            this.root = Compile(newExpression);
        }

        /// <summary>
        /// Sets the variable in the dictionary to the value.
        /// </summary>
        /// <param name="variableName"> Key in the dictionary. </param>
        /// <param name="variableValue"> Value of the variable. </param>
        public void SetVariable(string variableName, double variableValue = 0)
        {
            if (this.varDictionary.ContainsKey(variableName))
            {
                this.varDictionary[variableName] = variableValue;
            }
            else
            {
                this.varDictionary.Add(variableName, variableValue);
            }
        }

        /// <summary>
        /// TODO: evaluates the result of the exression.
        /// </summary>
        /// <returns> The value of the evaluated exprssion. </returns>
        public double Evaluate()
        {
            double result = int.MinValue;
            return result;
        }

        private static Node? Compile(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return null;
            }



            return null;
        }

        private abstract class Node
        {
        }

        private class ConstantNode : Node
        {
            public double Value { get; set; }
        }

        private class VariableNode : Node
        {
            public string? Name { get; set; }
        }

        private class OperatorNode : Node
        {
            public OperatorNode(char newOperator)
            {
                this.Operator = newOperator;
                this.Left = this.Right = null;
            }

            public char Operator { get; set; }

            public Node? Left { get; set; }

            public Node? Right { get; set; }
        }
    }
}
