using System;
using System.Collections.Generic;

class TorresDeHanoi
{
    // Definición de las tres torres usando pilas
    static Stack<int> origen = new Stack<int>();
    static Stack<int> auxiliar = new Stack<int>();
    static Stack<int> destino = new Stack<int>();

    static int pasos = 0; // Contador de movimientos

    /// Punto de entrada del programa.
    /// Inicializa los discos y ejecuta el algoritmo.
    static void Main()
    {
        Console.Write("Ingrese el número de discos: ");
        int n = int.Parse(Console.ReadLine());

        // Se cargan los discos en la torre origen (mayor abajo)
        for (int i = n; i >= 1; i--)
        {
            origen.Push(i);
        }

        MostrarTorres();
        ResolverHanoi(n, origen, destino, auxiliar, "Origen", "Destino", "Auxiliar");

        Console.WriteLine($"\nTotal de movimientos: {pasos}");
    }

    /// Algoritmo recursivo que resuelve el problema de Hanoi usando pilas.
    static void ResolverHanoi(int n, Stack<int> from, Stack<int> to, Stack<int> aux,
                              string nombreFrom, string nombreTo, string nombreAux)
    {
        // Caso base: mover un solo disco
        if (n == 1)
        {
            int disco = from.Pop();
            to.Push(disco);
            pasos++;
            Console.WriteLine($"Paso {pasos}: Mover disco {disco} de {nombreFrom} a {nombreTo}");
            MostrarTorres();
            return;
        }

        // Mover n-1 discos al auxiliar
        ResolverHanoi(n - 1, from, aux, to, nombreFrom, nombreAux, nombreTo);

        // Mover el disco mayor al destino
        int d = from.Pop();
        to.Push(d);
        pasos++;
        Console.WriteLine($"Paso {pasos}: Mover disco {d} de {nombreFrom} a {nombreTo}");
        MostrarTorres();

        // Mover los n-1 discos al destino
        ResolverHanoi(n - 1, aux, to, from, nombreAux, nombreTo, nombreFrom);
    }

    /// Muestra el contenido actual de cada torre.
    static void MostrarTorres()
    {
        Console.WriteLine("\nEstado de las Torres:");
        Console.WriteLine("Origen:   " + string.Join(", ", origen.ToArray()));
        Console.WriteLine("Auxiliar: " + string.Join(", ", auxiliar.ToArray()));
        Console.WriteLine("Destino:  " + string.Join(", ", destino.ToArray()));
    }
}
