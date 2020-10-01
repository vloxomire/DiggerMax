using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Drawing;

namespace DiggerMax
{
    class Menu : Escena
    {
        Sprite fondo, bNuevo, bCargar, bCreditos, bSalir, portal1, portal2;
        public enum EstadosPortal { Apertura, Transicion, Colapso, Cierre };
        private EstadosPortal portalAhora;
        private bool textoBooolNewGame,boolCredit,boolExit,cerrarventana;
        private Text textNewGame, textCredit, textExit;
        private RectangleShape textRectangulo,txtRecCred,txtRecExit;
        private Clock tiempo;
        private Sprite spriteNewGame, spriteCredits, spriteExit;
        private IntRect rect,rect2,rect3;
        private Animacion portalAnimado;
        private Animacion fisura01;
        private Animacion fisura02;
        private Animacion fisura03;
        private Animacion fisura04;
        private float velocidadAnimacion;
        public Menu()
        {
            textoBooolNewGame = false;
            boolCredit = true;
            boolExit = false;
            cerrarventana = false;
            velocidadAnimacion =0.3f;
        }
        public override void Inicio()
        {
            tiempo = new Clock();
            
            CargarContenido();
        }
        public override void Actualizar(float DeltaTime, Vector2i posicionRaton)
        {
            if (spriteNewGame.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y))
            {
                textoBooolNewGame = true;
            }
            else 
            {
                textoBooolNewGame = false;
            }

            if (spriteCredits.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y))
            {
                boolCredit = true;
            }
            else
            {
                boolCredit = false;
            }

            if (spriteExit.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y))
            {
                boolExit = true;
            }
            else
            {
                boolExit = false;
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (spriteNewGame.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y))
                {
                    GerenteDeEscena.CargarEscena(new ComoSeJuega());
                }

                if (spriteCredits.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y))
                {
                    GerenteDeEscena.CargarEscena(new Creditos());
                }

                if (spriteExit.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y))
                {
                    cerrarventana = true;
                }
            }
            //Animacion

            if (tiempo.ElapsedTime.AsSeconds() > velocidadAnimacion)
            {
                if (portalAnimado != null)
                {
                    rect.Top = portalAnimado.setArriba;
                    rect2.Top = portalAnimado.setArriba;
                    rect3.Top = portalAnimado.setArriba;
                    if (rect.Left == (portalAnimado.numeroDeFrames - 1) * 32)
                    {
                        rect.Left = 0;
                        rect2.Left = 0;
                        rect3.Left = 0;
                    }
                    else
                    {
                        rect.Left += 32;
                        rect2.Left += 32;
                        rect3.Left += 32;
                    }
                }
                tiempo.Restart();
            }
            spriteNewGame.TextureRect = rect;
            spriteCredits.TextureRect = rect2;
            spriteExit.TextureRect = rect3;
            switch (portalAhora)
            {
                case EstadosPortal.Apertura:
                    portalAnimado = fisura02;
                    portalAhora = EstadosPortal.Transicion;
                    break;
                case EstadosPortal.Transicion:
                    portalAnimado = fisura03;
                    portalAhora = EstadosPortal.Colapso;
                    break;
                case EstadosPortal.Colapso:
                    portalAnimado = fisura04;
                    portalAhora = EstadosPortal.Cierre;
                    break;
                case EstadosPortal.Cierre:
                    portalAnimado = fisura01;
                    portalAhora = EstadosPortal.Apertura;
                    break;
            }
            
        }
        public override void Dibujar(RenderWindow ventana)
        {
            ventana.Draw(fondo);
            ventana.Draw(spriteNewGame);
            ventana.Draw(spriteCredits);
            ventana.Draw(spriteExit);

            if (textoBooolNewGame)
            {
                ventana.Draw(textRectangulo);
                ventana.Draw(textNewGame);
            }
            if (boolCredit)
            {
                ventana.Draw(txtRecCred);
                ventana.Draw(textCredit);
            }
            if (boolExit)
            {
                ventana.Draw(txtRecExit);
                ventana.Draw(textExit);
            }
            if (cerrarventana) { ventana.Close(); }
            
        }
        public override void Destruir()
        {
            Console.WriteLine("Hola soy el destructor del menu");
        }
        private void CargarContenido()
        {
            fondo = new Sprite(new Texture("Sprite/juego.jpg"));
            rect = new IntRect(0, 0, 31, 32);
            rect2 = new IntRect(0, 0, 31, 32);
            rect3 = new IntRect(0, 0, 31, 32);
            fisura01 = new Animacion(0, 0, 3);
            fisura02 = new Animacion(32, 0, 3);
            fisura03 = new Animacion(64, 0, 3);
            fisura04 = new Animacion(96, 0, 3);

            spriteNewGame = new Sprite(new Texture("Sprite/portal1.png"), rect)
            {
                Position = new Vector2f(35, 270),
            };
            portalAhora = EstadosPortal.Apertura;
            portalAnimado = fisura01;

            textRectangulo = new RectangleShape(new Vector2f(150, 50))
            {
                Position = new Vector2f(spriteNewGame.Position.X + 20, spriteNewGame.Position.Y -50),
                //System y Graphic utilizan "Color" OMG
                FillColor = new SFML.Graphics.Color(SFML.Graphics.Color.Black),
            };

            textNewGame = new Text("New game", new Font("Fuentes/Mariokart.ttf"))
            {
                FillColor = new SFML.Graphics.Color(SFML.Graphics.Color.White),
                Position = new Vector2f(textRectangulo.Position.X + 10, textRectangulo.Position.Y + 5),
            };
            //********************************************************************
            //CREDITS
            spriteCredits = new Sprite(new Texture("Sprite/portal1.png"), rect2)
            {
                Position = new Vector2f(290, 420),
                Color = new SFML.Graphics.Color(SFML.Graphics.Color.Magenta),
            };
            txtRecCred = new RectangleShape(new Vector2f(130, 50))
            {
                Position = new Vector2f(spriteCredits.Position.X + 20, spriteCredits.Position.Y - 50),
                //System y Graphic utilizan "Color" OMG
                FillColor = new SFML.Graphics.Color(SFML.Graphics.Color.Black),
            };

            textCredit = new Text("Credits", new Font("Fuentes/Mariokart.ttf"))
            {
                FillColor = new SFML.Graphics.Color(SFML.Graphics.Color.White),
                Position = new Vector2f(txtRecCred.Position.X + 10, txtRecCred.Position.Y + 5),
            };
            //********************************************************************
            //EXIT
            spriteExit = new Sprite(new Texture("Sprite/portal1.png"), rect3)
            {
                Position = new Vector2f(560, 290),
                Color = new SFML.Graphics.Color(SFML.Graphics.Color.Red),
            };

            txtRecExit = new RectangleShape(new Vector2f(75, 50))
            {
                Position = new Vector2f(spriteExit.Position.X - 80, spriteExit.Position.Y - 50),
                //System y Graphic utilizan "Color" OMG
                FillColor = new SFML.Graphics.Color(SFML.Graphics.Color.Black),
            };

            textExit = new Text("Exit", new Font("Fuentes/Mariokart.ttf"))
            {
                FillColor = new SFML.Graphics.Color(SFML.Graphics.Color.White),
                Position = new Vector2f(txtRecExit.Position.X +5, txtRecExit.Position.Y + 5),
            };
        }

    }
}
