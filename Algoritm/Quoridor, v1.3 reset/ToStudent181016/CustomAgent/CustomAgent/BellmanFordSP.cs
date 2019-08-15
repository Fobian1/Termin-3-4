using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.AI {
    class BellmanFordSP {

        private double[] distTo; // length of path to v
        private int[] edgeTo; // last edge on path to v
        private bool[] onQ; // Is this vertex on the queue?
        private Queue<int> queue; // vertices being relaxed
        private int src;

        public BellmanFordSP(Graph graph, int src) {
            distTo = new double[graph.V];
            edgeTo = new int[graph.V];
            onQ = new bool[graph.V];
            queue = new Queue<int>();
            for (int v = 0; v < graph.V; v++) {
                distTo[v] = Double.MaxValue;
            }

            distTo[src] = 0.0;
            queue.Enqueue(src);
            onQ[src] = true;
            while (queue.Count() > 0) {
                int v = queue.Dequeue();
                //onQ[v] = false;
                Relax(graph, v);
            }
        }

        private void Relax(Graph graph, int v) {
            foreach (int w in graph.Adj(v)) {
                if (!onQ[w]) {
                    queue.Enqueue(w);
                    onQ[w] = true;
                    edgeTo[w] = v;
                }
            }
        }
        public bool HasPathTo(int v) {
            return onQ[v];
        }

        public Stack<int> PathTo(int v) {
            if (!HasPathTo(v)) return null;
            Stack<int> path = new Stack<int>();
            for (int i = v; i != src; i = edgeTo[i])
                path.Push(i);
            //path.Push(s);

            return path;
        }
    }
}
