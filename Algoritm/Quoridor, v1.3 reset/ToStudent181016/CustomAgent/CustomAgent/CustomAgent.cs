using System;
using System.Collections.Generic;
using System.Xml.Linq;


namespace Quoridor.AI {

    class CustomAgent : Agent {
        Graph graph;
        Tile[,] tiles;
        Tuple<int, int> nextCoordinates = null;
        int current1DPos, nextPlayer1DPos, goal, playerWalls, enemyWalls, wallX, wallY, playerGoal, enemyGoal, xDim, yDim;
        Action move, placeWall;
        Stack<int> playerPath, enemyPath;
        Point enemyPos;
        Player enemy, player;
        GameData data;
        bool placingWall, isVertical;

        public static void Main() {
            new CustomAgent().Start();
        }

        public override Action DoAction(GameData status) {
            tiles = status.Tiles;
            player = status.Self;
            xDim = tiles.GetLength(0);
            yDim = tiles.GetLength(1);

            if (status.Players[0] == player) {
                enemy = status.Players[1];
                playerGoal = 0;
                enemyGoal = tiles.Length - xDim;
            } else {
                enemy = status.Players[0];
                playerGoal = tiles.Length - xDim;
                enemyGoal = 0;
            }

            foreach (Player currentPlayer in status.Players) { //Får info om spelarna
                current1DPos = currentPlayer.Position.Y * tiles.GetLength(0) + currentPlayer.Position.X;
                
                //if (currentPlayer.Color == Color.Blue) {
                //    goal = tiles.Length - xDim;
                //    yOff = 0;
                //}
                //else {
                //    goal = 0;
                //    yOff = -1;
                //}

                if (currentPlayer == player) { //Spelaren
                    nextPlayer1DPos = enemy.Position.Y * tiles.GetLength(0) + enemy.Position.X;
                    CreateGraph(status, enemy);
                    RemoveEdge(nextPlayer1DPos);
                    playerWalls = currentPlayer.NumberOfWalls;
                    playerPath = Search(graph, current1DPos, playerGoal);
                }
                else { //Motståndaren
                    CreateGraph(status, player);
                    enemyPos = currentPlayer.Position;
                    enemyWalls = currentPlayer.NumberOfWalls;
                    enemyPath = Search(graph, current1DPos, enemyGoal);
                }
            }
            if(playerPath != null) {
                if (enemyPath.Count <= playerPath.Count && enemyPath.Count <= 4) { //Fienden har en närmare väg!
                    if (playerWalls > 0) {
                        placingWall = true;
                        ConvertToMove(enemyPath);
                        wallX = nextCoordinates.Item2;
                        wallY = nextCoordinates.Item1;
                        while (placingWall) {
                            if (enemyPos.Y == nextCoordinates.Item1) { //fienden ska rör sig i X-riktning
                                isVertical = true;
                                if (LegalWall(status)) {
                                    Console.WriteLine("Next wall is on (" + wallX + ", " + wallY + "). The wall is: Vertical");

                                    placeWall = new PlaceWallAction(wallX, wallY, WallOrientation.Vertical);
                                    return placeWall;
                                }
                                if (wallY < (nextCoordinates.Item1 + 1)) {
                                    placingWall = false; //Muren är onödig
                                }
                            } else { //fienden ska röra sig i Y riktning
                                isVertical = false;
                                if (LegalWall(status)) {
                                    //Console.WriteLine("Next wall is on (" + wallX + ", " + wallY + "). The wall is: Horizontal");
                                    placeWall = new PlaceWallAction(wallX, wallY, WallOrientation.Horizontal);
                                    return placeWall;
                                }
                                if (wallX < (nextCoordinates.Item2 - 1)) {
                                    placingWall = false; //Muren är onödig
                                }
                            }
                        }
                    }
                }
                ConvertToMove(playerPath);
            } else {
                Stall();
            }

            move = new MoveAction(nextCoordinates.Item2, nextCoordinates.Item1);
            return move;
        }

