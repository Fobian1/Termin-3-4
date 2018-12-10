using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Game {
    class Enemys : GameObjects {
        Game1 game; 

        Vector2 destination, direction, distance, enemyPos;
        Rectangle sourceRec, enemyRec;
        List<Rectangle> wallRecList;

        bool hitwall;
        int speed, scale, rotation, enemys;

        Random rnd;

        SpriteEffects enemyEF = SpriteEffects.None;

        public Enemys(Texture2D spritesheet, Vector2 enemyPos, List<Rectangle> wallRecList, int enemys) : base (spritesheet, enemyPos) {
            this.spritesheet = spritesheet;
            this.enemyPos = enemyPos;
            this.wallRecList = wallRecList;
            this.enemys = enemys;

            sourceRec = new Rectangle(37, 48, 25, 25);

            scale = 1;
            speed = 150;
            enemyRec = new Rectangle((int)enemyPos.X, (int)enemyPos.Y, 25, 25);
        }

        public override void Update(GameTime gameTime) {
            enemyRec.X = (int)enemyPos.X;
            enemyRec.Y = (int)enemyPos.Y;
        }



        public override void Draw(SpriteBatch sb) {
            sb.Draw(spritesheet, new Vector2(enemyPos.X + 12, enemyPos.Y + 12), sourceRec, Color.White, rotation, new Vector2(12.5f, 12.5f), scale, enemyEF, 1);
        }
    }
}
