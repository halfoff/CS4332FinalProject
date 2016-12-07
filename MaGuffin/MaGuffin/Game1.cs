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
        Texture2D txtr_grate;
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
        String[] npcText;
        int textProg, textMax;
        List<NPC> list_npc = new List<NPC>();
        List<Rectangle> list_scenery = new List<Rectangle>();
        Texture2D singlePixel;
        String protagInventory;

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
            v_protagLoc = new Vector2(187,375);
            v_protagSpd = Vector2.Zero;

            //Initialize single pixel texture
            singlePixel = new Texture2D(this.GraphicsDevice, 1, 1);
            singlePixel.SetData(new Color[] { Color.White });

            //Other
            count = 0;
            lockMovement = true;
            npcName = " ";
            npcText = new[] { "Press SPACE to advance text and talk to NPCs.\nUse the ARROW KEYS to move.",
                              "You are Mac MaGuffin, adventure extrodinare.",
                              "At least, you would be an adventurer if you\nhadn't lost your trusty sword.",
                              "Maybe someone in town will trade you one? You\ndon't have any money though, just this Cheese\nWheel..."
                            };
            textProg = 0;
            textMax = npcText.Length;
            protagInventory = "Cheese Wheel";

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
            txtr_grate = Content.Load<Texture2D>("npc_grate");

            txtr_citymap = Content.Load<Texture2D>("city");
            txtr_textbox = Content.Load<Texture2D>("textbox");

            //Fonts
            normalFont = Content.Load<SpriteFont>("font");
            headerFont = Content.Load<SpriteFont>("boldfont");

            setUpNPCs();
            setUpSceneryCollision();
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
            //Counters for checking input
            if (count == 0 && !lockMovement)
                checkInput();
            else if (count == 0 && lockMovement)
                checkTextProgression();

            v_protagLoc += v_protagSpd; //updates protag location based on speed

            //Reset counters
            if (!lockMovement && count >= 3) count = 0;
            else if(lockMovement && count >= 5) count = 0;
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
            for (int i = 0; i < list_npc.Count; i++)
            {
                spriteBatch.Draw(list_npc[i].getSprite(), list_npc[i].getLoc(), Color.White);
                //spriteBatch.Draw(singlePixel, list_npc[i].getCollision(), Color.Black * 0.75f); //draw collision boxes
            }

            spriteBatch.Draw(txtr_protag, v_protagLoc, Color.White);

            //Draw collision boxes
            //for (int i = 0; i < list_scenery.Count; i++)
                //spriteBatch.Draw(singlePixel, list_scenery[i], Color.White * 0.5f);

            //Draw textbox
            if (npcName != "" && npcText.Length > 0)
            {
                spriteBatch.Draw(txtr_textbox, Vector2.Zero, Color.White);
                spriteBatch.DrawString(headerFont, npcName, new Vector2(20, 275), Color.Black);
                spriteBatch.DrawString(normalFont, npcText[textProg], new Vector2(20, 300), Color.Black);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void checkInput()
        {
            KeyboardState keyboard = Keyboard.GetState();
            v_protagSpd = Vector2.Zero;
            int moveIncrement = 1; //indicates how far character will move each update

            if (keyboard.IsKeyDown(Keys.Up))
            {
                if(v_protagLoc.Y > 6 && !collideNPC(0) && !collideScenery(0))
                    v_protagSpd = new Vector2(0,-1*moveIncrement);
                txtr_protag = txtr_protagBack;
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                if (v_protagLoc.Y < w_height - 16 && !collideNPC(1) && !collideScenery(1))
                    v_protagSpd = new Vector2(0,moveIncrement);
                txtr_protag = txtr_protagFront;
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                if (v_protagLoc.X > 6 && !collideNPC(2) && !collideScenery(2))
                    v_protagSpd = new Vector2(-1*moveIncrement,0);
                txtr_protag = txtr_protagLeft;
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                if (v_protagLoc.X < w_width - 16 && !collideNPC(3) && !collideScenery(3))
                    v_protagSpd = new Vector2(moveIncrement,0);
                txtr_protag = txtr_protagRight;
            }
            else if (keyboard.IsKeyDown(Keys.Space))
            {
                int possible = canInteractNPC();
                if (possible > -1)
                {
                    Interaction i = list_npc[possible].getInteraction();
                    if (i == null) return; //if NPC is out of valid interactions, do nothing

                    lockMovement = true;
                    textProg = 0;
                    textMax = i.getText().Length;

                    //Check if NPC wants an item
                    if (i.getItemDesired().Length > 0)
                    {
                        if (i.getItemDesired().Equals(protagInventory))
                        {
                            textProg = 1;
                            protagInventory = i.getItemGiven();
                            i.decNumTimes();
                        }
                        else
                            textMax = 1;
                    }
                    else
                        i.decNumTimes();

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
                if (textProg < textMax - 1)
                    textProg++;
                else
                {
                    lockMovement = false; //allow player to move
                    textProg = 0; //clear variables
                    npcName = ""; 
                    npcText = new String[0];
                }
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

        /* 0 - up, 1 - down, 2 - left, 3 - right */
        public Boolean collideScenery(int dir)
        {
            Boolean c = false;
            for (int i = 0; i < list_scenery.Count; i++)
            {
                Rectangle obj = list_scenery[i];
                if (dir == 0) //up
                    c = v_protagLoc.X > obj.X - 16 && v_protagLoc.X < obj.X + obj.Width && v_protagLoc.Y > obj.Y && v_protagLoc.Y < obj.Y + obj.Height + 1;
                else if (dir == 1) //down
                    c = v_protagLoc.X > obj.X - 16 && v_protagLoc.X < obj.X + obj.Width && v_protagLoc.Y > obj.Y - 17 && v_protagLoc.Y < obj.Y;
                else if (dir == 2) //left
                    c = v_protagLoc.X > obj.X + obj.Width - 4 && v_protagLoc.X < obj.X + obj.Width + 2 && v_protagLoc.Y > obj.Y - 16 && v_protagLoc.Y < obj.Y + obj.Height;
                else //right
                    c = v_protagLoc.X > obj.X - 16 && v_protagLoc.X < obj.X && v_protagLoc.Y > obj.Y - 16 && v_protagLoc.Y < obj.Y + obj.Height;
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
            //BLACKSMITH
            NPC blacksmith = new NPC("Blacksmith", txtr_blacksmith, new Vector2(410, 230));
            blacksmith.addInteraction(1, new[] {"You want a sword, eh? What would a shrimp like you\ndo with a sword?"}, "", "");
            blacksmith.addInteraction(1, new[] { "I already told you no. I'm not giving you a\nsword. Shoo!",
                                                 "Wait, you have money? Hmmmm. Maybe I'll reconsider.",
                                                 "<GOLD has been exchanged for SWORD>"
                                               }, "Gold", "Sword");

            //SEAMSTRESS
            NPC seamstress = new NPC("Seamstress", txtr_seamstress, new Vector2(190, 135));
            seamstress.addInteraction(1, new[] { "I'm working on a new outfit. Isn't it lovely?",
                                                 "Woah, that hair comb you have is exquisite!\nWould you be willing to sell it to me?",
                                                 "...",
                                                 "Thank you! This will go fabulously with my\nnew dress!",
                                                 "<DECORATIVE COMB has been exchanged for GOLD>"
                                               }, "Decorative Comb", "Gold");
            seamstress.addInteraction(-1, new[] { "-hums happily" }, "", "");

            //FISHERMAN
            NPC fisherman = new NPC("Fisherman", txtr_fisherman, new Vector2(345, 135));
            fisherman.addInteraction(1, new[] {"I come here every afternoon to drop a line and\ncatch some fish."}, "", "");
            fisherman.addInteraction(1, new[] { "Curses! I seem to have run out of bait.",
                                                "Wait, you've found me more bait? Thank you,\nstranger! Let's see if the fish like it.",
                                                "-swish- -sploosh-",
                                                ".",
                                                "..",
                                                "...",
                                                "Wait! Oh, nevermind.",
                                                ".",
                                                "..",
                                                "!!!",
                                                "-whiiiiiirrrrrr-",
                                                "Wowwee! We caught a big 'un! This will feed my\nwhole family tonight!",
                                                "Here, it's well worn, but take my fishing pole.\nI've got others at home.",
                                                "<DEAD COCKROACHES has been exchanged for\nFISHING POLE>"
                                              }, "Dead Cockroaches", "Fishing Pole");
            fisherman.addInteraction(-1, new[] { "The Missus is going to be so happy tonight..." }, "", "");

            //GLUEMAKER
            NPC gluemaker = new NPC("Gluemaker", txtr_gluemaker, new Vector2(220, 325));
            gluemaker.addInteraction(1, new[] { "I've just moved to town to open up a shop.",
                                                "Hopefully I can find all of the supplies I need..."
                                              }, "", "");
            gluemaker.addInteraction(1, new[] { "I can't find any hooves to make glue with!\nWithout them, I'm doomed!", //text if item is not held
                                                "Look, I really don't have time right now, kid.\nI've got to find...",
                                                "Wait.",
                                                "Are those Boar Hooves you're holding?! You've got\nto give them to me! I'll reward you!",
                                                ".",
                                                "..",
                                                "...",
                                                "There, all done. You've really save my sorry hide,\nkid. Without you, I wouldn't have been able to\n" + 
                                                "get my glue shop off the ground.",
                                                "Here, take some of my first glue batch as a reward.",
                                                "<BOAR HOOVES has been exchanged for GLUE POT>"
                                              }, "Boar Hooves", "Glue Pot");
            gluemaker.addInteraction(-1, new[] { "Glue for sale! Freshly made glue! Buy yours now!" }, "", "");

            //HAROLD THE RUNNER
            NPC manA = new NPC("Harold", txtr_manA, new Vector2(440, 25));
            manA.addInteraction(1, new[] { "You shouldn't ever see this text. ;)", //text if item is not held
                                           "-pant pant- Let me catch my breath.",
                                           "-pant pant-",
                                           "Whew, running is tough work! I'm starving! You\nwouldn't happen to have anything to eat, would\nyou?",
                                           "...",
                                           "You do?! Wow, that Cheese Wheel looks delicious!\nThanks, stranger!",
                                           "They're not much, but I'll give you these Boar\nHooves I found while running as my thanks.",
                                           "<CHEESE WHEEL has been exchanged for BOAR HOOVES>"
                                         }, "Cheese Wheel", "Boar Hooves");
            manA.addInteraction(3, new[] { "-munch munch- Thanks again, stranger!" }, "", "");
            manA.addInteraction(-1, new[] { "-braaaappp- Wow, that really hit the spot!" }, "", "");

            //SAMSON THE SAILOR
            NPC manB = new NPC("Samson", txtr_manB, new Vector2(70, 230));
            manB.addInteraction(1, new[] { "I'm staying the night at this tavern while\nmy captain recruits new sailors.",
                                           "Our last crew was a bunch of sissies, so he\nfired them all. Not me though! I'm tough.",
                                           "-skitter skitter-",
                                           "EEEEEEEEE! Is that a cockroach? Kill it, kill it!"
                                         }, "", "");
            manB.addInteraction(1, new[] { "Hurry up and kill the roach! It almost touched me!",
                                           "<You smack the cockroach with the head of the\nbroom.>",
                                           "Thanks kid. Don't tell anybody you saw that, okay?\nIf you do, I'll find you and ... and bad things\nwill happen!",
                                           "Do you mind if I keep the broom in case more of\nthem come?",
                                           "<BROOM has been exchanged for DEAD COCKROACHES>",
                                         }, "Broom", "Dead Cockroaches");
            manB.addInteraction(-1, new[] { "You stay away from me, you disgusting insects!\nI'm warning you!",
                                            "-smack!-"
                                          }, "", "");

            //SEQUOIA THE TRAVELLER
            NPC womanB = new NPC("Sequoia", txtr_womanB, new Vector2(470, 110));
            womanB.addInteraction(1, new[] { "I seem to be terribly lost. Can you help me?"}, "", "");
            womanB.addInteraction(1, new[] { "Do you have a map or something I could use?",
                                             "Is that ... a compass? Wow, it smells. Where'd\nyou get it from, the sewer?",
                                             "Hahaha! That would be disgusting. I'm sure you\ndidn't do that.",
                                             "Anyways, thank you! Now I can find my way\nthrough the woods to the next town.",
                                             "Take this decorative hair comb as thanks.\nI picked it up on my travels.",
                                             "<COMPASS has been exchanged for DECORATIVE COMB>"
                                           }, "Compass", "Decorative Comb");
            womanB.addInteraction(-1, new[] { "Wait, is there slime on this?" }, "", "");

            //MARRI THE MAID
            NPC womanC = new NPC("Marri", txtr_womanC, new Vector2(120, 40));
            womanC.addInteraction(1, new[] { "Watch out, boy. Can't you see I'm sweeping here?",
                                             "-snap!-",
                                             "Awww, rats. My broom broke. If you can find\nsomething to fix it with, I'll reward you."
                                           }, "", "");
            womanC.addInteraction(1, new[] { "Didja find something to fix my broom yet?\nGet a move on! This walkway won't sweep itself,\nyou know!",
                                             "What's that you've got there, boy? A pot of glue?\nI think that just might work.",
                                             "...",
                                             "Good as new! Just gimme a minute here...",
                                             "-sweep sweep-",
                                             "-swish swish-",
                                             "All done. Since I don't need it anymore, you can\nhave the broom. You earned it.",
                                             "<GLUE POT has been exchanged for BROOM>"
                                           }, "Glue Pot", "Broom");
            womanC.addInteraction(1, new[] { "You think I ripped you off? I didn't say I was\ngoing to give you a *good* reward, did I?" }, "", "");
            womanC.addInteraction(-1, new[] { "I'm too busy to deal with you. Scram!" }, "", "");

            //SEWER GRATES
            NPC grate1 = new NPC("Sewer Grate", txtr_grate, new Vector2(220, 50));
            grate1.addInteraction(-1, new[] {"It's a sewer grate. There's unidentified sludge\nin it."}, "", "");
            NPC grate2 = new NPC("Sewer Grate", txtr_grate, new Vector2(440, 147));
            grate2.addInteraction(-1, new[] { "It's a sewer grate. There's unidentified sludge\nin it." }, "", "");
            NPC grate3 = new NPC("Sewer Grate", txtr_grate, new Vector2(125, 325));
            grate3.addInteraction(-1, new[] { "It's a sewer grate. There's unidentified sludge\nin it." }, "", "");
            NPC grate4 = new NPC("Sewer Grate", txtr_grate, new Vector2(315, 243));
            grate4.addInteraction(1, new[] { "It's a sewer grate. There's unidentified sludge\nin it.",
                                             "Wait, something's glinting down there. What could\nit be?"
                                           }, "", "");
            grate4.addInteraction(1, new[] { "Ewww. You're not reaching your hand in there.\nMaybe you could find something to help you get\nthe object out?",
                                             "-whhhhiiiirrrr-",
                                             "-squelch-",
                                             "-slurp-",
                                             "Got it! It's a golden compass. In your excitement,\nyou accidentally drop the fishing rod into\nthe sewer. Woops.",
                                             "<FISHING POLE has been exchanged for COMPASS>"
                                           }, "Fishing Pole", "Compass");
            grate4.addInteraction(-1, new[] { "It's a sewer grate. Your fishing pole sits in the\nmuck. No getting it back now." }, "", "");

            //Add everyone to the array
            list_npc.Add(blacksmith);
            list_npc.Add(seamstress);
            list_npc.Add(fisherman);
            list_npc.Add(gluemaker);
            list_npc.Add(manA);
            list_npc.Add(manB);
            list_npc.Add(womanB);
            list_npc.Add(womanC);
            list_npc.Add(grate1);
            list_npc.Add(grate2);
            list_npc.Add(grate3);
            list_npc.Add(grate4);
        }

        public void setUpSceneryCollision()
        {
            Rectangle topWall1 = new Rectangle(0, 0, 422, 38);
            Rectangle topWall2 = new Rectangle(469, 0, 80, 38);
            Rectangle leftBuilding1 = new Rectangle(13, 80, 80, 52);
            Rectangle leftBuilding2 = new Rectangle(13, 176, 80, 52);
            Rectangle leftBuilding3 = new Rectangle(13, 272, 80, 52);
            Rectangle buildingLine1 = new Rectangle(140, 80, 328, 52);
            Rectangle buildingLine2 = new Rectangle(140, 176, 328, 52);
            Rectangle buildingLine3 = new Rectangle(140, 272, 328, 52);
            Rectangle bottomWall1 = new Rectangle(0, 368, 172, 32);
            Rectangle bottomWall2 = new Rectangle(218, 368, 332, 32);
            list_scenery.Add(topWall1);
            list_scenery.Add(topWall2);
            list_scenery.Add(leftBuilding1);
            list_scenery.Add(leftBuilding2);
            list_scenery.Add(leftBuilding3);
            list_scenery.Add(buildingLine1);
            list_scenery.Add(buildingLine2);
            list_scenery.Add(buildingLine3);
            list_scenery.Add(bottomWall1);
            list_scenery.Add(bottomWall2);
        }
    }
}
