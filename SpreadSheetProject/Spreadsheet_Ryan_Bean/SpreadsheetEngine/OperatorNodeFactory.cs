// <copyright file="OperatorNodeFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Handles the creation of operator nodes.
    /// </summary>
    public class OperatorNodeFactory
    {
        /// <summary>
        /// Dictionary of all the operators.
        /// </summary>
        private Dictionary<char, Type> operators = new Dictionary<char, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory()
        {
            this.TraverseAvailableOperators((op, type) => this.operators.Add(op, type));
        }

        /// <summary>
        /// Used to make a lamda expression which adds an operator to the operator dictionary.
        /// </summary>
        /// <param name="op"> The char symbol of the operator ex '+'. </param>
        /// <param name="type"> The type of operatorNode associated with the symbol ex "AdditionNode". </param>
        private delegate void OnOperator(char op, Type type);

        /// <summary>
        /// Creates an operator node based on the operator passed into the function.
        /// </summary>
        /// <param name="op"> The operator. </param>
        /// <returns> A new instance of any supported operator node. </returns>
        /// <exception cref="NotSupportedException"> Should only trigger if an operator not supported is passed into the function. </exception>
        public OperatorNode CreateOperatorNode(char op)
        {
            if (this.operators.ContainsKey(op))
            {
                object operatorNodeObject = System.Activator.CreateInstance(this.operators[op]);
                if (operatorNodeObject is OperatorNode)
                {
                    return (OperatorNode)operatorNodeObject;
                }
            }

            throw new NotSupportedException();
        }

        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            Type operatorNodeType = typeof(OperatorNode);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> operatorTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));
                foreach (var type in operatorTypes)
                {
                    PropertyInfo operatorField = type.GetProperty("Operator");
                    if (operatorField != null)
                    {
                        object value = operatorField.GetValue(Activator.CreateInstance(type));
                        if (value is char)
                        {
                            char operatorSymbol = (char)value;
                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }
}
