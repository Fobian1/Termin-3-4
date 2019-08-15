using System;
using System.Collections.Generic;
using System.Xml.Linq;


namespace Quoridor.AI {

    class CustomAgent : Agent {
        Graph graph;
        Tile[,] tiles;
        Tuple<int, int> nextCoordinates = null;
        int current1DPos, goal;
        Action move;

        GameData data;
        List<MoveAction> moves = new List<MoveAction>();
        List<List<Node>> paths = new List<List<Node>>();
        int goalPosition;
        int optimalDirection;
        int enemyWalls = 1000;
        int distanceToGoal;
        int distanceFromStart;

        public static void Main() {
            
            new CustomAgent().Start();
            
        }

        public override Action DoAction(GameData status) {
            tiles = status.Tiles;
            CreateGraph(status);
            current1DPos = Get1DFrom2D(status.Self.Position);
            Search(graph, current1DPos);

            move = new MoveAction(nextCoordinates.Item2, nextCoordinates.Item1);
            Console.WriteLine("Next move is to " + nextCoordinates.Item2.ToString() + ", " +
            nextCoordinates.Item1.ToString() + " tile " + Get1DFrom2D(new Point(nextCoordinates.Item2, nextCoordinates.Item1)));

            return move;

            moves.RemoveAt(0);
            data = status;
            GetGoal(status);
            foreach (Player p in status.Players) {
                if (p != status.Self) {
                    if (p.NumberOfWalls < enemyWalls) {
                        if (goalPosition == 0) {
                            Point p1 = new Point(data.Tiles.GetLength(0) / 2, data.Tiles.GetLength(1));
                            Point p2 = new Point(data.Self.Position.X, data.Self.Position.Y);
                            distanceFromStart = (p1.X - p2.X) + (p1.Y - p2.Y);

                        } else {
                            distanceToGoal = data.Self.Position.Y;
                        }

                        enemyWalls = p.NumberOfWalls;
                        moves.Clear();
                        FindPath(status.Self.Position);
                    }
                }
            }
            return moves[0];
        }

        void CreateGraph(GameData status) {
            graph = new Graph(tiles, status.Self);
            for (int i = 0; i < status.HorizontalWall.Length; i++) {
                Tuple<int, int> index = Get2DFrom1D(i, status.Tiles.GetLength(1),
                    status.Tiles.GetLength(0));

                if (status.HorizontalWall[index.Item2, index.Item1]) {
                    graph.RemoveEdge(i, i + tiles.GetLength(0));
                }
            }
            for (int i = 0; i < status.VerticalWall.Length; i++) {
                Tuple<int, int> index = Get2DFrom1D(i, status.VerticalWall.GetLength(0), status.VerticalWall.GetLength(0));

                if (status.VerticalWall[index.Item2, index.Item1]) {
                    graph.RemoveEdge(i + index.Item1, i + 1 + index.Item1);
                }
            }

            if (status.Self.Color == Color.Blue) {
                goal = tiles.Length - tiles.GetLength(0);
            } else if (status.Self.Color == Color.Red) {
                goal = 0;
            }

            Player enemy = null;
            if (status.Players[0] == status.Self) {
                enemy = status.Players[1];
            } else if (status.Players[1] == status.Self) {
                enemy = status.Players[0];
            }
            int current1DEnemyPos = Get1DFrom2D(enemy.Position);
            graph.RemoveEdge(current1DEnemyPos, current1DEnemyPos + 1);
            graph.RemoveEdge(current1DEnemyPos, current1DEnemyPos - 1);
            graph.RemoveEdge(current1DEnemyPos, current1DEnemyPos + tiles.GetLength(0));
            graph.RemoveEdge(current1DEnemyPos, current1DEnemyPos - tiles.GetLength(1));
        }

        void ConvertToMove(Stack<int> path) {
            int index = path.Pop();
            nextCoordinates = Get2DFrom1D(index, tiles.GetLength(0), tiles.GetLength(1));
        }

        private int Get1DFrom2D(Point p) {
            int newIndex = p.Y * tiles.GetLength(0) + p.X;
            return newIndex;
        }

