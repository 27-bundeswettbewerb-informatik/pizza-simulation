using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Pizza_Simulation
{
    class Pizza
    {
        // Die Pizza hat die folgenden Eigenschaften:
        public Texture2D Sprite; // Das auf dem Bildschirm zu malende Bild
        public Vector2 Position; // DiePosition der Zutat auf dem Bildschirm


        public Pizza(Texture2D ZuLadendeTextur) 
        // Der Konstrucktor mit voreingestellten Eigenschaften
        {
            Sprite = ZuLadendeTextur; // Das Bild wird zur Vereinfachung schon 
            // mit dem Parameter des Konstrucktors deklariert
        }
    }
}
