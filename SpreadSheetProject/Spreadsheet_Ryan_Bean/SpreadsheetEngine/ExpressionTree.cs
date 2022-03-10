﻿// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SpreadsheetEngine;

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

        private Node? CompileTree(string? exp)
        {
            if (!string.IsNullOrEmpty(exp))
            {
                List<string> tokenizedExpressionList = this.CreateTokenizedExpression(exp);
            }
            return null;
        }

        /// <summary>
        /// This will return a list of strings that holds the tokens of the expression string.
        /// </summary>
        /// <param name="exp"> The string to be tokenized. </param>
        /// <returns> A list of tokens. </returns>
        public List<string> CreateTokenizedExpression(string exp)
        {
            List<string> tokens = new List<string>();
            int substringLength = 0;
            int i = 0; // index
            int start = 0;
            while (i < exp.Length)
            {
                start = i;
                while (i < exp.Length && !this.IsOperatorOrParentheses(exp[i]))
                {
                    substringLength++;
                    i++;
                }

                if (substringLength == 0)
                {
                    substringLength = 1;
                }

                tokens.Add(exp.Substring(start, substringLength).Trim());
                i = start + substringLength;
                substringLength = 0;
            }

            return tokens;
        }

        /// <summary>
        /// Boolen function to determine is the given character is an operator.
        /// </summary>
        /// <param name="c"> The Character in Question. </param>
        /// <returns> True if c is an operator, otherwise false. </returns>
        private bool IsOperatorOrParentheses(char c)
        {
            return c == '*' || c == '/' || c == '+' || c == '-' || c == '(' || c == ')';
        }
    }
}
