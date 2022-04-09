// <copyright file="Tests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_tests
{
    using System;
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
        /// Tests if the undo feature works in the spreadsheet for text.
        /// </summary>
        [Test]
        public void TestTextUndoRedo()
        {
            this.testSpreadsheet.SetCellText(0, 0, "hello");
            this.testSpreadsheet.SetCellText(0, 0, "world");
            this.testSpreadsheet.Undo();
            Assert.That("hello", Is.EqualTo(this.testSpreadsheet.cells[0, 0].Text));
            this.testSpreadsheet.Redo();
            Assert.That("world", Is.EqualTo(this.testSpreadsheet.cells[0, 0].Text));
        }

        /// <summary>
        /// Tests if the expression tree is implemented into the spreadsheet.
        /// </summary>
        [Test]
        public void TestSpreadsheetSupportingExpressionTree()
        {
            this.testSpreadsheet.cells[0, 0].Text = "=1+2*3/4";
            Assert.That(2.5, Is.EqualTo(Convert.ToDouble(this.testSpreadsheet.cells[0, 0].Value)));
        }

        /// <summary>
        /// This tests the logic that cells, will be update when another cell changes.
        /// </summary>
        [Test]
        public void TestCellUpdating()
        {
            this.testSpreadsheet.cells[0, 0].Text = "20";
            this.testSpreadsheet.cells[0, 1].Text = "=A1";
            Assert.That(this.testSpreadsheet.cells[0, 0].Value, Is.EqualTo(this.testSpreadsheet.cells[0, 1].Value));
            this.testSpreadsheet.cells[0, 0].Text = "10";
            Assert.That(this.testSpreadsheet.cells[0, 0].Value, Is.EqualTo(this.testSpreadsheet.cells[0, 1].Value));
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
        /// Tests the Evaulate() method in the Expression Tree class.
        /// </summary>
        [Test]
        public void TestEvaulateTree()
        {
            CptS321.ExpressionTree testTree = new CptS321.ExpressionTree("3 + 4 * (2 - 1)");
            Assert.That(7, Is.EqualTo(testTree.Evaluate()));
        }

        /// <summary>
        /// Tests the Evaulate() Method in the ExpressionTree class.
        /// Wow Check out this Edge Case testing!!!!.
        /// </summary>
        [Test]
        public void TestEvaulateTree2()
        {
            CptS321.ExpressionTree testTree = new CptS321.ExpressionTree();
            Assert.That(0, Is.EqualTo(testTree.Evaluate()));
        }
    }
}