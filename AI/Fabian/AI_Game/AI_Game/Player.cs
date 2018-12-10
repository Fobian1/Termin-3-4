using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Game {
    class Player : GameObjects {
        Vector2 direction, playerPos, destination, mousePos, distance;
        Rectangle playerHitBox, playerDest, sourceRec;
        List<Rectangle> wallRecList;
        KeyboardState keyState, oldKeyState;
        MouseState mouseState, oldMouseState;

        int newDestX, newDestY, player;
        bool hitWall, isMoving;
        float speed, scale, rotation;

        SpriteEffects playerEF = SpriteEffects.None;

        public Player(Texture2D spriteSheet, Vector2 playerPos, List<Rectangle> wallRecList, int player) : base (spriteSheet, playerPos) {
            this.spritesheet = spriteSheet;
            this.playerPos = playerPos;
            this.wallRecList = wallRecList;
            this.player = player;

            sourceRec = new Rectangle(4, 48, 25, 25);

            //isMoving = false;
            scale = 1;
            speed = 150;
            //playerEF = SpriteEffects.None;
            playerHitBox = new Rectangle((int)playerPos.X, (int)playerPos.Y, 25, 25);
        }

        public override void Update(GameTime gameTime) {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);
            distance.X = mousePos.X - playerPos.X;
            distance.Y = mousePos.Y - playerPos.Y;

            rotation = (float)Math.Atan2(distance.Y, distance.X) + (float)Math.PI / 2;

            if (!isMoving) {
                if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left)) {
                    ChangeDirection(new Vector2(-1, 0));

                } else if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right)) {
                    ChangeDirection(new Vector2(1, 0));

                } else if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up)) {
                    ChangeDirection(new Vector2(0, -1));

                } else if (keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down)) {
                    ChangeDirection(new Vector2(0, 1));
                }
            } else {
                playerPos += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Vector2.Distance(playerPos, destination) < 1) {
                    playerPos = destination;
                    playerHitBox = new Rectangle((int)playerPos.X, (int)playerPos.Y, 25, 25);
                    isMoving = false;
                }
            }


            oldKeyState = keyState;
            oldMouseState = mouseState;
        }

        #region Values
        public Rectangle GetRect() {
            return playerHitBox;
        }

        public Vector2 SendPos() {
            return playerPos;
        }
        #endregion

        private void ChangeDirection(Vector2 direction) {
            this.direction = direction;
            Vector2 newDestination = playerPos + (this.direction * 25);

            newDestX = (int)newDestination.X;
            newDestY = (int)newDestination.Y;
            playerDest = new Rectangle(newDestX, newDestY, 25, 25);

            foreach (Rectangle wallRec in wallRecList) {
                if (wallRec.Intersects(playerDest)) {
                    hitWall = true;
                    break;
                } else {
                    hitWall = false;
                }
            }

            if (!hitWall) {
                destination = newDestination;
                isMoving = true;
            }
            if (hitWall) {
                isMoving = false;
            }
        }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(spritesheet, new Vector2(playerPos.X + 12, playerPos.Y + 12), sourceRec, Color.White, rotation, new Vector2(12.5f, 12.5f), scale, playerEF, 1);
            //sb.Draw(spriteSheet, new Vector2(playerPos.X + 12, playerPos.Y + 12), sourceWeaponRect, Color.White, rotation + 3.1415f, new Vector2(12.5f, 12.5f), scale, playerFx, 1);
        }
    }
}
