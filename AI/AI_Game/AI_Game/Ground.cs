using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Game {
    class Ground : GameObjects{
        Rectangle groundRec;

        public Ground(Texture2D spriteSheet, Vector2 groundPos) : base(spriteSheet, groundPos) {
            this.spritesheet = spriteSheet;
            groundRec = new Rectangle((int)groundPos.X, (int)groundPos.Y, 25, 25);
        }
        public override void Update(GameTime gameTime) {
        }

        public override void Draw(SpriteBatch sb) {
        }
    }
}
