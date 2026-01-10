using System;
using System.Collections.Generic;

class Numeros
{
    private List<int> lista;

    public Numeros()
    {
        lista = new List<int>();
        for (int i = 1; i <= 10; i++)
        {
            lista.Add(i);
        }
    }

    public void MostrarInverso()
    {
        lista.Reverse();
        Console.WriteLine(string.Join(", ", lista));
    }
}

class Program
{
    static void Main()
    {
        Numeros numeros = new Numeros();
        numeros.MostrarInverso();
    }
}
