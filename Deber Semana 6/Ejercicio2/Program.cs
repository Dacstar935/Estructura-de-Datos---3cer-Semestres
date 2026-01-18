using System;

public class Nodo2
{
    public int Dato;
    public Nodo2 Siguiente;
    
    public Nodo2(int dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

public class ListaEnlazada2
{
    private Nodo2 cabeza;
    
    public ListaEnlazada2()
    {
        cabeza = null;
    }
    
    public void Agregar(int dato)
    {
        Nodo2 nuevo = new Nodo2(dato);
        
        if (cabeza == null)
        {
            cabeza = nuevo;
        }
        else
        {
            Nodo2 actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo;
        }
    }
    
    // MÉTODO DEL EJERCICIO 3
    public int Buscar(int valor)
    {
        int contador = 0;
        Nodo2 actual = cabeza;
        
        while (actual != null)
        {
            if (actual.Dato == valor)
            {
                contador++;
            }
            actual = actual.Siguiente;
        }
        
        return contador;
    }
    
    public void BuscarYMostrar(int valor)
    {
        int veces = Buscar(valor);
        
        if (veces > 0)
        {
            Console.WriteLine($"El valor {valor} se encontró {veces} vez(es)");
        }
        else
        {
            Console.WriteLine($"El valor {valor} NO fue encontrado");
        }
    }
    
    public void Mostrar()
    {
        Nodo2 actual = cabeza;
        Console.Write("Lista: ");
        while (actual != null)
        {
            Console.Write(actual.Dato + " ");
            actual = actual.Siguiente;
        }
        Console.WriteLine();
    }
}

class Program3
{
    static void Main()
    {
        ListaEnlazada2 lista = new ListaEnlazada2();
        
        lista.Agregar(10);
        lista.Agregar(20);
        lista.Agregar(10);
        lista.Agregar(30);
        lista.Agregar(20);
        
        lista.Mostrar();
        
        Console.WriteLine("\nBúsquedas:");
        lista.BuscarYMostrar(10);
        lista.BuscarYMostrar(20);
        lista.BuscarYMostrar(30);
        lista.BuscarYMostrar(50);
    }
}