// <copyright file="Tests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_tests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using SpreadsheetEngine;

    /// <summary>
    /// Where i do my tests.
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// Spreadsheet to use for testing.
        /// </summary>
        private SpreadsheetEngine.Spreadsheet? testSpreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tests"/> class.
        /// </summary>
        public Tests()
        {
            this.testSpreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
        }

        /// <summary>
        /// Method that runs before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// This test will test the get cell method.
        /// </summary>
        [Test]
        public void TestGetCell()
        {
            Assert.That(null, Is.EqualTo(this.testSpreadsheet.GetCell(100, 100)));
        }

        /// <summary>
        /// This method tests that the GetCellAtStringCoordinate() works.
        /// </summary>
        [Test]
        public void TestGetCellStringName()
        {
            Assert.That(null, Is.EqualTo(this.testSpreadsheet.GetCellAtStringCoordinate("A0")));
        }

        /// <summary>
        /// This method tests that the GetCellAtStringCoordinate() works.
        /// </summary>
        [Test]
        public void TestGetCellStringName2()
        {
            Assert.That(this.testSpreadsheet.GetCell(0, 0), Is.EqualTo(this.testSpreadsheet.GetCellAtStringCoordinate("A1")));
        }

        /// <summary>
        /// The will be a test that the expression tree works properly.
        /// </summary>
        [Test]
        public void TestExpressionTreeEmptyExpression()
        {
            CptS321.ExpressionTree testTree = new CptS321.ExpressionTree();
            Assert.That(0, Is.EqualTo(testTree.Evaluate()));
        }

        /// <summary>
        /// The will be a test that the expression tree works properly.
        /// </summary>
        [Test]
        public void TestExpressionTreeConstantExpression()
        {
            CptS321.ExpressionTree testTree = new CptS321.ExpressionTree("1+2+3");
            Assert.That(6, Is.EqualTo(testTree.Evaluate()));
        }

        /// <summary>
        /// Tests that the tokenized string method.
        /// </summary>
        [Test]
        public void TestCreateTokenizedExpressionList()
        {
            CptS321.ExpressionTree testTree = new CptS321.ExpressionTree();
            List<string> testList = new List<string>()
            {
                "3",
                "+",
                "4",
            };
            Assert.That(testList, Is.EqualTo(testTree.CreateTokenizedExpression("3 + 4")));
            testList.Clear();
            testList = new () // A1+(b4+5)-79*80
            {
                "A1",
                "+",
                "(",
                "b4",
                "+",
                "5",
                ")",
                "-",
                "79",
                "*",
                "80",
            };
            Assert.That(testList, Is.EqualTo(testTree.CreateTokenizedExpression("A1+(b4+5)-79*80")));
        }

        /// <summary>
        /// This test the method CreatePostfixExpression() in the ExpressionTree class.
        /// </summary>
        [Test]
        public void TestCreatePostfixExpression()
        {
            CptS321.ExpressionTree testTree = new CptS321.ExpressionTree();
            List<string> testListPrefix = new List<string>()
            {
                "3",
                "+",
                "4",
            };
            Queue<Node> testResult = testTree.CreatePostfixExpression(testListPrefix);
            string resultString = this.ConvertQueueToString(testResult);
            Assert.That("3 4 +", Is.EqualTo(resultString));
        }

        /// <summary>
        /// This test the method CreatePostfixExpression() in the ExpressionTree class.
        /// </summary>
        [Test]
        public void TestCreatePostfixExpression2()
        {
            CptS321.ExpressionTree testTree = new CptS321.ExpressionTree();
            string testAns = "3 4 2 1 - * +";
            List<string> testPrefix = new List<string>() // "3 + 4 * (2 − 1)"
            {
                "3",
                "+",
                "4",
                "*",
                "(",
                "2",
                "-",
                "1",
                ")",
            };
            Queue<Node> testResult = testTree.CreatePostfixExpression(testPrefix);
            string resultString = this.ConvertQueueToString(testResult);
            Assert.That(testAns, Is.EqualTo(resultString));
        }

        /// <summary>
        /// Tests the Evaulate() method in the Expression Tree class
        /// </summary>
        public void TestEvaulateTree()
        {
            CptS321.ExpressionTree testTree = new CptS321.ExpressionTree("3 + 4 * (2 - 1)");
            Assert.That(7, Is.EqualTo(testTree.Evaluate()));
        }

        private string ConvertQueueToString(Queue<Node> testResult)
        {
            string resultString = string.Empty;
            while (testResult.Count > 0)
            {
                Node temp = testResult.Dequeue();
                if (temp is OperatorNode)
                {
                    resultString += ((OperatorNode)temp).Operator.ToString();
                }
                else if (temp is VariableNode)
                {
                    resultString += ((VariableNode)temp).Name;
                }
                else if (temp is ConstantNode)
                {
                    resultString += ((ConstantNode)temp).Value.ToString();
                }

                resultString += " ";
            }

            return resultString.Trim();
        }
    }
}