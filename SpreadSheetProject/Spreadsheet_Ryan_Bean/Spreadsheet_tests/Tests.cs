// <copyright file="Tests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_tests
{
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
    }
}