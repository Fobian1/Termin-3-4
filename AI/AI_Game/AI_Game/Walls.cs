using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Game {
    class Walls : GameObjects {
        Rectangle sourceRec;

        public Walls(Texture2D spriteSheet, Vector2 wallPos) : base(spriteSheet, wallPos) {
            this.spritesheet = spriteSheet;
            sourceRec = new Rectangle(5, 5, 25, 25);
            wallRec = new Rectangle((int)wallPos.X, (int)wallPos.Y, 25, 25);
        }

        public override void Update(GameTime gameTime) {

        }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(spritesheet, wallRec, sourceRec, Color.White);
        }
    }
}
