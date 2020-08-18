using System;

namespace DiggerMax
{
    class GerenteDeEscena
    {
        private static Escena escenaActual = null;
        public static Escena DameEscenaActual() 
        {
            return escenaActual;
        }
        public static void CargarEscena(Escena escena) 
        {
            if (escenaActual != null)
            {
                escenaActual.Destruir();
            }
            escenaActual = escena;
            escenaActual.Inicio();
        }
    }
}
