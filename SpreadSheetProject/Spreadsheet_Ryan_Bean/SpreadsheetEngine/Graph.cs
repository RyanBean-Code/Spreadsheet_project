// <copyright file="Graph.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Grpah class which represents a directed graph will be used to find a cirucular reference in the spreadsheet.
    /// </summary>
    internal class Graph
    {
        /// <summary>
        /// Integrer representing the number of vertices in the graph.
        /// Each cell will be a vertices so there will be 1250 vertices.
        /// </summary>
        private int vertices;

        /// <summary>
        /// List which represents the graph.
        /// </summary>
        private List<List<int>> adj;

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph"/> class.
        /// </summary>
        public Graph()
        {
            this.vertices = 1250;
            this.adj = new List<List<int>>(this.vertices);

            for (int i = 0; i < this.vertices; i++)
            {
                this.adj.Add(new List<int>());
            }
        }

        /// <summary>
        /// Adds an edge to the graph.
        /// </summary>
        /// <param name="sou"> Source vertice. </param>
        /// <param name="dest"> Destination Vertice. </param>
        public void AddEdge(int sou, int dest)
        {
            this.adj[sou].Add(dest);
        }

        /// <summary>
        /// Removes an edge from the graph.
        /// </summary>
        /// <param name="sou"> source. </param>
        /// <param name="dest"> destination. </param>
        public void RemoveEdge(int sou, int dest)
        {
            this.adj[sou].Remove(dest);
        }

        /// <summary>
        /// Checks if theres a cycle in the graph.
        /// </summary>
        /// <returns> True if there is a cycle. </returns>
        public bool IsCyclic()
        {
            // Mark all the vertices as not visited and
            // not part of recursion stack
            bool[] visited = new bool[this.vertices];
            bool[] recStack = new bool[this.vertices];

            for (int i = 0; i < this.vertices; i++)
            {
                if (this.IsCyclicUtil(i, visited, recStack))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Used to help find a cycle in the graph.
        /// </summary>
        /// <param name="i"> The current vertice. </param>
        /// <param name="visited"> If we've visited this vertice before. </param>
        /// <param name="recStack"> Recursion Stack. </param>
        /// <returns> True or False. </returns>
        private bool IsCyclicUtil(int i, bool[] visited, bool[] recStack)
        {
            // Mark the current node as visited and
            // part of recursion stack
            if (recStack[i])
            {
                return true;
            }

            if (visited[i])
            {
                return false;
            }

            visited[i] = true;

            recStack[i] = true;
            List<int> children = this.adj[i];

            foreach (int c in children)
            {
                if (this.IsCyclicUtil(c, visited, recStack))
                {
                    return true;
                }
            }

            recStack[i] = false;

            return false;
        }
    }
}
