using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaGuffin
{
    
    class Player : GameEntity
    {
        //States describing player's current action
        private enum PlayerState
        { moveL,moveR,moveU,moveD,attack,damage,interact };
        //The currently active sprite
        Texture2D currentSprite;
        //SpriteBatch containing player sprites for movement and attacking
        SpriteBatch batch;
        //The character archetype of the player (May be moved to its own subclass)
        String archetype;
        //Determines the speed of the player
        Vector2 spd;

        public Player(SpriteBatch sprites, Vector2 location, Vector2 size): base(location, (int)size.X, (int)size.Y)
        {
            
            batch = sprites;
            spd = Vector2.Zero;
            
        }

    }
}
