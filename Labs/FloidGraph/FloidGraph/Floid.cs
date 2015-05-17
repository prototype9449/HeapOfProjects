using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FloidGraph
{
    public class Floid
    {
        public int[,] GetShortPathMatrix(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1)) throw new InvalidOperationException("Array should have equal number of elements in the all dimension");
            var resultMatrix = new int[matrix.GetLength(0), matrix.GetLength(0)];
            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.GetLength(0); j++)
                    if (matrix[i, j] == 0)
                    {
                        resultMatrix[i, j] = int.MaxValue/2-1;
                    }
                    else
                    {
                        resultMatrix[i, j] = matrix[i, j];
                    }
            }

            for (int k = 0; k < resultMatrix.GetLength(0); k++)
            {
                for (int i = 0; i < resultMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < resultMatrix.GetLength(0); j++)
                    {
                        resultMatrix[i, j] = Math.Min(resultMatrix[i, j], resultMatrix[i, k] + resultMatrix[k, j]);
                    }
                }
            }
            return resultMatrix;
        }

        public int[,] GetShortPathMatrix(string pathToFile)
        {
            var edges = new List<Edge>();
            using (var reader = new StreamReader(pathToFile))
            {
                string line = string.Empty;
                while ((line = reader.ReadLine())!= null)
                {
                    var symbols = line.Split(new[] { ',', ' ', ';', }, StringSplitOptions.RemoveEmptyEntries).Select(item => Convert.ToInt32(item)).ToArray();
                    if(symbols.Length !=3) throw new InvalidOperationException();
                    edges.Add(new Edge(symbols[0],symbols[1],symbols[2]));
                }
            }
            var firstEdges = from edge in edges select edge.FirstVertex;
            var secondEdges = from edge in edges select edge.SecondVertex;
            var numberEdges = firstEdges.Union(secondEdges).Distinct().Count();

            var matrix = new int[numberEdges, numberEdges];
            foreach (var edge in edges)
            {
                matrix[edge.FirstVertex, edge.SecondVertex] = edge.Weight;
            }
            return GetShortPathMatrix(matrix);
        }

    

    }

    struct Edge
    {
        public int FirstVertex;
        public int SecondVertex;
        public int Weight;

        public Edge(int firstVertex, int secondVertex, int weight)
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            Weight = weight;
        }
    }
}
