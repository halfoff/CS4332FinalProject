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
        { interact };
        Texture2D sprite; //The currently active sprite
        Vector2 location;

        public NPC(Texture2D s, Vector2 l)
        {
            sprite = s;
            location = l;
        }

        public Texture2D getSprite() { return sprite; }
        public Vector2 getLoc() { return location; }

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
    }
}
