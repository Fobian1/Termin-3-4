﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace AI_Game {
    
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Ground ground;
        Walls walls;
        Player thePlayer;
        Enemys [] theEnemys;

        List<GameObjects> gameList;
        List<Ground> groundList;
        List<Rectangle> wallRecList;
        List<Vector2> playerPosList, wallPosList, groundPosList, posList, enemyPosList;
        Vector2 pos, playerPos, enemyPos;
        Texture2D spritesheet;        

        string getLine;
        string[] printMap, printObjects;
        private Rectangle wallRec;
        int groundX, groundY, player, enemyAmount;
        public int level, currentLevel, enemys;
        char letters;

        
        KeyboardState keyState, oldKeyState;
        MouseState mouseState, oldMouse;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1925;
            graphics.PreferredBackBufferHeight = 1100;

            gameList = new List<GameObjects>();
            posList = new List<Vector2>();
            playerPosList = new List<Vector2>();
            enemyPosList = new List<Vector2>();
            wallPosList = new List<Vector2>();
            groundPosList = new List<Vector2>();
            groundList = new List<Ground>();
            wallRecList = new List<Rectangle>();
            enemys = 1;
            level = 1;
            currentLevel = 0;

            printMap = new String[level];
            printObjects = new string[level];

            FileReader();
            enemyAmount = 0;
        }
        protected override void Initialize() {         

            base.Initialize();
        }

        protected override void LoadContent() {
            Textures();
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            thePlayer = new Player(spritesheet, playerPos, wallRecList, player);
            theEnemys = new Enemys[enemys];

            playerPosList = posGiver(printObjects, 'p');
            foreach (Vector2 pos in playerPosList) {
                    thePlayer = new Player(spritesheet, pos, wallRecList, player);
            }

            enemyPosList = posGiver(printObjects, 'e');
            foreach (Vector2 pos in enemyPosList) {
                if (enemyAmount < enemys) {
                    theEnemys[enemyAmount] = new Enemys(spritesheet, pos, wallRecList, enemyAmount);
                    enemyAmount++;
                }
            }

            groundPosList = posGiver(printMap, '-');
            foreach (Vector2 pos in groundPosList) {
                ground = new Ground(spritesheet, pos);
                gameList.Add(ground);
                groundList.Add(ground);
            }

            wallPosList = posGiver(printMap, 'v');
            foreach (Vector2 pos in wallPosList) {
                walls = new Walls(spritesheet, pos);
                gameList.Add(walls);
                wallRec = new Rectangle((int)pos.X, (int)pos.Y, 25, 25);
                wallRecList.Add(wallRec);
            }
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            keyState = Keyboard.GetState();

            Play(gameTime);

            base.Update(gameTime);
        }

        #region Map
        public void FileReader() { 
            for (int i = 0; i < 1; i++) {
                StreamReader fileMap = new StreamReader("Map.txt"); 
                while (!fileMap.EndOfStream) {
                    getLine += fileMap.ReadLine();
                }
                fileMap.Close();
                printMap[i] = getLine;
                getLine = "";

                StreamReader fileObjects = new StreamReader("objMap.txt");
                while (!fileObjects.EndOfStream) {
                    getLine += fileObjects.ReadLine();
                }
                fileObjects.Close();
                printObjects[i] = getLine;
                getLine = "";
            }
        }
        public List<Vector2> posGiver(String[] printLevel, char getLetter) {
            posList.Clear();
            for (int i = 0; i < printLevel[currentLevel].Length; i++) {
                letters = printLevel[currentLevel][i];
                if (letters == getLetter) {
                    pos = new Vector2(groundX, groundY);
                    groundX += 25;
                    posList.Add(pos);
                } else if (letters == '|') {
                    groundX = 0;
                    groundY += 25;
                } else if (letters == '#') {
                    groundX = 0;
                    groundY = 0;
                } else {
                    groundX += 25;
                }
            }
            return posList;
        }

            public void Textures() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spritesheet = Content.Load<Texture2D>("Spritesheet");
        }
        #endregion

        public void Play(GameTime gameTime) {
            IsMouseVisible = true;
            thePlayer.Update(gameTime);
            theEnemys[enemys].Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            foreach (GameObjects gameO in gameList) {
                gameO.Draw(spriteBatch);
            }

            for (int i = 0; i < enemys; i++) {
                theEnemys[i].Draw(spriteBatch);
            }            

            thePlayer.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
