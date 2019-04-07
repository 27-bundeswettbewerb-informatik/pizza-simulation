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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Pizza_Simulation_Allgemein : Microsoft.Xna.Framework.Game
    {
        //Die Klassen GraphicsDeviceManager, SpriteBatch und SpriteFont vom XNA Framework werden geladen
        GraphicsDeviceManager graphics; //Allgemeine Einstellungen wie Fenstergr��e, Hintergrundfarbe, etc.
        SpriteBatch spriteBatch; //Die Klasse zum Ausgeben von Sprites und Strings
        SpriteFont spriteFontgamefont; //Die benutze Schriftart f�r den spriteBatch

        MouseState Vorheriger_Maus_Status = Mouse.GetState(); //Die Position und Status(Buttons) der Maus
        Texture2D Maus_Texture; //Die Texture f�r die Maus

        int AnzahlDerSorten = 12; // Die Anzahl der Sorten wurde durch die Aufgabe auf 12 festgelegt

        //Die Felder f�r die Buttons
        Button[] ErstellButton; //Die Buttons zum Aufrufen einer Sorte
        Button[] L�schButton; //Die Buttons zum Wiederrufen einer Sorte

        Sorten[] Sorte; //Das Feld mit den Sorten

        Pizza Pizzaboden; //Einmaliges Aufrufen der Klasse Pizza mit ihrem Sprite und der Position (tr�gt nur zur Strucktur bei)

        private static Random Zufall = new Random(); //Instanzierung der Klasse Random f�r Zufall zum Erzeugen von Zufallswerten                        
                                                                            
        public Pizza_Simulation_Allgemein()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //Fenstergr��e wird festgelegt
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            //Das Fenster wird im Vollbildmodus gestartet 
            graphics.ToggleFullScreen();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFontgamefont = Content.Load<SpriteFont>("Schriftarten\\gamefont");

            Sorte = new Sorten[AnzahlDerSorten]; //Das Feld Sorten wird instanziert, mit maximal 12 Dimensionen
            for (int i = 0; i <= (AnzahlDerSorten - 1); i++)
            {
                //Solange nicht alle Sorten betrachtet wurden, bekommen diese je nach Aufz�hlung einen Sprite zugewiesen
                //Der Index des Feldes ist somit gleichzeitig die Sorte
                //�ber die Enumeration Aufz�hlungDerZutaten bekommt der Integer einen Sortennamen zugewiesen
                //Die Textur l�d sich somit mit der zusammensetzung eines Strings bestehend aus Pfad des Ordners und Name der Sorte
                //Daher ist es wichtig das die Dateien der Sorten auch den gleichen Namen haben
                Sorte[i] = new Sorten(Content.Load<Texture2D>("Sprites\\Zutaten\\" + (Aufz�hlungDerZutaten)i));
            }

            Maus_Texture = Content.Load<Texture2D>("Sprites\\Pizza\\Pizza Maus"); //Die Maus bekommt ihren Sprite zugewiesen

            ErstellButton = new Button[AnzahlDerSorten]; //Das Feld der Erstellungsbuttons wird instanziert, mit maximal 12 Dimensionen
            float ButtonAbstandVonX = 0; //ButtonAbstandVonX dient im Folgenden der Automatisierung der Platzierung der Buttons auf dem Bildschirm
            float ButtonAbstandVonY = 0; //ButtonAbstandVonY dient im Folgenden der Automatisierung der Platzierung der Buttons auf dem Bildschirm
            for (int i = 0; i <= (AnzahlDerSorten - 1); i++)
            {//Solange nicht alle Sorten betrachtet wurden wird mit den Erstellbuttons Folgendes gemacht:
                ErstellButton[i] = new Button();//Der Erstellbutton wird instanziert
                ErstellButton[i].Sprite = Content.Load<Texture2D>("Sprites\\Buttons\\Button " + (Aufz�hlungDerZutaten)i);
                //Er bekommen je nach der Aufz�hlung Aufz�hlungDerZutaten einen Sprite zugewiesen
                ErstellButton[i].Viereckgr��e = new Rectangle((int)ButtonAbstandVonX, (int)ButtonAbstandVonY, ErstellButton[i].Sprite.Width, ErstellButton[i].Sprite.Height);
                //Seine Viereckgr��e setzt sich aus 0 in X und ButtonAbstandVonY in Y, Seiner Breite von der Bildbreite und seiner H�he von der Bildh�he zusammen
                ErstellButton[i].Position = new Vector2(ButtonAbstandVonX, ButtonAbstandVonY);
                //Seine Position setzt sich aus 0 in X und ButtonAbstandVonY zusammen
                //ButtonAbstandVonY ist variabel und ver�ndert sich innerhalb der Schleife
                ButtonAbstandVonY += ErstellButton[0].Sprite.Height;
                //Der ButtonAbstandVonY wird um die H�he des Bildes erh�ht, damit im n�chsten Aufrufen der Schleife der Button unter dem N�chsten erstellt wird
            }

            L�schButton = new Button[AnzahlDerSorten]; //Das Feld der L�schbuttons wird instanziert, mit maximal 12 Dimensionen
            ButtonAbstandVonX = 160;//ButtonAbstandVonX dient im Folgenden der Automatisierung der Platzierung der Buttons auf dem Bildschirm
            ButtonAbstandVonY = 0;  //ButtonAbstandVonY dient im Folgenden der Automatisierung der Platzierung der Buttons auf dem Bildschirm
            for (int i = 0; i <= (AnzahlDerSorten - 1); i++)
            {//Solange nicht alle Sorten betrachtet wurden wird mit den L�schbuttons Folgendes gemacht:
                L�schButton[i] = new Button();//Der L�schbuttons wird instanziert
                L�schButton[i].Sprite = Content.Load<Texture2D>("Sprites\\Buttons\\Button L�schen");
                //Er bekommen je nach der Aufz�hlung Aufz�hlungDerZutaten einen Sprite zugewiesen
                L�schButton[i].Viereckgr��e = new Rectangle((int)ButtonAbstandVonX, (int)ButtonAbstandVonY, L�schButton[i].Sprite.Width, L�schButton[i].Sprite.Height);
                //Seine Viereckgr��e setzt sich aus 160 in X und ButtonAbstandVonY in Y, Seiner Breite von der Bildbreite und seiner H�he von der Bildh�he zusammen
                L�schButton[i].Position = new Vector2(ButtonAbstandVonX, ButtonAbstandVonY);
                //Seine Position setzt sich aus 160 in X und ButtonAbstandVonY zusammen
                //ButtonAbstandVonY ist variabel und ver�ndert sich innerhalb der Schleife
                ButtonAbstandVonY += ErstellButton[0].Sprite.Height;
                //Der ButtonAbstandVonY wird um die H�he des Bildes erh�ht, damit im n�chsten Aufrufen der Schleife der Button unter dem N�chsten erstellt wird

            }
            Pizzaboden = new Pizza(Content.Load<Texture2D>("Sprites\\Pizza\\Pizza Boden"));
            //Der Pizzaboden wird instanziert und erh�lt gleich noch seinen Sprite
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            MouseState Momentaner_Maus_Status = Mouse.GetState(); //Der momentane Maus-Status wird aktualisiert

            //Wenn die Maus in der vorherigen 80stel Sekunde die linkte Maustaste nicht gedr�ckt hatte und in dieser 80stel Sekunde doch, werden im folgenden die jeweiligen Funktionen der Buttons aufgerufen
            if ((Momentaner_Maus_Status.LeftButton == ButtonState.Pressed) && (Vorheriger_Maus_Status.LeftButton == ButtonState.Released))
            {
                for (int i = 0; i <= (AnzahlDerSorten - 1); i++)//Solange nicht alle Sorten ber�cksichtigt wurden, wird diese Schleife ausgef�hrt
                {//Im Folgenden wird bei jedem Button abgefragt ob die Maus mit einer Viereckgr��e der ErstellButtons einen Punkt gemeinsam hat
                    if (ErstellButton[i].Viereckgr��e.Contains(new Point((int)Momentaner_Maus_Status.X, (int)Momentaner_Maus_Status.Y)))
                    {
                        Sorte[i].WurdeErstellt = true; //Die Sorte mit dem Index i wird als Wurdeerstellt deklariert
                    }
                    //Im Folgenden wird bei jedem Button abgefragt ob die Maus mit einer Viereckgr��e der L�schButtons einen Punkt gemeinsam hat
                if (L�schButton[i].Viereckgr��e.Contains(new Point((int)Momentaner_Maus_Status.X, (int)Momentaner_Maus_Status.Y)))
                    {
                        Sorte[i].WurdeErstellt = false; //Die Sorte mit dem Index i wird als wurde nicht erstellt deklariert
                    }
                }

            }

            Vorheriger_Maus_Status = Momentaner_Maus_Status; 
            //Der vorheriger Maus-Status wird am Ende der 80stel Sekunde aktualisiert 
            //Er ist der vorherige Momentaner_Maus_Status, welcher jedoch am Anfang der n�chsten 80stel Sekunde aktualisiert wird
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            MouseState Momentaner_Maus_Status = Mouse.GetState(); //Der momentane Maus-Status wird aktualisiert

            graphics.GraphicsDevice.Clear(Color.CornflowerBlue); //Der Hintergrund wird gef�llt

            //Alle Buttons und der Pizzaboden werden gemalt, unter Ber�cksichtigung der Alpha Textur
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.Draw(Pizzaboden.Sprite, new Vector2(247, 0), new Rectangle(0, 0, 890, 870), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1);
            for (int i = 0; i <= (AnzahlDerSorten - 1); i++)
            {//Solange nicht alle Sorten ber�cksichtigt wurden, werden derren Erstellbuttons und L�schbuttons normal gezeichnet
                spriteBatch.Draw(ErstellButton[i].Sprite, ErstellButton[i].Position, Color.White);
                spriteBatch.Draw(L�schButton[i].Sprite, L�schButton[i].Position, Color.White);
            }
            spriteBatch.End();

            spriteBatch.Begin(SpriteBlendMode.Additive);
            for (int i = 0; i <= (AnzahlDerSorten - 1); i++)
            {//Solange nicht alle Sorten ber�cksichtigt wurden, werden derren Erstellbuttons und L�schbuttons gezeichnet und mit dem zuvor gemaltem vermischt
                if (Sorte[i].WurdeErstellt)
                {//Wobei darauf geachtet wird, dass die Erstellbuttons nur erneut gezeichnet werden, wenn ihre Zutat ausgew�hlt wurde
                    spriteBatch.Draw(ErstellButton[i].Sprite, ErstellButton[i].Position, Color.White);
                }
                else
                {
                    if (ErstellButton[i].Viereckgr��e.Contains(new Point((int)Momentaner_Maus_Status.X, (int)Momentaner_Maus_Status.Y)))
                    {//Wenn sie nicht ausgew�hlt wurden, aber der Mauszeiger mit ihrer Viereckgr��e einen Punkt gemeinsam hat, werden sie ebenso gemalt
                        spriteBatch.Draw(ErstellButton[i].Sprite, ErstellButton[i].Position, Color.White);
                    }
                }
                if (L�schButton[i].Viereckgr��e.Contains(new Point((int)Momentaner_Maus_Status.X, (int)Momentaner_Maus_Status.Y)))
                {//Die L�schbuttons werden immer nur dann nochmal gezeichnet, wenn der Mauszeiger mit ihrer Viereckgr��e einen Punkt gemeinsam hat
                    spriteBatch.Draw(L�schButton[i].Sprite, L�schButton[i].Position, Color.White);
                }


            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            for (int i = 0; i <= (AnzahlDerSorten - 1); i++)
            {//Solange nicht alle Sorten ber�cksichtigt wurden werden:                
                if (Sorte[i].WurdeErstellt)//die Sorten werden sofern sie ausgew�hlt wurden normal unter Ber�cksichtigung der Alpha Texture gemalt
                    spriteBatch.Draw(Sorte[i].Sprite, new Vector2(247, 0), new Rectangle(0, 0, 890, 870), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1);
            
            }
            spriteBatch.Draw(Maus_Texture, new Vector2(Momentaner_Maus_Status.X, Momentaner_Maus_Status.Y), new Rectangle(0, 0, Content.Load<Texture2D>("Sprites\\Pizza\\Pizza Maus").Width, Content.Load<Texture2D>("Sprites\\Pizza\\Pizza Maus").Height), Color.WhiteSmoke, 0, new Vector2(0, 0), 0.2f, SpriteEffects.None, 1);
            //Die Maus wird mit der Maus_Texture zu der Mausposition gemalt

            spriteBatch.End();
            Vorheriger_Maus_Status = Momentaner_Maus_Status;
            //Der vorheriger Maus-Status wird am Ende der 80stel Sekunde aktualisiert 
            //Er ist der vorherige Momentaner_Maus_Status, welcher jedoch am Anfang der n�chsten 80stel Sekunde aktualisiert wird
            base.Draw(gameTime);
        }
    }
}
