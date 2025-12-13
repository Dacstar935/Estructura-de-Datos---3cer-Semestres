using System;

namespace RegistroEstudiante
{
    // Definición de la clase Estudiante
    class Estudiante
    {
        public int Id;
        public string Nombres;
        public string Apellidos;
        public string Direccion;

        // Array para almacenar tres números de teléfono
        public string[] Telefonos = new string[3];

        // Método para mostrar los datos del estudiante
        public void MostrarDatos()
        {
            Console.WriteLine("\n--- DATOS DEL ESTUDIANTE ---");
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Nombres: " + Nombres);
            Console.WriteLine("Apellidos: " + Apellidos);
            Console.WriteLine("Dirección: " + Direccion);

            Console.WriteLine("Teléfonos:");
            for (int i = 0; i < Telefonos.Length; i++)
            {
                Console.WriteLine($"Teléfono {i + 1}: {Telefonos[i]}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Crear un objeto de la clase Estudiante
            Estudiante estudiante = new Estudiante();

            // Ingreso de datos
            Console.Write("Ingrese ID: ");
            estudiante.Id = int.Parse(Console.ReadLine());

            Console.Write("Ingrese nombres: ");
            estudiante.Nombres = Console.ReadLine();

            Console.Write("Ingrese apellidos: ");
            estudiante.Apellidos = Console.ReadLine();

            Console.Write("Ingrese dirección: ");
            estudiante.Direccion = Console.ReadLine();

            // Ingreso de los teléfonos usando el array
            for (int i = 0; i < estudiante.Telefonos.Length; i++)
            {
                Console.Write($"Ingrese teléfono {i + 1}: ");
                estudiante.Telefonos[i] = Console.ReadLine();
            }

            // Mostrar resultados
            estudiante.MostrarDatos();

            Console.WriteLine("\nPresione una tecla para salir...");
            Console.ReadKey();
        }
    }
}
