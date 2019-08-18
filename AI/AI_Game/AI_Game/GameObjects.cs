using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Game {
    abstract class GameObjects {
        protected Vector2 pos;
        protected Texture2D spritesheet;
        protected List<Vector2> posList, wallList;

        protected int groundX, groundY;
        bool hitWall;

        protected Rectangle wallRec;
        Ground ground;
        Player player;
        public GameObjects(Texture2D spriteSheet, Vector2 pos) {
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch sb);

    }
}
