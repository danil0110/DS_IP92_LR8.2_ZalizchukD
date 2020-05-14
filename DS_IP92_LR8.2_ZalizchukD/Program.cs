using System;
using System.Collections.Generic;
using System.IO;

namespace DS_IP92_LR8._2_ZalizchukD
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "graph.txt";
            Graph graph = new Graph(path);
            graph.OutputWeight();
            Console.WriteLine();
            graph.MinimumSpanningTree();

        }
    }

    class Graph
    {
        private int n, m;
        private int[,] weight, result;
     
        public Graph(string info)
        {
            StreamReader sr = new StreamReader(info);
            string[] temp;
            string line = sr.ReadLine();
            temp = line.Split(' ');
            n = Convert.ToInt32(temp[0]);
            m = Convert.ToInt32(temp[1]);
            weight = new int[n, n];
            result = new int[n, n];

            int a, b; // Начальная и конечная вершина ребра
            for (int i = 0; i < m; i++)
            {
                line = sr.ReadLine();
                temp = line.Split(' ');
                a = Convert.ToInt32(temp[0]) - 1;
                b = Convert.ToInt32(temp[1]) - 1;
                weight[a, b] = Convert.ToInt32(temp[2]);
                weight[b, a] = Convert.ToInt32(temp[2]);
            }
            
            sr.Close();
        }

        public void MinimumSpanningTree()
        {
            List<int> marked = new List<int>();
            marked.Add(0);
            int min_root = 0, min_vertex = 0, min = 0, count = 1, sum = 0;

            while (count < n)
            {
                foreach (var vertex in marked)
                {
                    if (min == 0)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            if (weight[vertex, i] != 0)
                            {
                                min = weight[vertex, i];
                                min_root = vertex;
                                min_vertex = i;
                                break;
                            }
                        }
                        
                    }

                    for (int i = 0; i < n; i++)
                    {
                        if (min > weight[vertex, i] && weight[vertex, i] != 0 && !marked.Contains(i))
                        {
                            min = weight[vertex, i];
                            min_root = vertex;
                            min_vertex = i;
                        }
                    }
                }

                result[min_root, min_vertex] = min;
                result[min_vertex, min_root] = min;
                marked.Add(min_vertex);
                sum += min;
                min = 0;
                count++;

            }
            
            Console.WriteLine("Полученное минимальное остовное дерево");
            OutputMatrix(result);
            Console.WriteLine($"Минимальная сумма ребер - {sum}");
            
        }

        public void OutputWeight()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{weight[i, j],4}");
                }
                Console.WriteLine();
            }
        }

        private void OutputMatrix(int[,] graph)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{graph[i, j],4}");
                }
                Console.WriteLine();
            }
        }
        
    }
    
}