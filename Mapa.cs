using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiggerMax
{
    class Mapa
    {
        int CmapaAncho;
        int CmapaAlto;
        int patronesTamanio;
        int mapaAncho;
        int mapaAlto;
        private Texture textura;
        Sprite[] mapaPatrones;
        Sprite[,] patrones;
        public Mapa()
        {
            //Con el Tiled se definio el tamaño del mapa de 100x100 patrones y
            //con un tamaño por patrón dde32px x 32px
            CmapaAncho = 21;//terrain
            CmapaAlto = 23;//terrain
            mapaAlto = 20;
            mapaAncho = 20;
            patronesTamanio = 32;
            textura = new Texture("Mapas/terrain.png");

            mapaPatrones = new Sprite[CmapaAncho * CmapaAlto];
            for (int y = 0; y < CmapaAlto; y++)
            {
                for (int x = 0; x < CmapaAncho; x++)
                {
                    IntRect rect = new IntRect(x * patronesTamanio, y * patronesTamanio, patronesTamanio, patronesTamanio);
                    mapaPatrones[(y * CmapaAncho) + x] = new Sprite(textura, rect);
                }
            }

            patrones = new Sprite[mapaAncho, mapaAlto];
            StreamReader lector = new StreamReader("Mapas/mapa1.csv");
            for (int y = 0; y < mapaAlto; y++)
            {
                string linea = lector.ReadLine();
                string[] objetos = linea.Split(',');

                for (int x = 0; x < mapaAncho; x++)
                {
                    int id = Convert.ToInt32(objetos[x]);
                    patrones[x, y] = new Sprite(mapaPatrones[id]);
                    patrones[x, y].Position = new Vector2f(patronesTamanio * x, patronesTamanio * y);
                }
            }
            lector.Close();
        }
        public void Draw(RenderWindow ventana)
        {
            for (int y = 0; y < mapaAlto; y++)
            {
                for (int x = 0; x < mapaAncho; x++)
                {
                    //En el Game crear un objeto para poder llamarlo
                    ventana.Draw(patrones[x, y]);
                }
            }
        }
    }
}
