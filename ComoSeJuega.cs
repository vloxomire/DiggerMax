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
            personaje = new Personaje();//pj init
            enemigo = new Enemigo();
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
            enemigo.Dibujar(ventana);
        }
        public override void Destruir()
        {

        }
    }
}