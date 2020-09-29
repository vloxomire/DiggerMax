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
        private bool textoBooolNewGame;
        private Text textNewGame;
        private RectangleShape textRectangulo;
        private Clock tiempo;
        private Sprite spritePortal;
        private IntRect rect;
        private Animacion portalAnimado;
        private Animacion fisura01;
        private Animacion fisura02;
        private Animacion fisura03;
        private Animacion fisura04;
        private float velocidadAnimacion;
        public Menu()
        {
            textoBooolNewGame = false;
            velocidadAnimacion =0.3f;
        }
        public override void Inicio()
        {
            tiempo = new Clock();
            
            CargarContenido();
        }
        public override void Actualizar(float DeltaTime, Vector2i posicionRaton)
        {
            if (spritePortal.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y))
            {
                textoBooolNewGame = true;
            }
            else 
            {
                textoBooolNewGame = false;
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (spritePortal.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y))
                {
                    GerenteDeEscena.CargarEscena(new ComoSeJuega());
                }
                /*if (bCargar.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y))
                {

                }
                if (bCreditos.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y))
                {
                    GerenteDeEscena.CargarEscena(new Creditos());
                }
                if (bSalir.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y))
                {
                }*/
            }
            //Animacion

            if (tiempo.ElapsedTime.AsSeconds() > velocidadAnimacion)
            {
                if (portalAnimado != null)
                {
                    rect.Top = portalAnimado.setArriba;
                    if (rect.Left == (portalAnimado.numeroDeFrames - 1) * 32)
                    {
                        rect.Left = 0;
                    }
                    else
                    {
                        rect.Left += 32;
                    }
                }
                tiempo.Restart();
            }
            spritePortal.TextureRect = rect;
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
            ventana.Draw(spritePortal);
            if (!textoBooolNewGame)
            { return; }
            else 
            {
                ventana.Draw(textRectangulo);
                ventana.Draw(textNewGame);
            }
        }
        public override void Destruir()
        {
            Console.WriteLine("Hola soy el destructor del menu");
        }
        private void CargarContenido()
        {
            fondo = new Sprite(new Texture("Sprite/juego.jpg"));
            rect = new IntRect(0, 0, 31, 32);
            fisura01 = new Animacion(0, 0, 3);
            fisura02 = new Animacion(32, 0, 3);
            fisura03 = new Animacion(64, 0, 3);
            fisura04 = new Animacion(96, 0, 3);

            spritePortal = new Sprite(new Texture("Sprite/portal1.png"), rect)
            {
                Position = new Vector2f(35, 270),
            };
            portalAhora = EstadosPortal.Apertura;
            portalAnimado = fisura01;

            textRectangulo = new RectangleShape(new Vector2f(150, 50))
            {
                Position = new Vector2f(spritePortal.Position.X + 20, spritePortal.Position.Y -50),
                //System y Graphic utilizan "Color" OMG
                FillColor = new SFML.Graphics.Color(SFML.Graphics.Color.Black),
            };

            textNewGame = new Text("New game", new Font("Fuentes/Mariokart.ttf"))
            {
                FillColor = new SFML.Graphics.Color(SFML.Graphics.Color.White),
                Position = new Vector2f(textRectangulo.Position.X + 10, textRectangulo.Position.Y + 5),
            };
        }

    }
}
