// <copyright file="ClassToDemoTestingNonPublicTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_tests
{
    using SpreadsheetEngine;
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Reflection;
    using CptS321;
    using NUnit.Framework;

    /// <summary>
    /// Summary.
    /// </summary>
    public class ClassToDemoTestingNonPublicTest
    {
        private ExpressionTree objectUnderTest = new ExpressionTree();

        /// <summary>
        /// Tests that the tokenized string method.
        /// </summary>
        [Test]
        public void TestCreateTokenizedExpressionList()
        {
            MethodInfo methodInfo = this.GetMethod("CreateTokenizedExpression");
            CptS321.ExpressionTree testTree = new CptS321.ExpressionTree();
            List<string> testList = new List<string>()
            {
                "3",
                "+",
                "4",
            };
            Assert.That(testList, Is.EqualTo(methodInfo.Invoke(this.objectUnderTest, new object[] { "3 + 4" })));
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
            Assert.That(testList, Is.EqualTo(methodInfo.Invoke(this.objectUnderTest, new object[] { "A1+(b4+5)-79*80" })));
        }

        /// <summary>
        /// This test the method CreatePostfixExpression() in the ExpressionTree class.
        /// </summary>
        [Test]
        public void TestCreatePostfixExpression()
        {
            MethodInfo methodInfo = this.GetMethod("CreatePostfixExpression");
            List<string> testListPrefix = new List<string>()
            {
                "3",
                "+",
                "4",
            };
            Queue<Node> testResult = (Queue<Node>)methodInfo.Invoke(this.objectUnderTest, new object[] { testListPrefix });
            string resultString = this.ConvertQueueToString(testResult);
            Assert.That("3 4 +", Is.EqualTo(resultString));
        }

        /// <summary>
        /// This test the method CreatePostfixExpression() in the ExpressionTree class.
        /// </summary>
        [Test]
        public void TestCreatePostfixExpression2()
        {
            MethodInfo methodInfo = this.GetMethod("CreatePostfixExpression");
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
            Queue<Node> testResult = (Queue<Node>)methodInfo.Invoke(this.objectUnderTest, new object[] { testPrefix });
            string resultString = this.ConvertQueueToString(testResult);
            Assert.That(testAns, Is.EqualTo(resultString));
        }

        /// <summary>
        /// Used to retrieve private methods for testing.
        /// </summary>
        /// <param name="methodName"> The name of the Private methof to test. </param>
        /// <returns> A MethodInfo type with the information to call the private method. </returns>
        private MethodInfo GetMethod(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                Assert.Fail("method name cannot be null or whitespace");
            }

            var method = this.objectUnderTest.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            if (method == null)
            {
                Assert.Fail("{0} method not found!", methodName);
            }

            return method;
        }

        /// <summary>
        /// Just used in some tests to convert a queue object into a string .
        /// </summary>
        /// <param name="testResult"> The Queue to be converted into a string. </param>
        /// <returns> A String. </returns>
        private string ConvertQueueToString(Queue<Node> testResult)
        {
            string resultString = string.Empty;
            while (testResult.Count > 0)
            {
                Node temp = testResult.Dequeue();
                if (temp is OperatorNode)
                {
                    OperatorNode temp1 = ((OperatorNode)temp);
                    resultString += temp1.Operator.ToString();
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
