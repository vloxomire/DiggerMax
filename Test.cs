using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class Test : Escena
    {
        RectangleShape c1,c2;
        float XposC1, YposC1;
        float velocidad;
        public Test() 
        {
            Inicio();
            CargarContenido();
        }

        public override void Inicio()
        {
            XposC1 = 10f;
            YposC1 = 10f;
            velocidad = 150f;
        }

        public void CargarContenido()
        {
            c1 = new RectangleShape(new Vector2f(50f, 50f))
            {
                Position=new Vector2f(XposC1,YposC1),
                FillColor=new Color(Color.Blue),
            };
            c2 = new RectangleShape(new Vector2f(50f, 50f))
            {
                Position = new Vector2f(300f, 300f),
                FillColor = new Color(Color.Red),
            };
        }

        public override void Actualizar(float DeltaTime, Vector2i posicionRaton)
        {
            MovimientoC1(DeltaTime);
            ColisionadorxZona();
        }

        public override void Dibujar(RenderWindow ventana)
        {
            ventana.Draw(c1);
            ventana.Draw(c2);
        }

        public override void Destruir()
        {
            
        }

        private void MovimientoC1(float time) 
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                XposC1 -= velocidad * time;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                XposC1 += velocidad * time;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                YposC1 -= velocidad * time;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                YposC1 += velocidad * time;
            }
            c1.Position = new Vector2f(XposC1, YposC1);
        }

        private void ColisionadorxZona() 
        {
            if (c1.GetGlobalBounds().Intersects(c2.GetGlobalBounds()))
            {
                c1.FillColor = Color.Green;
                c1.Position.X =
            }
            else
            {
                c1.FillColor = Color.Blue;
            }

        }
    }
}
