using SFML.Graphics;
using SFML.System;
using System;
using SFML.Window;
using System.Collections.Generic;

namespace DiggerMax
{
    class ComoSeJuega : Escena
    {
        /// <summary>
        /// 
        /// </summary>
        //Creo una matriz del tipo casillero
        private Personaje personaje;
        private Enemigo enemigo;
        private Mapa mapa;
        private View camara;//camara
        public override void Inicio()
        {
            //Camara
            camara = new View(new Vector2f(0,0),new Vector2f(800,600));//camara init
            //camara = new View(new Vector2f(Juego.width, Juego.height), new Vector2f(Juego.width, Juego.height));
            mapa = new Mapa();//mapa init

            personaje = new Personaje()//pj init
            {
                XPOS_ANIMA = 150.0f,
                YPOS_ANIMA = 0.0f
            };

            enemigo = new Enemigo() 
            {
                XPOS_ANIMA = 150.0f,
                YPOS_ANIMA = 300.0f
            };
            //PATRON DE CAMINATA
            enemigo.PuntoCaminoLista = new List<PuntoCamino>();
            enemigo.PuntoCaminoLista.Add(new PuntoCamino(0,0));
            enemigo.PuntoCaminoLista.Add(new PuntoCamino(0, 100));
            enemigo.PuntoCaminoLista.Add(new PuntoCamino(100, 100));
            enemigo.PuntoCaminoLista.Add(new PuntoCamino(100,0));
        }
        public override void Actualizar(float deltaTiempo, Vector2i posicionRaton)
        {
            //En el actualizar del GamePlay un rejunte de los demas actualizar(update),que intervienen en el play*/
            personaje.Actualizar(deltaTiempo);
            enemigo.Actualizar(deltaTiempo);
        }
        public override void Dibujar(RenderWindow ventana)
        {
            //CAMARA
            camara.Center = new Vector2f(personaje.XPOS_ANIMA, personaje.YPOS_ANIMA);//Centro camara en pj
            ventana.SetView(camara);//la ventana es la camara

            mapa.Draw(ventana);
            personaje.Dibujar(ventana);
            //aca va a ir el cheqqueo de colisiones , que se converitra en una clase o ira con elpj en su actualizar
            //COLISIONES
            enemigo.Dibujar(ventana);
        }
        public override void Destruir()
        {

        }
    }
}