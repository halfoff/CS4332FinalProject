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
    class Screen
    {
        //background
        Texture2D background;
        List<NPC> inhabitants; 
        List<Rectangle> scenery;
        Vector2 PlayerEnterLocation;
        public int id;

        public Screen(int d, Texture2D bg, NPC[] npcs, List<Rectangle> r, Vector2 pel)
        {
            id = d;
            PlayerEnterLocation = pel;
            inhabitants = new List<NPC>();
            background = bg;
            for (int i = 0; i < npcs.Length; i++)
            {
                inhabitants.Add(npcs[i]);
            }
            scenery = r;

        }

        public List<NPC> getNPC()
        {
            return inhabitants;
        }

        public Texture2D getBackground()
        {
            return background;
        }

        public void setBackground(Texture2D nt)
        {
            background = nt;
        }
        public void addNPC(NPC n)
        {
            inhabitants.Add(n);
        }
        public void removeNPC(NPC n)
        {
            inhabitants.Remove(n);
        }
        
        public Vector2 getPEL()
        {
            return PlayerEnterLocation;
        }
        public List<Rectangle> getScenery()
        {
            return scenery;
        }

    }
}
