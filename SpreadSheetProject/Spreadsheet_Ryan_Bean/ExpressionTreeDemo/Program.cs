// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTreeDemo
{
    using System;

    /// <summary>
    /// Where to program is.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Where the Program starts.
        /// </summary>
        public static void Main()
        {
            int option = 0;
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree();
            do
            {
                option = 0;
                Console.Write("Expression Tree Demo\n1. Enter Expression\n2. Set Varible Value\n3. Evaluate The Expression\n4. Exit\n   Enter Option: ");
                option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1: // Enter Expression
                        // Console.Clear();
                        Console.Write("Enter An Expression: ");
                        try
                        {
                            tree = new CptS321.ExpressionTree(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("Expression Formatted Incorrectly!");
                        }

                        break;
                    case 2: // Set Varible Value
                        // Console.Clear();
                        Console.Write("Enter the Variable Name: ");
                        string? varName = Console.ReadLine();
                        Console.Write("\nEnter the Variable Value: ");
                        string? varValue = Console.ReadLine();
                        tree.SetVariable(varName, Convert.ToDouble(varValue));
                        break;
                    case 3: // Evaluate The Expression
                        // Console.Clear();
                        Console.WriteLine("Evaluation: " + Convert.ToString(tree.Evaluate()) + "\n");
                        break;
                    case 4: // Exit
                        Console.WriteLine("Thanks For Using The ExpressionTree Demo!");
                        break;
                    default:
                        Console.WriteLine("Invalid Option!");
                        break;
                }
            } while (option != 4);
        }
    }
}