        private bool LegalWall(GameData status) {
            int wall1DPos = wallY * tiles.GetLength(0) + wallX;
            nextPlayer1DPos = enemy.Position.Y * tiles.GetLength(0) + enemy.Position.X;
            if (!(wallX >= 0 && (wallX + 1) < xDim)) { //wallX når utanför spelplanen
                wallX--;
                return false;
            }
            if (!(wallY >= 0 && (wallY + 1) < yDim)) { //wallY når utanför spelplanen
                wallY--;
                return false;
            }
            
            //if (wallX >= 0 && (wallX + 1) <= tiles.GetLength(0) - 1 && wallY >= 0 && (wallY + 1) <= tiles.GetLength(1) - 1) { // nya muren kommer vara inanför spelet
            if (isVertical) { //Den ska vara vertikal
                for (int i = (wallY - 1); i <= (wallY + 1); i++) {
                    Console.WriteLine("Place wall on [" + wallX + ", " + i + "]");
                    if (i >= 0 && i + 1 <= tiles.GetLength(1) && wallX < 8) {
                        if (status.VerticalWall[wallX, i]) {
                            wallY--; //Den krockar med en vertical mur
                            return false;
                        }
                    }
                }
                if (wallY > 0 && wallX > 0) {
                    if (status.HorizontalWall[wallX - 1, wallY - 1]) {
                        wallY--; //Den krockar med en horizontel mur
                        return false;
                    }
                }
                CreateGraph(status, enemy);
                graph.RemoveEdge(wall1DPos + wallY, wall1DPos + 1 + wallY);
                enemyPath = Search(graph, nextPlayer1DPos, enemyGoal);
                if (enemyPath != null) {
                    return true;
                } else {
                    wallY--;
                    return false;
                }
            } else {
                for (int i = (wallX - 1); i <= (wallX + 1); i++) {
                    if (i >= 0 && i + 1 < xDim) {
                        if (status.HorizontalWall[i, wallY]) {
                            wallX--; //Den krockar med en horizontel mur
                            return false;
                        }
                    }
                }
                if (wallY > 0 && wallX > 0) {
                    if (status.VerticalWall[wallX - 1, wallY - 1]) {
                        wallX--; //Den krockar med en vertical mur
                        return false;
                    }
                }
                CreateGraph(status, enemy);
                Console.WriteLine("What is wall1DPos? " + wall1DPos);
                graph.RemoveEdge(wall1DPos, wall1DPos + xDim);
                graph.RemoveEdge(wall1DPos + 1, wall1DPos + 1 + xDim);
                enemyPath = Search(graph, nextPlayer1DPos, enemyGoal);
                if (enemyPath != null) {
                    return true;
                } else {
                    wallX--;
                    return false;
                }
            }
        }

        void CreateGraph(GameData status, Player nextPlayer) {
            graph = new Graph(tiles.Length, tiles);

            for (int i = 0; i < status.HorizontalWall.Length; i++) {
                Tuple<int, int> index = Get2DFrom1D(i, status.Tiles.GetLength(1),status.Tiles.GetLength(0));

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
        }

        void RemoveEdge(int v) {
            graph.RemoveEdge(v, v + 1);
            graph.RemoveEdge(v, v - 1);
            graph.RemoveEdge(v, v + tiles.GetLength(0));
            graph.RemoveEdge(v, v - tiles.GetLength(1));
        }

        void ConvertToMove(Stack<int> path) {
            int index = path.Pop();
            nextCoordinates = Get2DFrom1D(index, tiles.GetLength(0), tiles.GetLength(1));
        }

        private Tuple<int, int> Get2DFrom1D(int index, int xDim, int yDim) {
            int i = 0;
            int j = 0;
            i = index / xDim;
            j = index % yDim; 
            Tuple<int, int> newIndex = new Tuple<int, int>(i, j);
            return newIndex;
        }

        Stack<int> Search(Graph graph, int src, int goal) {
            BreadthFirstPaths pathSearch = new BreadthFirstPaths(graph, src);
            Stack<int>[] paths = null;
            Stack<int> pathToChoose = null;

            paths = new Stack<int>[tiles.GetLength(0)];
            for (int i = 0; i < tiles.GetLength(0); i++) {
                paths[i] = new Stack<int>();
                paths[i] = pathSearch.PathTo(goal + i);
            }
            for (int j = 0; j < paths.Length; j++) {
                if (pathToChoose == null) {
                    pathToChoose = paths[j];
                }
                if (paths[j] != null) {
                    if (pathToChoose.Count > paths[j].Count) {
                        pathToChoose = paths[j];
                    }
                }
            }
            return pathToChoose;
        }

        void Stall() {
            List<int> moves = graph.Adj(current1DPos);
            if (moves.Count > 0) {
                nextCoordinates = Get2DFrom1D(moves[0], tiles.GetLength(0), tiles.GetLength(1));
            }
        }
    }
}