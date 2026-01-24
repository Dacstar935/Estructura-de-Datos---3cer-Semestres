using System;
using System.Collections.Generic;

class TorresDeHanoi
{
    static Stack<int> origen = new Stack<int>();
    static Stack<int> auxiliar = new Stack<int>();
    static Stack<int> destino = new Stack<int>();
    static int pasos = 0;

    static void Main()
    {
        Console.Write("Ingrese el número de discos: ");
        int n = int.Parse(Console.ReadLine());

        // Inicializar la torre origen
        for (int i = n; i >= 1; i--)
        {
            origen.Push(i);
        }

        MostrarTorres();
        ResolverHanoi(n, origen, destino, auxiliar, "Origen", "Destino", "Auxiliar");

        Console.WriteLine($"\nTotal de movimientos: {pasos}");
    }

    static void ResolverHanoi(int n, Stack<int> from, Stack<int> to, Stack<int> aux,
                              string nombreFrom, string nombreTo, string nombreAux)
    {
        if (n == 1)
        {
            int disco = from.Pop();
            to.Push(disco);
            pasos++;
            Console.WriteLine($"Paso {pasos}: Mover disco {disco} de {nombreFrom} a {nombreTo}");
            MostrarTorres();
            return;
        }

        ResolverHanoi(n - 1, from, aux, to, nombreFrom, nombreAux, nombreTo);

        int d = from.Pop();
        to.Push(d);
        pasos++;
        Console.WriteLine($"Paso {pasos}: Mover disco {d} de {nombreFrom} a {nombreTo}");
        MostrarTorres();

        ResolverHanoi(n - 1, aux, to, from, nombreAux, nombreTo, nombreFrom);
    }

    static void MostrarTorres()
    {
        Console.WriteLine("\nEstado de las Torres:");
        Console.WriteLine("Origen:   " + string.Join(", ", origen.ToArray()));
        Console.WriteLine("Auxiliar: " + string.Join(", ", auxiliar.ToArray()));
        Console.WriteLine("Destino:  " + string.Join(", ", destino.ToArray()));
    }
}