        private Tuple<int, int> Get2DFrom1D(int index, int xDim, int yDim) {
            int i = 0, j = 0;
            int row = xDim;
            int col = yDim;
            i = index / row;
            j = index % col; //row?
            Tuple<int, int> newIndex = new Tuple<int, int>(i, j);
            return newIndex;
        }

        private void FindPath(Point pos) {
            int i = FindLowestWeight();
            if (!WallBetween(data.Self.Position, new Point(data.Self.Position.X, data.Self.Position.Y + optimalDirection)) && IsWithinGameBoard(data.Self.Position.X, data.Self.Position.Y + optimalDirection)) {
                paths.Add(new List<Node> { new Node(distanceFromStart, goalPosition - data.Self.Position.Y, data.Self.Position) });
                paths[i].Add(new Node(distanceFromStart, goalPosition - data.Self.Position.Y, data.Self.Position));
            } else if (!WallBetween(data.Self.Position, new Point(data.Self.Position.X + 1, data.Self.Position.Y)) && IsWithinGameBoard(data.Self.Position.X + 1, data.Self.Position.Y)) {

            } else if (!WallBetween(data.Self.Position, new Point(data.Self.Position.X - 1, data.Self.Position.Y)) && IsWithinGameBoard(data.Self.Position.X - 1, data.Self.Position.Y)) {

            } else if (!WallBetween(data.Self.Position, new Point(data.Self.Position.X, data.Self.Position.Y - optimalDirection)) && IsWithinGameBoard(data.Self.Position.X, data.Self.Position.Y - optimalDirection)) {

            } else {
                Console.WriteLine("Something dun goofd");
            }
            return;
        }
        private int FindLowestWeight() {
            int i = 0;
            foreach (List<Node> path in paths) {
                foreach (Node node in path) {
                    foreach (List<Node> nextPath in paths) {
                        foreach (Node nextNode in nextPath) {
                            if (node.weight < nextNode.weight) {
                                i = paths.IndexOf(path);
                            }
                        }
                    }
                }
            }
            return i;
        }

        private bool WallBetween(Point start, Point end) {
            if (start.X == end.X) {
                int num = Math.Min(start.Y, end.Y);
                return data.HorizontalWall[start.X, num];
            }
            if (start.Y == end.Y) {
                int num2 = Math.Min(start.X, end.X);
                return data.VerticalWall[num2, start.Y];
            }
            return true;
        }

        protected bool IsWithinGameBoard(int column, int row) {
            if (0 <= column && column < data.Tiles.GetLength(0) && 0 <= row) {
                return row < data.Tiles.GetLength(1);
            }
            return false;
        }

        private void GetGoal(GameData status) {
            if (status.Self.Position.Y == 0) {
                goalPosition = status.Tiles.GetLength(1);
                optimalDirection = 1;
            } else if (status.Self.Position.Y == status.Tiles.GetLength(1)) {
                goalPosition = 0;
                optimalDirection = -1;
            }
        }

        void Search(Graph g, int src) {
            BellmanFordSP pathSearch = new BellmanFordSP(graph, src);
            Stack<int>[] paths;
            Stack<int> pathToChoose = null;
            paths = new Stack<int>[tiles.GetLength(0)];
            for (int i = 0; i < tiles.GetLength(0); i++) {
                paths[i] = new Stack<int>();
                paths[i] = pathSearch.PathTo(goal + i);
            }
            for (int j = 0; j < paths.Length; j++) {
                if (j >= 0 && pathToChoose == null) {
                    pathToChoose = paths[j];
                }
                if (paths[j] != null) {
                    if (pathToChoose.Count > paths[j].Count) {
                        pathToChoose = paths[j];
                    }
                }
            }
            if (pathToChoose != null) {
                ConvertToMove(pathToChoose);
            } else {
                Stall();
            }
        }

        void Stall() {
            List<int> moves = graph.Adj(current1DPos);
            if (moves.Count > 0) {
                nextCoordinates = Get2DFrom1D(moves[0], tiles.GetLength(0), tiles.GetLength(1));
            }
        }
    }
}