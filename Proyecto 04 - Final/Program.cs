using System;
using System.Collections.Generic;
using System.Linq;

namespace VuelosBaratosGrafos
{
    // Clase que representa una arista (vuelo)
    public class Arista
    {
        public string Destino { get; set; }
        public int Costo { get; set; }

        public Arista(string destino, int costo)
        {
            Destino = destino;
            Costo = costo;
        }

        public override string ToString()
        {
            return $"-> {Destino} (${Costo})";
        }
    }

    // Clase que representa el grafo de vuelos
    public class GrafoVuelos
    {
        private Dictionary<string, List<Arista>> _adyacencia;

        public GrafoVuelos()
        {
            _adyacencia = new Dictionary<string, List<Arista>>();
        }

        // Agregar un vuelo (arista dirigida ponderada)
        public void AgregarVuelo(string origen, string destino, int costo)
        {
            if (!_adyacencia.ContainsKey(origen))
            {
                _adyacencia[origen] = new List<Arista>();
            }
            _adyacencia[origen].Add(new Arista(destino, costo));
        }

        // Consultar vuelos desde una ciudad
        public List<Arista> VuelosDesde(string ciudad)
        {
            if (_adyacencia.ContainsKey(ciudad))
            {
                return _adyacencia[ciudad];
            }
            return new List<Arista>();
        }

        // Mostrar todo el grafo
        public void MostrarGrafo()
        {
            Console.WriteLine("\n=== GRAFO DE VUELOS ===");
            foreach (var origen in _adyacencia.Keys)
            {
                Console.Write($"{origen}: ");
                foreach (var vuelo in _adyacencia[origen])
                {
                    Console.Write($"{vuelo} ");
                }
                Console.WriteLine();
            }
        }

        // Algoritmo de Dijkstra para ruta más barata
        public (List<string> Ruta, int CostoTotal) RutaMasBarata(string inicio, string fin)
        {
            // Diccionario de distancias mínimas
            var distancias = new Dictionary<string, int>();
            var previos = new Dictionary<string, string>();

            // Inicializar distancias para todas las ciudades conocidas
            var todasLasCiudades = ObtenerTodasLasCiudades();
            foreach (var ciudad in todasLasCiudades)
            {
                distancias[ciudad] = int.MaxValue;
                previos[ciudad] = null;
            }

            if (!distancias.ContainsKey(inicio))
            {
                return (null, int.MaxValue);
            }

            distancias[inicio] = 0;

            // Cola de prioridad usando SortedSet
            var colaPrioridad = new SortedSet<(int Distancia, string Ciudad)>();
            colaPrioridad.Add((0, inicio));

            while (colaPrioridad.Count > 0)
            {
                var actual = colaPrioridad.Min;
                colaPrioridad.Remove(actual);

                int distActual = actual.Distancia;
                string ciudadActual = actual.Ciudad;

                if (distActual > distancias[ciudadActual])
                    continue;

                if (ciudadActual == fin)
                    break;

                if (!_adyacencia.ContainsKey(ciudadActual))
                    continue;

                foreach (var arista in _adyacencia[ciudadActual])
                {
                    int nuevaDist = distActual + arista.Costo;
                    if (nuevaDist < distancias[arista.Destino])
                    {
                        distancias[arista.Destino] = nuevaDist;
                        previos[arista.Destino] = ciudadActual;
                        colaPrioridad.Add((nuevaDist, arista.Destino));
                    }
                }
            }

            // Reconstruir ruta
            var ruta = new List<string>();
            string ciudadActualReconstruccion = fin;

            if (!previos.ContainsKey(fin) && fin != inicio)
            {
                return (null, int.MaxValue);
            }

            while (ciudadActualReconstruccion != null)
            {
                ruta.Insert(0, ciudadActualReconstruccion);
                if (!previos.ContainsKey(ciudadActualReconstruccion))
                    break;
                ciudadActualReconstruccion = previos[ciudadActualReconstruccion];
            }

            if (ruta.Count > 0 && ruta[0] == inicio)
            {
                return (ruta, distancias[fin]);
            }
            else
            {
                return (null, int.MaxValue);
            }
        }

        // Obtener todas las ciudades del grafo
        public List<string> ObtenerCiudades()
        {
            return ObtenerTodasLasCiudades();
        }

        private List<string> ObtenerTodasLasCiudades()
        {
            var ciudades = new HashSet<string>();
            foreach (var origen in _adyacencia.Keys)
            {
                ciudades.Add(origen);
                foreach (var vuelo in _adyacencia[origen])
                {
                    ciudades.Add(vuelo.Destino);
                }
            }
            return ciudades.ToList();
        }
    }

    // Programa principal
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== SISTEMA DE BÚSQUEDA DE VUELOS BARATOS ===\n");

            // Base de datos ficticia de vuelos
            var vuelos = new GrafoVuelos();

            // Agregar vuelos (origen, destino, costo)
            vuelos.AgregarVuelo("Quito", "Guayaquil", 80);
            vuelos.AgregarVuelo("Quito", "Cuenca", 100);
            vuelos.AgregarVuelo("Quito", "Manta", 150);
            vuelos.AgregarVuelo("Guayaquil", "Cuenca", 60);
            vuelos.AgregarVuelo("Guayaquil", "Manta", 50);
            vuelos.AgregarVuelo("Cuenca", "Manta", 120);
            vuelos.AgregarVuelo("Cuenca", "Guayaquil", 70);
            vuelos.AgregarVuelo("Manta", "Quito", 140);

            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
                Console.WriteLine("1. Mostrar grafo de vuelos");
                Console.WriteLine("2. Consultar vuelos desde una ciudad");
                Console.WriteLine("3. Buscar ruta más barata entre dos ciudades");
                Console.WriteLine("4. Mostrar todas las ciudades disponibles");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        vuelos.MostrarGrafo();
                        break;

                    case "2":
                        Console.Write("Ingrese ciudad de origen: ");
                        string origen = Console.ReadLine();
                        var destinos = vuelos.VuelosDesde(origen);
                        if (destinos.Count > 0)
                        {
                            Console.WriteLine($"\nVuelos desde {origen}:");
                            foreach (var v in destinos)
                            {
                                Console.WriteLine($"  {v}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No hay vuelos disponibles desde {origen}");
                        }
                        break;

                    case "3":
                        Console.Write("Ingrese ciudad de origen: ");
                        string inicio = Console.ReadLine();
                        Console.Write("Ingrese ciudad de destino: ");
                        string fin = Console.ReadLine();

                        var (ruta, costo) = vuelos.RutaMasBarata(inicio, fin);

                        if (ruta != null && costo < int.MaxValue)
                        {
                            Console.WriteLine($"\n Ruta más barata encontrada:");
                            Console.WriteLine($"   {string.Join(" → ", ruta)}");
                            Console.WriteLine($"   Costo total: ${costo}");
                        }
                        else
                        {
                            Console.WriteLine($"\n No existe una ruta disponible entre {inicio} y {fin}");
                        }
                        break;

                    case "4":
                        var ciudades = vuelos.ObtenerCiudades();
                        Console.WriteLine("\nCiudades disponibles en el sistema:");
                        foreach (var ciudad in ciudades.OrderBy(c => c))
                        {
                            Console.WriteLine($"  • {ciudad}");
                        }
                        break;

                    case "5":
                        continuar = false;
                        Console.WriteLine("¡Hasta luego Ing, Muchas gracias por impartirnos esta materia este semestre!");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }
        }
    }
}