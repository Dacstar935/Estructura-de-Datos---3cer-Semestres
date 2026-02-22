using System;
using System.Collections.Generic;
using System.Linq;

namespace CampaniaVacunacion
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Crear conjunto ficticio de 500 ciudadanos
            HashSet<string> todosCiudadanos = new HashSet<string>();
            for (int i = 1; i <= 500; i++)
            {
                todosCiudadanos.Add($"Ciudadano {i}");
            }

            // 2. Crear conjunto ficticio de 75 vacunados con Pfizer
            HashSet<string> vacunadosPfizer = new HashSet<string>();
            Random random = new Random();
            var ciudadanosList = todosCiudadanos.ToList();
            
            while (vacunadosPfizer.Count < 75)
            {
                int index = random.Next(0, 500);
                vacunadosPfizer.Add(ciudadanosList[index]);
            }

            // 3. Crear conjunto ficticio de 75 vacunados con AstraZeneca
            HashSet<string> vacunadosAstraZeneca = new HashSet<string>();
            while (vacunadosAstraZeneca.Count < 75)
            {
                int index = random.Next(0, 500);
                string ciudadano = ciudadanosList[index];
                
                // Evitar que un ciudadano esté en ambos grupos de vacunados
                if (!vacunadosPfizer.Contains(ciudadano))
                {
                    vacunadosAstraZeneca.Add(ciudadano);
                }
            }

            // 4. Calcular conjuntos requeridos usando teoría de conjuntos

            // Total de vacunados (unión de Pfizer y AstraZeneca)
            HashSet<string> totalVacunados = new HashSet<string>(vacunadosPfizer);
            totalVacunados.UnionWith(vacunadosAstraZeneca);

            // Ciudadanos que no se han vacunado (diferencia: todos - totalVacunados)
            HashSet<string> noVacunados = new HashSet<string>(todosCiudadanos);
            noVacunados.ExceptWith(totalVacunados);

            // Ciudadanos que han recibido ambas dosis (intersección: Pfizer ∩ AstraZeneca)
            HashSet<string> ambasDosis = new HashSet<string>(vacunadosPfizer);
            ambasDosis.IntersectWith(vacunadosAstraZeneca);
            // Nota: En este caso será vacío porque evitamos duplicados al generar

            // Ciudadanos que solo han recibido Pfizer (diferencia: Pfizer - AstraZeneca)
            HashSet<string> soloPfizer = new HashSet<string>(vacunadosPfizer);
            soloPfizer.ExceptWith(vacunadosAstraZeneca);

            // Ciudadanos que solo han recibido AstraZeneca (diferencia: AstraZeneca - Pfizer)
            HashSet<string> soloAstraZeneca = new HashSet<string>(vacunadosAstraZeneca);
            soloAstraZeneca.ExceptWith(vacunadosPfizer);

            // 5. Mostrar resultados
            Console.WriteLine("==========================================");
            Console.WriteLine("RESULTADOS DE LA CAMPAÑA DE VACUNACIÓN");
            Console.WriteLine("==========================================\n");

            Console.WriteLine($"Total de ciudadanos registrados: {todosCiudadanos.Count}");
            Console.WriteLine($"Total de vacunados con Pfizer: {vacunadosPfizer.Count}");
            Console.WriteLine($"Total de vacunados con AstraZeneca: {vacunadosAstraZeneca.Count}");
            Console.WriteLine($"Total de vacunados (general): {totalVacunados.Count}");
            Console.WriteLine($"Ciudadanos con ambas dosis: {ambasDosis.Count}");
            Console.WriteLine();

            // Listados solicitados
            Console.WriteLine("---------- LISTADOS SOLICITADOS ----------\n");

            Console.WriteLine("1. CIUDADANOS QUE NO SE HAN VACUNADO:");
            Console.WriteLine($"   Total: {noVacunados.Count}");
            if (noVacunados.Count > 0)
            {
                Console.WriteLine("   Primeros 10:");
                foreach (var ciudadano in noVacunados.Take(10))
                {
                    Console.WriteLine($"     - {ciudadano}");
                }
                if (noVacunados.Count > 10)
                    Console.WriteLine("     (mostrando solo 10 de " + noVacunados.Count + ")");
            }
            Console.WriteLine();

            Console.WriteLine("2. CIUDADANOS CON AMBAS DOSIS:");
            Console.WriteLine($"   Total: {ambasDosis.Count}");
            if (ambasDosis.Count > 0)
            {
                foreach (var ciudadano in ambasDosis)
                {
                    Console.WriteLine($"     - {ciudadano}");
                }
            }
            else
            {
                Console.WriteLine("   No hay ciudadanos con ambas dosis (generación sin duplicados)");
            }
            Console.WriteLine();

            Console.WriteLine("3. CIUDADANOS QUE SOLO RECIBIERON PFIZER:");
            Console.WriteLine($"   Total: {soloPfizer.Count}");
            if (soloPfizer.Count > 0)
            {
                Console.WriteLine("   Primeros 10:");
                foreach (var ciudadano in soloPfizer.Take(10))
                {
                    Console.WriteLine($"     - {ciudadano}");
                }
                if (soloPfizer.Count > 10)
                    Console.WriteLine("     (mostrando solo 10 de " + soloPfizer.Count + ")");
            }
            Console.WriteLine();

            Console.WriteLine("4. CIUDADANOS QUE SOLO RECIBIERON ASTRAZENECA:");
            Console.WriteLine($"   Total: {soloAstraZeneca.Count}");
            if (soloAstraZeneca.Count > 0)
            {
                Console.WriteLine("   Primeros 10:");
                foreach (var ciudadano in soloAstraZeneca.Take(10))
                {
                    Console.WriteLine($"     - {ciudadano}");
                }
                if (soloAstraZeneca.Count > 10)
                    Console.WriteLine("     (mostrando solo 10 de " + soloAstraZeneca.Count + ")");
            }
            Console.WriteLine();

            // 6. Demostración de operaciones de teoría de conjuntos utilizadas
            Console.WriteLine("\n========== OPERACIONES APLICADAS ==========");
            Console.WriteLine("1. Unión: Total vacunados = Pfizer ∪ AstraZeneca");
            Console.WriteLine("2. Diferencia: No vacunados = Todos - Total vacunados");
            Console.WriteLine("3. Intersección: Ambas dosis = Pfizer ∩ AstraZeneca");
            Console.WriteLine("4. Diferencia: Solo Pfizer = Pfizer - AstraZeneca");
            Console.WriteLine("5. Diferencia: Solo AstraZeneca = AstraZeneca - Pfizer");
        }
    }
}