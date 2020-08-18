using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    abstract class Escena
    {
        public abstract void Inicio();
        public abstract void Destruir();
        public abstract void Actualizar(float DeltaTime,Vector2i posicionRaton);
        public abstract  void Dibujar(RenderWindow ventana);
    }
}
