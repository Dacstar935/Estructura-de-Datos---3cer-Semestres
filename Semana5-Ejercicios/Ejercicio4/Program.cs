using System;
using System.Collections.Generic;

class Loteria
{
    private List<int> numeros;

    public Loteria()
    {
        numeros = new List<int>();
    }

    public void IngresarNumeros()
    {
        Console.WriteLine("Ingrese 6 números ganadores de la lotería:");

        for (int i = 0; i < 6; i++)
        {
            Console.Write($"Número {i + 1}: ");
            numeros.Add(int.Parse(Console.ReadLine()));
        }
    }

    public void MostrarOrdenados()
    {
        numeros.Sort();
        Console.WriteLine("Números ordenados:");
        foreach (int numero in numeros)
        {
            Console.Write(numero + " ");
        }
    }
}

class Program
{
    static void Main()
    {
        Loteria loteria = new Loteria();
        loteria.IngresarNumeros();
        loteria.MostrarOrdenados();
    }
}