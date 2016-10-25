using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MaGuffin
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Textures
        Texture2D txtr_protag;
        Texture2D txtr_protagFront;
        Texture2D txtr_protagBack;
        Texture2D txtr_protagLeft;
        Texture2D txtr_protagRight;
        Texture2D txtr_blacksmith;
        Texture2D txtr_fisherman;
        Texture2D txtr_gluemaker;
        Texture2D txtr_seamstress;
        Texture2D txtr_manA;
        Texture2D txtr_manB;
        Texture2D txtr_manC;
        Texture2D txtr_manD;
        Texture2D txtr_womanA;
        Texture2D txtr_womanB;
        Texture2D txtr_womanC;
        Texture2D txtr_womanD;
        Texture2D txtr_womanE;
        //Vectors
        Vector2 v_protagLoc;
        Vector2 v_protagSpd;
        //Other
        int count;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Initialize vector values
            v_protagLoc = new Vector2(64,64);
            v_protagSpd = Vector2.Zero;

            //Other
            count = 0;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load texture files
            txtr_protagFront = Content.Load<Texture2D>("protag_forward");
            txtr_protagBack = Content.Load<Texture2D>("protag_back");
            txtr_protagLeft = Content.Load<Texture2D>("protag_left");
            txtr_protagRight = Content.Load<Texture2D>("protag_right");
            txtr_protag = txtr_protagFront;

            txtr_blacksmith = Content.Load<Texture2D>("npc_blacksmith_forward");
            txtr_fisherman = Content.Load<Texture2D>("npc_fisherman_forward");
            txtr_gluemaker = Content.Load<Texture2D>("npc_gluemaker_forward");
            txtr_seamstress = Content.Load<Texture2D>("npc_seamstress_forward");
            txtr_manA = Content.Load<Texture2D>("npc_manA_forward");
            txtr_manB = Content.Load<Texture2D>("npc_manB_forward");
            txtr_manC = Content.Load<Texture2D>("npc_manC_forward");
            txtr_manD = Content.Load<Texture2D>("npc_manD_forward");
            txtr_womanA = Content.Load<Texture2D>("npc_womanA_forward");
            txtr_womanB = Content.Load<Texture2D>("npc_womanB_forward");
            txtr_womanC = Content.Load<Texture2D>("npc_womanC_forward");
            txtr_womanD = Content.Load<Texture2D>("npc_womanD_forward");
            txtr_womanE = Content.Load<Texture2D>("npc_womanE_forward");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                //this.Exit();

            if (count == 0)
                checkInput();

            v_protagLoc += v_protagSpd; //updates protag location based on speed

            if (count == 3) count = 0;
            else count++;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            int x = 16;
            int y = 16;
            int space = 16;

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            //Draw sprites
            spriteBatch.Draw(txtr_protag, v_protagLoc, Color.White);
            
            spriteBatch.Draw(txtr_blacksmith, new Vector2(x,y), Color.White);
            spriteBatch.Draw(txtr_fisherman, new Vector2(x + (space * 1), y), Color.White);
            spriteBatch.Draw(txtr_gluemaker, new Vector2(x + (space * 2), y), Color.White);
            spriteBatch.Draw(txtr_seamstress, new Vector2(x + (space * 3), y), Color.White);
            spriteBatch.Draw(txtr_manA, new Vector2(x, y + (space * 1)), Color.White);
            spriteBatch.Draw(txtr_manB, new Vector2(x + (space * 1), y + (space * 1)), Color.White);
            spriteBatch.Draw(txtr_manC, new Vector2(x + (space * 2), y + (space * 1)), Color.White);
            spriteBatch.Draw(txtr_manD, new Vector2(x + (space * 3), y + (space * 1)), Color.White);
            spriteBatch.Draw(txtr_womanA, new Vector2(x, y + (space * 2)), Color.White);
            spriteBatch.Draw(txtr_womanB, new Vector2(x + (space * 1), y + (space * 2)), Color.White);
            spriteBatch.Draw(txtr_womanC, new Vector2(x + (space * 2), y + (space * 2)), Color.White);
            spriteBatch.Draw(txtr_womanD, new Vector2(x + (space * 3), y + (space * 2)), Color.White);
            spriteBatch.Draw(txtr_womanE, new Vector2(x + (space * 4), y + (space * 2)), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void checkInput()
        {
            KeyboardState keyboard = Keyboard.GetState();
            int moveIncrement = 3; //indicates how far character will move each update

            if (keyboard.IsKeyDown(Keys.Up)) { 
                v_protagSpd = new Vector2(0,-1*moveIncrement);
                txtr_protag = txtr_protagBack;
            }
            else if (keyboard.IsKeyDown(Keys.Down)){ 
                v_protagSpd = new Vector2(0,moveIncrement);
                txtr_protag = txtr_protagFront;
            }
            else if(keyboard.IsKeyDown(Keys.Left)){ 
                v_protagSpd = new Vector2(-1*moveIncrement,0);
                txtr_protag = txtr_protagLeft;
            }
            else if(keyboard.IsKeyDown(Keys.Right)){ 
                v_protagSpd = new Vector2(moveIncrement,0);
                txtr_protag = txtr_protagRight;
            }
            else{ v_protagSpd = Vector2.Zero; }
        }
    }
}
