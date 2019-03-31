using System;
using System.Collections.Generic;

namespace Quoridor.AI
{
    class CustomAgent : Agent
    {
        public static void Main()
        {
            new CustomAgent().Start();
        }

        public override Action DoAction(GameData status)
        {
            throw new NotImplementedException();
        }
    }
}