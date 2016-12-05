using System;
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
            interactions = new List<Interaction>();
        }

        public String getName() { return name; }
        public Texture2D getSprite() { return sprite; }
        public Vector2 getLoc() { return location; }

        public void addInteraction(int num, String text)
        {
            interactions.Add(new Interaction(num, text));
        }

        /* 0 - up, 1 - down, 2 - left, 3 - right */
        public Boolean checkCollision(int dir, Vector2 player)
        {
            if (dir == 0) //up
                return player.X > location.X - 12 && player.X < location.X + 12 && player.Y > location.Y && player.Y < location.Y + 10;
            else if (dir == 1) //down
                return player.X > location.X - 12 && player.X < location.X + 12 && player.Y > location.Y - 16 && player.Y < location.Y;
            else if (dir == 2) //left
                return player.X > location.X + 12 && player.X < location.X + 16 && player.Y > location.Y - 16 && player.Y < location.Y + 10;
            else //right
                return player.X > location.X - 16 && player.X < location.X && player.Y > location.Y - 16 && player.Y < location.Y + 10;
        }

        public Boolean canInteract(Vector2 player)
        {
            return player.X > location.X - 12 && player.X < location.X + 12 && player.Y > location.Y && player.Y < location.Y + 10;
        }

        //Returns first valid
        public Interaction getInteraction()
        {
            for (int i = 0; i < interactions.Count(); i++)
                if (interactions[i].validInteraction())
                {
                    interactions[i].decNumTimes();
                    return interactions[i];
                }
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
