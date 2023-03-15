using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Collections.Generic;

namespace Aula
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont SpriteFont;
        private int nrLinhas = 0;
        private int nrColunas = 0;
        private char[,] level;
        private Texture2D player, dot, box, wall;
        int tileSize = 64;
        public List<Point> boxes;
        private Player sokoban;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            
            LoadLevel("level1.txt");
            _graphics.PreferredBackBufferHeight = tileSize * level.GetLength(1); //definição da altura
            _graphics.PreferredBackBufferWidth = tileSize * level.GetLength(0); //definição da largura
            _graphics.ApplyChanges(); //aplica a atualização da janela
            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteFont = Content.Load<SpriteFont>("Filei");
            player = Content.Load<Texture2D>("Character4");
            dot = Content.Load<Texture2D>("EndPoint_Blue");
            box = Content.Load<Texture2D>("Crate_Brown");
            wall = Content.Load<Texture2D>("Wall_Brown");
            

            // TODO: use this.Content to load your game content here
        }

        void LoadLevel(string levelFile)
        {
            string[] linhas = File.ReadAllLines($"Content/{levelFile}"); // "Content/" + level
            nrLinhas = linhas.Length;
            nrColunas = linhas[0].Length;
            level = new char[nrColunas, nrLinhas];
            boxes = new List<Point>();

            for (int x = 0; x < nrColunas; x++)
            {
                for (int y = 0; y < nrLinhas; y++)
                {
                    if (linhas[y][x] == 'Y')
                    {
                        sokoban = new Player(x, y);
                        level[x, y] = ' '; // put a blank instead of the sokoban 'Y'
                    }

                    else
                    {
                        level[x, y] = linhas[y][x];
                    }
                }

            }
            for (int x = 0; x < nrColunas; x++)
            {
                for (int y = 0; y < nrLinhas; y++)
                {
                    if (linhas[y][x] == '#')
                    {
                        boxes.Add(new Point(x, y));
                        level[x, y] = ' '; // put a blank instead of the box '#'
                    }
                    else if (linhas[y][x] == 'Y')
                    {
                        sokoban = new Player(x, y);
                        level[x, y] = ' '; // put a blank instead of the sokoban 'Y'
                    }
                    else
                    {
                        level[x, y] = linhas[y][x];
                    }
                }
            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
           
            Rectangle position = new Rectangle(0, 0, tileSize, tileSize);
            for (int x = 0; x < level.GetLength(0); x++) //pega a primeira dimensão
            {
                for (int y = 0; y < level.GetLength(1); y++) //pega a segunda dimensão
                {
                    position.X = x * tileSize; // define o position
                    position.Y = y * tileSize; // define o position
                    switch (level[x, y])
                    {
                        case 'Y':
                            _spriteBatch.Draw(player, position, Color.White);
                            break;
                        case '#':
                            _spriteBatch.Draw(box, position, Color.White);
                            break;
                        case '.':
                            _spriteBatch.Draw(dot, position, Color.White);
                            break;
                        case 'X':
                            _spriteBatch.Draw(wall, position, Color.White);
                            break;
                    }
                }
            }
            //case 'Y':
            // _spriteBatch.Draw(player, position, Color.White);
            // break;
            position.X = sokoban.Position.X * tileSize; //posição do Player
            position.Y = sokoban.Position.Y * tileSize; //posição do Player
            _spriteBatch.Draw(player, position, Color.White); //desenha o Player
                                                              //case '#':
                                                              // _spriteBatch.Draw(box, position, Color.White);
                                                              // break;
            foreach (Point b in boxes)
            {
                position.X = b.X * tileSize;
                position.Y = b.Y * tileSize;
                _spriteBatch.Draw(box, position, Color.White);
            }

            _spriteBatch.End();
            
            _spriteBatch.DrawString(SpriteFont, $"Numero de Linhas = {nrLinhas} -- Numero de Colunas ) = {nrColunas}", new Vector2(0, 0), Color.Black);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}