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
        Font font = new Font("Fuentes/MarioKart.ttf");
        public Enemigo() : base("Sprite/Zombie.png",64) 
        {
            nombre = "ZoMbIe";
            arriba = new Animacion(512, 0, 9);
            abajo = new Animacion(640, 0, 9);
            izquierda = new Animacion(578, 0, 9);
            derecha = new Animacion(704, 0, 9);
            MorirAnim = new Animacion(1280,0,6);
            
            clock = new Clock();
            tiempoAtaque = new Text();
        }
        public override void Actualizar(float deltaTiempo)
        {
            tiempoFloat = clock.ElapsedTime.AsSeconds();
            tiempoAtaque = new Text(tiempoFloat.ToString(), font);
            tiempoAtaque.Position = new Vector2f(GetSprite().Position.X, GetSprite().Position.Y + 70);
            base.Actualizar(deltaTiempo);
        }
        public override void Dibujar(RenderWindow ventana)
        {
            ventana.Draw(tiempoAtaque);
            base.Dibujar(ventana);
        }
    }
}
