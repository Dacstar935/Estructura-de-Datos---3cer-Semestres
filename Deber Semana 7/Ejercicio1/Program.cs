using System;
using System.Collections.Generic;

class BalanceoParentesis
{
    /// Punto de entrada del programa.
    /// Solicita una expresión al usuario y muestra si está balanceada.
    static void Main()
    {
        Console.WriteLine("Ingrese una expresión matemática:");
        string expresion = Console.ReadLine();

        // Llamada al método que valida el balanceo
        if (EstaBalanceada(expresion))
            Console.WriteLine("Fórmula balanceada.");
        else
            Console.WriteLine("Fórmula NO balanceada.");
    }

    /// Verifica si los paréntesis, llaves y corchetes están balanceados.
    /// <param name="texto">Expresión matemática a evaluar</param>
    /// <returns>True si está balanceada, False si no</returns>
    static bool EstaBalanceada(string texto)
    {
        Stack<char> pila = new Stack<char>(); // Pila para almacenar símbolos de apertura

        foreach (char c in texto)
        {
            // Si es símbolo de apertura, se apila
            if (c == '(' || c == '{' || c == '[')
            {
                pila.Push(c);
            }
            // Si es símbolo de cierre
            else if (c == ')' || c == '}' || c == ']')
            {
                // Si no hay nada que cerrar → error
                if (pila.Count == 0) return false;

                char tope = pila.Pop(); // Sacamos el último símbolo abierto

                // Comprobamos que coincidan
                if ((c == ')' && tope != '(') ||
                    (c == '}' && tope != '{') ||
                    (c == ']' && tope != '['))
                {
                    return false;
                }
            }
        }

        // Si la pila quedó vacía, todo cerró bien
        return pila.Count == 0;
    }
}

