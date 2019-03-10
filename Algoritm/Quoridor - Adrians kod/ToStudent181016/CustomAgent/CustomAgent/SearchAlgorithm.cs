using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.AI
{
    class SearchAlgorithm
    {
        private bool[] marked; //Shortest path to this vertex known?
        private int[] edgeTo; //Last vertex on know path to this vertex
        private int src;

        public SearchAlgorithm(Graph graph, int src)
        {
            marked = new bool[graph.V];
            edgeTo = new int[graph.V];
            this.src = src;
            BFS(graph, src);
        }

        //private void DFS(Graph graph, int v)
        //{
        //    marked[v] = true;
        //    foreach(int w in graph.Adj(v))
        //    {
        //        if (!marked[w])
        //        {
        //            edgeTo[w] = v;
        //            DFS(graph, w);
        //        }
        //    }
        //}

        private void BFS(Graph g, int s)
        {
            Queue<int> q = new Queue<int>();
            marked[s] = true; //Mark the source
            q.Enqueue(s); //And put in on the queue
            while (q.Count() > 0)
            {
                int v = q.Dequeue();  // Remove next vertex from the queue.
                foreach (int w in g.Adj(v))   // For every unmarked adjacent vertex,
                {
                    if (!marked[w])
                    {
                        edgeTo[w] = v;   // save last edge on a shortest path,
                        marked[w] = true; // mark it because path is known,
                        q.Enqueue(w); // And add it to the queue
                    }
                }
            }
        }

        public bool HasPathTo(int v)
        {
            return marked[v];
        }

        public Stack<int> PathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            Stack<int> path = new Stack<int>();
            for (int i = v; i != src; i = edgeTo[i])
                path.Push(i);
            //path.Push(s);

            return path;
        }
    }
}
