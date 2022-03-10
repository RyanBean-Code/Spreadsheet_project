// <copyright file="Tests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_tests
{
    using System.Collections.Generic;
    using NUnit.Framework;

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

    }
}