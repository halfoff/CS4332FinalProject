﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaGuffin
{
    class NPC : GameEntity
    {
        //States describing player's current action
        private enum NPCState
        { interact, idle };
        private String name;
        private List<Interaction> interactions;
        private Texture2D sprite; //The currently active sprite
        private Vector2 location;
        private Rectangle collision;
        //Used for giving the player an item
        private String requiredItem;
        private String givingItem;
        //Flag for changing dialog when item is given
        private bool gaveItemFlag;

        public NPC(String n, Texture2D s, Vector2 l)
        {
            name = n;
            sprite = s;
            location = l;
            collision = new Rectangle((int)l.X + 4, (int)l.Y, 8, 8); 
            interactions = new List<Interaction>();
        }

        public String getName() { return name; }
        public Texture2D getSprite() { return sprite; }
        public Vector2 getLoc() { return location; }
        public Rectangle getCollision() { return collision; }

        public void addInteraction(int num, String[] text, String i1, String i2)
        {
            interactions.Add(new Interaction(num, text, i1, i2));
        }

        /* 0 - up, 1 - down, 2 - left, 3 - right */
        public Boolean checkCollision(int dir, Vector2 player)
        {
            if (dir == 0) //up
                return player.X > collision.X - 16 && player.X < collision.X + collision.Width && player.Y > collision.Y && player.Y < collision.Y + collision.Height + 1;
            else if (dir == 1) //down
                return player.X > collision.X - 16 && player.X < collision.X + collision.Width && player.Y > collision.Y - 17 && player.Y < collision.Y;
            else if (dir == 2) //left
                return player.X > collision.X + collision.Width - 4 && player.X < collision.X + collision.Width + 2 && player.Y > collision.Y - 16 && player.Y < collision.Y + collision.Height;
            else //right
                return player.X > collision.X - 16 && player.X < collision.X && player.Y > collision.Y - 16 && player.Y < collision.Y + collision.Height;
        }

        public Boolean canInteract(Vector2 player)
        {
            return player.X > collision.X - 16 && player.X < collision.X + collision.Width && player.Y > collision.Y && player.Y < collision.Y + collision.Height + 5;
        }

        //Returns first valid
        public Interaction getInteraction()
        {
            for (int i = 0; i < interactions.Count(); i++)
                if (interactions[i].validInteraction())
                    return interactions[i];
            return null;
        }

        private bool checkItem(String itemToCheck)
        {
            return (itemToCheck.Equals(requiredItem));
           
        }

        public String givePlayerItem(String playerItem)
        {
            if(this.checkItem(playerItem))
            {
                gaveItemFlag = true;
                return this.givingItem;
            }

            return playerItem;
        }
    }
}
