using System;
using System.Collections.Generic;

class Asignatura
{
    public string Nombre { get; set; }
    public double Nota { get; set; }
}

class Curso
{
    private List<Asignatura> asignaturas;

    public Curso()
    {
        asignaturas = new List<Asignatura>
        {
            new Asignatura { Nombre = "Matemáticas" },
            new Asignatura { Nombre = "Física" },
            new Asignatura { Nombre = "Química" },
            new Asignatura { Nombre = "Historia" },
            new Asignatura { Nombre = "Lengua" }
        };
    }

    public void PedirNotas()
    {
        foreach (var asignatura in asignaturas)
        {
            Console.Write($"Ingrese la nota de {asignatura.Nombre}: ");
            asignatura.Nota = double.Parse(Console.ReadLine());
        }
    }

    public void MostrarNotas()
    {
        foreach (var asignatura in asignaturas)
        {
            Console.WriteLine($"En {asignatura.Nombre} has sacado {asignatura.Nota}");
        }
    }
}

class Program
{
    static void Main()
    {
        Curso curso = new Curso();
        curso.PedirNotas();
        curso.MostrarNotas();
    }
}