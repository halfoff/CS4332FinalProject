using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaGuffin
{
    
    class GameEntity
    {
        //Position of the GE
        private Vector2 loc;
        //Graphic representation of GE
        Texture2D sprite;
        //Size of GameEntity in px
        private int height, width;

        public GameEntity()
        {
            loc = new Vector2(0, 0);
        }
    }
}
