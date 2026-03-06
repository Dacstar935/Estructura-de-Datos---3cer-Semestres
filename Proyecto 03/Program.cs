using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TorneoFutbol
{
    // Clase Jugador con comparación por número de camiseta
    public class Jugador
    {
        public int NumeroCamiseta { get; set; }
        public string Nombre { get; set; }

        public Jugador(int numero, string nombre)
        {
            NumeroCamiseta = numero;
            Nombre = nombre;
        }

        // Sobrescribir Equals y GetHashCode para que HashSet funcione por número
        public override bool Equals(object obj)
        {
            return obj is Jugador jugador &&
                   NumeroCamiseta == jugador.NumeroCamiseta;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NumeroCamiseta);
        }

        public override string ToString()
        {
            return $"#{NumeroCamiseta} - {Nombre}";
        }
    }

    // Clase Equipo que contiene un HashSet de jugadores
    public class Equipo
    {
        public string Nombre { get; set; }
        public HashSet<Jugador> Plantilla { get; set; }

        public Equipo(string nombre)
        {
            Nombre = nombre;
            Plantilla = new HashSet<Jugador>(); // Garantiza unicidad
        }

        public bool AgregarJugador(Jugador j)
        {
            return Plantilla.Add(j); // Retorna false si ya existe
        }

        public List<Jugador> ObtenerJugadoresOrdenados()
        {
            return Plantilla.OrderBy(j => j.NumeroCamiseta).ToList();
        }
    }

    // Clase principal del Torneo
    public class Torneo
    {
        // Dictionary: clave = nombre del equipo, valor = objeto Equipo
        private Dictionary<string, Equipo> _equipos;

        public Torneo()
        {
            _equipos = new Dictionary<string, Equipo>(StringComparer.OrdinalIgnoreCase); // Ignorar mayúsculas/minúsculas
        }

        // Método para registrar equipo
        public string RegistrarEquipo(string nombreEquipo)
        {
            if (string.IsNullOrWhiteSpace(nombreEquipo))
            {
                return "Error: El nombre del equipo no puede estar vacío.";
            }

            if (_equipos.ContainsKey(nombreEquipo))
            {
                return "Error: El equipo ya existe.";
            }

            _equipos[nombreEquipo] = new Equipo(nombreEquipo);
            return $"Equipo '{nombreEquipo}' registrado con éxito.";
        }

        // Método que demuestra el uso de Dictionary y HashSet
        public string RegistrarJugador(string nombreEquipo, int numero, string nombreJugador)
        {
            if (string.IsNullOrWhiteSpace(nombreJugador))
            {
                return "Error: El nombre del jugador no puede estar vacío.";
            }

            if (numero <= 0)
            {
                return "Error: El número de camiseta debe ser positivo.";
            }

            // 1. Búsqueda eficiente del equipo en el DICTIONARY (O(1))
            if (!_equipos.TryGetValue(nombreEquipo, out Equipo equipo))
            {
                return $"Error: Equipo '{nombreEquipo}' no encontrado.";
            }

            // 2. Crear el jugador y usar el HASHSET para añadirlo (O(1))
            Jugador nuevoJugador = new Jugador(numero, nombreJugador);
            if (equipo.AgregarJugador(nuevoJugador))
            {
                return $"Jugador {nombreJugador} registrado con éxito en '{nombreEquipo}'.";
            }
            else
            {
                return $"Error: Ya existe un jugador con el número {numero} en el equipo '{nombreEquipo}'.";
            }
        }

        // Método para listar equipos
        public List<string> ListarEquipos()
        {
            return _equipos.Keys.ToList();
        }

        // Método para mostrar plantilla
        public string MostrarPlantilla(string nombreEquipo)
        {
            if (string.IsNullOrWhiteSpace(nombreEquipo))
            {
                return "Error: El nombre del equipo no puede estar vacío.";
            }

            if (!_equipos.TryGetValue(nombreEquipo, out Equipo equipo))
            {
                return $"Error: Equipo '{nombreEquipo}' no encontrado.";
            }

            if (equipo.Plantilla.Count == 0)
            {
                return $"El equipo '{nombreEquipo}' no tiene jugadores registrados.";
            }

            var jugadores = equipo.ObtenerJugadoresOrdenados();
            var resultado = $"Plantilla del {nombreEquipo}:\n";
            resultado += "------------------------\n";
            foreach (var j in jugadores)
            {
                resultado += $"{j}\n";
            }
            resultado += "------------------------\n";
            resultado += $"Total de jugadores: {equipo.Plantilla.Count}";

            return resultado;
        }

        // Método para consultar un jugador específico
        public string ConsultarJugador(string nombreEquipo, int numero)
        {
            if (!_equipos.TryGetValue(nombreEquipo, out Equipo equipo))
            {
                return $"Error: Equipo '{nombreEquipo}' no encontrado.";
            }

            // Búsqueda eficiente en HashSet O(1)
            Jugador jugadorBuscado = new Jugador(numero, "");
            if (equipo.Plantilla.TryGetValue(jugadorBuscado, out Jugador jugadorEncontrado))
            {
                return $"Jugador encontrado: {jugadorEncontrado.Nombre} lleva el número {numero} en '{nombreEquipo}'.";
            }
            else
            {
                return $"No existe un jugador con el número {numero} en '{nombreEquipo}'.";
            }
        }

        // Método para análisis de rendimiento
        public (long tiempoMs, bool exitoso) PruebaRendimientoInsercion(int cantidadJugadores)
        {
            try
            {
                // Crear un equipo de prueba
                string equipoPrueba = "EquipoTest" + DateTime.Now.Ticks;
                RegistrarEquipo(equipoPrueba);

                Stopwatch sw = Stopwatch.StartNew();

                // Insertar jugadores con números únicos
                for (int i = 1; i <= cantidadJugadores; i++)
                {
                    RegistrarJugador(equipoPrueba, i, $"Jugador{i}");
                }

                sw.Stop();

                // Limpiar equipo de prueba
                _equipos.Remove(equipoPrueba);

                return (sw.ElapsedMilliseconds, true);
            }
            catch (Exception ex)
            {
                return (0, false);
            }
        }
    }

    // Programa principal con menú interactivo
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Torneo torneo = new Torneo();
            bool salir = false;

            // Registrar algunos datos de ejemplo
            torneo.RegistrarEquipo("Barcelona SC");
            torneo.RegistrarEquipo("Emelec");
            torneo.RegistrarJugador("Barcelona SC", 10, "Damián Díaz");
            torneo.RegistrarJugador("Barcelona SC", 1, "Javier Burrai");
            torneo.RegistrarJugador("Emelec", 10, "Alexis Zapata");

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine("  SISTEMA DE GESTIÓN DE TORNEO DE FÚTBOL");
                Console.WriteLine("=========================================");
                Console.WriteLine("1. Registrar equipo");
                Console.WriteLine("2. Registrar jugador");
                Console.WriteLine("3. Listar equipos");
                Console.WriteLine("4. Ver plantilla de un equipo");
                Console.WriteLine("5. Consultar jugador por número");
                Console.WriteLine("6. Prueba de rendimiento (10000 jugadores)");
                Console.WriteLine("7. Salir");
                Console.WriteLine("=========================================");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Nombre del equipo: ");
                        string nombreEquipo = Console.ReadLine().Trim();
                        Console.WriteLine(torneo.RegistrarEquipo(nombreEquipo));
                        break;

                    case "2":
                        Console.Write("Nombre del equipo: ");
                        string eq = Console.ReadLine().Trim();
                        Console.Write("Número de camiseta: ");
                        if (!int.TryParse(Console.ReadLine(), out int num))
                        {
                            Console.WriteLine("Error: Número inválido");
                            break;
                        }
                        Console.Write("Nombre del jugador: ");
                        string nom = Console.ReadLine().Trim();
                        Console.WriteLine(torneo.RegistrarJugador(eq, num, nom));
                        break;

                    case "3":
                        var equipos = torneo.ListarEquipos();
                        if (equipos.Count == 0)
                        {
                            Console.WriteLine("No hay equipos registrados.");
                        }
                        else
                        {
                            Console.WriteLine("Equipos registrados:");
                            foreach (var e in equipos)
                            {
                                Console.WriteLine($"  • {e}");
                            }
                        }
                        break;

                    case "4":
                        Console.Write("Nombre del equipo: ");
                        string eqVer = Console.ReadLine().Trim();
                        Console.WriteLine(torneo.MostrarPlantilla(eqVer));
                        break;

                    case "5":
                        Console.Write("Nombre del equipo: ");
                        string eqCons = Console.ReadLine().Trim();
                        Console.Write("Número de camiseta: ");
                        if (!int.TryParse(Console.ReadLine(), out int numCons))
                        {
                            Console.WriteLine("Error: Número inválido");
                            break;
                        }
                        Console.WriteLine(torneo.ConsultarJugador(eqCons, numCons));
                        break;

                    case "6":
                        Console.WriteLine("Ejecutando prueba de inserción de 10000 jugadores...");
                        Console.WriteLine("Esto puede tomar unos segundos...");
                        var resultado = torneo.PruebaRendimientoInsercion(10000);
                        if (resultado.exitoso)
                        {
                            Console.WriteLine($" Tiempo total: {resultado.tiempoMs} ms");
                            Console.WriteLine(" Esto demuestra la eficiencia O(1) del HashSet.");
                            Console.WriteLine("   Si se usara una List, tomaría varios segundos.");
                        }
                        else
                        {
                            Console.WriteLine("Error en la prueba.");
                        }
                        break;

                    case "7":
                        salir = true;
                        Console.WriteLine("¡Hasta luego!");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}
