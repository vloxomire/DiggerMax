using SFML.Graphics;
using SFML.System;
using System;
using SFML.Window;

namespace DiggerMax
{
    class ComoSeJuega : Escena
    {
        /// <summary>
        /// 
        /// </summary>
        //Creo una matriz del tipo casillero
        Casillero[,] casillero;
        Personaje pj;
        Sprite fondo;
        const int fila = 20;
        const int columna = 20;
        Text radar;
        int xPj=0;
        int yPj=0;

        public override void Inicio()
        {
            //Inicializo la matriz
            casillero = new Casillero[fila, columna];
            //Creo una funcion para llenar la matriz
            LLenarMatriz();
            fondo = new Sprite(new Texture("sprite/NuevoJuego2.png"));
            fondo.Color = Color.Blue;

            pj = new Personaje(new Texture("sprite/mineroDer.png"), Keyboard.Key.A, Keyboard.Key.D, Keyboard.Key.W, Keyboard.Key.S, 50.0f, 50.0f);

            

            radar = new Text("x:" + xPj + "/y:" + yPj, new Font("fuentes/Minecraft.ttf"));
            radar.Position = new Vector2f(0.0f, 0.0f);
            radar.Scale = new Vector2f(1.0f,1.0f);


            
        }
        public override void Actualizar(float deltaTime, Vector2i posicionRaton)
        {
            //Variables auxiliares
            Casillero casilleroAhora = null;
            xPj = (int)pj.GetRenderer().Position.X;
            yPj = (int)pj.GetRenderer().Position.Y;

            for (int f = 0; f < fila; f++)
            {
                for (int c = 0; c < columna; c++)
                {
                    if (pj.GetGlobalBounds().Intersects(casillero[f, c].GetGlobalBounds()))
                    {
                        casilleroAhora = casillero[f, c];
                    }

                    casillero[f, c].Actualizar(deltaTime);
                    float x=casillero[f, c].GetPosicionX();
                    float y= casillero[f, c].GetPosicionY();
                    //Si el pj intersecciona el objeto cambia
                    if (casilleroAhora!=null)
                    {
                        //casillero[f , c] = new Casillero(new Texture("sprite/caminoH.png"), x, y);
                        if (pj.GetGlobalBounds().Intersects(casillero[f, c].GetGlobalBounds()))
                        {
                            casillero[f, c] = new Casillero(new Texture("sprite/caminoH.png"), x, y);
                        }
                    }
                }
            }
            //En el actualizar del GamePlay un rejunte de los demas actualizar(update),que intervienen en el play
            pj.Actualizar(deltaTime);
        }
        public override void Dibujar(RenderWindow ventana)
        {
            ventana.Draw(fondo);
            //pedir el renderer de casillero para que se dibuje
            DibujarMatriz(ventana);
            ventana.Draw(radar);
            ventana.Draw(pj.GetRenderer());
        }
        public override void Destruir()
        {

        }
        private void LLenarMatriz() 
        {
            int x = 0;
            int y = 50;
            for (int f = 0; f < fila; f++)
            {
                for (int c = 0; c < columna; c++)
                {
                    //Sumar x e y progresivamente
                    casillero[f, c]=new Casillero(new Texture("sprite/tile1.png"),x,y);
                    x +=57;
                    if (x >= 1000)
                    {
                        x = 0;
                        y += 66;
                    }
                }
            }
        }
        private void DibujarMatriz(RenderWindow ventana) 
        {
            for (int f = 0; f < fila; f++)
            {
                for (int c = 0; c < columna; c++)
                {
                    //devolver el renderer de cada objeto
                    ventana.Draw(casillero[f, c].GetRenderer());
                }
            }
        }
    }
}