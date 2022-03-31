// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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

        private OperatorNodeFactory factory;// = new OperatorNodeFactory();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="newExpression"> This is the expression to be evaulated. </param>
        public ExpressionTree(string? newExpression = "A1+B1+C1")
        {
            this.factory = new OperatorNodeFactory();
            this.varDictionary = new Dictionary<string, double>(10);
            this.root = this.CompileTree(newExpression);
        }

        /// <summary>
        /// Used to get a list of all the variable names in the expression.
        /// This will make setting the value of each varible much easier.
        /// </summary>
        /// <returns> A list of all the variables in the expression. </returns>
        public List<string> GetVariableNames()
        {
            List<string> varNames = new List<string>();
            foreach (KeyValuePair<string, double> entry in this.varDictionary)
            {
                varNames.Add(entry.Key);
            }

            return varNames;
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
        /// This methods uses Dijkstras algortihm to create a postfix expression.
        /// </summary>
        /// <param name="prefixExp"> The expression in prefix form. </param>
        /// <returns> A list of Tokens in Postfix order. </returns>
        private Queue<Node> CreatePostfixExpression(List<string> prefixExp)
        {
            Queue<Node> postfixExp = new Queue<Node>();
            Stack<string> opStack = new Stack<string>(); // operator stack
            double number = 0;
            foreach (string token in prefixExp)
            {
                // if the token is a number add it to the queue
                if (double.TryParse(token, out number))
                {
                    postfixExp.Enqueue(new ConstantNode(number));
                }

                // if the token is a varible I also want to add it to the queue
                // One issue here is that the varible name must be longer the 1 letter
                // "A1", "c39" will be accepted but "x" wont be accepted.
                else if (token.Length > 1)
                {
                    postfixExp.Enqueue(new VariableNode(token));
                    this.SetVariable(token);
                }

                // if the token is a function then push it onto the stack
                // the Expression tree does not yet support functions

                // if the token is an operator then I push it on the stack
                // an operator should only be 1 character
                else if (this.IsOperator(token[0]))
                {
                    while (opStack.Count > 0 && opStack.Peek() != "(" && this.GetOperatorPresidence(opStack.Peek()[0]) >= this.GetOperatorPresidence(token[0]))
                    {
                        string top = opStack.Peek();
                        postfixExp.Enqueue(this.factory.CreateOperatorNode(top[0]));
                        opStack.Pop();
                    }

                    opStack.Push(token);
                }

                // if the token if left parentheses
                else if (token == "(")
                {
                    opStack.Push(token);
                }

                // if the token is right parentheses
                else if (token == ")")
                {
                    string top = opStack.Peek();
                    while (opStack.Peek() != "(")
                    {
                        Debug.Assert(opStack.Count > 0, "Error Compiling Expression Tree: Incorrect Parentheses");
                        top = opStack.Pop();
                        postfixExp.Enqueue(this.factory.CreateOperatorNode(top[0]));
                    }

                    Debug.Assert(opStack.Peek() == "(", "Error Compiling Expression Tree: Incorrect Parentheses");
                    opStack.Pop();

                    // if there is a function token at the top of the stack then
                    // pop the function from the operator stack into the output queue
                    // The tree does not yet support functions
                }
            }

            while (opStack.Count != 0)
            {
                Debug.Assert(opStack.Peek() != "(", "Error Compiling Expression Tree: Incorrect Parentheses");
                string top = opStack.Pop();
                postfixExp.Enqueue(this.factory.CreateOperatorNode(top[0]));
            }

            return postfixExp;
        }

        /// <summary>
        /// This will return a list of strings that holds the tokens of the expression string.
        /// </summary>
        /// <param name="exp"> The string to be tokenized. </param>
        /// <returns> A list of tokens. </returns>
        private List<string> CreateTokenizedExpression(string exp)
        {
            List<string> tokens = new List<string>();
            int substringLength = 0;
            int i = 0; // index
            int start = 0;
            while (i < exp.Length)
            {
                start = i;
                while (i < exp.Length && !(this.IsOperator(exp[i]) || this.IsParentheses(exp[i])))
                {
                    substringLength++;
                    i++;
                }

                if (substringLength == 0)
                {
                    substringLength = 1;
                }

                string tokenString = exp.Substring(start, substringLength).Trim();
                if (!string.IsNullOrEmpty(tokenString))
                {
                    tokens.Add(tokenString);
                }

                i = start + substringLength;
                substringLength = 0;
            }

            return tokens;
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
                OperatorNode node1 = (OperatorNode)node;
                switch ((OperatorNode)node)
                {
                    case AdditionNode:
                        return this.Evaluate(node1.Left) + this.Evaluate(node1.Right);
                    case SubtractionNode:
                        return this.Evaluate(node1.Left) - this.Evaluate(node1.Right);
                    case MultiplicationNode:
                        return this.Evaluate(node1.Left) * this.Evaluate(node1.Right);
                    case DivisionNode:
                        return this.Evaluate(node1.Left) / this.Evaluate(node1.Right);
                    default:
                        throw new NotSupportedException("Operator " + node1.Operator.ToString() + " Not Supported.");
                }
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Builds the Tree using an expression in postfix form.
        /// </summary>
        /// <param name="exp"> The Expression in Postfix Form. </param>
        /// <returns> The Root node of the tree. </returns>
        private Node? CompileTree(string? exp)
        {
            if (!string.IsNullOrEmpty(exp))
            {
                List<string> prefixExpression = this.CreateTokenizedExpression(exp);
                Queue<Node> postfixExpression = this.CreatePostfixExpression(prefixExpression);
                Stack<Node> operandStack = new Stack<Node>();
                while (postfixExpression.Count > 0)
                {
                    Node temp = postfixExpression.Dequeue();
                    if (temp is OperatorNode)
                    {
                        ((OperatorNode)temp).Right = operandStack.Pop();
                        ((OperatorNode)temp).Left = operandStack.Pop();
                        operandStack.Push(temp);
                    }
                    else
                    {
                        operandStack.Push(temp);
                    }
                }

                return operandStack.Pop();
            }

            return null;
        }

        /// <summary>
        /// Boolen function to determine is the given character is an operator.
        /// </summary>
        /// <param name="c"> The Character in Question. </param>
        /// <returns> True if c is an operator, otherwise false. </returns>
        private bool IsOperator(char c)
        {
            return c == '*' || c == '/' || c == '+' || c == '-';
        }

        /// <summary>
        /// Boolean function to determine if the given character is a paranthes.
        /// </summary>
        /// <param name="c"> The Character in question. </param>
        /// <returns> True is c is an parenthese. </returns>
        private bool IsParentheses(char c)
        {
            return c == '(' || c == ')';
        }

        /// <summary>
        /// Returns the presidence of the operator given.
        /// </summary>
        /// <param name="op"> the operator. </param>
        /// <returns> an integer. </returns>
        /// <exception cref="NotSupportedException"> if the operator is not supported. </exception>
        private int GetOperatorPresidence(char op)
        {
            switch (op)
            {
                case '*':
                    return 2;
                case '/':
                    return 2;
                case '+':
                    return 1;
                case '-':
                    return 1;
            }

            throw new NotSupportedException();
        }
    }
}
