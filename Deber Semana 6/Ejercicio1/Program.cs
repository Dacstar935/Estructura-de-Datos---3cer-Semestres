using System;

public class Nodo
{
    public int Dato;
    public Nodo Siguiente;
    
    public Nodo(int dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

public class ListaEnlazada
{
    private Nodo cabeza;
    
    public ListaEnlazada()
    {
        cabeza = null;
    }
    
    public void Agregar(int dato)
    {
        Nodo nuevo = new Nodo(dato);
        
        if (cabeza == null)
        {
            cabeza = nuevo;
        }
        else
        {
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo;
        }
    }
    
    // MÉTODO DEL EJERCICIO 1
    public int ContarElementos()
    {
        int contador = 0;
        Nodo actual = cabeza;
        
        while (actual != null)
        {
            contador++;
            actual = actual.Siguiente;
        }
        
        return contador;
    }
    
    public void Mostrar()
    {
        Nodo actual = cabeza;
        Console.Write("Lista: ");
        while (actual != null)
        {
            Console.Write(actual.Dato + " ");
            actual = actual.Siguiente;
        }
        Console.WriteLine();
    }
}

class Program1
{
    static void Main()
    {
        ListaEnlazada lista = new ListaEnlazada();
        
        lista.Agregar(5);
        lista.Agregar(10);
        lista.Agregar(15);
        lista.Agregar(20);
        
        lista.Mostrar();
        Console.WriteLine("Total elementos: " + lista.ContarElementos());
    }
}
