using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Game {
    public enum PlayerStates {
        Patrol,
        Attack,
        Flee,
    }
    class Enemys : GameObjects {
        Game1 game;

        Vector2 destination, direction, distance, enemyPos;
        Rectangle sourceRec, enemyRec, enemyDest;
        List<Rectangle> wallRecList;

        bool hitWall, moving;
        int speed, scale, rotation, enemys, newDestX, newDestY;

        Random rnd;

        SpriteEffects enemyEF = SpriteEffects.None;
        PlayerStates CurrentPS;

        public Enemys(Texture2D spritesheet, Vector2 enemyPos, List<Rectangle> wallRecList, int enemys) : base(spritesheet, enemyPos) {
            this.spritesheet = spritesheet;
            this.enemyPos = enemyPos;
            this.wallRecList = wallRecList;
            this.enemys = enemys;
            hitWall = false;

            sourceRec = new Rectangle(37, 48, 25, 25);

            scale = 1;
            speed = 150;
            enemyRec = new Rectangle((int)enemyPos.X, (int)enemyPos.Y, 25, 25);
        }

        public override void Update(GameTime gameTime) {
            enemyRec.X = (int)enemyPos.X;
            enemyRec.Y = (int)enemyPos.Y;

            CurrentPS = PlayerStates.Patrol;
            //Movement();
            while (true)
            {
                switch (CurrentPS)
                {
                    case PlayerStates.Patrol:
                        PatrolState(gameTime);
                        break;

                    case PlayerStates.Flee:
                        FleeState(gameTime);
                        break;

                    case PlayerStates.Attack:
                        AttackState(gameTime);
                        break;
                }
            }

        }
        
        #region FSM
        public void Movement(GameTime gameTime)
        {
            int dir = rnd.Next(1, 5);
            if (!moving)
            {
                if (dir == 1)
                {
                    ChangeDirection(new Vector2(-1, 0));
                }
                else if (dir == 2)
                {
                    ChangeDirection(new Vector2(1, 0));
                }
                else if (dir == 3)
                {
                    ChangeDirection(new Vector2(0, -1));
                }
                else if (dir == 4)
                {
                    ChangeDirection(new Vector2(0, 1));
                }
            }
            else
            {
                pos += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Vector2.Distance(pos, destination) < 1)
                {
                    pos = destination;
                    moving = false;
                }
            }
        }

        private void ChangeDirection(Vector2 direction)
        {
            this.direction = direction;
            Vector2 newDestination = enemyPos + (this.direction * 25);

            newDestX = (int)newDestination.X;
            newDestY = (int)newDestination.Y;
            enemyDest = new Rectangle(newDestX, newDestY, 25, 25);

            foreach (Rectangle wallRec in wallRecList)
            {
                if (wallRec.Intersects(enemyDest))
                {
                    hitWall = true;
                    break;
                }
                else
                {
                    hitWall = false;
                }
            }

            if (!hitWall)
            {
                destination = newDestination;
                moving = true;
            }
            if (hitWall)
            {
                moving = false;
            }
        }

        public void PatrolState(GameTime gameTime) {
            Movement(gameTime);
        }

        public void AttackState(GameTime gameTime) {

        }

        public void FleeState(GameTime gameTime) {

        }

        #endregion        

        public override void Draw(SpriteBatch sb) {
            sb.Draw(spritesheet, new Vector2(enemyPos.X + 12, enemyPos.Y + 12), sourceRec, Color.White, rotation, new Vector2(12.5f, 12.5f), scale, enemyEF, 1);
        }
    }
}
