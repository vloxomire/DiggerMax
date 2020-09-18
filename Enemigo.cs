using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace DiggerMax
{
    class Enemigo : AnimaIA
    {
        private Clock clock;
        private Text tiempoAtaque;
        private float tiempoFloat;
        private string secundero;
        Font fuente = new Font("Fuentes/MarioKart.ttf");
        public Enemigo() : base("Sprite/Zombie.png",64) 
        {
            //Animacion
            arriba = new Animacion(512, 0, 9);
            abajo = new Animacion(640, 0, 9);
            izquierda = new Animacion(578, 0, 9);
            derecha = new Animacion(704, 0, 9);
            MorirAnim = new Animacion(1280,0,6);
            //Campos
            clock = new Clock();
            tiempoAtaque = new Text();
            nombre = "ZoMbIe";
            secundero = "";
            Init();
        }
        public void Init()
        {
            dialogo = "GrRr!!!";
            text = new Text(dialogo,fuente);
            text.FillColor = Color.Transparent;
        }
        public override void Actualizar(float deltaTiempo)
        {
            //TiempoEnEnemigo
            tiempoFloat = clock.ElapsedTime.AsSeconds();
            secundero = "sec "+ tiempoFloat.ToString();
            tiempoAtaque = new Text(secundero, fuente);
            tiempoAtaque.Position = new Vector2f(GetSprite().Position.X-20, GetSprite().Position.Y + 70);
            //dialogo
            var coorDiag = new Vector2f(GetSprite().Position.X+50,GetSprite().Position.Y+50);
            text.Position = coorDiag;
            ProximoAtaque();
            base.Actualizar(deltaTiempo);
        }
        public override void Dibujar(RenderWindow ventana)
        {
            ventana.Draw(tiempoAtaque);
            ventana.Draw(text);
            base.Dibujar(ventana);
            
        }
        private void ProximoAtaque() 
        {
            if (tiempoFloat < 5f) 
            {
                text.FillColor = Color.Transparent;
                return;
            }
            else 
            {
                tiempoAtaque.FillColor = Color.Red;
                text.FillColor = Color.White;
                if (tiempoFloat > 7f) 
                {
                    Time time = clock.Restart();
                }
                
            }
        }
    }
}
