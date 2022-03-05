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
        /// <summary>
        /// The root of the tree.
        /// </summary>
        private Node? root;

        /// <summary>
        /// Dictionary which will contian the varible for my expression.
        /// </summary>
        private Dictionary<string, double>? varDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="newExpression"> This is the expression to be evaulated. </param>
        public ExpressionTree(string? newExpression = "A1+B1+C1")
        {
            this.root = Compile(newExpression);
            this.varDictionary = new Dictionary<string, double>(10);
        }

        /// <summary>
        /// Compiles the whole tree together.
        /// </summary>
        /// <param name="expression"> The expression to compile into a tree. </param>
        /// <returns> A tree which is fully compiled. </returns>
        private static Node? Compile(string? expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return null;
            }

            // define the operators we want to look for in that order
            char[] operators = { '+', '-', '*', '/', /*'^'*/ };
            foreach (char op in operators)
            {
                Node n = Compile(expression, op);
                if (n != null)
                {
                    return n;
                }
            }

            // what can we see here?
            double number;

            // a constant
            if (double.TryParse(expression, out number))
            {
                // We need a ConstantNode
                return new ConstantNode()
                {
                    Value = number,
                };
            }

            // or variable
            else
            {
                // We need a VariableNode
                return new VariableNode()
                {
                    Name = expression,
                };
            }
        }

        /// <summary>
        /// Overloaded instance of Compile to linkn together operator nodes.
        /// </summary>
        /// <param name="expression"> the Expression being evaulated. </param>
        /// <param name="op"> The operator of the node. </param>
        /// <returns> A tree of operators linked togther. </returns>
        private static Node? Compile(string? expression, char op)
        {
            for (int i = expression.Length - 1; i >= 0; i--)
            {
                if (op == expression[i])
                {
                    OperatorNode operatorNode = new OperatorNode(expression[i]);
                    operatorNode.Left = Compile(expression.Substring(0, i));
                    operatorNode.Right = Compile(expression.Substring(i + 1));
                    return operatorNode;
                }
            }

            return null;
        }

        /// <summary>
        /// Sets the variable in the dictionary to the value.
        /// </summary>
        /// <param name="variableName"> Key in the dictionary. </param>
        /// <param name="variableValue"> Value of the variable. </param>
        public void SetVariable(string? variableName, double variableValue = 0)
        {
            if (!string.IsNullOrEmpty(variableName))
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
        }

        /// <summary>
        /// Evaluates the result of the exression.
        /// </summary>
        /// <returns> The value of the evaluated exprssion. </returns>
        public double Evaluate()
        {
            return this.Evaluate(this.root);
        }

        /// <summary>
        /// Used for error checking when evaluating the tree.
        /// If the user immediatley pushes evaulate in the menu the answer will just be zero.
        /// </summary>
        /// <param name="varName"> The name of the variable. </param>
        /// <returns> The Value of the variable if its in the dictionary otherwise 0. </returns>
        private double GetVariableValue(string varName)
        {
            if (this.varDictionary.ContainsKey(varName))
            {
                return this.varDictionary[varName];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Overloaded Evaulate function which accepts a node as an agruement.
        /// This is used do we can recursively evaulate the tree.
        /// </summary>
        /// <param name="node"> The node to evaulate. </param>
        /// <returns> The result of the expression tree. </returns>
        /// <exception cref="NotSupportedException"> If a non supported operator is entered. </exception>
        private double Evaluate(Node? node)
        {
            if (node is ConstantNode && node != null)
            {
                return ((ConstantNode)node).Value;
            }

            if (node is VariableNode && node != null)
            {
                return this.GetVariableValue(((VariableNode)node).Name);
            }

            if (node is OperatorNode && node != null)
            {
                switch (((OperatorNode)node).Operator)
                {
                    case '+':
                        return this.Evaluate(((OperatorNode)node).Left) + this.Evaluate(((OperatorNode)node).Right);
                    case '-':
                        return this.Evaluate(((OperatorNode)node).Left) - this.Evaluate(((OperatorNode)node).Right);
                    case '*':
                        return this.Evaluate(((OperatorNode)node).Left) * this.Evaluate(((OperatorNode)node).Right);
                    case '/':
                        return this.Evaluate(((OperatorNode)node).Left) / this.Evaluate(((OperatorNode)node).Right);
                    /*case '^':
                        return Math.Pow(this.Evaluate(((OperatorNode)node).Left), this.Evaluate(((OperatorNode)node).Right));*/
                    default:
                        throw new NotSupportedException("Operator " + ((OperatorNode)node).Operator.ToString() + " Not Supported.");
                }
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Abtract base class of node so all nodes are compatible with eachother.
        /// </summary>
        private abstract class Node
        {
        }

        /// <summary>
        /// This node represents a Constant Value, eg 1.5.
        /// </summary>
        private class ConstantNode : Node
        {
            /// <summary>
            /// Gets or sets the value of the node.
            /// </summary>
            public double Value { get; set; }
        }

        /// <summary>
        /// Represents a variable, eg "A1".
        /// The value of this node is stored in the var dictionary.
        /// </summary>
        private class VariableNode : Node
        {
            /// <summary>
            /// Gets or sets the name of the variable.
            /// </summary>
            public string? Name { get; set; }
        }

        /// <summary>
        /// This node represents and operator, eg '+' or '*'.
        /// </summary>
        private class OperatorNode : Node
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
}
