// <copyright file="GlobalSuppressions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "This Field shouldn't be private per the requirements of the assignment.", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.text")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "This Field shouldn't be private per the requirements of the assignment.", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.value")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:Element parameters should be documented", Justification = "The Element parameters actually are documented.", Scope = "member", Target = "~M:SpreadsheetEngine.Spreadsheet.#ctor(System.Int32,System.Int32)")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "No I'm going to do it this way.  In class i was told to do it the other way.", Scope = "member", Target = "~E:SpreadsheetEngine.CellBase.PropertyChanged")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "As Per the requirements this will be protected.", Scope = "member", Target = "~F:SpreadsheetEngine.CellBase.value")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "As Per the requirements this will be protected.", Scope = "member", Target = "~F:SpreadsheetEngine.CellBase.text")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:Accessible fields should begin with upper-case letter", Justification = "No it shouldn't.  The professor said to make them lowercase.", Scope = "member", Target = "~F:SpreadsheetEngine.Spreadsheet.cells")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "No it shouldn't, I'm Following the requirements of the project.", Scope = "member", Target = "~F:SpreadsheetEngine.Spreadsheet.cells")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "No I'm not going to do that.", Scope = "member", Target = "~E:SpreadsheetEngine.Spreadsheet.CellPropertyChanged")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "It's either this or have static members come first.  I'm going to get an error no matter what.", Scope = "member", Target = "~M:CptS321.ExpressionTree.SetVariable(System.String,System.Double)")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:Accessible fields should begin with upper-case letter", Justification = "Professor told us only proterties should have uppercase letters.", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.dependantCells")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "No Public is better, no reason for this to be private, that would make it unnesscessarily complicated.", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.dependantCells")]
