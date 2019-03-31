using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.AI
{
    class Graph
    {
        private int v;
        private int e;
        public int[,] adjMatrix;

        private Tile[,] tiles;
        private List<int>[] adj;
        private int[] vertices;
        private Player player;

        public int V { get { return v; } }
        public int E { get { return e; } }
        public List<int> Adj(int v) { return adj[v]; } //Get adjacent vertex

        public Graph(Tile[,] tiles, Player player)
        {
            v = tiles.Length;
            this.player = player;
            //adjMatrix = new int[v, v];
            adj = new List<int>[v];
            for (int i = 0; i < v; i++)
            {
                adj[i] = new List<int>();
            }

            vertices = new int[v];

            for (int i = 0; i < v; i++)
            {
                vertices[i] = i;

                if (!((i + 1) % tiles.GetLength(0) == 0))
                {
                    AddEdge(i, i + 1);
                }
                if (!(i % tiles.GetLength(0) == 0))
                {
                    AddEdge(i, i - 1);
                }
                if (i < tiles.GetLength(0) * (tiles.GetLength(0) - 1))
                {
                    AddEdge(i, i + tiles.GetLength(1));
                }
                if (i >= tiles.GetLength(1))
                {
                    AddEdge(i, i - tiles.GetLength(0));
                }
            }
        }
        public void AddEdge(int src, int dest)
        {
            //adjMatrix[src - 1, dest - 1] = 1;

            adj[src].Add(dest);
            //e++;
        }
        public void RemoveEdge(int src, int dest)
        {
            //adjMatrix[src - 1, dest - 1] = 0;

            if (adj[src].Contains(dest))
            {
                adj[src].Remove(dest);
                adj[dest].Remove(src);
                //e--;
                //e--;
            }
        }
    }
}
