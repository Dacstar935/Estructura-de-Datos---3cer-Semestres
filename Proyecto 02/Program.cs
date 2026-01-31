using System;
using System.Collections.Generic;

namespace ColaAtraccion
{
    // Clase que representa a cada persona que llega a la atracción
    class Persona
    {
        // Propiedades para guardar el nombre y el ID de la persona
        public string Nombre { get; set; }
        public int Id { get; set; }

        // Constructor de la clase Persona
        public Persona(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        // Método para mostrar la información de la persona en texto
        public override string ToString()
        {
            return $"ID: {Id} - Nombre: {Nombre}";
        }
    }

    // Clase que controla la cola y la asignación de asientos
    class Atraccion
    {
        // Creamos una cola para manejar a las personas en orden de llegada
        private Queue<Persona> cola = new Queue<Persona>();

        // Capacidad máxima de la atracción (30 asientos)
        private int capacidad = 30;

        // Contador para asignar IDs únicos a cada persona
        private int contador = 1;

        // Método para agregar personas a la cola
        public void AgregarPersona(string nombre)
        {
            // Verificamos si todavía hay asientos disponibles
            if (cola.Count < capacidad)
            {
                // Agregamos la persona al final de la cola
                cola.Enqueue(new Persona(contador++, nombre));
                Console.WriteLine("Persona agregada correctamente a la cola.\n");
            }
            else
            {
                // Si ya se llenaron los asientos, no se puede agregar más
                Console.WriteLine("❌ Todos los asientos ya fueron vendidos.\n");
            }
        }

        // Método para asignar un asiento (sacar al primero de la cola)
        public void AsignarAsiento()
        {
            // Verificamos que haya personas en la cola
            if (cola.Count > 0)
            {
                // Quitamos a la primera persona que llegó
                Persona p = cola.Dequeue();
                Console.WriteLine($"🎫 Asiento asignado a: {p}\n");
            }
            else
            {
                // Si no hay nadie en la cola
                Console.WriteLine("⚠ No hay personas esperando en la cola.\n");
            }
        }

        // Método para mostrar todas las personas que están en la cola
        public void VerCola()
        {
            Console.WriteLine("📋 Personas que están en la cola:");
            foreach (var p in cola)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine();
        }

        // Método para saber cuántas personas hay en total en la cola
        public int TotalEnCola()
        {
            return cola.Count;
        }
    }

    // Clase principal donde se ejecuta el programa
    class Program
    {
        static void Main(string[] args)
        {
            // Creamos el objeto de la atracción
            Atraccion atraccion = new Atraccion();
            int opcion;

            // Menú principal del sistema
            do
            {
                Console.WriteLine("=== MENÚ DE LA ATRACCIÓN ===");
                Console.WriteLine("1. Agregar persona a la cola");
                Console.WriteLine("2. Asignar asiento");
                Console.WriteLine("3. Ver cola (reportería)");
                Console.WriteLine("4. Ver total en cola");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                // Leemos la opción del usuario
                opcion = int.Parse(Console.ReadLine());

                // Evaluamos la opción seleccionada
                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese el nombre de la persona: ");
                        string nombre = Console.ReadLine();
                        atraccion.AgregarPersona(nombre);
                        break;

                    case 2:
                        atraccion.AsignarAsiento();
                        break;

                    case 3:
                        atraccion.VerCola();
                        break;

                    case 4:
                        Console.WriteLine($"Total de personas en la cola: {atraccion.TotalEnCola()}\n");
                        break;
                }

            } while (opcion != 0); // El programa se repite hasta que el usuario elija salir
        }
    }
}
