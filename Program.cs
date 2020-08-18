using System;
using SFML.Window;
using SFML.System;

namespace DiggerMax
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Presiona Esc, para salir de la ventana");
            Juego juego = new Juego();
            Console.WriteLine("Todo, hecho!!!");
            juego.Correr();
        }

    }
}
