using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Quoridor.AI {
    class CustomAgent : Agent {
        GraphicalObject graoh;
        Tile[,] tiles;
        Tuple<int, int> nextCoordinates;
        GameData data;
        List<MoveAction> moves;
        List<List<XNode>> paths = new List<List<Node>>();
        int current1dPos, goal, goalPos, optimalDir, enemyWalls, distToGoal, distFromStart;
        Action move;

        public static void Main() {
            
            new CustomAgent().Start();
            
        }

        public override Action DoAction(GameData status) {
            nextCoordinates = null;
            moves = new List<MoveAction>();
            paths = new List<List<Node>>();
            return moves;
        }
    }
}