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
    class Sorten // Jeder Sorte liegt der Klasse Sorten zu Grunde
    {
        // Alle Sorten haben die folgenden Eigenschaften:
        public Texture2D Sprite; // Das auf dem Bildschirm zu malende Bild
        public bool WurdeErstellt; // Ob die Zutat erstellt wurde oder nicht

        public Sorten(Texture2D ZuLadendeTextur) 
        // Der Konstrucktor mit voreingestellten Eigenschaften
        {
            Sprite = ZuLadendeTextur; // Das Bild wird zur Vereinfachung schon 
                                      // mit dem Parameter des Konstrucktors deklariert
            WurdeErstellt = false; // Alle Sorten dürfen zu Anfang nicht zu sehen sein
            
        }
    }
}
