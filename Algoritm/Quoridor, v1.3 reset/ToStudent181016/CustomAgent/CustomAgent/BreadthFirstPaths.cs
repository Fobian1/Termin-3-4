using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.AI {
    class BreadthFirstPaths {
        private bool[] marked; // Is a shortest path to this vertex known?
        private int[] edgeTo; // last vertex on known path to this vertex
        private int src; // source

        public BreadthFirstPaths(Graph graph, int src) {
            marked = new bool[graph.V];
            edgeTo = new int[graph.V];
            this.src = src;
            BFS(graph, src);
        }

        private void BFS(Graph graph, int src) {
            Queue<int> queue = new Queue<int>();
            marked[src] = true; // Mark the source
            queue.Enqueue(src); // and put it on the queue.
            while (queue.Count() > 0) {
                int v = queue.Dequeue(); // Remove next vertex from the queue.
                foreach (int w in graph.Adj(v))
                    if (!marked[w]) // For every unmarked adjacent vertex,
                    {
                        edgeTo[w] = v; // save last edge on a shortest path,
                        marked[w] = true; // mark it because path is known,
                        queue.Enqueue(w); // and add it to the queue.
                    }
            }
        }
        public bool HasPathTo(int v) {
            return marked[v];
        }

        public Stack<int> PathTo(int v) {
            if(!HasPathTo(v)) {
                return null;
            }
            Stack<int> path = new Stack<int>();
            for(int x = v; x != src; x = edgeTo[x]) {
                path.Push(x);
            }
            //path.Push(src);
            return path;
        }
    }
}
