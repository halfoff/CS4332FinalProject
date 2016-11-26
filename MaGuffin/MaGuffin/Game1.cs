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
        int w_height = 400; //screen height
        int w_width = 544; //screen width
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
        Texture2D txtr_citymap;
        Texture2D txtr_textbox;
        //Vectors
        Vector2 v_protagLoc;
        Vector2 v_protagSpd;
        //Font
        SpriteFont normalFont;
        SpriteFont headerFont;
        //Other
        int count;
        Boolean lockMovement;
        String npcName;
        String npcText;
        List<NPC> list_npc = new List<NPC>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = w_height;
            graphics.PreferredBackBufferWidth = w_width;
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
            lockMovement = false;
            npcName = "";
            npcText = "";

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

            txtr_citymap = Content.Load<Texture2D>("city");
            txtr_textbox = Content.Load<Texture2D>("textbox");

            //Fonts
            normalFont = Content.Load<SpriteFont>("font");
            headerFont = Content.Load<SpriteFont>("boldfont");

            setUpNPCs();
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

            if (count == 0 && !lockMovement)
                checkInput();
            else if (count == 0 && lockMovement)
                checkTextProgression();

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

            //Draw map
            spriteBatch.Draw(txtr_citymap, Vector2.Zero, Color.White);

            //Draw NPC sprites
            for(int i = 0; i < list_npc.Count; i++)
                spriteBatch.Draw(list_npc[i].getSprite(), list_npc[i].getLoc(), Color.White);

            spriteBatch.Draw(txtr_protag, v_protagLoc, Color.White);

            //Draw textbox
            if (npcName != "" && npcText != "")
            {
                spriteBatch.Draw(txtr_textbox, Vector2.Zero, Color.White);
                spriteBatch.DrawString(headerFont, npcName, new Vector2(20, 275), Color.Black);
                spriteBatch.DrawString(normalFont, npcText, new Vector2(20, 300), Color.Black);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void checkInput()
        {
            KeyboardState keyboard = Keyboard.GetState();
            v_protagSpd = Vector2.Zero;
            int moveIncrement = 2; //indicates how far character will move each update

            if (keyboard.IsKeyDown(Keys.Up)) {
                if(v_protagLoc.Y > 6 && !collideNPC(0))
                    v_protagSpd = new Vector2(0,-1*moveIncrement);
                txtr_protag = txtr_protagBack;
            }
            else if (keyboard.IsKeyDown(Keys.Down) && !collideNPC(1))
            {
                if (v_protagLoc.Y < w_height-16)
                    v_protagSpd = new Vector2(0,moveIncrement);
                txtr_protag = txtr_protagFront;
            }
            else if (keyboard.IsKeyDown(Keys.Left) && !collideNPC(2))
            {
                if (v_protagLoc.X > 6)
                    v_protagSpd = new Vector2(-1*moveIncrement,0);
                txtr_protag = txtr_protagLeft;
            }
            else if (keyboard.IsKeyDown(Keys.Right) && !collideNPC(3))
            {
                if (v_protagLoc.X < w_width-16)
                    v_protagSpd = new Vector2(moveIncrement,0);
                txtr_protag = txtr_protagRight;
            }
            else if (keyboard.IsKeyDown(Keys.Space))
            {
                int possible = canInteractNPC();
                if (possible > -1)
                {
                    lockMovement = true;
                    Interaction i = list_npc[possible].getInteraction();
                    npcName = list_npc[possible].getName();
                    npcText = i.getText();
                }
            }
        }

        public void checkTextProgression()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Space))
            {
                lockMovement = false;
                npcName = "";
                npcText = "";
            }
        }

        /* 0 - up, 1 - down, 2 - left, 3 - right */
        public Boolean collideNPC(int dir)
        {
            for (int i = 0; i < list_npc.Count; i++)
            {
                Boolean c = list_npc[i].checkCollision(dir, v_protagLoc);
                if (c) return true;
            }
            return false;
        }

        public int canInteractNPC()
        {
            for (int i = 0; i < list_npc.Count; i++)
            {
                Boolean c = list_npc[i].canInteract(v_protagLoc);
                if (c) return i;
            }
            return -1;
        }

        public void setUpNPCs()
        {
            NPC blacksmith = new NPC("Blacksmith", txtr_blacksmith, new Vector2(410, 230));
            blacksmith.addInteraction(1, "You want a sword, eh? What would a shrimp like you\ndo with a sword?");
            blacksmith.addInteraction(-1, "I already told you no. I'm not giving you a\nsword. Shoo!");

            NPC seamstress = new NPC("Seamstress", txtr_seamstress, new Vector2(190, 135));
            seamstress.addInteraction(-1, "I'm working on a new outfit. Isn't it lovely?");

            NPC fisherman = new NPC("Fisherman", txtr_fisherman, new Vector2(345, 135));
            fisherman.addInteraction(-1, "I come here every afternoon to drop a line and\ncatch some fish.");

            NPC gluemaker = new NPC("Craftsman", txtr_gluemaker, new Vector2(220, 325));
            gluemaker.addInteraction(-1, "I've just moved to town. I hope the citizens\nhere will buy my wares.");

            NPC manA = new NPC("Harold", txtr_manA, new Vector2(440, 25));
            manA.addInteraction(-1, "-pant pant- Let me catch my breath.");

            NPC manB = new NPC("Samson", txtr_manB, new Vector2(70, 230));
            manB.addInteraction(-1, "What are you looking at?");

            NPC womanB = new NPC("Sequoia", txtr_womanB, new Vector2(470, 110));
            womanB.addInteraction(-1, "I seem to be terribly lost. Can you help me?");

            NPC womanC = new NPC("Marri", txtr_womanC, new Vector2(120, 40));
            womanC.addInteraction(-1, "I'm not doing anything! Honest! Leave me alone!");

            list_npc.Add(blacksmith);
            list_npc.Add(seamstress);
            list_npc.Add(fisherman);
            list_npc.Add(gluemaker);
            list_npc.Add(manA);
            list_npc.Add(manB);
            list_npc.Add(womanB);
            list_npc.Add(womanC);
        }
    }
}
