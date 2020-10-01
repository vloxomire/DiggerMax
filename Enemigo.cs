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
        private Clock enemyTime;
        private Text tiempoAtaque;
        private float tiempoFloat;
        public float NEXTATTACK;
        private string secundero;
        public bool ATACO;
        public bool CONTACTO;
        public bool HABLAR;
        
        private Animacion arribaCombate;
        private Animacion izquierdaCombate;
        private Animacion abajoCombate;
        private Animacion derechaCombate;

        public Enemigo() : base("Sprite/Zombie.png", 64)
        {
            //Animacion
            arriba = new Animacion(512, 0, 9);
            abajo = new Animacion(640, 0, 9);
            izquierda = new Animacion(578, 0, 9);
            derecha = new Animacion(704, 0, 9);
            MorirAnim = new Animacion(1280, 0, 6);

            //Campos
            enemyTime = new Clock();
            tiempoAtaque = new Text();
            nombre = "ZoMbIe";
            secundero = "";
            ATACO = false;
            CONTACTO = false;
            HABLAR = false;
            
            Inicializar();
            CargarContenido();
        }
        public override void Inicializar()
        {
            //Dialogo
            stringVoz = "GRRR!!!";
            textDialogo = new Text(stringVoz, fuenteMonstruo);
            textDialogo.FillColor = Color.Transparent;

            NEXTATTACK=3f;
        }

        public override void CargarContenido()
        {
            //Ataque
            atqArb = new Animacion(704, 0, 6);
            atqIzq = new Animacion(768, 0, 6);
            atqAbj = new Animacion(832, 0, 6);
            atqDer = new Animacion(896, 0, 6);
    }

        public override void Actualizar(float deltaTiempo)
        {
            //TiempoEnEnemigo
            tiempoFloat = enemyTime.ElapsedTime.AsSeconds();
            secundero = "sec " + tiempoFloat.ToString();
            tiempoAtaque = new Text(secundero, fuenteMonstruo);
            tiempoAtaque.Scale = new Vector2f(0.8f, 0.8f);
            tiempoAtaque.Position = new Vector2f(GetSprite().Position.X - 20, GetSprite().Position.Y + 70);

            //dialogo
            var coorDiag = new Vector2f(GetSprite().Position.X + 50, GetSprite().Position.Y + 50);
            textDialogo.Position = coorDiag;
            GestionarWarCry();
            base.Actualizar(deltaTiempo);
        }
        public override void Dibujar(RenderWindow ventana)
        {
            if (!MUERTO)
            {
                ventana.Draw(tiempoAtaque);
                ventana.Draw(textDialogo);
            }
            
            base.Dibujar(ventana);

        }
        private void Attack() 
        {
           
        }
        private void GestionarWarCry()
        {
            if (tiempoFloat < 4f)
            {
                textDialogo.FillColor = Color.Transparent;
            }
            else
            {
                textDialogo.FillColor = Color.White;
                textDialogo.CharacterSize = 12;
                if (tiempoFloat > 5.0f)
                {
                    textDialogo.CharacterSize = 18;
                }
                if (tiempoFloat > 7f)
                {
                    textDialogo.CharacterSize = 25;
                }
                if (tiempoFloat > 13f)
                {
                    Time time = enemyTime.Restart();
                }
            }
        }
        public float GetTempoEnemigo() 
        {
            return this.tiempoFloat;
        }
        public Clock GetClock() 
        {
            return enemyTime;
        }
        
    }
}
