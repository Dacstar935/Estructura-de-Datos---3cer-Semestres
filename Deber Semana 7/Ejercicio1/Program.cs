using System;
using System.Collections.Generic;

class BalanceoParentesis
{
    static void Main()
    {
        Console.WriteLine("Ingrese una expresión matemática:");
        string expresion = Console.ReadLine();

        if (EstaBalanceada(expresion))
            Console.WriteLine("Fórmula balanceada.");
        else
            Console.WriteLine("Fórmula NO balanceada.");
    }

    static bool EstaBalanceada(string texto)
    {
        Stack<char> pila = new Stack<char>();

        foreach (char c in texto)
        {
            if (c == '(' || c == '{' || c == '[')
            {
                pila.Push(c);
            }
            else if (c == ')' || c == '}' || c == ']')
            {
                if (pila.Count == 0) return false;

                char tope = pila.Pop();

                if ((c == ')' && tope != '(') ||
                    (c == '}' && tope != '{') ||
                    (c == ']' && tope != '['))
                {
                    return false;
                }
            }
        }

        return pila.Count == 0;
    }
}

