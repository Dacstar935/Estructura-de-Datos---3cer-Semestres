using System;

public class Nodo
{
    public int Valor { get; set; }
    public Nodo Izquierdo { get; set; }
    public Nodo Derecho { get; set; }

    public Nodo(int valor)
    {
        Valor = valor;
        Izquierdo = null;
        Derecho = null;
    }
}

public class ArbolBinarioBusqueda
{
    private Nodo raiz;

    public ArbolBinarioBusqueda()
    {
        raiz = null;
    }

    // Insertar valor
    public void Insertar(int valor)
    {
        raiz = InsertarRecursivo(raiz, valor);
    }

    private Nodo InsertarRecursivo(Nodo nodo, int valor)
    {
        if (nodo == null)
        {
            return new Nodo(valor);
        }

        if (valor < nodo.Valor)
        {
            nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
        }
        else if (valor > nodo.Valor)
        {
            nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);
        }

        return nodo;
    }

    // Buscar valor
    public bool Buscar(int valor)
    {
        return BuscarRecursivo(raiz, valor);
    }

    private bool BuscarRecursivo(Nodo nodo, int valor)
    {
        if (nodo == null)
        {
            return false;
        }

        if (valor == nodo.Valor)
        {
            return true;
        }
        else if (valor < nodo.Valor)
        {
            return BuscarRecursivo(nodo.Izquierdo, valor);
        }
        else
        {
            return BuscarRecursivo(nodo.Derecho, valor);
        }
    }

    // Eliminar valor
    public void Eliminar(int valor)
    {
        raiz = EliminarRecursivo(raiz, valor);
    }

    private Nodo EliminarRecursivo(Nodo nodo, int valor)
    {
        if (nodo == null)
        {
            return null;
        }

        if (valor < nodo.Valor)
        {
            nodo.Izquierdo = EliminarRecursivo(nodo.Izquierdo, valor);
        }
        else if (valor > nodo.Valor)
        {
            nodo.Derecho = EliminarRecursivo(nodo.Derecho, valor);
        }
        else
        {
            // Nodo a eliminar encontrado
            if (nodo.Izquierdo == null && nodo.Derecho == null)
            {
                return null;
            }
            if (nodo.Izquierdo == null)
            {
                return nodo.Derecho;
            }
            if (nodo.Derecho == null)
            {
                return nodo.Izquierdo;
            }
            
            Nodo sucesor = ObtenerMinimoNodo(nodo.Derecho);
            nodo.Valor = sucesor.Valor;
            nodo.Derecho = EliminarRecursivo(nodo.Derecho, sucesor.Valor);
        }

        return nodo;
    }

    private Nodo ObtenerMinimoNodo(Nodo nodo)
    {
        while (nodo.Izquierdo != null)
        {
            nodo = nodo.Izquierdo;
        }
        return nodo;
    }

    // Recorridos
    public void RecorridoPreorden()
    {
        Console.Write("Recorrido Preorden: ");
        PreordenRecursivo(raiz);
        Console.WriteLine();
    }

    private void PreordenRecursivo(Nodo nodo)
    {
        if (nodo != null)
        {
            Console.Write(nodo.Valor + " ");
            PreordenRecursivo(nodo.Izquierdo);
            PreordenRecursivo(nodo.Derecho);
        }
    }

    public void RecorridoInorden()
    {
        Console.Write("Recorrido Inorden: ");
        InordenRecursivo(raiz);
        Console.WriteLine();
    }

    private void InordenRecursivo(Nodo nodo)
    {
        if (nodo != null)
        {
            InordenRecursivo(nodo.Izquierdo);
            Console.Write(nodo.Valor + " ");
            InordenRecursivo(nodo.Derecho);
        }
    }

    public void RecorridoPostorden()
    {
        Console.Write("Recorrido Postorden: ");
        PostordenRecursivo(raiz);
        Console.WriteLine();
    }

    private void PostordenRecursivo(Nodo nodo)
    {
        if (nodo != null)
        {
            PostordenRecursivo(nodo.Izquierdo);
            PostordenRecursivo(nodo.Derecho);
            Console.Write(nodo.Valor + " ");
        }
    }

    // Mínimo, máximo y altura
    public int? ObtenerMinimo()
    {
        if (raiz == null) return null;
        Nodo actual = raiz;
        while (actual.Izquierdo != null) actual = actual.Izquierdo;
        return actual.Valor;
    }

    public int? ObtenerMaximo()
    {
        if (raiz == null) return null;
        Nodo actual = raiz;
        while (actual.Derecho != null) actual = actual.Derecho;
        return actual.Valor;
    }

    public int ObtenerAltura()
    {
        return CalcularAltura(raiz);
    }

    private int CalcularAltura(Nodo nodo)
    {
        if (nodo == null) return -1;
        return Math.Max(CalcularAltura(nodo.Izquierdo), CalcularAltura(nodo.Derecho)) + 1;
    }

    public void Limpiar()
    {
        raiz = null;
        Console.WriteLine("Árbol limpiado completamente.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ArbolBinarioBusqueda arbol = new ArbolBinarioBusqueda();
        bool continuar = true;

        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   IMPLEMENTACIÓN DE ÁRBOL BST");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Insertar valor");
            Console.WriteLine("2. Buscar valor");
            Console.WriteLine("3. Eliminar valor");
            Console.WriteLine("4. Mostrar recorrido Preorden");
            Console.WriteLine("5. Mostrar recorrido Inorden");
            Console.WriteLine("6. Mostrar recorrido Postorden");
            Console.WriteLine("7. Mostrar valor mínimo");
            Console.WriteLine("8. Mostrar valor máximo");
            Console.WriteLine("9. Mostrar altura del árbol");
            Console.WriteLine("10. Limpiar árbol");
            Console.WriteLine("0. Salir");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el valor a insertar: ");
                    if (int.TryParse(Console.ReadLine(), out int valorIns))
                        arbol.Insertar(valorIns);
                    else
                        Console.WriteLine("Valor inválido.");
                    break;

                case "2":
                    Console.Write("Ingrese el valor a buscar: ");
                    if (int.TryParse(Console.ReadLine(), out int valorBus))
                        Console.WriteLine(arbol.Buscar(valorBus) ? "SÍ se encuentra." : "NO se encuentra.");
                    else
                        Console.WriteLine("Valor inválido.");
                    break;

                case "3":
                    Console.Write("Ingrese el valor a eliminar: ");
                    if (int.TryParse(Console.ReadLine(), out int valorEli))
                    {
                        if (arbol.Buscar(valorEli))
                        {
                            arbol.Eliminar(valorEli);
                            Console.WriteLine("Valor eliminado.");
                        }
                        else
                            Console.WriteLine("El valor no existe.");
                    }
                    break;

                case "4":
                    arbol.RecorridoPreorden();
                    break;

                case "5":
                    arbol.RecorridoInorden();
                    break;

                case "6":
                    arbol.RecorridoPostorden();
                    break;

                case "7":
                    int? min = arbol.ObtenerMinimo();
                    Console.WriteLine(min.HasValue ? $"Mínimo: {min}" : "Árbol vacío.");
                    break;

                case "8":
                    int? max = arbol.ObtenerMaximo();
                    Console.WriteLine(max.HasValue ? $"Máximo: {max}" : "Árbol vacío.");
                    break;

                case "9":
                    Console.WriteLine($"Altura: {arbol.ObtenerAltura()}");
                    break;

                case "10":
                    arbol.Limpiar();
                    break;

                case "0":
                    continuar = false;
                    Console.WriteLine("¡Hasta luego!");
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            if (continuar && opcion != "0")
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
