using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.AI {
    class Graph {
        private int v; //antal vertices
        private int e; //antal edges
        private List<int>[] adj; //närliggande lists
        

        public Graph(int V, Tile[,] tiles) {
            this.v = V;
            this.e = 0;
            adj = new List<int>[V];
            for (int v = 0; v < V; v++) {
                adj[v] = new List<int>();
            }

            for (int i = 0; i < v; i++) {
                if (!((i + 1) % tiles.GetLength(0) == 0)) {
                    AddEdge(i, i + 1);
                }
                if (!(i % tiles.GetLength(0) == 0)) {
                    AddEdge(i, i - 1);
                }
                if (i < tiles.GetLength(0) * (tiles.GetLength(0) - 1)) {
                    AddEdge(i, i + tiles.GetLength(1));
                }
                if (i >= tiles.GetLength(1)) {
                    AddEdge(i, i - tiles.GetLength(0));
                }
            }
        }

        public int V { get { return v; } }
        public int E { get { return e; } }
        public List<int> Adj(int v) { return adj[v]; }

        public void AddEdge(int v, int w) {
            adj[v].Add(w); //Lägger till w till v's lista
            //adj[w].Add(v); //Lägger till v till w's lista
            //e++;
        }

        public void RemoveEdge(int v, int w) {
            if(adj[v].Contains(w)) {
                adj[v].Remove(w);
                adj[w].Remove(v);
                //e--;
            }
        }
    }
}
